using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Diagnostics;
using System.Reflection;

using System.Windows.Forms;

namespace mveEngine
{
    public class Updater
    {
        static Config Config
        {
            get { return Config.Instance; }
        }

        public static Version CurrentVersion
        {
            get
            {
                string folderApp = Assembly.GetExecutingAssembly().Location;
                string exePath = Path.Combine(Directory.GetParent(folderApp).FullName, "MetaVideoEditor.exe");
                if (File.Exists(exePath))
                    return new Version(FileVersionInfo.GetVersionInfo(exePath).FileVersion);
                return Assembly.GetExecutingAssembly().GetName().Version;
            }
        }

        public static string UpdatePath = "http://www.media-center7.fr/download/MetaVideoEditor/";

        public static XmlDocument DownloadUpdateFile()
        {
            XmlDocument doc = new XmlDocument();
            if (!File.Exists(ApplicationPaths.UpdateFile) || (DateTime.Now - Config.LastUpdateCheck).TotalDays > 1)
            {
                doc = Helper.Fetch(UpdatePath + "update.xml");
                if (doc != null)
                    doc.Save(ApplicationPaths.UpdateFile);
                Config.LastUpdateCheck = DateTime.Now;
                return doc;
            }
            doc.Load(ApplicationPaths.UpdateFile);
            return doc;
        }

        public static int UpdateAvailable()
        {
            int count = 0;
            if (!File.Exists(ApplicationPaths.UpdateFile))
                return count;

            if (AppUpdateVersion > CurrentVersion)
                count++;            
            count += PluginSourceCollection.Instance.PluginsToUpdate.Count;  
            return count;
        }

        public static Version AppUpdateVersion
        {
            get
            {
                if (!File.Exists(ApplicationPaths.UpdateFile))
                    return CurrentVersion;

                XmlDocument doc = new XmlDocument();
                doc.Load(ApplicationPaths.UpdateFile);
                if (doc != null)
                {
                    XmlNode appNode = doc.SelectSingleNode("Update/Application");
                    return new Version(appNode.SafeGetString("Version"));
                }
                return CurrentVersion;
            }
        }

        private static string AppUpdatePath
        {
            get
            {
                if (!File.Exists(ApplicationPaths.UpdateFile))
                    return "";

                XmlDocument doc = new XmlDocument();
                doc.Load(ApplicationPaths.UpdateFile);
                if (doc != null)
                {
                    XmlNode appNode = doc.SelectSingleNode("Update/Application");
                    return appNode.SafeGetString("Filename");
                }
                return "";
            }
        }

        private ProgressBar m_progress;
        private Form m_window;
        private Delegate m_done;

        public void DownloadUpdate(ProgressBar progress, Form window, Delegate done)
        {
            m_progress = progress;
            m_window = window;
            m_done = done;

            PluginInstallUpdateCB updateDelegate = new PluginInstallUpdateCB(InstallUpdate);
            PluginInstallFinishCB doneDelegate = new PluginInstallFinishCB(DownloadDone);
            PluginInstallErrorCB errorDelegate = new PluginInstallErrorCB(InstallError);

            try
            {
                DownloadMSI(Updater.UpdatePath + AppUpdatePath, updateDelegate, doneDelegate, errorDelegate);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Impossible d'installer le plugin.\n" + ex.Message, "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void DownloadMSI(string path,
                PluginInstallUpdateCB updateCB,
                PluginInstallFinishCB doneCB,
                PluginInstallErrorCB errorCB)
        {
            localFile = Path.Combine(Path.GetTempPath(), Path.GetFileName(path));

            // Initialise Async Web Request
            int BUFFER_SIZE = 1024;
            Uri fileURI = new Uri(path);

            WebRequest request = WebRequest.Create(fileURI);
            State requestState = new State(BUFFER_SIZE, localFile);
            requestState.request = request;
            requestState.fileURI = fileURI;
            requestState.progCB = updateCB;
            requestState.doneCB = doneCB;
            requestState.errorCB = errorCB;
            requestState.name = "";

            IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(ResponseCallback), requestState);
        }

        private void InstallUpdate(double pctComplete, string name)
        {
            SetProgressValue(pctComplete);
        }

        private delegate void SetProgressValueDelegate(double pctComplete);
        private void SetProgressValue(double pctComplete)
        {
            if (m_progress.InvokeRequired)
            {
                m_progress.Invoke(new SetProgressValueDelegate(this.SetProgressValue), new object[] { pctComplete });
                return;
            }
            m_progress.Value = (int)pctComplete;
        }

        private void DownloadDone()
        {
            DownloadComplete();
            InstallFinish();
        }

        private void InstallFinish()
        {
            if (m_window.InvokeRequired)
            {
                m_window.Invoke(new PluginInstallFinishCB(this.InstallFinish), new object[] { });
                return;
            }
            m_window.Invoke(m_done);
        }

