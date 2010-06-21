using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class ConfigWizard : Form
    {

        public static Config Config
        {
            get { return Config.Instance; }
        }

        public ConfigWizard()
        {
            InitializeComponent();
            Localize();
            Config.IsFirstRun = false;
            UpdateFolderList();
            Updater.DownloadUpdateFile();
            List<IPlugin> PluginsCopy = new List<IPlugin>(Kernel.Instance.Plugins);
            foreach (IPlugin plugin in PluginsCopy)
                Kernel.Instance.DeletePlugin(plugin);
            foreach (IPlugin plugin in PluginSourceCollection.Instance.AvailablePlugins)
            {
                if (plugin.Type == PluginType.Provider)
                {
                    checkedListBox1.Items.Add(plugin.Name);
                }
            }
        }

        private void Localize()
        {
            this.Text = Kernel.Instance.GetString("TitleWiz");
            wizardControl1.CancelButtonText = Kernel.Instance.GetString("CancelStr");
            wizardControl1.NextButtonText = string.Format("{0} >", Kernel.Instance.GetString("NextWiz"));
            wizardControl1.BackButtonText = string.Format("< {0}", Kernel.Instance.GetString("BackWiz"));
            wizardControl1.FinishButtonText = Kernel.Instance.GetString("FinishWiz");
            startStep1.Title = Kernel.Instance.GetString("WelcomeWiz");
            startStep1.Subtitle = Kernel.Instance.GetString("WelcSubWiz");
            intermediateStep1.Title = Kernel.Instance.GetString("MCSoftWiz");
            label1.Text = Kernel.Instance.GetString("MCSoftQuestionWiz");
            useDVDIDbox.Text = Kernel.Instance.GetString("DVDlibWiz");
            intermediateStep2.Title = Kernel.Instance.GetString("FoldersConfWiz");
            label2.Text = Kernel.Instance.GetString("FoldersQuestionWiz");
            AddButton.Text = Kernel.Instance.GetString("AddSet");
            DeleteButton.Text = Kernel.Instance.GetString("RemoveSet");
            ImportButton.Text = Kernel.Instance.GetString("MBFoldersSet");
            intermediateStep3.Title = Kernel.Instance.GetString("ProvidersWiz");
            label3.Text = Kernel.Instance.GetString("ProvQuestionWiz");
        }

        private void wizardControl1_CancelButtonClick(object sender, EventArgs e)
        {
            if (MessageBox.Show(Kernel.Instance.GetString("QuitWiz"), "MetaVideoEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question)
                == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void wizardControl1_FinishButtonClick(object sender, EventArgs e)
        {
            Config.RootFolders = Folders;
            List<IPlugin> plugins = new List<IPlugin>();
            foreach (IPlugin plugin in PluginSourceCollection.Instance.AvailablePlugins)
            {
                if (checkedListBox1.CheckedItems.Contains(plugin.Name))
                    plugins.Add(plugin);
                if (UseMBbox.Checked)
                {
                    if (plugin.Name == "MediaBrowser Reader" || plugin.Name == "Folder.jpg Reader" || plugin.Name == "MediaBrowser Saver"
                        || plugin.Name == "Folder.jpg Saver")
                        plugins.Add(plugin);
                }
                if (useDVDIDbox.Checked)
                {
                    if (plugin.Name == "DvD-ID" || plugin.Name == "Folder.jpg Reader" || plugin.Name == "Folder.jpg Saver")
                    {
                        if (!plugins.Contains(plugin))
                            plugins.Add(plugin);
                    }
                }
                if (useXBMCbox.Checked)
                {
                    if (plugin.Name == "XBMC Reader" || plugin.Name == "XBMC Saver")
                        plugins.Add(plugin);
                }
            }
            if (plugins.Count > 0)
            {
                var form = new InstallPlugins();
                form.Owner = this.Owner;
                form.BeginInstall(plugins);
                form.ShowDialog();
            }
            this.Close();
        }

        private List<string> Folders = new List<string>();

        private void UpdateFolderList()
        {
            FolderList.Clear();
            foreach (string b in Folders)
                FolderList.Items.Add(b);
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult res = fb.ShowDialog();
            if (res == DialogResult.OK)
            {
                Folders.Add(fb.SelectedPath);
                UpdateFolderList();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (FolderList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lv in FolderList.SelectedItems)
                    Folders.Remove(lv.Text);
                UpdateFolderList();
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            List<string> MBfolders = FileUtil.GetMBfolders();
            foreach (string f in MBfolders)
            {
                if (!Folders.Contains(f))
                    Folders.Add(f);
            }
            UpdateFolderList();
        }

        private void FolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteButton.Enabled = true;
        }

        

        

        
    }
}
