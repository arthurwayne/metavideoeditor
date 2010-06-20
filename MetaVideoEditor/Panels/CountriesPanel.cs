using System;
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
    public partial class CountriesPanel : BasePanel
    {
        public CountriesPanel()
        {
            InitializeComponent();
            label1.Text = Kernel.Instance.GetString("AddCountryCoTab");
            label2.Text = Kernel.Instance.GetString("NoCountryCoTab");
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
                if (SelectedItem.Countries.IsNonEmpty())
                {
                    label2.Visible = false;
                    int index = 0;
                    foreach (string text in SelectedItem.Countries)
                    {
                        TextPanel panel = new TextPanel();
                        panel.Location = new Point(20, 40 + (index * 30));
                        panel.Size = new Size(panel2.Width - 40, 30);
                        panel.text = text;
                        panel.index = index;
                        panel.OnMouseDownEvent += new MouseEventHandler(panel_OnMouseDownEvent);
                        panel.OnMouseMoveEvent += new MouseEventHandler(panel_OnMouseMoveEvent);
                        panel.OnMouseUpEvent += new MouseEventHandler(panel_OnMouseUpEvent);
                        panel.OnTextChange += new EventHandler(panel_OnTextChanged);
                        panel.OnTextDelete += new EventHandler(panel_OnTextDelete);
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


        void panel_OnTextDelete(object sender, EventArgs e)
        {
            TextPanel ap = sender as TextPanel;
            SelectedItem.Countries.RemoveAll(a => a == ap.text);
            HasChanged(); 
            UpdateData();
        }


        void panel_OnTextChanged(object sender, EventArgs e)
        {
            TextPanel ap = sender as TextPanel;
            string genre = SelectedItem.Countries.Find(a => a == ap.text);
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
            Cursor = Cursors.Default;
            TextPanel act = sender as TextPanel;
            act.Location = new Point(20, 40 + (act.index * 30) + panel2.AutoScrollPosition.Y);
        }

        void panel_OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            if (drag)
            {
                TextPanel tex = sender as TextPanel;
                tex.BringToFront();
                int index = (int)((tex.Top - panel2.AutoScrollPosition.Y) / 30)-1;
                tex.Location = new Point(20, e.Y + tex.Top - yLoc);
                if (tex.index != index)
                {
                    TextPanel ap = FindTexPanelByIndex(index);
                    if (ap != null)
                    {
                        string tmp = SelectedItem.Countries[tex.index];
                        SelectedItem.Countries[tex.index] = SelectedItem.Countries[index];
                        SelectedItem.Countries[index] = tmp;
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
                if (e.Button == MouseButtons.Left)
                {
                    drag = true;
                    yLoc = e.Y;
                    Cursor.Current = new Cursor(global::MetaVideoEditor.Properties.Resources.drag_cursor.GetHicon());
                }

                TextPanel aPanel = sender as TextPanel;
                foreach (Control control in panel2.Controls)
                {
                    TextPanel ap = control as TextPanel;
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
                AddCountry();
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
            AddCountry();
        }

        private void AddCountry()
        {
            textBox.Visible = false;            
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (SelectedItem.Countries == null)
                    SelectedItem.Countries = new List<string>();
                SelectedItem.Countries.Add(textBox.Text);
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
