using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Windows.Forms.Layout;

namespace CustomControls
{

    public class ContextMenuItem : ToolStripMenuItem
    {

        public bool DefaultItem;
        public string Shortcut;

        protected override void OnPaint(PaintEventArgs e)
        {
            if (this.Selected)
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.ClipRectangle);
            }
            Font font;
            Brush brush = Brushes.Black;
            if (DefaultItem)
                font = new Font(this.Font.FontFamily, this.Font.Size, FontStyle.Bold);
            else
                font = this.Font;
            if (!Enabled)
                brush = Brushes.Gray;
            e.Graphics.DrawString(this.Text, font, brush, e.ClipRectangle.X + 34, e.ClipRectangle.Y + 2);
            if (this.Checked)
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox_checked, e.ClipRectangle.X + 6, e.ClipRectangle.Y);

            if (this.Image != null)
                e.Graphics.DrawImage(this.Image, 3, 3, 16,16);

            if (string.IsNullOrEmpty(Shortcut) && ShortcutKeys != Keys.None)
            {
                var kc = new KeysConverter();
                Shortcut = kc.ConvertToString(ShortcutKeys);
            }
            if (!string.IsNullOrEmpty(this.Shortcut))
            {
                SizeF textsize = e.Graphics.MeasureString(this.Shortcut, font);
                e.Graphics.DrawString(this.Shortcut, font, brush, e.ClipRectangle.Width - (int)textsize.Width, e.ClipRectangle.Y + 2);
            }
        }

    }
}