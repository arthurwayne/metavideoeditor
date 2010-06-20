using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace MetaVideoEditor
{    
    public static class SplashScreen
    {
        static SplashScreenForm sf = null;

        public static void ShowSplashScreen()
        {
            if (sf == null)
            {
                sf = new SplashScreenForm();
                sf.ShowSplashScreen();
            }
        }

        public static void CloseSplashScreen()
        {
            if (sf != null)
            {
                sf.CloseSplashScreen();
                sf = null;
            }
        }

        public static void HideSplashScreen()
        {
            if (sf != null)
            {
                sf.Hide();
            }
        }

        public static void ReShowSplashScreen()
        {
            if (sf != null)
            {
                sf.Show();
                sf.Activate();
            }
        }

        public static void UdpateStatusText(string Text)
        {
            if (sf != null)
                sf.UdpateStatusText(Text);
        }

        public static void UdpateProgressBar(int percent)
        {
            if (sf != null)
                sf.UdpateProgressBar(percent);
        }
    }

}
