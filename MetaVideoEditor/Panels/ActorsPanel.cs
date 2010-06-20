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
    public partial class ActorsPanel : BasePanel
    {
        public ActorsPanel()
        {
            InitializeComponent();
            label1.Text = Kernel.Instance.GetString("AddActorActTab");
            MainPanel = panel1;
        }

        public override void UpdateData()
        {
            this.panel2.Controls.Clear();
            this.doubleBufferPanel1.Width = panel2.Width - 40;
            this.panel2.Controls.Add(this.doubleBufferPanel1);
            if (SelectedItem != null)
            {
                if (SelectedItem.Actors.IsNonEmpty())
                {
                    int index = 0;
                    foreach (Actor actor in SelectedItem.Actors)
                    {
                        ActorPanel panel = new ActorPanel();
                        panel.Location = new Point(20, 40 + (index * 70));
                        panel.Size = new Size(panel2.Width-40, 70);
                        panel.actor = actor;
                        panel.index = index;
                        panel.OnMouseDownEvent += new MouseEventHandler(panel_OnMouseDownEvent);
                        panel.OnMouseMoveEvent += new MouseEventHandler(panel_OnMouseMoveEvent);
                        panel.OnMouseUpEvent += new MouseEventHandler(panel_OnMouseUpEvent);
                        panel.OnActorNameChanged += new EventHandler(panel_OnActorNameChanged);
                        panel.OnActorRoleChanged += new EventHandler(panel_OnActorRoleChanged);
                        panel.OnActorImgChanged += new EventHandler(panel_OnActorImgChanged);
                        panel.OnActorDelete += new EventHandler(panel_OnActorDelete);
                        this.panel2.Controls.Add(panel);
                        index++;
                    }
                }
            }
        }

        void panel_OnActorImgChanged(object sender, EventArgs e)
        {
            ActorPanel ap = sender as ActorPanel;
            Actor actor = SelectedItem.Actors.Find(a => a.Name == ap.actor.Name);
            actor.ImagePath = ap.actor.ImagePath;
            HasChanged();
            ap.actor = actor;
        }

        void panel_OnActorDelete(object sender, EventArgs e)
        {
            ActorPanel ap = sender as ActorPanel;
            SelectedItem.Actors.RemoveAll(a => a.Name == ap.actor.Name);
            HasChanged();
            UpdateData();
        }

        void panel_OnActorRoleChanged(object sender, EventArgs e)
        {
            ActorPanel ap = sender as ActorPanel;
            Actor actor = SelectedItem.Actors.Find(a => a.Name == ap.actor.Name);
            actor.Role = ap.roleBox.Text;
            HasChanged();
            ap.roleBox.Text = ap.actor.Role;
            ap.actor = actor;
        }

        void panel_OnActorNameChanged(object sender, EventArgs e)
        {
            ActorPanel ap = sender as ActorPanel;
            Actor actor = SelectedItem.Actors.Find(a => a.Name == ap.actor.Name);
            actor.Name = ap.nameBox.Text;
            HasChanged();
            ap.nameBox.Text = ap.actor.Name;
            ap.actor = actor;
        }

        bool drag;
        int yLoc;
        void panel_OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Default;
            ActorPanel act = sender as ActorPanel;
            act.Location = new Point(20, 40 + (act.index * 70) + panel2.AutoScrollPosition.Y);
        }

        void panel_OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            if (drag)
            {
                ActorPanel act = sender as ActorPanel;
                act.BringToFront();
                int index = (int)((act.Top - panel2.AutoScrollPosition.Y) / 70);
                act.Location = new Point(20, e.Y + act.Top - yLoc);
                if (act.index != index)
                {
                    ActorPanel ap = FindActPanelByIndex(index);
                    if (ap != null)
                    {
                        Actor tmp = SelectedItem.Actors[act.index];
                        SelectedItem.Actors[act.index] = SelectedItem.Actors[index];
                        SelectedItem.Actors[index] = tmp;
                        ap.index = act.index;
                        ap.Location = new Point(20, 40 + (70 * ap.index) + panel2.AutoScrollPosition.Y);
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
                ActorPanel aPanel = sender as ActorPanel;
                foreach (Control control in panel2.Controls)
                {
                    ActorPanel ap = control as ActorPanel;
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
                AddActor();
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
            AddActor();
        }

        private void AddActor()
        {
            textBox.Visible = false;            
            if (!string.IsNullOrEmpty(textBox.Text))
            {
                if (SelectedItem.Actors == null)
                    SelectedItem.Actors = new List<Actor>();
                SelectedItem.Actors.Add(new Actor { Name = textBox.Text });
                textBox.Text = "";
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

        ActorPanel FindActPanelByIndex(int index)
        {
            foreach (Control control in panel2.Controls)
            {
                ActorPanel ap = control as ActorPanel;
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
