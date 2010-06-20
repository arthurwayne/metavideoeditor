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
    public partial class BannersPanel : BasePanel
    {
        public BannersPanel()
        {
            InitializeComponent();
            label1.Text = Kernel.Instance.GetString("AddBannerBaTab");
            label2.Text = Kernel.Instance.GetString("NoBannerBaTab");
            MainPanel = panel1;
        }

        ImageBox SelectedBox
        {
            get
            {
                foreach (Control control in panel2.Controls)
                {
                    ImageBox ib = control as ImageBox;
                    if (ib != null && ib.IsSelected)
                        return ib;
                }
                return null;
            }
        }

        Poster SelectedPoster
        {
            get
            {
                if (SelectedBox != null)
                    return SelectedBox.ImgPoster;
                return null;
            }
        }

        public override void UpdateData()
        {
            this.panel2.Controls.Clear();
            this.doubleBufferPanel1.Width = panel2.Width - 40;
            this.panel2.Controls.Add(this.doubleBufferPanel1);
            this.panel2.Controls.Add(this.label2);
            if (SelectedItem != null)
            {
                if (SelectedItem.BannersPaths.IsNonEmpty())
                {
                    label2.Visible = false;
                    //Ensure only one poster is checked
                    bool check = false;
                    foreach (Poster p in SelectedItem.BannersPaths)
                    {
                        if (!check && p.Checked)
                            check = true;
                        else if (check)
                            p.Checked = false;
                    }

                    int index = 0;
                    foreach (Poster img in SelectedItem.BannersPaths)
                    {
                        ImageBox panel = new ImageBox(false);
                        panel.Location = new Point(20, 40 + (index * 100));
                        panel.Size = new Size(panel2.Width - 40, 100);
                        panel.ImgPoster = img;
                        panel.IsSelected = img.Checked;
                        panel.index = index;
                        panel.OnMouseDownEvent += new MouseEventHandler(panel_OnMouseDownEvent);
                        panel.OnMouseMoveEvent += new MouseEventHandler(panel_OnMouseMoveEvent);
                        panel.OnMouseUpEvent += new MouseEventHandler(panel_OnMouseUpEvent);
                        panel.OnCheckChanged += new EventHandler(panel_OnCheckChanged);
                        panel.OnPictureChanged += new EventHandler(panel_OnPictureChanged);
                        panel.OnPictureDeleted += new EventHandler(panel_OnPictureDeleted);
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

        void panel_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.BannersPaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            UpdateData();
        }

        void panel_OnPictureChanged(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.HasChanged = true;
            HasChanged();
            ib.IsSelected = true;
        }

        void panel_OnCheckChanged(object sender, EventArgs e)
        {
            ImageBox iBox = (ImageBox)sender;
            SelectedItem.HasChanged = true;
            if (iBox.IsChecked)
            {
                foreach (Control control in panel2.Controls)
                {
                    ImageBox ib = control as ImageBox;
                    if (ib != null)
                        ib.IsChecked = (ib.ImgPoster.Image == iBox.ImgPoster.Image);
                }
            }
        }


        bool drag;
        int yLoc;
        void panel_OnMouseUpEvent(object sender, MouseEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Default;
            ImageBox act = sender as ImageBox;
            act.Location = new Point(20, 40 + (act.index * 100) + panel2.AutoScrollPosition.Y);
        }

        void panel_OnMouseMoveEvent(object sender, MouseEventArgs e)
        {
            
            if (drag)
            {
                ImageBox tex = sender as ImageBox;
                tex.BringToFront();
                int index = (int)((tex.Top - panel2.AutoScrollPosition.Y) / 100);
                tex.Location = new Point(20, e.Y + tex.Top - yLoc);
                if (tex.index != index)
                {
                    ImageBox ap = FindTexPanelByIndex(index);
                    if (ap != null)
                    {
                        Poster tmp = SelectedItem.BannersPaths[tex.index];
                        SelectedItem.BannersPaths[tex.index] = SelectedItem.BannersPaths[index];
                        SelectedItem.BannersPaths[index] = tmp;
                        ap.index = tex.index;
                        ap.Location = new Point(20, 40 + (100 * ap.index) + panel2.AutoScrollPosition.Y);
                        tex.index = index;
                        SelectedItem.HasChanged = true;
                    }
                }
            }
        }

        void panel_OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                yLoc = e.Y;
                Cursor.Current = new Cursor(global::MetaVideoEditor.Properties.Resources.drag_cursor.GetHicon());
            }

            ImageBox iBox = (ImageBox)sender;
            foreach (Control control in panel2.Controls)
            {
                ImageBox ib = control as ImageBox;
                if (ib != null)
                    ib.IsSelected = (ib.index == iBox.index);
            }
        }


        private void doubleBufferPanel1_DoubleClick(object sender, EventArgs e)
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
                    if (SelectedItem.BannersPaths == null)
                        SelectedItem.BannersPaths = new List<Poster>();
                    SelectedItem.BannersPaths.Add(new Poster { Image = img });
                    HasChanged();
                    UpdateData();
                }
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

        ImageBox FindTexPanelByIndex(int index)
        {
            foreach (Control control in panel2.Controls)
            {
                ImageBox ap = control as ImageBox;
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

        private void doubleBufferPanel1_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
                e.Effect = DragDropEffects.Copy;
        }

        private void doubleBufferPanel1_DragDrop(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent("FileDrop"))
            {
                string img = ImageUtil.GetLocalImagePath(((string[])e.Data.GetData("FileDrop"))[0]);
                if (!string.IsNullOrEmpty(img))
                {
                    if (SelectedItem.BannersPaths == null)
                        SelectedItem.BannersPaths = new List<Poster>();
                    SelectedItem.BannersPaths.Add(new Poster { Image = img });
                    HasChanged();
                    UpdateData();
                }
            }
        }

    }
}
