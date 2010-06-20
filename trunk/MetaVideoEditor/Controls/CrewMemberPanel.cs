using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;
using mveEngine;

namespace CustomControls
{

    public class CrewMemberPanel : DoubleBufferPanel
    {

        public CrewMemberPanel()
        {
            this.MouseDown += new MouseEventHandler(CrewPanel_MouseDown);
            this.MouseMove += new MouseEventHandler(CrewPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(CrewPanel_MouseUp);
            this.MouseLeave += new EventHandler(CrewPanel_MouseLeave);
            this.MouseEnter += new EventHandler(CrewPanel_MouseEnter);
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(CrewPanel_PreviewKeyDown);
            this.AllowDrop = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.FixedSingle;

            nameLabel = new Label();
            nameLabel.AutoSize = false;
            nameLabel.Location = new Point(0, 0);
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.DoubleClick += new EventHandler(NameLabel_DoubleClick);
            nameLabel.MouseDown += new MouseEventHandler(CrewPanel_MouseDown);
            nameLabel.MouseMove += new MouseEventHandler(CrewPanel_MouseMove);
            nameLabel.MouseUp += new MouseEventHandler(CrewPanel_MouseUp);
            nameLabel.MouseLeave += new EventHandler(CrewPanel_MouseLeave);
            nameLabel.MouseEnter += new EventHandler(CrewPanel_MouseEnter);
            nameLabel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(nameLabel);

            activityLabel = new Label();
            activityLabel.AutoSize = false;
            activityLabel.TextAlign = ContentAlignment.MiddleLeft;
            activityLabel.DoubleClick += new EventHandler(activityLabel_DoubleClick);
            activityLabel.MouseDown += new MouseEventHandler(CrewPanel_MouseDown);
            activityLabel.MouseMove += new MouseEventHandler(CrewPanel_MouseMove);
            activityLabel.MouseUp += new MouseEventHandler(CrewPanel_MouseUp);
            activityLabel.MouseLeave += new EventHandler(CrewPanel_MouseLeave);
            activityLabel.MouseEnter += new EventHandler(CrewPanel_MouseEnter);
            activityLabel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(activityLabel);

            nameBox = new AdjHeightTextBox();
            nameBox.Dock = DockStyle.Fill;
            nameBox.TextAlign = HorizontalAlignment.Left;
            nameBox.Leave += new EventHandler(nameBox_Leave);
            nameBox.KeyPress += new KeyPressEventHandler(nameBox_KeyPress);
            nameBox.Visible = false;
            nameLabel.Controls.Add(nameBox);

            activityBox = new AdjHeightTextBox();
            activityBox.Dock = DockStyle.Fill;
            activityBox.TextAlign = HorizontalAlignment.Left;
            activityBox.Leave += new EventHandler(roleBox_Leave);
            activityBox.KeyPress += new KeyPressEventHandler(roleBox_KeyPress);
            activityBox.Visible = false;
            activityLabel.Controls.Add(activityBox);

            DeleteBox = new PictureBox();
            DeleteBox.Width = 20;
            DeleteBox.Dock = DockStyle.Right;
            DeleteBox.BackgroundImageLayout = ImageLayout.Zoom;
            DeleteBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.delete;
            DeleteBox.MouseLeave += new EventHandler(CrewPanel_MouseLeave);
            DeleteBox.MouseEnter += new EventHandler(CrewPanel_MouseEnter);
            DeleteBox.Visible = false;
            this.Controls.Add(DeleteBox);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ContextMenuItem editNameMenu = new ContextMenuItem();
            editNameMenu.Text = Kernel.Instance.GetString("EditNameActTab");
            editNameMenu.DefaultItem = true;
            editNameMenu.Click += new EventHandler(NameLabel_DoubleClick);
            ContextMenuItem editActivityMenu = new ContextMenuItem();
            editActivityMenu.Text = Kernel.Instance.GetString("EditActivityCreTab");
            editActivityMenu.Click += new EventHandler(activityLabel_DoubleClick);
            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Click += new EventHandler(deleteMenu_Click);

            contextMenu.Items.Add(editNameMenu);
            contextMenu.Items.Add(editActivityMenu);
            contextMenu.Items.Add(deleteMenu);
            this.ContextMenuStrip = contextMenu;
        }

        void deleteMenu_Click(object sender, EventArgs e)
        {
            OnCrewDelete(this, e);
        }
               
        void CrewPanel_MouseEnter(object sender, EventArgs e)
        {
            DeleteBox.Visible = true;
        }

        void CrewPanel_MouseLeave(object sender, EventArgs e)
        {
            DeleteBox.Visible = false;
        }

        void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                this.Parent.Focus();
            else if (e.KeyChar == (char)Keys.Escape)
            {
                nameBox.Text = crewMember.Name;
                nameBox.Visible = false;
            }
        }

