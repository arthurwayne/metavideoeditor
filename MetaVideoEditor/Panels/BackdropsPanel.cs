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
    public partial class BackdropsPanel : BasePanel
    {
        public BackdropsPanel()
        {
           InitializeComponent();
           MainPanel = panel1;         
        }

        ImageBox SelectedBox
        {
            get
            {
                foreach (Control control in picsPanel.Controls)
                {
                    ImageBox ib = control as ImageBox;
                    if (ib.IsSelected)
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

        public override void DisposeControls()
        {
            foreach (Control c in picsPanel.Controls)
            {
                ImageBox ib = c as ImageBox;
                if (ib != null)
                {
                    ib.DisposeBox();
                }
            }
            if (SelectedBox != null) SelectedBox.DisposeBox();
            this.picsPanel.Controls.Clear();
            
        }

        public override void UpdateData()
        {
            DisposeControls();
            if (SelectedItem.BackdropImagePaths.IsNonEmpty())
            {
                //Ensure we doesn't have too many checked bd
                int check = 0;
                foreach (Poster p in SelectedItem.BackdropImagePaths)
                {
                    if (p.Checked && check < Config.Instance.MaxBdSaved)
                    {
                        check++;
                    }
                    else if (check >= Config.Instance.MaxBdSaved)
                        p.Checked = false;
                }
                if (!SelectedItem.BackdropImagePaths.Exists(p => p.Checked))
                    SelectedItem.BackdropImagePaths[0].Checked = true;

                int index = 0;
                bool select = false;
                foreach (Poster p in SelectedItem.BackdropImagePaths)
                {
                    ImageBox imgBox = new ImageBox();
                    imgBox.ImgPoster = p;
                    imgBox.Size = new Size(230, this.picsPanel.Height - 20);
                    imgBox.Location = new Point(230 * index, 0);
                    imgBox.index = index;
                    imgBox.IsChecked = p.Checked;
                    if (p.Checked && !select) { select = imgBox.IsSelected = true; }
                    imgBox.OnCheckChanged += new EventHandler(OnCheckChanged);
                    imgBox.OnPictureDeleted += new EventHandler(imgBox_OnPictureDeleted);
                    imgBox.OnPictureChanged += new EventHandler(imgBox_OnPictureChanged);
                    imgBox.OnMouseUpEvent += new MouseEventHandler(imgBox_MouseUp);
                    imgBox.OnMouseDownEvent += new MouseEventHandler(imgBox_MouseDown);
                    imgBox.OnMouseMoveEvent += new MouseEventHandler(imgBox_MouseMove);
                    this.picsPanel.Controls.Add(imgBox);
                    index++;
                    if (index > 15) break; //Don't need too many pics
                }
                UpdateMainPoster();
            }
            else
                imageBox1.ImgPoster = null;
        }

        private void UpdateMainPoster()
        {
            if (SelectedPoster != null)
            {
                imageBox1.ImgPoster = SelectedPoster;
                imageBox1.IsSelected = true;
                imageBox1.IsChecked = SelectedPoster.Checked;
            }
            else
            {
                imageBox1.ImgPoster = null;
                imageBox1.IsSelected = imageBox1.IsChecked = false;
            }
        }

        void imgBox_OnPictureChanged(object sender, EventArgs e)
        {            
            ImageBox ib = sender as ImageBox;
            HasChanged();
            
            if (ib != imageBox1)
            {
                SelectedBox.IsSelected = false;
                ib.IsSelected = true;
                imageBox1.ImgPoster = ib.ImgPoster;
                imageBox1.RefreshBox();
            }
            else
            {
                if (!SelectedItem.BackdropImagePaths.IsNonEmpty())
                {
                    SelectedItem.BackdropImagePaths = new List<Poster>();
                    SelectedItem.BackdropImagePaths.Add(ib.ImgPoster);
                    UpdateData();
                }
                else
                    SelectedBox.RefreshBox();
            }
        }

        void imgBox_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.BackdropImagePaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            UpdateData();
        }

        bool drag;
        int xLoc;
        void imgBox_MouseMove(object sender, MouseEventArgs e)
        {
            if (drag)
            {
                ImageBox img = sender as ImageBox;
                img.BringToFront();
                int index = (int)((img.Left - picsPanel.AutoScrollPosition.X )/ 230);
                
                img.Location = new Point(e.X + img.Left - xLoc, 0);
                if (img.index != index)
                {
                    ImageBox ib = FindImgBoxByIndex(index);
                    if (ib != null)
                    {
                        Poster tmp = new Poster(SelectedItem.BackdropImagePaths[img.index]);
                        SelectedItem.BackdropImagePaths[img.index] = new Poster(SelectedItem.BackdropImagePaths[index]);
                        SelectedItem.BackdropImagePaths[index] = tmp;
                        ib.index = img.index;
                        ib.Location = new Point((230 * ib.index) + picsPanel.AutoScrollPosition.X, 0);
                        img.index = index;
                        HasChanged();
                    }
                }
            }
        }

        void imgBox_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                drag = true;
                xLoc = e.X;
                Cursor.Current = new Cursor(global::MetaVideoEditor.Properties.Resources.drag_cursor.GetHicon());
            }

            ImageBox iBox = (ImageBox)sender;
            foreach (Control control in picsPanel.Controls)
            {
                ImageBox ib = control as ImageBox;
                ib.IsSelected = (ib.index == iBox.index);
            }
            UpdateMainPoster();
        }

        void imgBox_MouseUp(object sender, MouseEventArgs e)
        {
            drag = false;
            Cursor = Cursors.Default;
            ImageBox img = sender as ImageBox;
            img.Location = new Point((img.index * 230) + picsPanel.AutoScrollPosition.X, 0);
        }

        ImageBox FindImgBoxByIndex(int index)
        {
            foreach (Control control in picsPanel.Controls)
            {
                ImageBox ib = control as ImageBox; 
                if (ib.index == index)
                    return ib;
            }
            return null;
        }

        ImageBox FindImgBoxByPoster(string image)
        {
            foreach (Control control in picsPanel.Controls)
            {
                ImageBox ib = control as ImageBox;
                if (ib.ImgPoster.Image == image)
                    return ib;
            }
            return null;
        }


        private void OnCheckChanged(object sender, EventArgs e)
        {
            ImageBox iBox = (ImageBox)sender;
            
            if (iBox.IsChecked && SelectedItem.BackdropImagePaths.FindAll(p => p.Checked).Count >= Config.Instance.MaxBdSaved)
            {
                SelectedPoster.Checked = false;
                Poster poster = SelectedItem.BackdropImagePaths.FindLast(p => p.Checked);
                ImageBox ib = FindImgBoxByPoster(poster.Image);
                ib.IsChecked = false;
                SelectedPoster.Checked = true;
            }

            HasChanged();
            SelectedBox.IsChecked = iBox.IsChecked;
            UpdateMainPoster();
        }
    }
}
