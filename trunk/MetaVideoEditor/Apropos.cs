using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MetaVideoEditor
{
    public partial class Apropos : Form
    {
        public Apropos()
        {
            InitializeComponent();
            this.Text = mveEngine.Kernel.Instance.GetString("AboutMW");
            VersionLabel.Text = string.Format("{0} {1}", mveEngine.Kernel.Instance.GetString("CurrentVersionAb"), 
                System.Reflection.Assembly.GetExecutingAssembly().GetName().Version.ToString());
            label2.Text = mveEngine.Kernel.Instance.GetString("FrSupportAb");
            label1.Text = mveEngine.Kernel.Instance.GetString("DownloadAb");
        }


        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.media-center7.fr/forum/forum.html?id=31");
        }

        private void linkLabel2_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            System.Diagnostics.Process.Start("http://code.google.com/p/metavideoeditor/downloads/list");
        }
    }
}
