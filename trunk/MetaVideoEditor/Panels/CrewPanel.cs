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
    public partial class CrewPanel : BasePanel
    {
        public CrewPanel()
        {
            InitializeComponent();
            label1.Text = Kernel.Instance.GetString("AddCrewMemberCreTab");
            MainPanel = panel1;
        }

        public override void UpdateData()
        {
            this.panel2.Controls.Clear();
            this.doubleBufferPanel1.Width = panel2.Width - 40;
            this.panel2.Controls.Add(this.doubleBufferPanel1);
            if (SelectedItem != null)
            {
                if (SelectedItem.Crew.IsNonEmpty())
                {
                    int index = 0;
                    foreach (CrewMember crewMember in SelectedItem.Crew)
                    {
                        CrewMemberPanel panel = new CrewMemberPanel();
                        panel.Location = new Point(20, 40 + (index * 30));
                        panel.Size = new Size(panel2.Width-40, 30);
                        panel.crewMember = crewMember;
                        panel.index = index;
                        panel.OnMouseDownEvent += new MouseEventHandler(panel_OnMouseDownEvent);
                        panel.OnMouseMoveEvent += new MouseEventHandler(panel_OnMouseMoveEvent);
                        panel.OnMouseUpEvent += new MouseEventHandler(panel_OnMouseUpEvent);
                        panel.OnCrewNameChanged += new EventHandler(panel_OnCrewNameChanged);
                        panel.OnCrewActivityChanged += new EventHandler(panel_OnCrewActivityChanged);
                        panel.OnCrewDelete += new EventHandler(panel_OnCrewDelete);
                        this.panel2.Controls.Add(panel);
                        index++;
                    }
                }
            }
        }

        void panel_OnCrewDelete(object sender, EventArgs e)
        {
            CrewMemberPanel ap = sender as CrewMemberPanel;
            SelectedItem.Crew.RemoveAll(a => a.Name == ap.crewMember.Name);
            HasChanged();
            UpdateData();
        }

        void panel_OnCrewActivityChanged(object sender, EventArgs e)
        {
            CrewMemberPanel ap = sender as CrewMemberPanel;
            CrewMember member = SelectedItem.Crew.Find(a => a.Name == ap.crewMember.Name);
            member.Activity = ap.activityBox.Text;
            HasChanged();
            ap.activityBox.Text = ap.crewMember.Activity;
            ap.crewMember = member;
        }

        void panel_OnCrewNameChanged(object sender, EventArgs e)
        {
            CrewMemberPanel ap = sender as CrewMemberPanel;
            CrewMember member = SelectedItem.Crew.Find(a => a.Name == ap.crewMember.Name);
            member.Name = ap.nameBox.Text;
            HasChanged();
            ap.nameBox.Text = ap.crewMember.Name;
            ap.crewMember = member;
        }

        bool drag;
        int yLoc;
        void panel_OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Default;
            CrewMemberPanel act = sender as CrewMemberPanel;
            act.Location = new Point(20, 40 + (act.index * 30) + panel2.AutoScrollPosition.Y);
        }

        void panel_OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            if (drag)
            {
                CrewMemberPanel act = sender as CrewMemberPanel;
                act.BringToFront();
                int index = (int)((act.Top - panel2.AutoScrollPosition.Y) / 30)-1;
                act.Location = new Point(20, e.Y + act.Top - yLoc);
                if (act.index != index)
                {
                    CrewMemberPanel ap = FindCrewPanelByIndex(index);
                    if (ap != null)
                    {
                        CrewMember tmp = SelectedItem.Crew[act.index];
                        SelectedItem.Crew[act.index] = SelectedItem.Crew[index];
                        SelectedItem.Crew[index] = tmp;
                        ap.index = act.index;
                        ap.Location = new Point(20, 40 + (30 * ap.index) + panel2.AutoScrollPosition.Y);
                        act.index = index;
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

                CrewMemberPanel aPanel = sender as CrewMemberPanel;
                foreach (Control control in panel2.Controls)
                {
                    CrewMemberPanel ap = control as CrewMemberPanel;
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
                AddMember();
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
            AddMember();
        }

        private void AddMember()
        {
            textBox.Visible = false;            
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (SelectedItem.Crew == null)
                    SelectedItem.Crew = new List<CrewMember>();
                SelectedItem.Crew.Add(new CrewMember { Name = textBox.Text });
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

        CrewMemberPanel FindCrewPanelByIndex(int index)
        {
            foreach (Control control in panel2.Controls)
            {
                CrewMemberPanel ap = control as CrewMemberPanel;
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
