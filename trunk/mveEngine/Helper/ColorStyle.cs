using System;
using System.Drawing;
using mveEngine;

namespace mveEngine
{
    public class ColorStyle
    {
        public static ColorsTheme GetColors()
        {
            ColorsTheme cTheme = new ColorsTheme();
            switch (Config.Instance.ColorTheme)
            {

                case "Metal":
                    cTheme.BackColor = Color.FromArgb(210, 210, 210);
                    cTheme.HaloColor = Color.FromArgb(200, 200, 200);
                    cTheme.BaseColor = Color.FromArgb(110, 110, 110);
                    break;
                case "Dark":
                    cTheme.BackColor = Color.FromArgb(88, 77, 69);
                    cTheme.HaloColor = Color.FromArgb(200, 200, 200);
                    cTheme.BaseColor = Color.FromArgb(87, 61, 53);
                    break;
                case "Nature":
                    cTheme.BackColor = Color.FromArgb(78, 127, 52);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(73, 118, 46);
                    break;
                case "Dawn":
                    cTheme.BackColor = Color.FromArgb(177, 108, 45);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor =Color.FromArgb(172, 99, 39);
                    break;
                case "Corn":
                    cTheme.BackColor = Color.FromArgb(230, 193, 106);
                    cTheme.HaloColor = Color.FromArgb(191, 219, 255);
                    cTheme.BaseColor = Color.FromArgb(225, 184, 100);                    
                    break;
                case "Chocolate":
                    cTheme.BackColor = Color.FromArgb(87, 54, 34);
                    cTheme.HaloColor = Color.FromArgb(232, 80, 90);
                    cTheme.BaseColor = Color.FromArgb(82, 45, 28);
                    break;
                case "Navy":
                    cTheme.BackColor = Color.FromArgb(88, 121, 169);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(84, 112, 163);
                    break;
                case "Ice":
                    cTheme.BackColor = Color.FromArgb(235, 243, 236);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(228, 234, 230);
                    break;
                case "Vanilla":
                    cTheme.BackColor = Color.FromArgb(233, 243, 213);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(228, 234, 207);
                    break;
                case "Canela":
                    cTheme.BackColor = Color.FromArgb(235, 226, 197);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(228, 217, 191);
                    break;
                case "Cake":
                    cTheme.BackColor = Color.FromArgb(235, 213, 197);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(228, 204, 198);
                    break;
                default:
                    cTheme.BackColor = Color.FromArgb(191, 219, 255);
                    cTheme.HaloColor = Color.FromArgb(254, 209, 94);
                    cTheme.BaseColor = Color.FromArgb(215, 227, 242);
                    break;
            }
            return cTheme;
        }

    }

    public class ColorsTheme
    {
        public Color BackColor { get; set; }
        public Color HaloColor { get; set; }
        public Color BaseColor { get; set; }

    }
}