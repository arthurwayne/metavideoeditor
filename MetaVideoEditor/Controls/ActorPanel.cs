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

    public class ActorPanel : DoubleBufferPanel
    {

        public ActorPanel()
        {
            this.MouseDown += new MouseEventHandler(ActorPanel_MouseDown);
            this.MouseMove += new MouseEventHandler(ActorPanel_MouseMove);
            this.MouseUp += new MouseEventHandler(ActorPanel_MouseUp);
            this.MouseLeave += new EventHandler(ActorPanel_MouseLeave);
            this.MouseEnter += new EventHandler(ActorPanel_MouseEnter);
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(ActorPanel_PreviewKeyDown);
            this.DragEnter += new DragEventHandler(ActorPanel_DragEnter);
            this.DragDrop += new DragEventHandler(ActorPanel_DragDrop);
            this.AllowDrop = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.FixedSingle;

            picBox = new PictureBox();
            picBox.DoubleClick += new EventHandler(NameLabel_DoubleClick);
            picBox.MouseDown += new MouseEventHandler(ActorPanel_MouseDown);
            picBox.MouseMove += new MouseEventHandler(ActorPanel_MouseMove);
            picBox.MouseUp += new MouseEventHandler(ActorPanel_MouseUp);
            picBox.MouseLeave += new EventHandler(ActorPanel_MouseLeave);
            picBox.MouseEnter += new EventHandler(ActorPanel_MouseEnter);
            picBox.BackgroundImageLayout = ImageLayout.Zoom;
            picBox.Dock = DockStyle.Left;
            picBox.Width = 60;
            picBox.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(picBox);

            nameLabel = new Label();
            nameLabel.AutoSize = false;
            nameLabel.Location = new Point(80, 10);
            nameLabel.TextAlign = ContentAlignment.MiddleLeft;
            nameLabel.DoubleClick += new EventHandler(NameLabel_DoubleClick);
            nameLabel.MouseDown += new MouseEventHandler(ActorPanel_MouseDown);
            nameLabel.MouseMove += new MouseEventHandler(ActorPanel_MouseMove);
            nameLabel.MouseUp += new MouseEventHandler(ActorPanel_MouseUp);
            nameLabel.MouseLeave += new EventHandler(ActorPanel_MouseLeave);
            nameLabel.MouseEnter += new EventHandler(ActorPanel_MouseEnter);
            nameLabel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(nameLabel);

            roleLabel = new Label();
            roleLabel.AutoSize = false;
            roleLabel.TextAlign = ContentAlignment.MiddleLeft;
            roleLabel.DoubleClick += new EventHandler(roleLabel_DoubleClick);
            roleLabel.MouseDown += new MouseEventHandler(ActorPanel_MouseDown);
            roleLabel.MouseMove += new MouseEventHandler(ActorPanel_MouseMove);
            roleLabel.MouseUp += new MouseEventHandler(ActorPanel_MouseUp);
            roleLabel.MouseLeave += new EventHandler(ActorPanel_MouseLeave);
            roleLabel.MouseEnter += new EventHandler(ActorPanel_MouseEnter);
            roleLabel.BorderStyle = BorderStyle.FixedSingle;
            this.Controls.Add(roleLabel);

            nameBox = new AdjHeightTextBox();
            nameBox.Dock = DockStyle.Fill;
            nameBox.TextAlign = HorizontalAlignment.Left;
            nameBox.Leave += new EventHandler(nameBox_Leave);
            nameBox.KeyPress += new KeyPressEventHandler(nameBox_KeyPress);
            nameBox.Visible = false;
            nameLabel.Controls.Add(nameBox);

            roleBox = new AdjHeightTextBox();
            roleBox.Dock = DockStyle.Fill;
            roleBox.TextAlign = HorizontalAlignment.Left;
            roleBox.Leave += new EventHandler(roleBox_Leave);
            roleBox.KeyPress += new KeyPressEventHandler(roleBox_KeyPress);
            roleBox.Visible = false;
            roleLabel.Controls.Add(roleBox);

            DeleteBox = new PictureBox();
            DeleteBox.Width = 20;
            DeleteBox.Dock = DockStyle.Right;
            DeleteBox.BackgroundImageLayout = ImageLayout.Zoom;
            DeleteBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.delete;
            DeleteBox.MouseLeave += new EventHandler(ActorPanel_MouseLeave);
            DeleteBox.MouseEnter += new EventHandler(ActorPanel_MouseEnter);
            DeleteBox.Visible = false;
            this.Controls.Add(DeleteBox);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ContextMenuItem editNameMenu = new ContextMenuItem();
            editNameMenu.Text = Kernel.Instance.GetString("EditNameActTab");
            editNameMenu.DefaultItem = true;
            editNameMenu.Click += new EventHandler(NameLabel_DoubleClick);
            ContextMenuItem editRoleMenu = new ContextMenuItem();
            editRoleMenu.Text = Kernel.Instance.GetString("EditRoleActTab");
            editRoleMenu.Click += new EventHandler(roleLabel_DoubleClick);
            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Click += new EventHandler(deleteMenu_Click);
            ContextMenuItem imgMenu = new ContextMenuItem();
            imgMenu.Text = Kernel.Instance.GetString("ChangeImageActTab");
            imgMenu.Click += new EventHandler(imgMenu_Click);

            contextMenu.Items.Add(editNameMenu);
            contextMenu.Items.Add(editRoleMenu);
            contextMenu.Items.Add(imgMenu);
            contextMenu.Items.Add(deleteMenu);
            this.ContextMenuStrip = contextMenu;

        }

        void deleteMenu_Click(object sender, EventArgs e)
        {
            OnActorDelete(this, e);
        }

        void imgMenu_Click(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            //fd.Filter = "jpg (*.jpg)|*.jpg|All files (*.*)|*.*";
            fd.CheckFileExists = true;
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                if (!string.IsNullOrEmpty(ImageUtil.GetLocalImagePath(fd.FileName)))
                {
                    actor.ImagePath = ImageUtil.GetLocalImagePath(fd.FileName);
                    if (OnActorImgChanged != null) OnActorImgChanged(this, e);
                }
            }
        }

        void ActorPanel_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                actor.ImagePath = ImageUtil.GetLocalImagePath(((string[])e.Data.GetData("FileDrop"))[0]);
                if (OnActorImgChanged != null) OnActorImgChanged(this, e);
            }
        }

        void ActorPanel_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                e.Effect = DragDropEffects.Copy;
            }            
        }

        
        void ActorPanel_MouseEnter(object sender, EventArgs e)
        {
            DeleteBox.Visible = true;
        }

        void ActorPanel_MouseLeave(object sender, EventArgs e)
        {
            DeleteBox.Visible = false;
        }

        void nameBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                this.Parent.Focus();
            else if (e.KeyChar == (char)Keys.Escape)
            {
                nameBox.Text = actor.Name;
                nameBox.Visible = false;
            }
        }

        void nameBox_Leave(object sender, EventArgs e)
        {
            nameLabel.Text = nameBox.Text;
            nameBox.Visible = false;
            if (OnActorNameChanged != null && e != null) OnActorNameChanged(this, e);
        }

        void roleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                this.Parent.Focus();
            else if (e.KeyChar == (char)Keys.Escape)
            {
                roleBox.Text = actor.Role;
                roleBox.Visible = false;
            }
        }

        void roleBox_Leave(object sender, EventArgs e)
        {
            roleLabel.Text = roleBox.Text;
            roleBox.Visible = false;
            if (OnActorRoleChanged != null && e != null) OnActorRoleChanged(this, e);
        }

        void roleLabel_DoubleClick(object sender, EventArgs e)
        {
            roleBox.Visible = true;
            roleBox.Focus();
        }

        void NameLabel_DoubleClick(object sender, EventArgs e)
        {
            nameBox.Visible = true;
            nameBox.Focus();
        }


        public event MouseEventHandler OnMouseUpEvent;
        void ActorPanel_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseUpEvent != null && e != null) OnMouseUpEvent(this, e);
        }
        public event MouseEventHandler OnMouseMoveEvent;
        void ActorPanel_MouseMove(object sender, MouseEventArgs e)
        {
            if (OnMouseMoveEvent != null && e != null) OnMouseMoveEvent(this, e);            
        }
        public event MouseEventHandler OnMouseDownEvent;
        void ActorPanel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > this.Width - 20 && e.Y > 25 && e.Y < 41) OnActorDelete(this, e);
            else if (OnMouseDownEvent != null && e != null) OnMouseDownEvent(this, e);
        }

        void ActorPanel_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (OnActorDelete != null && e != null)
            {
                if (e.KeyCode == Keys.Delete) OnActorDelete(this, e);
                if (e.KeyCode == Keys.Return) NameLabel_DoubleClick(sender, e);
            }
        }

        public event EventHandler OnActorNameChanged;
        public event EventHandler OnActorRoleChanged;
        public event EventHandler OnActorDelete;
        public event EventHandler OnActorImgChanged;
       


        private PictureBox picBox;
        private PictureBox DeleteBox; 
        private Label nameLabel;
        private Label roleLabel;
        public AdjHeightTextBox nameBox;
        public AdjHeightTextBox roleBox;

        private Actor _actor;
        private bool _isSelected;

        private Image MainImage;

        public int index = 0;

        public Actor actor
        {
            get
            {
                return _actor; 
            }
            set
            {
                _actor = value;
                if (_actor != null)
                {
                    nameLabel.Text = nameBox.Text = _actor.Name;
                    roleLabel.Text = roleBox.Text = actor.Role;
                    string imgPath;
                    if (ImageUtil.LocalFileExists(actor.ImagePath, out imgPath))
                    {
                        try
                        {
                            FileStream fs = File.Open(imgPath, FileMode.Open);
                            MainImage = Image.FromStream(fs);
                            fs.Close();
                        }
                        catch { }
                        DisplayImage();
                    }
                    else if (!string.IsNullOrEmpty(actor.ImagePath))
                    {
                        Async.Queue(Kernel.Instance.GetString("DownloadAsync") + " " + actor.ImagePath, () =>
                            {
                                if (File.Exists(ImageUtil.GetLocalImagePath(actor.ImagePath)))
                                {
                                    FileStream fs = File.Open(ImageUtil.GetLocalImagePath(actor.ImagePath), FileMode.Open);
                                    MainImage = Image.FromStream(fs);
                                    fs.Close();
                                }
                            }, DisplayImage);
                    }
                    else
                        picBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.MissingPerson;
                    this.Refresh();
                }                
            }
        }

        private void DisplayImage()
        {
           
            if (picBox.InvokeRequired)
            {
                SetImageCallback d = new SetImageCallback(setImage);
                this.Invoke(d, new object[] { });
            }
            else
            {
                setImage();
            }
            
        }

        delegate void SetVisibilityCallback(Label label, bool visibility);
        private void setVisibility(Label label, bool visibility)
        {
            label.Visible = visibility;
        }

        delegate void SetTextCallback(Label label, string text);
        private void setText(Label label, string text)
        {
            label.Text = text;
        }

        delegate void SetImageCallback();
        private void setImage()
        {
            picBox.BackgroundImage = MainImage;
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
               // this.Focus();
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
            //picBox.Location = new Point((int)(this.Width*.025), (int)(this.Height*.01));
            //picBox.Size = new Size((int)(this.Width*.95), (int)(this.Height*.9));
           // nameLabel.Height = (int)(this.Height * .1);
            
            Font font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            nameLabel.Font = roleLabel.Font = font;
            nameLabel.Size = new Size((int)((this.Width -100) / 2), 50);
            //nameBox.Size = new Size((int)((this.Width - 60) / 2), 50);
            roleLabel.Location = new Point(80 + (int)((this.Width -100) / 2), 10);
            roleLabel.Size = new Size((int)((this.Width -100) / 2), 50);
            
        }

    }
}