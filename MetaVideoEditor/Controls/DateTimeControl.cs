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

    public class DateTimeControl : Panel
    {

        public DateTimeControl()
        {
            label = new Label();
            label.Dock = DockStyle.Fill;
            label.Click += new EventHandler(label_Click);
            this.Controls.Add(label);

            dtBox = new DateTimePicker();
            dtBox.Dock = DockStyle.Fill;
            dtBox.Visible = false;
            dtBox.DropDownAlign = LeftRightAlignment.Left;
            dtBox.Leave += new EventHandler(dtBox_Leave);
            dtBox.ValueChanged += new EventHandler(dtBox_ValueChanged);
            label.Controls.Add(dtBox);
        }

        void dtBox_ValueChanged(object sender, EventArgs e)
        {
            if (this.Parent != null)
                this.Parent.Focus();
            dateTime = dtBox.Value;
            if (OnValueChanged != null) OnValueChanged(this, e);
        }

       

        void dtBox_Leave(object sender, EventArgs e)
        {                      
            dtBox.Visible = false;
            if (dateTime != dtBox.Value)
            {
                dateTime = dtBox.Value;  
            }
        }

        Label label;
        DateTimePicker dtBox;

        void label_Click(object sender, EventArgs e)
        {
            GeneralItemPanel panel = Parent as GeneralItemPanel;
            panel.GeneralItemPanel_Click(sender, e);            
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Focus();
            dtBox.Visible = true;
            dtBox.Select(); 
        }

        DateTime _dateTime;
        public DateTime dateTime
        {
            get
            {
                return _dateTime;
            }
            set
            {
                _dateTime = value;
                label.Text = value.ToString();
            }
        }

        public event EventHandler OnValueChanged;

    }
}