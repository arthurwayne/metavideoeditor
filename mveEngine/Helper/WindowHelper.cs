using System;
using System.Windows.Forms;
using System.Drawing;

namespace mveEngine
{
    public class WindowHelper
    {

        public static void GeometryFromString(string thisWindowGeometry, Form formIn)
        {
            if (string.IsNullOrEmpty(thisWindowGeometry) == true)
            {
                return;
            }
            string[] numbers = thisWindowGeometry.Split('|');
            string windowString = numbers[4];
            if (windowString == "Normal")
            {
                Point windowPoint = new Point(int.Parse(numbers[0]),
                    int.Parse(numbers[1]));
                Size windowSize = new Size(int.Parse(numbers[2]),
                    int.Parse(numbers[3]));

                bool locOkay = GeometryIsBizarreLocation(windowPoint, windowSize);
                bool sizeOkay = GeometryIsBizarreSize(windowSize);

                if (locOkay == true && sizeOkay == true)
                {
                    formIn.Location = windowPoint;
                    formIn.Size = windowSize;
                    formIn.StartPosition = FormStartPosition.Manual;
                    formIn.WindowState = FormWindowState.Normal;
                }
                else if (sizeOkay == true)
                {
                    formIn.Size = windowSize;
                }
            }
            else if (windowString == "Maximized")
            {
                formIn.Location = new Point(100, 100);
                formIn.StartPosition = FormStartPosition.Manual;
                formIn.WindowState = FormWindowState.Maximized;
            }

            foreach (Control control in formIn.Controls)
            {
                if (control.GetType() == typeof(SplitContainer))
                {
                    SplitContainer sc = control as SplitContainer;
                    try { sc.SplitterDistance = int.Parse(numbers[5]); }
                    catch { }

                    foreach (Control c in sc.Panel2.Controls)
                    {
                        foreach (Control ct in c.Controls)
                        {
                            if (ct.Name == "tabControl")
                            {
                                try
                                {
                                    TabControl tc = ct as TabControl;
                                    for (int i = 0; i < tc.TabCount; i++)
                                    {
                                        TabPage currentTab = GetTabPageByName(tc, numbers[6 + i]);
                                        tc.TabPages.Remove(currentTab);
                                        tc.TabPages.Add(currentTab);
                                    }
                                }
                                catch { }
                            }
                        }
                    }
                }
            }
        }

        private static bool GeometryIsBizarreLocation(Point loc, Size size)
        {
            bool locOkay;
            if (loc.X < 0 || loc.Y < 0)
            {
                locOkay = false;
            }
            else if (loc.X + size.Width > Screen.PrimaryScreen.WorkingArea.Width)
            {
                locOkay = false;
            }
            else if (loc.Y + size.Height > Screen.PrimaryScreen.WorkingArea.Height)
            {
                locOkay = false;
            }
            else
            {
                locOkay = true;
            }
            return locOkay;
        }

        private static bool GeometryIsBizarreSize(Size size)
        {
            return (size.Height <= Screen.PrimaryScreen.WorkingArea.Height &&
                size.Width <= Screen.PrimaryScreen.WorkingArea.Width);
        }

        public static string GeometryToString(Form mainForm)
        {
            string SplitterDistance = "";
            string TabOrder = "";
            foreach (Control control in mainForm.Controls)
            {
                if (control.GetType() == typeof(SplitContainer))
                {
                    SplitContainer sc = control as SplitContainer;
                    SplitterDistance = sc.SplitterDistance.ToString();
                    foreach (Control c in sc.Panel2.Controls)
                    {
                        foreach (Control ct in c.Controls)
                        {
                            if (ct.Name == "tabControl")
                            {
                                TabControl tc = ct as TabControl;
                                for (int i = 0; i < tc.TabCount; i++)
                                {
                                    TabOrder += tc.TabPages[i].Name + "|";
                                }
                            }
                        }
                    }
                }
            }

            return mainForm.Location.X.ToString() + "|" +
                mainForm.Location.Y.ToString() + "|" +
                mainForm.Size.Width.ToString() + "|" +
                mainForm.Size.Height.ToString() + "|" +
                mainForm.WindowState.ToString() + "|" +
                SplitterDistance + "|" +
                TabOrder;

        }

        static TabPage GetTabPageByName(TabControl tc, string name)
        {
            foreach (TabPage tb in tc.TabPages)
            {
                if (tb.Name == name)
                    return tb;
            }
            return null;
        }

    }


}