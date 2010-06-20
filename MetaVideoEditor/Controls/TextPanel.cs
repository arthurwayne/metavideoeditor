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

    public class TextPanel : DoubleBufferPanel
    {

        public TextPanel()
        {
            this.MouseDown += new MouseEventHandler(TextPanel_MouseDown);
            this.MouseMove += new MouseEventHandler(TextPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(TextPanel_MouseUp);
            this.MouseLeave += new EventHandler(TextPanel_MouseLeave);
            this.MouseEnter += new EventHandler(TextPanel_MouseEnter);
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(TextPanel_PreviewKeyDown);
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.FixedSingle;

            textLabel = new Label();
            textLabel.AutoSize = false;
            textLabel.Font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            textLabel.Dock = DockStyle.Fill;
            textLabel.TextAlign = ContentAlignment.MiddleLeft;
            textLabel.DoubleClick += new EventHandler(TextLabel_DoubleClick);
            textLabel.MouseDown += new MouseEventHandler(TextPanel_MouseDown);
            textLabel.MouseMove += new MouseEventHandler(TextPanel_MouseMove);
            textLabel.MouseUp += new MouseEventHandler(TextPanel_MouseUp);
            textLabel.MouseLeave += new EventHandler(TextPanel_MouseLeave);
            textLabel.MouseEnter += new EventHandler(TextPanel_MouseEnter);
            this.Controls.Add(textLabel);

            textBox = new AdjHeightTextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.TextAlign = HorizontalAlignment.Left;
            textBox.Leave += new EventHandler(textBox_Leave);
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            textBox.Visible = false;
            textLabel.Controls.Add(textBox);

            DeleteBox = new PictureBox();
            DeleteBox.Width = 20;
            DeleteBox.Dock = DockStyle.Right;
            DeleteBox.BackgroundImageLayout = ImageLayout.Zoom;
            DeleteBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.delete;
            DeleteBox.MouseLeave += new EventHandler(TextPanel_MouseLeave);
            DeleteBox.MouseEnter += new EventHandler(TextPanel_MouseEnter);
            DeleteBox.Visible = false;
            this.Controls.Add(DeleteBox);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ContextMenuItem editMenu = new ContextMenuItem();
            editMenu.Text = Kernel.Instance.GetString("EditMW");
            editMenu.Shortcut = "Return";
            editMenu.Image = global::MetaVideoEditor.Properties.Resources.edit;
            editMenu.DefaultItem = true;
            editMenu.Click += new EventHandler(TextLabel_DoubleClick);
            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Shortcut = "Del";
            deleteMenu.Image = global::MetaVideoEditor.Properties.Resources.delete;
            deleteMenu.Click += new EventHandler(deleteMenu_Click);

            contextMenu.Items.Add(editMenu);
            contextMenu.Items.Add(deleteMenu);
            this.ContextMenuStrip = contextMenu;

        }

        void deleteMenu_Click(object sender, EventArgs e)
        {
            OnTextDelete(this, e);
        }
        
        void TextPanel_MouseEnter(object sender, EventArgs e)
        {
            DeleteBox.Visible = true;
        }

        void TextPanel_MouseLeave(object sender, EventArgs e)
        {
            DeleteBox.Visible = false;
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                this.Parent.Focus();
            else if (e.KeyChar == (char)Keys.Escape)
            {
                textBox.Text = text;
                textBox.Visible = false;
                this.Parent.Focus();
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {
            textLabel.Text = textBox.Text;
            textBox.Visible = false;
            if (OnTextChange != null && e != null) OnTextChange(this, e);
        }

        void TextLabel_DoubleClick(object sender, EventArgs e)
        {
            textBox.Visible = true;
            textBox.Focus();
        }


        public event MouseEventHandler OnMouseUpEvent;
        void TextPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseUpEvent != null && e != null) OnMouseUpEvent(this, e);
        }
        public event MouseEventHandler OnMouseMoveEvent;
        void TextPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (OnMouseMoveEvent != null && e != null) OnMouseMoveEvent(this, e);            
        }
        public event MouseEventHandler OnMouseDownEvent;
        void TextPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > this.Width - 20) OnTextDelete(this, e);
            else if (OnMouseDownEvent != null && e != null) OnMouseDownEvent(this, e);
        }

        void TextPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (OnTextDelete != null && e != null)
            {
                if (e.KeyCode == Keys.Delete) OnTextDelete(this, e);
                if (e.KeyCode == Keys.Return) TextLabel_DoubleClick(sender, e);
            }
        }

        public event EventHandler OnTextChange;
        public event EventHandler OnTextDelete;

        private PictureBox DeleteBox; 
        private Label textLabel;
        public AdjHeightTextBox textBox;

        private string _text;
        private bool _isSelected;

        public int index = 0; 

        public string text
        {
            get
            {
                return _text;  
            }
            set
            {
                _text = value;
                if (!string.IsNullOrEmpty(_text))
                {
                    textLabel.Text = textBox.Text = _text;
                    this.Refresh();
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
    }
}