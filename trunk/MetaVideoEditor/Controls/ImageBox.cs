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

    public class ImageBox : DoubleBufferPanel
    {
        public ImageBox()
        {
            ShowResolution = true;
            Init();
        }
        public ImageBox(bool ShowRes)
        {
            ShowResolution = ShowRes;
            Init();
        }

        public void Init()
        {
            this.DoubleClick += new EventHandler(ImageBox_DoubleClick);
            this.MouseDown += new MouseEventHandler(ImageBox_MouseDown);
            this.MouseMove += new MouseEventHandler(ImageBox_MouseMove);
            this.MouseUp += new MouseEventHandler(ImageBox_MouseUp);
            this.MouseEnter += new EventHandler(ImageBox_MouseEnter);
            this.MouseLeave += new EventHandler(ImageBox_MouseLeave);
            this.DragDrop += new DragEventHandler(ImageBox_DragDrop);
            this.DragEnter += new DragEventHandler(ImageBox_DragEnter);            
            this.PreviewKeyDown += new PreviewKeyDownEventHandler(ImageBox_PreviewKeyDown);
            this.AllowDrop = true;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.BackColor = Color.Transparent;
            this.BorderStyle = BorderStyle.FixedSingle;

            picBox = new PictureBox();
            picBox.DoubleClick += new EventHandler(ImageBox_DoubleClick);
            picBox.MouseDown += new MouseEventHandler(ImageBox_MouseDown);
            picBox.MouseMove += new MouseEventHandler(ImageBox_MouseMove);
            picBox.MouseUp += new MouseEventHandler(ImageBox_MouseUp);
            picBox.MouseEnter += new EventHandler(ImageBox_MouseEnter);
            picBox.MouseLeave += new EventHandler(ImageBox_MouseLeave);
            picBox.BackgroundImageLayout = ImageLayout.Zoom;            
            this.Controls.Add(picBox);

            checkedPic = new PictureBox();
            checkedPic.Location = new Point(0, 0);
            checkedPic.Size = new Size(20, 20);
            checkedPic.Image = global::MetaVideoEditor.Properties.Resources.checkbox_checked;
            checkedPic.BackColor = Color.Transparent;
            checkedPic.SizeMode = PictureBoxSizeMode.StretchImage;
            picBox.Controls.Add(checkedPic);

            if (ShowResolution)
            {
                sizeLabel = new Label();
                sizeLabel.AutoSize = false;
                sizeLabel.Dock = DockStyle.Bottom;
                sizeLabel.TextAlign = ContentAlignment.MiddleCenter;
                sizeLabel.DoubleClick += new EventHandler(ImageBox_DoubleClick);
                sizeLabel.MouseDown += new MouseEventHandler(sizeLabel_MouseDown);
                sizeLabel.MouseEnter += new EventHandler(ImageBox_MouseEnter);
                sizeLabel.MouseLeave += new EventHandler(ImageBox_MouseLeave);
                this.Controls.Add(sizeLabel);
            }

            imgStateLabel = new Label();
            imgStateLabel.AutoSize = false;
            imgStateLabel.TextAlign = ContentAlignment.MiddleCenter;
            imgStateLabel.Dock = DockStyle.Fill;
            picBox.Controls.Add(imgStateLabel);

            deleteBox = new PictureBox();
            deleteBox.Width = 18;
            deleteBox.Dock = DockStyle.Right;
            deleteBox.BackgroundImageLayout = ImageLayout.Zoom;
            deleteBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.delete;            
            deleteBox.Visible = false;
            if (ShowResolution)
                sizeLabel.Controls.Add(deleteBox);
            else
                this.Controls.Add(deleteBox);

            ContextMenuItem openMenu = new ContextMenuItem();
            openMenu.Text = Kernel.Instance.GetString("OpenMW");
            openMenu.DefaultItem = true;
            openMenu.Shortcut = "Return";
            openMenu.Image = global::MetaVideoEditor.Properties.Resources.folder;
            openMenu.Click += new EventHandler(openImg);

            checkMenu = new ContextMenuItem();
            checkMenu.Shortcut = "Space";
            checkMenu.Click += new EventHandler(CheckUnCheck);

            ContextMenuStrip contextMenu = new ContextMenuStrip();
            ContextMenuItem editMenu = new ContextMenuItem();
            editMenu.Text = Kernel.Instance.GetString("ChangeImageActTab");
            editMenu.Shortcut = "+";
            editMenu.Image = global::MetaVideoEditor.Properties.Resources.edit;
            editMenu.Click += new EventHandler(ChangeImg);
            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Shortcut = "Del";
            deleteMenu.Image = global::MetaVideoEditor.Properties.Resources.delete;
            deleteMenu.Click += new EventHandler(deleteMenu_Click);

            contextMenu.Items.Add(openMenu);
            contextMenu.Items.Add(checkMenu);
            contextMenu.Items.Add(editMenu);
            contextMenu.Items.Add(deleteMenu);
            
            this.ContextMenuStrip = contextMenu;
            
        }

        void ImageBox_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Delete && ImgPoster != null && OnPictureDeleted != null)
            {
                OnPictureDeleted(this, e);
            }
            else if (e.KeyCode == Keys.Return)
                openImg(sender, e);
            else if (e.KeyCode == Keys.Space)
                CheckUnCheck(sender, e);
            else if (e.KeyCode == Keys.Add)
                ChangeImg(sender, e);

        }

        void ImageBox_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
                e.Effect = DragDropEffects.Copy;
        }

        void ImageBox_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                string img = ImageUtil.GetLocalImagePath(((string[])e.Data.GetData("FileDrop"))[0]);
                if (ImgPoster == null)
                {
                    ImgPoster = new Poster { Checked = true };
                }
                ImgPoster.Image = img;
                RefreshBox();
                if (OnPictureChanged != null) OnPictureChanged(this, e);
            }
        }

        void ChangeImg(object sender, EventArgs e)
        {
            OpenFileDialog fd = new OpenFileDialog();
            //fd.Filter = "jpg (*.jpg)|*.jpg|All files (*.*)|*.*";
            fd.CheckFileExists = true;
            fd.Multiselect = false;
            if (fd.ShowDialog() == DialogResult.OK)
            {
                string img = ImageUtil.GetLocalImagePath(fd.FileName);
                if (!string.IsNullOrEmpty(img))
                {
                    if (ImgPoster == null)
                        ImgPoster = new Poster { Checked = true };
                    ImgPoster.Image = img;
                    RefreshBox();
                    if (OnPictureChanged != null) OnPictureChanged(this, e);
                }
            }
        }

        void openImg(object sender, EventArgs e)
        {
            string imgPath = "";
            if (ImgPoster == null || !ImageUtil.LocalFileExists(ImgPoster.Image, out imgPath)) return;
            System.Diagnostics.Process.Start(imgPath);
        }

        void ImageBox_DoubleClick(object sender, EventArgs e)
        {
            openImg(sender, e);
        }

        public event EventHandler OnCheckChanged;
        void CheckUnCheck(object sender, EventArgs e)
        {
            if (this.ImgPoster == null) return;
            this.IsChecked = !this.IsChecked;
            if (OnCheckChanged != null) OnCheckChanged(this, e);
        }        

        void deleteMenu_Click(object sender, EventArgs e)
        {
           if (OnPictureDeleted != null && ImgPoster != null)  OnPictureDeleted(this, e);
        }

        void sizeLabel_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.X > deleteBox.Bounds.X && e.Y > deleteBox.Bounds.Y && OnPictureDeleted != null)
                OnPictureDeleted(this, e);
        }

        void ImageBox_MouseLeave(object sender, EventArgs e)
        {
            deleteBox.Visible = false;
        }

        void ImageBox_MouseEnter(object sender, EventArgs e)
        {
            deleteBox.Visible = true;
        }

        public event MouseEventHandler OnMouseUpEvent;
        void ImageBox_MouseUp(object sender, MouseEventArgs e)
        {
            if (OnMouseUpEvent != null) OnMouseUpEvent(this, e);
        }
        public event MouseEventHandler OnMouseMoveEvent;
        void ImageBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (OnMouseMoveEvent != null) OnMouseMoveEvent(this, e);
        }
        public event MouseEventHandler OnMouseDownEvent;
        void ImageBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (!ShowResolution && e.X > deleteBox.Bounds.X && e.Y > deleteBox.Bounds.Y && OnPictureDeleted != null)
                OnPictureDeleted(this, e);
            else if (OnMouseDownEvent != null) OnMouseDownEvent(this, e);
        }

        public event EventHandler OnPictureDeleted;
        public event EventHandler OnPictureChanged;

        private PictureBox picBox;
        private PictureBox checkedPic;
        private PictureBox deleteBox;
        private Label sizeLabel;
        private Label imgStateLabel;

        private Poster _imgPoster;
        private bool _isSelected;

        private Image MainImage;
        private string sizeText;

        private ContextMenuItem checkMenu;

        public int index = 0;
        public bool ShowResolution;

        public Poster ImgPoster
        {
            get
            {
                return _imgPoster;
            }
            set
            {
                if (_imgPoster != null && value != null && _imgPoster.Image == value.Image) return;
                _imgPoster = value;
                RefreshBox();
            }
        }

        private void DisplayImage()
        {
            if (_imgPoster == null) return;
            if (imgStateLabel.InvokeRequired)
            {
                SetVisibilityCallback d = new SetVisibilityCallback(setVisibility);
                this.Invoke(d, new object[] { imgStateLabel, false });
            }
            else
            {
                setVisibility(imgStateLabel, false);
            }
            if (picBox.InvokeRequired)
            {
                SetImageCallback d = new SetImageCallback(setImage);
                this.Invoke(d, new object[] { });
            }
            else
            {
                setImage();
            }
            if (_imgPoster != null && !string.IsNullOrEmpty(_imgPoster.width) && !string.IsNullOrEmpty(_imgPoster.height))
                sizeText = string.Format("{0}x{1}", _imgPoster.width, _imgPoster.height);
            if (ShowResolution)
            {
                if (sizeLabel.InvokeRequired)
                {
                    SetTextCallback d = new SetTextCallback(setText);
                    this.Invoke(d, new object[] { sizeLabel, sizeText });
                }
                else
                {
                    setText(sizeLabel, sizeText);
                }
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
            string imgPath;
            if (MainImage == null && ImageUtil.LocalFileExists(_imgPoster.Image, out imgPath))
            {
                FileStream fs = File.Open(imgPath, FileMode.Open);
                try { MainImage = Image.FromStream(fs); }
                catch (Exception ex) { Logger.ReportException("Error reading image file " + imgPath, ex); } 
                fs.Close();
            }
            picBox.BackgroundImage = MainImage;
        }

        public void RefreshBox()
        {            
            if (_imgPoster != null)
            {
                
                if (ShowResolution) sizeLabel.Text = "";
                string imgPath;
                if (ImageUtil.LocalFileExists(_imgPoster.Image, out imgPath))
                {
                    FileStream fs = File.Open(imgPath, FileMode.Open);
                    try { MainImage = Image.FromStream(fs); }
                    catch (Exception ex) { Logger.ReportException("Error reading image file " + imgPath, ex); }
                    fs.Close();
                    if (MainImage != null)
                        sizeText = string.Format("{0}x{1}", MainImage.Width, MainImage.Height);
                    DisplayImage();
                }
                else if (!string.IsNullOrEmpty(_imgPoster.Image))
                {
                    imgStateLabel.Visible = true;
                    imgStateLabel.Text = Kernel.Instance.GetString("LoadingPoTab") + "...";
                    picBox.BackgroundImage = null;
                    Async.Queue(Kernel.Instance.GetString("DownloadAsync") + " " + _imgPoster.Image, () =>
                    {                        
                        imgPath = ImageUtil.GetLocalImagePath(_imgPoster.Image);
                        if (!string.IsNullOrEmpty(imgPath))
                        {
                            FileStream fs = File.Open(imgPath, FileMode.Open);
                            try { MainImage = Image.FromStream(fs); }
                            catch (Exception ex) { Logger.ReportException("Error reading image file " + imgPath, ex); }
                            fs.Close();
                            if (MainImage != null)
                                sizeText = string.Format("{0}x{1}", MainImage.Width, MainImage.Height);
                        }
                    }, DisplayImage);
                }

                IsChecked = _imgPoster.Checked;
            }
            else
            {
                picBox.BackgroundImage = null;
                sizeLabel.Text = "";
                this.IsChecked = this.IsSelected = false;
                imgStateLabel.Visible = true;
                imgStateLabel.Text = Kernel.Instance.GetString("NoImagePoTab");
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
                    this.Focus();
                    this.BackgroundImage = global::MetaVideoEditor.Properties.Resources.B_click2;
                }
                else
                    this.BackgroundImage = null;
            }
        }

        public bool IsChecked
        {
            get
            {
                if (ImgPoster != null)
                    return ImgPoster.Checked;
                else
                    return false;
            }
            set
            {
                if (ImgPoster != null) ImgPoster.Checked = value;
                checkedPic.Visible = value;
                if (value)
                {
                    checkMenu.Text = Kernel.Instance.GetString("UnCheckPoTab");
                    checkMenu.Image = global::MetaVideoEditor.Properties.Resources.checkbox_checked;
                }
                else
                {
                    checkMenu.Text = Kernel.Instance.GetString("CheckPoTab");
                    checkMenu.Image = global::MetaVideoEditor.Properties.Resources.checkbox;
                }
            }
        }
       

        protected override void OnSizeChanged(EventArgs e)
        {
            base.OnSizeChanged(e);
            picBox.Location = new Point((int)(this.Width*.025), (int)(this.Height*.01));
            picBox.Size = new Size((int)(this.Width*.95), (int)(this.Height*.9));
            
            Font font;
            if (this.Width < 250)
            {
                font = new Font("Microsoft Sans Serif", 11F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            }
            else
            {
                font = new Font("Microsoft Sans Serif", 14.25F, FontStyle.Regular, GraphicsUnit.Point, ((byte)(0)));
            }
            imgStateLabel.Font = font;
            if (ShowResolution)
            {
                sizeLabel.Height = (int)(this.Height * .1);
                sizeLabel.Font = font;
            }
        }

        public void DisposeBox()
        {            
            ImgPoster = null;
            if (picBox != null) picBox.Dispose();
            if (MainImage != null) MainImage.Dispose();
            this.Dispose(true);
            GC.SuppressFinalize(this);            
        }

        /*protected override void Dispose(bool disposing)
        {
            //MessageBox.Show("dispose");
            //base.Dispose(disposing);
            ImgPoster = null;
            MainImage = null;
            
            //GC.SuppressFinalize(this);
        }*/

    }
}