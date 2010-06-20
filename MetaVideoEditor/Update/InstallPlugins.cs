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
    public partial class InstallPlugins : Form
    {
        public InstallPlugins()
        {
            InitializeComponent();
            this.Text = Kernel.Instance.GetString("PluginInstallUp");
        }

        public void BeginInstall(List<IPlugin> plugins)
        {
            PluginInstaller p = new PluginInstaller();
            callBack done = new callBack(InstallFinished);
            p.InstallPlugin(plugins, progressBar1, label1, this, done);
        }

        private delegate void callBack();

        public void InstallFinished()
        {
            //called when the install is finished - we want to close
            this.Close();
        }
    }
}
