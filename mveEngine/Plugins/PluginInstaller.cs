using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Threading;
using System.Windows.Threading;
using System.Net;

namespace mveEngine
{
    public class PluginInstaller
    {
        ManualResetEvent done = new ManualResetEvent(false);
        ManualResetEvent exited = new ManualResetEvent(false);

        private ProgressBar m_progress;
        private Label m_label;
        private Form m_window;
        private Delegate m_done;
        private int PluginsToInstallCount;

        public void InstallPlugin(List<IPlugin> plugins, ProgressBar progress, Label label, Form window, Delegate done)
        {
            m_window = window;
            m_progress = progress;
            m_label = label;
            m_done = done;
            PluginsToInstallCount = plugins.Count;

            List<string> paths = new List<string>();
            foreach (IPlugin plugin in plugins)
                paths.Add(Updater.UpdatePath + plugin.FileName);
            foreach (IPlugin plugin in plugins)
            {
                PluginInstallUpdateCB updateDelegate = new PluginInstallUpdateCB(PluginInstallUpdate);
                PluginInstallFinishCB doneDelegate = new PluginInstallFinishCB(PluginInstallFinish);
                PluginInstallErrorCB errorDelegate = new PluginInstallErrorCB(PluginInstallError);

                try
                {
                    Kernel.Instance.InstallPlugin(Updater.UpdatePath + plugin.FileName, plugin.Name, updateDelegate, doneDelegate, errorDelegate);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Impossible d'installer le plugin.\n" + ex.Message, "MetaVideoEditor",  MessageBoxButtons.OK, MessageBoxIcon.Error);                    
                }
            }
        }

        private delegate void SetLabelDelegate(string name);
        private void PluginInstallBegin(string name)
        {
            if (m_label.InvokeRequired)
            {
                m_label.Invoke(new SetLabelDelegate(PluginInstallBegin), new object[] { name });
                return;
            }
            m_label.Text = "Installation de " + name;
        }

        private void PluginInstallUpdate(double pctComplete, string name)
        {
            SetProgressValue(pctComplete);
            PluginInstallBegin(name);
            
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

        private void PluginInstallFinish()
        {
            PluginsToInstallCount--;
            if (PluginsToInstallCount == 0)
            {
                InstallDone();
            }
        }

        private void InstallDone()
        {
            if (m_window.InvokeRequired)
            {
                m_window.Invoke(new PluginInstallFinishCB(this.InstallDone), new object[] { });
                return;
            }
            m_window.Invoke(m_done);
        }

        private void PluginInstallError(WebException ex)
        {
            PluginsToInstallCount--;
            MessageBox.Show(string.Format("Erreur lors du téléchargement du plugin : {0}", ex.Message));
                
        }
    }
}
