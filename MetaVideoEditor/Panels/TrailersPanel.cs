using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class TrailersPanel : BasePanel
    {
        public TrailersPanel()
        {
            InitializeComponent();
            label1.Text = Kernel.Instance.GetString("AddTrailerTraTab");
            label2.Text = Kernel.Instance.GetString("NoTrailerTraTab");
            MainPanel = panel1;
        }

        public override void UpdateData()
        {
            this.panel2.Controls.Clear();
            this.doubleBufferPanel1.Width = panel2.Width - 40;
            this.panel2.Controls.Add(this.doubleBufferPanel1);
            this.panel2.Controls.Add(this.label2);
            if (SelectedItem != null)
            {
                if (SelectedItem.TrailerFiles.IsNonEmpty())
                {
                    label2.Visible = false;
                    int index = 0;
                    foreach (string text in SelectedItem.TrailerFiles)
                    {
                        TrailerPanel panel = new TrailerPanel();
                        panel.Location = new Point(20, 40 + (index * 30));
                        panel.Size = new Size(panel2.Width - 40, 30);
                        panel.text = text;
                        panel.index = index;
                        panel.OnMouseDownEvent += new MouseEventHandler(panel_OnMouseDownEvent);
                        //panel.OnMouseMoveEvent += new MouseEventHandler(panel_OnMouseMoveEvent);
                        //panel.OnMouseUpEvent += new MouseEventHandler(panel_OnMouseUpEvent);
                        panel.OnTextChange += new EventHandler(panel_OnTextChanged);
                        panel.OnTextDelete += new EventHandler(panel_OnTextDelete);
                        panel.OnDownloadComplete += new EventHandler(panel_OnDownloadComlete);
                        panel.OnPlay += new EventHandler(panel_OnPlay);
                        this.panel2.Controls.Add(panel);
                        index++;
                    }
                }
                else
                {
                    label2.Visible = true;
                }
            }
        }

        void panel_OnDownloadComlete(object sender, EventArgs e)
        {
            TrailerPanel tp = sender as TrailerPanel;
            SelectedItem.TrailerFiles[SelectedItem.TrailerFiles.FindIndex(t => t == tp.text)] = tp.localFile;
            tp.text = tp.localFile;
        }

        void panel_OnPlay(object sender, EventArgs e)
        {
            TrailerPanel tp = sender as TrailerPanel;
            axWindowsMediaPlayer1.URL = tp.text;
        }

        void panel_OnTextDelete(object sender, EventArgs e)
        {
            TrailerPanel ap = sender as TrailerPanel;
            SelectedItem.TrailerFiles.RemoveAll(a => a == ap.text);            
            HasChanged();
            UpdateData();
        }


        void panel_OnTextChanged(object sender, EventArgs e)
        {
            TrailerPanel ap = sender as TrailerPanel;
            string genre = SelectedItem.TrailerFiles.Find(a => a == ap.text);
            genre = ap.textBox.Text;
            HasChanged();
            ap.textBox.Text = genre;
            ap.Text = genre;
        }

        bool drag;
        int yLoc;
        void panel_OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            drag = false;
            TrailerPanel act = sender as TrailerPanel;
            act.Location = new Point(20, 40 + (act.index * 30) + panel2.AutoScrollPosition.Y);
        }

        void panel_OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            if (drag)
            {
                TrailerPanel tex = sender as TrailerPanel;
                tex.BringToFront();
                int index = (int)((tex.Top - panel2.AutoScrollPosition.Y) / 30)-1;
                tex.Location = new Point(20, e.Y + tex.Top - yLoc);
                if (tex.index != index)
                {
                    TextPanel ap = FindTexPanelByIndex(index);
                    if (ap != null)
                    {
                        string tmp = SelectedItem.TrailerFiles[tex.index];
                        SelectedItem.TrailerFiles[tex.index] = SelectedItem.TrailerFiles[index];
                        SelectedItem.TrailerFiles[index] = tmp;
                        ap.index = tex.index;
                        ap.Location = new Point(20, 40 + (30 * ap.index) + panel2.AutoScrollPosition.Y);
                        tex.index = index;
                        HasChanged();
                    }
                }
            }
        }

        void panel_OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left || e.Button == MouseButtons.Right)
            {
                //drag = true;
                //yLoc = e.Y;

                TrailerPanel aPanel = sender as TrailerPanel;
                foreach (Control control in panel2.Controls)
                {
                    TrailerPanel ap = control as TrailerPanel;
                    if (ap != null)
                    {
                        bool selected = (ap.index == aPanel.index);
                        ap.IsSelected = selected;
                        if (selected) ap.Focus();
                    }
                }
                
            }
        }


        private void doubleBufferPanel1_DoubleClick(object sender, EventArgs e)
        {
            textBox.Visible = true;
            textBox.Focus();
        }

        private void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                AddGenre();
            }
            if (e.KeyChar == (char)Keys.Escape)
            {
                textBox.Text = "";
                textBox.Visible = false;
                panel2.Focus();
            }
        }

        private void textBox_Leave(object sender, EventArgs e)
        {
            AddGenre();
        }

        private void AddGenre()
        {
            textBox.Visible = false;            
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (SelectedItem.TrailerFiles == null)
                    SelectedItem.TrailerFiles = new List<string>();
                SelectedItem.TrailerFiles.Add(textBox.Text);
                textBox.Text = "";
                HasChanged();
                UpdateData();
            }
        }

        private void label1_DoubleClick(object sender, EventArgs e)
        {
            doubleBufferPanel1_DoubleClick(sender, e);
        }

        private void panel2_Click(object sender, EventArgs e)
        {
            panel2.Focus();
        }

        TextPanel FindTexPanelByIndex(int index)
        {
            foreach (Control control in panel2.Controls)
            {
                TextPanel ap = control as TextPanel;
                if (ap != null && ap.index == index)
                    return ap;
            }
            return null;
        }

        private void panel2_Resize(object sender, EventArgs e)
        {
            foreach (Control c in panel2.Controls)
                c.Width = panel2.Width - 40; 
        }


    }
}
