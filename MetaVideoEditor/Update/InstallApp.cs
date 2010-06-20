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
    public partial class InstallApp : Form
    {
        public InstallApp()
        {
            InitializeComponent();
            this.Text = Kernel.Instance.GetString("UpdatesUp");
            label1.Text = Kernel.Instance.GetString("DownloadUp");
        }

        public void BeginInstall()
        {
            Updater u = new Updater();
            callBack done = new callBack(InstallFinished);
            u.DownloadUpdate(progressBar1, this, done);
        }

        private delegate void callBack();

        public void InstallFinished()
        {
            //called when the install is finished - we want to close
            Application.Exit();
        }
    }
}
