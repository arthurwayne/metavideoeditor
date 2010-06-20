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

    public class NumericBoxControl : Panel
    {

        public NumericBoxControl()
        {
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.TextAlign = ContentAlignment.MiddleLeft;
            label.Click += new EventHandler(label_Click);
            this.Controls.Add(label);

            numericBox = new NumericUpDown();
            numericBox.Dock = DockStyle.Fill;
            numericBox.Visible = false;
            numericBox.Maximum = 1000;
            numericBox.Minimum = 0;  
            numericBox.Leave += new EventHandler(textBox_Leave); 
            numericBox.KeyPress += new KeyPressEventHandler(textBox_KeyPress);
            label.Controls.Add(numericBox);
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
                numericBox.Value = value;
                if (this.Parent != null)
                    this.Parent.Focus();
            }
        }

        void textBox_Leave(object sender, EventArgs e)
        {                      
            numericBox.Visible = false;
            if ((value != null && value != numericBox.Value) || (value == null && numericBox.Text != null))
            {
                value = numericBox.Value;  
                if (OnTextChanged != null) OnTextChanged(this, e);
            }
        }

        Label label;
        NumericUpDown numericBox;
        int _decimal = 0;
        public int decimals
        {
            get
            {
                return _decimal;
            }
            set
            {
                _decimal = value;
                numericBox.DecimalPlaces = value; 
            }
        }

        public decimal increment
        {
            get
            {
                return numericBox.Increment;
            }
            set
            {
                numericBox.Increment = value;
            }
        }

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

        decimal _value;
        public decimal value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                label.Text = value.ToString();
                numericBox.Value = value; 
            }
        }

        public string TextDisplayed
        {
            set
            {
                label.Text = value;
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Focus();
            numericBox.Visible = true;
            numericBox.Select();
        }

        public event EventHandler OnTextChanged;

    }
}