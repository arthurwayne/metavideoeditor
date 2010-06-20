using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace mveEngine
{
    public class Kernel
    {
        static object sync = new object();
        static Kernel kernel;

        public static void Init()
        {
            ConfigData config = null;
            config = ConfigData.FromFile(ApplicationPaths.ConfigFile);
            Init(config);
        }

        public static void Init(ConfigData config)
        {
            lock (sync)
            {
                if (Logger.LoggerInstance != null)
                {
                    Logger.LoggerInstance.Dispose();
                }

                Logger.LoggerInstance = GetDefaultLogger(config);

                var kernel = GetDefaultKernel(config);
                Kernel.Instance = kernel;
            }
            Logger.ReportInfo("kernel loaded");            
        }

        static Kernel GetDefaultKernel(ConfigData config)
        {
            var kernel = new Kernel()
            {
                Plugins = DefaultPlugins(),
                StringData = new LocalizedStrings(),
                ConfigData = config
            };
            foreach (var plugin in kernel.Plugins.ToList())
            {
                try
                {
                    plugin.Init(kernel);
                }
                catch 
                {
                    kernel.Plugins.Remove(plugin);
                }
            }
            
            kernel.Plugins.Sort(delegate(IPlugin p1, IPlugin p2) { return p1.Options.Order.CompareTo(p2.Options.Order); });
            int index = 0;
            foreach (var plugin in kernel.Plugins.ToList())
            {
                if (plugin.Options.Order != index)
                {
                    plugin.Options.Order = index;
                    plugin.Save();
                }
                index++;
            }
            kernel.ItemCollection = new ItemCollection(config.RootFolders);
            kernel.Message = new GenerateMessage();

            return kernel;
        }

        public List<IPlugin> Plugins { get; set; }
        public LocalizedStrings StringData { get; set; }
        public ConfigData ConfigData { get; set; }
        public ItemCollection ItemCollection;
        public GenerateMessage Message;

        public static void ReloadKernel()
        {
            kernel = null;
            Init();
        }

        public static Kernel Instance
        {
            get
            {
                if (kernel != null) return kernel;
                lock (sync)
                {
                    if (kernel != null) return kernel;
                    Init();
                }
                return kernel;
            }
            set
            {
                lock (sync)
                {
                    kernel = value;
                }
            }
        }

        public string GetString(string name)
        {
            return this.StringData.GetString(name);
        }

        public static List<IPlugin> DefaultPlugins()
        {
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (var file in Directory.GetFiles(ApplicationPaths.AppPluginPath))
            {
                if (file.ToLower().EndsWith(".dll"))
                {
                    try
                    {
                        plugins.Add(new Plugin(Path.Combine(ApplicationPaths.AppPluginPath, file)));
                    }
                    catch 
                    {
                    }
                }
            }

            return plugins;
        }

        public void DeletePlugin(IPlugin plugin)
        {
            if (!(plugin is Plugin))
            {
                Logger.ReportWarning("Attempting to remove a plugin that we have no location for!");
                throw new ApplicationException("Attempting to remove a plugin that we have no location for!");
            }

            (plugin as Plugin).Delete();
            Plugins.Remove(plugin);
        }

        

        public void InstallPlugin(string path, 
                string name,
                PluginInstallUpdateCB updateCB,
                PluginInstallFinishCB doneCB,
                PluginInstallErrorCB errorCB)
        {
            string target = Path.Combine(ApplicationPaths.AppPluginPath, Path.GetFileName(path));

            if (path.ToLower().StartsWith("http"))
            {
                // Initialise Async Web Request
                int BUFFER_SIZE = 1024;
                Uri fileURI = new Uri(path);

                WebRequest request = WebRequest.Create(fileURI);
                State requestState = new State(BUFFER_SIZE, target);
                requestState.request = request;
                requestState.fileURI = fileURI;
                requestState.progCB = updateCB;
                requestState.doneCB = doneCB;
                requestState.errorCB = errorCB;
                requestState.name = name;

                IAsyncResult result = (IAsyncResult)request.BeginGetResponse(new AsyncCallback(ResponseCallback), requestState);
            }
            else
            {
                File.Copy(path, target);
                InitialisePlugin(target);
            }

            // Moved code to InitialisePlugin()
            //Function needs to be called at end of Async dl process as well
        }

        private void InitialisePlugin(string target)
        {
            var plugin = Plugin.FromFile(target, true);

            try
            {
                plugin.Init(this);
            }
            catch (InvalidCastException e)
            {
                // this happens if the assembly with the exact same version is loaded 
                // AND the Init process tries to use types defined in its assembly 
                throw new PluginAlreadyLoadedException("Failed to init plugin as its already loaded", e);
            }
            IPlugin pi = Plugins.Find(p => p.FileName == plugin.FileName);
            if (pi != null) Plugins.Remove(pi); //we were updating
            Plugins.Add(plugin);


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
                    InitialisePlugin(requestState.downloadDest.Name);
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

        private static MultiLogger GetDefaultLogger(ConfigData config)
        {
            var logger = new MultiLogger();

            if (config.EnableTraceLogging)
            {
                logger.AddLogger(new FileLogger(ApplicationPaths.AppLogPath));
#if (!DEBUG)
                logger.AddLogger(new TraceLogger());
#endif
            }
#if DEBUG
            logger.AddLogger(new TraceLogger());
#endif
            return logger;
        }


    }

    [global::System.Serializable]
    public class PluginAlreadyLoadedException : Exception
    {
        public PluginAlreadyLoadedException() { }
        public PluginAlreadyLoadedException(string message) : base(message) { }
        public PluginAlreadyLoadedException(string message, Exception inner) : base(message, inner) { }
        protected PluginAlreadyLoadedException(
          System.Runtime.Serialization.SerializationInfo info,
          System.Runtime.Serialization.StreamingContext context)
            : base(info, context) { }
    }
}