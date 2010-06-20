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

    public class ComboBoxControl : Panel
    {

        public ComboBoxControl()
        {
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.Click += new EventHandler(label_Click);
            this.Controls.Add(label);

            comboBox = new ComboBox();
            comboBox.Dock = DockStyle.Fill;
            comboBox.Visible = false;
            comboBox.Leave += new EventHandler(textBox_Leave);
            comboBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            label.Controls.Add(comboBox);
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
                comboBox.Text = text;
                if (this.Parent != null)
                    this.Parent.Focus();
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {                      
            comboBox.Visible = false;
            if ((text != null && text != comboBox.Text) || (text == null && !string.IsNullOrEmpty(comboBox.Text)))
            {
                text = comboBox.Text;  
                if (OnTextChanged != null) OnTextChanged(this, e);
            }
        }

        Label label;
        ComboBox comboBox;

        void label_Click(object sender, EventArgs e)
        {
            GeneralItemPanel panel = Parent as GeneralItemPanel;
            panel.GeneralItemPanel_Click(sender, e);
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
                label.Text = comboBox.Text = value;
            }
        }

        public string[] choices
        {
            set
            {
                comboBox.Items.AddRange(value);
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Focus();
            comboBox.Visible = true;
            comboBox.Select();
            comboBox.DroppedDown = true;
        }

        public event EventHandler OnTextChanged;

    }
}