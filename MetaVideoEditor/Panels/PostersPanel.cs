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
    public partial class PostersPanel : BasePanel
    {
        public PostersPanel()
        {
           InitializeComponent();
           MainPanel = panel1;
           //this.CreateGraphics();           
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
                    ib.DisposeBox();                
            }
            if (SelectedBox != null) SelectedBox.DisposeBox();
            this.picsPanel.Controls.Clear();
        }

        public override void UpdateData()
        {
            DisposeControls();
            if (SelectedItem.ImagesPaths.IsNonEmpty())
            {
                //Ensure only one poster is checked
                bool check = false;
                foreach (Poster p in SelectedItem.ImagesPaths)
                {
                    if (!check && p.Checked)
                        check = true;
                    else if (check)
                        p.Checked = false;
                }

                int index = 0;
                foreach (Poster p in SelectedItem.ImagesPaths)
                {
                    ImageBox imgBox = new ImageBox();
                    imgBox.ImgPoster = p;
                    imgBox.Size = new Size(180, this.picsPanel.Height - 20);
                    imgBox.Location = new Point(180 * index, 0);
                    imgBox.index = index;
                    imgBox.IsSelected = p.Checked;
                    imgBox.OnMouseDownEvent += new MouseEventHandler(imgBox_OnMouseDownEvent);
                    imgBox.OnCheckChanged += new EventHandler(OnCheckChanged);
                    imgBox.OnPictureDeleted += new EventHandler(imageBox1_OnPictureDeleted);
                    imgBox.OnPictureChanged += new EventHandler(imageBox1_OnPictureChanged);
                    this.picsPanel.Controls.Add(imgBox);
                    index++;
                }                
            }
            else
                imageBox1.ImgPoster = null;
            UpdateMainPoster();
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

        void imgBox_OnMouseDownEvent(object sender, MouseEventArgs e)
        {
            ImageBox iBox = (ImageBox)sender;
            int index = 0;
            foreach (Control control in picsPanel.Controls)
            {
                ImageBox ib = control as ImageBox;
                ib.IsSelected = (index == iBox.index);
                index++;
            }
            UpdateMainPoster();
        }

        

        private void imageBox1_OnPictureChanged(object sender, EventArgs e)
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
                if (!SelectedItem.ImagesPaths.IsNonEmpty())
                {
                    SelectedItem.ImagesPaths = new List<Poster>();
                    SelectedItem.ImagesPaths.Add(ib.ImgPoster);
                    UpdateData();
                }
                else
                    SelectedBox.RefreshBox();
            }
        }

        private void imageBox1_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.ImagesPaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            UpdateData();
        }

        private void OnCheckChanged(object sender, EventArgs e)
        {
            ImageBox iBox = (ImageBox)sender;
            HasChanged();
            if (iBox.IsChecked)
            {
                foreach (Control control in picsPanel.Controls)
                {
                    ImageBox ib = control as ImageBox;
                    ib.IsChecked = (ib.ImgPoster.Image == iBox.ImgPoster.Image);
                }
            }
            SelectedBox.IsChecked = iBox.IsChecked;
            UpdateMainPoster();
        }
    }
}
