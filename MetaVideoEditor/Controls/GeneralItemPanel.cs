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

    public class GeneralItemPanel : Panel
    {

        public GeneralItemPanel(string title, Control editControl, int height)
        {
            this.EditControl = editControl;
            this.Anchor = (AnchorStyles)(AnchorStyles.Left | AnchorStyles.Right);
            this.Height = height;
            this.BorderStyle = BorderStyle.FixedSingle;
            this.BackgroundImageLayout = ImageLayout.Stretch;
            this.Leave += new EventHandler(GeneralItemPanel_Leave);

            label = new Label();
            label.Text = title;
            label.AutoSize = false;
            label.Location = new Point(0, 2);
            label.Width = 200;
            this.Controls.Add(label);

            EditControl.Dock = DockStyle.Right;
            this.Controls.Add(EditControl);

            this.Click +=new EventHandler(GeneralItemPanel_Click);
            label.Click += new EventHandler(GeneralItemPanel_Click);
        }

        void GeneralItemPanel_Leave(object sender, EventArgs e)
        {
            BackgroundImage = null;
        }

        public void GeneralItemPanel_Click(object sender, EventArgs e)
        {
            BackgroundImage = global::MetaVideoEditor.Properties.Resources.B_click2;
            EditControl.Refresh();
        }

        public Control EditControl;
        Label label;
       
        private void SetSize()
        {
            if (this.Parent != null)
            {
                if (this.Width != this.Parent.Width)
                {
                    this.Width = this.Parent.Width;
                    EditControl.Width = this.Width - 200;
                }
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            SetSize();
            base.OnPaint(e);
        }

    }
}