        private void InstallError(WebException ex)
        {
            MessageBox.Show(string.Format("Erreur lors du téléchargement du programme : {0}", ex.Message));
            InstallFinish();
        }

        /// <summary>
        /// Main callback invoked in response to the Stream.BeginRead method, when we have some data.
        /// </summary>
        private void ReadCallback(IAsyncResult asyncResult)
        {
            State requestState = ((State)(asyncResult.AsyncState));

            try
            {
                Stream responseStream = requestState.streamResponse;

                // Get results of read operation
                int bytesRead = responseStream.EndRead(asyncResult);

                // Got some data, need to read more
                if (bytesRead > 0)
                {
                    // Save Data
                    requestState.downloadDest.Write(requestState.bufferRead, 0, bytesRead);

                    // Report some progress, including total # bytes read, % complete, and transfer rate
                    requestState.bytesRead += bytesRead;
                    double percentComplete = ((double)requestState.bytesRead / (double)requestState.totalBytes) * 100.0f;

                    //Callback to GUI to update progress
                    if (requestState.progCB != null)
                    {
                        requestState.progCB(percentComplete, requestState.name);
                    }

                    // Kick off another read
                    IAsyncResult ar = responseStream.BeginRead(requestState.bufferRead, 0, requestState.bufferRead.Length, new AsyncCallback(ReadCallback), requestState);
                    return;
                }

                // EndRead returned 0, so no more data to be read
                else
                {
                    responseStream.Close();
                    requestState.response.Close();
                    requestState.downloadDest.Flush();
                    requestState.downloadDest.Close();

                    //Callback to GUI to report download has completed
                    if (requestState.doneCB != null)
                    {
                        requestState.doneCB();
                    }

                    // Initialise the Plugin
                    //InitialisePlugin(requestState.downloadDest.Name);
                }
            }
            catch (PluginAlreadyLoadedException)
            {
                Logger.ReportWarning("Attempting to install a plugin that is already loaded: " + requestState.fileURI);
            }
            catch (WebException ex)
            {
                //Callback to GUI to report an error has occured.
                if (requestState.errorCB != null)
                {
                    requestState.errorCB(ex);
                }
            }
        }

        /// <summary>
        /// Main response callback, invoked once we have first Response packet from
        /// server.  This is where we initiate the actual file transfer, reading from
        /// a stream.
        /// </summary>
        /// <param name="asyncResult"></param>
        public void ResponseCallback(IAsyncResult asyncResult)
        {
            State requestState = ((State)(asyncResult.AsyncState));

            try
            {
                WebRequest req = requestState.request;

                // HTTP 
                if (requestState.fileURI.Scheme == Uri.UriSchemeHttp)
                {
                    HttpWebResponse resp = ((HttpWebResponse)(req.EndGetResponse(asyncResult)));
                    requestState.response = resp;
                    requestState.totalBytes = requestState.response.ContentLength;
                }
                else
                {
                    throw new ApplicationException("Unexpected URI");
                }

                // Set up a stream, for reading response data into it
                Stream responseStream = requestState.response.GetResponseStream();
                requestState.streamResponse = responseStream;

                // Begin reading contents of the response data
                IAsyncResult ar = responseStream.BeginRead(requestState.bufferRead, 0, requestState.bufferRead.Length, new AsyncCallback(ReadCallback), requestState);

                return;
            }
            catch (WebException ex)
            {
                //Callback to GUI to report an error has occured.
                if (requestState.errorCB != null)
                {
                    requestState.errorCB(ex);
                }
            }
        }

        private static string localFile;

        // Process the completed update download.
        public static void DownloadComplete()
        {    
            // put together a batch file to execute the installer in silent mode and restart VB.
            string updateBat = "msiexec.exe /qf /i \"" + localFile + "\"\n";
            updateBat += "\"" + Path.Combine(Path.Combine(Path.Combine(ProgramFilesx86(), "MetaVideoEditor"), "MetaVideoEditor"), "MetaVideoEditor.exe") + "\"";
            string filename = System.IO.Path.GetTempFileName();
            filename += ".bat";
            System.IO.File.WriteAllText(filename, updateBat);

            // Start the batch file minimized so they don't notice.
            Process toDo = new Process();
            toDo.StartInfo.WindowStyle = ProcessWindowStyle.Minimized;
            toDo.StartInfo.FileName = filename;

            toDo.Start();
        }

        static string ProgramFilesx86()
        {
            if (8 == IntPtr.Size
                || (!String.IsNullOrEmpty(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITEW6432"))))
            {
                return Environment.GetEnvironmentVariable("ProgramFiles(x86)");
            }

            return Environment.GetEnvironmentVariable("ProgramFiles");
        }

        
    }
}