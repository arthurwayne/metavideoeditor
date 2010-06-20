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

    public class TextBoxControl : Panel
    {

        public TextBoxControl()
        {
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.Click += new EventHandler(label_Click);
            this.Controls.Add(label);

            textBox = new TextBox();
            textBox.Dock = DockStyle.Fill;
            textBox.Visible = false;
            textBox.ScrollBars = ScrollBars.Vertical;
            textBox.Leave += new EventHandler(textBox_Leave);
            textBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            label.Controls.Add(textBox);
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Return)
            {
                if (this.Parent != null)
                    this.Parent.Focus();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                textBox.Text = text;
                if (this.Parent != null)
                    this.Parent.Focus();
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {                      
            textBox.Visible = false;
            if ((text != null && text != textBox.Text) || (text == null && !string.IsNullOrEmpty(textBox.Text)))
            {
                text = textBox.Text;  
                if (OnTextChanged != null) OnTextChanged(this, e);
            }
        }

        public Label label;
        TextBox textBox;

        void label_Click(object sender, EventArgs e)
        {
            GeneralItemPanel panel = Parent as GeneralItemPanel;
            if (panel != null)
            {
                panel.GeneralItemPanel_Click(sender, e);
                return;
            }
            Refresh();
        }

        public bool multiLine
        {
            get
            {
                return textBox.Multiline;
            }
            set
            {
                textBox.Multiline = value;
            }
        }

        string _text;
        public string text
        {
            get
            {
                return _text;
            }
            set
            {
                _text = value;
                label.Text = textBox.Text = value;
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Focus();
            textBox.Visible = true;
            textBox.Select();
        }

        public event EventHandler OnTextChanged;

    }
}