using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class Search : Form
    {
        public Search(Item item)
        {
            InitializeComponent();
            this.Text = Kernel.Instance.GetString("SearchMW");
            label2Title.Text = Kernel.Instance.GetString("SearchSW");
            label1Title.Text = Kernel.Instance.GetString("ResultsSW");
            label1.Text = Kernel.Instance.GetString("NoResultW");
            SetStyle();

            currentItem = new Item(item);

            if (!string.IsNullOrEmpty(item.Title))
                TitleBox.Text = item.Title;
            TitleBox.Select();
        }

        public Item currentItem;

        private void SearchButton_Click(object sender, EventArgs e)
        {
            SearchItem();
        }

        private void SearchItem()
        {
            if (string.IsNullOrEmpty(TitleBox.Text))
            {
                return;
            }

            Cursor = Cursors.WaitCursor;
            treeView1.Nodes.Clear();
            List<Item> results = ProvidersUtil.FindPossible(new Item { Title = TitleBox.Text, Type = currentItem.Type });

            label1Title.Text = string.Format(Kernel.Instance.GetString("ResultsSW") + " ({0})", results.Count.ToString());
            if (results.Count == 0)
            {
                label1.Visible = true;
                Cursor = Cursors.Default;
                return;
            }
            label1.Visible = false;
            
            foreach (Item res in results)
            {
                TreeNode tn = new TreeNode();
                tn.Tag = res;
                treeView1.Nodes.Add(tn);

            }
            treeView1.Focus();
            Cursor = Cursors.Default;
        }

        private void treeView1_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Brush brush = Brushes.Black;
            Item item = (Item)e.Node.Tag;
            if (e.Node.IsSelected)
            {
                if (treeView1.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }
            Image pic;
            if (item.PrimaryImage != null && !string.IsNullOrEmpty(ImageUtil.GetLocalImagePath(item.PrimaryImage.Image)))
            {
                pic = new Bitmap(ImageUtil.GetLocalImagePath(item.PrimaryImage.Image));
            }
            else
                pic = global::MetaVideoEditor.Properties.Resources.default_movie;
            e.Graphics.DrawImage(pic, e.Bounds.X, e.Bounds.Y, 50, 75);

            string title = item.Title;
            if (item.Year != null)
                title += " (" + item.Year.ToString() + ")";
            e.Graphics.DrawString(title, new Font("Microsoft Sans Serif", 12F, FontStyle.Bold, GraphicsUnit.Point, ((byte)(0))), brush, e.Bounds.X + 60, e.Bounds.Y + 4);
            

            Rectangle rect = new Rectangle(e.Node.Bounds.X + 60, e.Node.Bounds.Y + 25, e.Bounds.X + e.Bounds.Width-80, e.Node.Bounds.Y + 30);            
            string overview = item.Overview;
            if (!string.IsNullOrEmpty(overview) && overview.Length > 200)
            {
                overview = overview.Remove(200);
                overview = overview.Remove(overview.LastIndexOf(" "));
                overview += "...";
            }
            e.Graphics.DrawString(overview, treeView1.Font, brush, rect);

            if (item.ProvidersId != null && item.ProvidersId.Count > 0)
            {
                string provider = Kernel.Instance.GetString("ProvidedBy") + " " + item.ProvidersId[0].Name;
                Font font = treeView1.Font; ;
                if (!string.IsNullOrEmpty(item.ProvidersId[0].Url))
                {
                    brush = Brushes.Blue;
                    font = new Font("Microsoft Sans Serif", 9F, FontStyle.Underline, GraphicsUnit.Point, ((byte)(0)));
                }
                SizeF textsize = e.Graphics.MeasureString(provider, treeView1.Font);
                
                e.Graphics.DrawString(provider, font, brush, e.Bounds.X + e.Bounds.Width - textsize.Width - 5, e.Bounds.Y + 4);
            }
        }

        private void treeView1_DoubleClick(object sender, EventArgs e)
        {
            Cursor = Cursors.WaitCursor; 
            Item i = ProvidersUtil.Fetch((Item)treeView1.SelectedNode.Tag);
            currentItem = Helper.UpdateItem(currentItem, i);
            currentItem.HasChanged = true;
            Cursor = Cursors.Default;
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void TitleBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
                SearchItem();
        }

        private void treeView1_MouseDown(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            if (treeView1.SelectedNode != clickedNode)
            {
                treeView1.SelectedNode = clickedNode;
                treeView1.LabelEdit = false;
            }
            else
            {
                treeView1.LabelEdit = true;
            }
        }

        private void SetStyle()
        {
            ColorsTheme colors = ColorStyle.GetColors();
            this.BackColor = colors.BackColor;
            int R = colors.BaseColor.R;
            int G = colors.BaseColor.G;
            int B = colors.BaseColor.B;
            foreach (Control control in this.Controls)
            {
                //foreach (Control control in cont.Controls)
                {
                    if (control.GetType() == typeof(Label) && control.Name.EndsWith("Title"))
                    {

                        Label label = (Label)control;
                        label.BackColor = colors.BaseColor;
                        if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
                        {
                            try
                            {
                                label.ForeColor = Color.FromArgb(R + 76, G + 71, B + 66);
                            }
                            catch
                            {
                                label.ForeColor = Color.FromArgb(250, 250, 250);
                            }
                        }
                        else
                        {
                            try
                            {
                                label.ForeColor = Color.FromArgb(R - 96, G - 91, B - 86);
                            }
                            catch
                            {
                                label.ForeColor = Color.FromArgb(10, 10, 10);
                            }
                        }
                    }
                }
            }
        }

        private void treeView1_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (Cursor == Cursors.Hand)
            {
                try
                {
                    TreeNode clickedNode = treeView1.GetNodeAt(e.X, e.Y);
                    System.Diagnostics.Process.Start(((Item)clickedNode.Tag).ProvidersId[0].Url);
                }
                finally { Cursor = Cursors.Default; }                
            }
        }

        private void treeView1_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode clickedNode = treeView1.GetNodeAt(e.X, e.Y);
            Item item;
            try
            {
                item = (Item)clickedNode.Tag;
            }
            catch { Cursor = Cursors.Default; return; }
            if (item.ProvidersId != null && item.ProvidersId.Count > 0 && !string.IsNullOrEmpty(item.ProvidersId[0].Url))
            {
                int y = e.Y % treeView1.ItemHeight;
                if (y > 6 && y < 18)
                {
                    Graphics g = CreateGraphics();
                    string provider = Kernel.Instance.GetString("ProvidedBy") + item.ProvidersId[0].Name;
                    SizeF textsize = g.MeasureString(provider, treeView1.Font);
                    if (e.X > clickedNode.Bounds.X + treeView1.Width - textsize.Width - 15)// && e.X < clickedNode.Bounds.X + clickedNode.Bounds.Width)
                    {
                        Cursor = Cursors.Hand;
                        return;
                    }
                    
                }
                
            }
            Cursor = Cursors.Default;
        }


 
    }
}
