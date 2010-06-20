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
    public partial class SplashScreenForm : Form
    {
        delegate void StringParameterDelegate(string Text);
        delegate void UpdateProgressBarDelegate(int percent);
        delegate void SplashShowCloseDelegate();

        /// <summary>
        /// To ensure splash screen is closed using the API and not by keyboard or any other things
        /// </summary>
        bool CloseSplashScreenFlag = false;

        /// <summary>
        /// Base constructor
        /// </summary>
        public SplashScreenForm()
        {
            InitializeComponent();
            this.label1.Parent = this.pictureBox1;
            this.label1.BackColor = Color.Transparent;            
            progressBar1.Show();
        }

        public void ShowSplashScreen()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SplashShowCloseDelegate(ShowSplashScreen));
                return;
            }
            this.Show();
            Application.Run(this);
        }

        public void CloseSplashScreen()
        {
            if (InvokeRequired)
            {
                BeginInvoke(new SplashShowCloseDelegate(CloseSplashScreen));
                return;
            }
            CloseSplashScreenFlag = true;
            this.Close();
        }

        public void UdpateStatusText(string Text)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new StringParameterDelegate(UdpateStatusText), new object[] { Text });
                return;
            }
            label1.Text = Text;
        }

        public void UdpateProgressBar(int percent)
        {
            if (InvokeRequired)
            {
                BeginInvoke(new UpdateProgressBarDelegate(UdpateProgressBar), new object[] { percent });
                return;
            }
            progressBar1.Value = percent;
        }

        private void SplashForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (CloseSplashScreenFlag == false)
                e.Cancel = true;
        }

    }
}