        void nameBox_Leave(object sender, EventArgs e)
        {
            nameLabel.Text = nameBox.Text;
            nameBox.Visible = false;
            if (OnCrewNameChanged != null && e != null) OnCrewNameChanged(this, e);
        }

        void roleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                this.Parent.Focus();
            else if (e.KeyChar == (char)Keys.Escape)
            {
                activityBox.Text = crewMember.Activity;
                activityBox.Visible = false;
            }
        }

        void roleBox_Leave(object sender, EventArgs e)
        {
            activityLabel.Text = activityBox.Text;
            activityBox.Visible = false;
            if (OnCrewActivityChanged != null && e != null) OnCrewActivityChanged(this, e);
        }

        void activityLabel_DoubleClick(object sender, EventArgs e)
        {
            activityBox.Visible = true;
            activityBox.Focus();
        }

        void NameLabel_DoubleClick(object sender, EventArgs e)
        {
            nameBox.Visible = true;
            nameBox.Focus();
        }


        public event MouseEventHandler OnMouseUpEvent;
        void CrewPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseUpEvent != null && e != null) OnMouseUpEvent(this, e);
        }
        public event MouseEventHandler OnMouseMoveEvent;
        void CrewPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (OnMouseMoveEvent != null && e != null) OnMouseMoveEvent(this, e);            
        }
        public event MouseEventHandler OnMouseDownEvent;
        void CrewPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > this.Width - 20 && e.Y > 25 && e.Y < 41) OnCrewDelete(this, e);
            else if (OnMouseDownEvent != null && e != null) OnMouseDownEvent(this, e);
        }

        void CrewPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (OnCrewDelete != null && e != null)
            {
                if (e.KeyCode == Keys.Delete) OnCrewDelete(this, e);
                if (e.KeyCode == Keys.Return) NameLabel_DoubleClick(sender, e);
            }
        }

        public event EventHandler OnCrewNameChanged;
        public event EventHandler OnCrewActivityChanged;
        public event EventHandler OnCrewDelete;      


        private PictureBox DeleteBox; 
        private Label nameLabel;
        private Label activityLabel;
        public AdjHeightTextBox nameBox;
        public AdjHeightTextBox activityBox;

        private CrewMember _crewMember;
        private bool _isSelected;

        public int index = 0;

        public CrewMember crewMember
        {
            get
            {
                return _crewMember; 
            }
            set
            {
                _crewMember = value;
                if (_crewMember != null)
                {
                    nameLabel.Text = nameBox.Text = _crewMember.Name;
                    activityLabel.Text = activityBox.Text = crewMember.Activity;           
                }                
            }
        }

       

        public bool IsSelected
        {
            get
            {
                return _isSelected;
            }
            set
            {
                _isSelected = value;
                if (value)
                {
                    this.BackgroundImage = global::MetaVideoEditor.Properties.Resources.B_click2;
                }
                else
                    this.BackgroundImage = null;
            }
        }


        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            
            Font font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            nameLabel.Font = activityLabel.Font = font;
            nameLabel.Size = new Size((int)((this.Width -20) / 2), 30);
            activityLabel.Location = new Point((int)((this.Width -20) / 2), 0);
            activityLabel.Size = new Size((int)((this.Width -20) / 2), 30);
            
        }

    }
}