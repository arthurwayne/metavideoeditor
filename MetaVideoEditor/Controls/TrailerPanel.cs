using System;
using System.IO;
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

    public class TrailerPanel : DoubleBufferPanel
    {

        public TrailerPanel()
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

            contextMenu = new ContextMenuStrip();
            ContextMenuItem playMenu = new ContextMenuItem();
            playMenu.Text = Kernel.Instance.GetString("PlayTraTab");
            playMenu.DefaultItem = true;
            playMenu.Image = global::MetaVideoEditor.Properties.Resources.play;
            playMenu.Click += new EventHandler(playMenu_Click);

            dlMenu = new ContextMenuItem();
            dlMenu.Text = Kernel.Instance.GetString("DownloadTraTab");
            dlMenu.Image = global::MetaVideoEditor.Properties.Resources.download;
            dlMenu.Click += new EventHandler(dlMenu_Click);

            ContextMenuItem editMenu = new ContextMenuItem();
            editMenu.Text = Kernel.Instance.GetString("EditMW");
            editMenu.Image = global::MetaVideoEditor.Properties.Resources.edit;
            editMenu.Click += new EventHandler(editMenu_Click);

            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Image = global::MetaVideoEditor.Properties.Resources.delete;
            deleteMenu.Click += new EventHandler(deleteMenu_Click);

            contextMenu.Items.Add(playMenu);
            contextMenu.Items.Add(editMenu);
            contextMenu.Items.Add(dlMenu);
            contextMenu.Items.Add(deleteMenu);
            this.ContextMenuStrip = contextMenu;

        }

        void editMenu_Click(object sender, EventArgs e)
        {
            textBox.Visible = true;
            textBox.Focus();
        }

        ContextMenuStrip contextMenu;
        ContextMenuItem dlMenu;

        void playMenu_Click(object sender, EventArgs e)
        {
            if (OnPlay != null) OnPlay(this, e);
        }

        void dlMenu_Click(object sender, EventArgs e)
        {
            Async.Queue(Kernel.Instance.GetString("DownloadAsync") + " " + text, () =>
            {                
                HtmlUtil.Download(localFile, text);
            }, DownloadComplete);
        }

        void DownloadComplete()
        {
            if (File.Exists(localFile))
            {                
                if (OnDownloadComplete != null) OnDownloadComplete(this, new EventArgs());
            }
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
            playMenu_Click(sender, e);
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
        public event EventHandler OnDownloadComplete;
        public event EventHandler OnPlay;

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
                    SetLabelText();
                    SetBoxText();
                }
                SetDLenable();
            }
        }

        delegate void SetTextDelegate();
        void SetLabelText()
        {
            if (textLabel.InvokeRequired)
            {
                Invoke(new SetTextDelegate(SetLabelText), new object[] { });
                return;
            }
            textLabel.Text = _text;
        }

        void SetBoxText()
        {
            if (textBox.InvokeRequired)
            {
                Invoke(new SetTextDelegate(SetBoxText), new object[] { });
                return;
            }
            textBox.Text = _text;
        }

        delegate void SetDLenableDelegate();
        void SetDLenable()
        {
            if (contextMenu.InvokeRequired)
            {
                Invoke(new SetDLenableDelegate(SetDLenable), new object[] { });
                return;
            }
            if (_text.StartsWith("http://"))
                dlMenu.Enabled = true;
            else
                dlMenu.Enabled = false;
        }

        public string localFile
        {
            get
            {
                if (!Directory.Exists(Config.Instance.TrailerPath))
                    Directory.CreateDirectory(Config.Instance.TrailerPath);
                if (string.IsNullOrEmpty(text))
                    return "";
                if (File.Exists(text))
                    return text;
                if (text.StartsWith("http://"))
                    return Path.Combine(Config.Instance.TrailerPath, Path.GetFileName(text));
                return "";
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