using System;
using System.ComponentModel;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Windows.Forms.Design;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Design;

namespace SettingsStyle{

    /// <summary>
    /// Tab is a specialized ToolStripButton with extra padding
    /// </summary>
    [System.ComponentModel.DesignerCategory("code")]
    [ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.None)] // turn off the ability to add this in the DT, the TabPageSwitcher designer will provide this.
    public class Tab : ToolStripButton {
        
        private PanelPage panelPage;

        public bool b_on = false;
        public bool b_selected = false;
        public bool b_active = false;
        public bool b_fading = true;
        public int o_opacity = 180;
        public int e_opacity = 40;
        public int i_opacity;
        private Timer timer = new Timer();
        
        /// <summary>
        /// Constructor for tab - support all overloads.
        /// </summary>
        public Tab() {
            Initialize();
        }
        public Tab(string text):base(text,null,null) {
            Initialize();
        }
        public Tab(Image image):base(null,image,null) {
            Initialize();
        }
        public Tab(string text, Image image):base(text,image,null) {
            Initialize();
        }
        public Tab(string text, Image image, EventHandler onClick):base(text,image,onClick) {
            Initialize();            
        }
        public Tab(string text, Image image, EventHandler onClick, string name):base(text,image,onClick,name) {
            Initialize();
        }

        /// <summary>
        /// Common initialization code between all CTORs.
        /// </summary>
        private void Initialize() {
            this.AutoSize = false;
            this.Width = 160;
            this.Height = 30;
            CheckOnClick = true;  // Tab will use the "checked" property in order to represent the "selected tab".
            this.ForeColor = Color.FromArgb(44, 90, 154);
            this.Font = new Font("Segoe UI", 9);
            i_opacity = o_opacity;            
        }

        /// <summary>
        /// Hide the CheckOnClick from the Property Grid so that we can use it for our own purposes. 
        /// </summary>
        [DefaultValue(true)]
        public new bool CheckOnClick {
            get { return base.CheckOnClick; }
            set { base.CheckOnClick = value; }
        }

        /// <summary>
        /// Specify the default display style to be ImageAndText
        /// </summary>
        protected override ToolStripItemDisplayStyle DefaultDisplayStyle {
            get {
                return ToolStripItemDisplayStyle.ImageAndText;
            }
        }


        /// <summary>
        /// The associated PanelPage - when Tab is clicked, this TabPage will be selected.
        /// </summary>
        [DefaultValue("null")]
        public PanelPage PanelPage {
            get {
                return panelPage;
            }
            set {
                panelPage = value;
            }
        }
        
        
        protected override void OnPaint(PaintEventArgs e)
        {
            Rectangle rect = e.ClipRectangle;
            Graphics g = e.Graphics;
            TabPageSwitcher panelPageSwitcher = this.PanelPage.Parent as TabPageSwitcher;
            if (panelPageSwitcher != null && panelPageSwitcher.SelectedPanelPage.Name == this.PanelPage.Name)
            {
                g.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, rect.X + 2, e.ClipRectangle.Y + 2, rect.Width - 4, rect.Height - 4);
            }
            else if (this.Selected)
            {
                g.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, rect.X + 2, rect.Y + 2, rect.Width - 4, rect.Height - 4);
            }
            SizeF textsize = g.MeasureString(this.Text, this.Font);
            int X = rect.X + 5;
            int Y = rect.Y + rect.Height; Y = Y - (int)textsize.Height - 8;
            Point _textpos = new Point(X, Y);
            g.DrawString(this.Text, this.Font, Brushes.Black, _textpos);
        }
        
      
    }
}
