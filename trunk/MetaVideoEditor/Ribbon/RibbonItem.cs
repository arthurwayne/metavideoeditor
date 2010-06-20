using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Text;
using System.Drawing.Imaging;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using RibbonStyle;

namespace RibbonStyle
{
    public partial class RibbonItem : MenuStrip
    {

        private Image _img_on;
        private Image _img_click;
        private Image _img_back;
        private Image _img_fad;
        private Image _img;
        private Image _toshow;
        
        public RibbonItem()
        {
            this.BackColor = Color.Transparent;
            this.Dock = DockStyle.None;
            this.AutoSize = false;

            TsItem.AutoSize = false;
            TsItem.Width = 0;
            TsItem.Height = this.Height;
            TsItem.DropDownClosed += new EventHandler(this.ClearBackground);
            
        }

        //Properties
        public Image img_on
        {
            get { return _img_on; }
            set { _img_on = value; }
        }
        public Image img_click
        {
            get { return _img_click; }
            set { _img_click = value; }
        }
        public Image img_back
        {
            get { return _img_back; }
            set
            {
                _img_back = value;
            }
        }
        public Image img
        {
            get { return _img; }
            set
            {
                _img = value;
            }
        }

        public void ClearBackground(object sender, EventArgs e)
        {
            DropDownOpened = false;
            this.BackgroundImage = _img_back;
            _toshow = _img_back;
        }

        private ToolStripMenuItem TsItem
        {
            get 
            {
                ToolStripMenuItem ts = new ToolStripMenuItem();
                if (this.Items.Count > 0)
                    ts = this.Items[0] as ToolStripMenuItem;
                return ts;
            }
            set
            {
            }
        }

        //Fading
        bool b_fad = false;
        int i_fad = 0; //0 nothing, 1 entering, 2 leaving
        int i_value = 255; //Level of transparency

        protected override void OnPaint(PaintEventArgs pevent)
        {
            if (this.Parent != null)
            {
                GraphicsContainer cstate = pevent.Graphics.BeginContainer();
                pevent.Graphics.TranslateTransform(-this.Left, -this.Top);
                Rectangle clip = pevent.ClipRectangle;
                clip.Offset(this.Left, this.Top);
                PaintEventArgs pe = new PaintEventArgs(pevent.Graphics, clip);
                //pinta el fondo del contenedor
                //  InvokePaintBackground(this.Parent, pe);
                //pinta el resto del contenedor
                InvokePaint(this.Parent, pe);
                //restaura el Graphics a su estado original
                pevent.Graphics.EndContainer(cstate);

                Graphics g = pevent.Graphics;
                try { g.DrawImage(_toshow, pevent.ClipRectangle); }
                catch { }

                Rectangle rect = pevent.ClipRectangle;
                int X = (rect.Width - 28) / 2;// 4;
                try
                {
                    int newwidth = 28;// (rect.Width - 8);

                    int newheigth = 28;// newwidth * _img.Height / _img.Width;
                    Point _imgpos = new Point(X, 4);
                    Rectangle r = new Rectangle(_imgpos, new Size(newwidth, newheigth));
                    g.DrawImage(_img, r);
                }
                catch { }
                try
                {
                    SizeF textsize = g.MeasureString(TsItem.Text, this.Font);
                    X = rect.X + rect.Width; X = (X - (int)textsize.Width) / 2;
                    int Y = rect.Y + rect.Height; Y = Y - (int)textsize.Height - 8;
                    Point _textpos = new Point(X, Y);
                    Pen PForeColor = new Pen(this.ForeColor);

                    g.DrawString(TsItem.Text, this.Font, PForeColor.Brush, _textpos);
                }
                catch { }

                g.DrawImage(global::MetaVideoEditor.Properties.Resources.down_triangle, new Point((rect.X+rect.Width-12)/2, rect.Y+rect.Height-10));

            }
            else
                base.OnPaint(pevent);
        }

        //Methods
        public void PaintBackground()
        {
            if (b_fad)
            {
                object _img_temp = new object();
                if (i_fad == 1)
                {
                    _img_temp = _img_on.Clone();
                }
                else if (i_fad == 2)
                {
                    _img_temp = _img_back.Clone();
                }
                _img_fad = (Image)_img_temp;
                Graphics _grf = Graphics.FromImage(_img_fad);
                SolidBrush brocha = new SolidBrush(Color.FromArgb(i_value, 255, 255, 255));
                _grf.FillRectangle(brocha, 0, 0, _img_fad.Width, _img_fad.Height);
                this.BackgroundImage = _img_fad;
            }
        }

        bool DropDownOpened = false;
        protected override void OnClick(EventArgs e)
        {
            if (!DropDownOpened)
            {
                this.BackgroundImage = _img_click;
                this._toshow = _img_click;
                TsItem.ShowDropDown();
                DropDownOpened = true;
            }
            else
            {
                this.BackgroundImage = _img_on;
                this._toshow = _img_on;
                TsItem.HideDropDown();
                DropDownOpened = false;
            }
        }
        protected override void OnMouseEnter(EventArgs e)
        {
            this.BackgroundImage = _img_on;
            _toshow = _img_on;
            base.OnMouseEnter(e);
        }
        
        protected override void OnMouseLeave(EventArgs e)
        {
            if (!DropDownOpened)
            {
                this.BackgroundImage = _img_back;
                _toshow = _img_back;
            }
            base.OnMouseLeave(e);

        }

    }
}