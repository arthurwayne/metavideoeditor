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

    public class IdControl : Panel
    {

        public IdControl()
        {
            ProviderLabel = new Label();
            ProviderLabel.Dock = DockStyle.Left;
            ProviderLabel.Width = 120;
            ProviderLabel.Click += new EventHandler(label_Click);
            this.Controls.Add(ProviderLabel);

            IdLabel = new Label();
            IdLabel.AutoSize = false;
            IdLabel.Location = new Point(120, 0);
            IdLabel.Click += new EventHandler(label_Click);
            this.Controls.Add(IdLabel);

            butBox = new Button();
            butBox.BackgroundImage = global::MetaVideoEditor.Properties.Resources.link;
            butBox.BackgroundImageLayout = ImageLayout.Stretch;
            butBox.Click += new EventHandler(picBox_Click);
            butBox.Visible = false;
            butBox.Dock = DockStyle.Right;
            butBox.Width = 24;
            IdLabel.Controls.Add(butBox);            

            textBox = new TextBox();
            textBox.Visible = false;
            textBox.Dock = DockStyle.Fill;
            textBox.KeyPress +=new KeyPressEventHandler(textBox_KeyPress);
            IdLabel.Controls.Add(textBox);

            comboBox = new ComboBox();
            comboBox.Visible = false;
            comboBox.DropDownStyle = ComboBoxStyle.DropDownList;
            comboBox.SelectedIndexChanged += new EventHandler(comboBox_SelectedIndexChanged);
            comboBox.Dock = DockStyle.Fill;            
            ProviderLabel.Controls.Add(comboBox);

            this.Leave += new EventHandler(this_Leave);
        }

        void picBox_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(CurrentProvider.Url))
                System.Diagnostics.Process.Start(CurrentProvider.Url);
        }

        void comboBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            ProviderLabel.Text = comboBox.Text;
            IdLabel.Text = textBox.Text = "";
            butBox.Visible = false;
            if (ProviderIds == null) return;
            CurrentProvider = ProviderIds.Find(p => p.Name == comboBox.Text);
            if (CurrentProvider != null)
            {
                IdLabel.Text = textBox.Text = CurrentProvider.Id;
                butBox.Visible = true;
            }
        }

        DataProviderId _currentProvider;
        DataProviderId CurrentProvider
        {
            get
            {
                return _currentProvider;
            }
            set
            {
                _currentProvider = value;
            }
        }

        void textBox_KeyPress(object sender, KeyPressEventArgs e)
        {
            
            if (e.KeyChar == (char)Keys.Return)
            {
                changed = true;
                if (this.Parent != null)
                    this.Parent.Focus();
            }
            else if (e.KeyChar == (char)Keys.Escape)
            {
                changed = false;
                IdLabel.Text = textBox.Text = CurrentProvider.Id;
                if (this.Parent != null)
                    this.Parent.Focus();
            }
        }

        void this_Leave(object sender, EventArgs e)
        {                      
            comboBox.Visible = textBox.Visible = false;
            
            if (IdLabel.Text != textBox.Text)
            {
                IdLabel.Text = textBox.Text;
                if (string.IsNullOrEmpty(textBox.Text))
                {
                    butBox.Visible = false;
                    if (CurrentProvider != null)
                        ProviderIds.Remove(CurrentProvider);
                }
                else
                {
                    butBox.Visible = true;
                    if (CurrentProvider != null)
                        CurrentProvider.Id = textBox.Text;
                    else
                    {
                        ProviderIds.Add(new DataProviderId { Name = comboBox.Text, Id = textBox.Text, Url = GetUrl(comboBox.Text, textBox.Text) });
                        CurrentProvider = ProviderIds.Find(p => p.Name == comboBox.Text);
                    }
                }
                if (OnTextChanged != null) OnTextChanged(this, e);
            }
        }

        Label ProviderLabel;
        Label IdLabel;
        TextBox textBox;
        ComboBox comboBox;
        Button butBox;

        void label_Click(object sender, EventArgs e)
        {
            GeneralItemPanel panel = Parent as GeneralItemPanel;
            panel.GeneralItemPanel_Click(sender, e);
        }

        bool changed = false;

        List<DataProviderId> _providerIds;
        public List<DataProviderId> ProviderIds
        {
            get
            {
                return _providerIds;
            }
            set
            {
                _providerIds = value;
                if (_providerIds != null && _providerIds.Count > 0)
                {
                    CurrentProvider = _providerIds[0];
                    comboBox.Text = ProviderLabel.Text = CurrentProvider.Name;
                }
                else
                {
                    comboBox.Text = ProviderLabel.Text = (string)comboBox.Items[0];
                }
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
                ProviderLabel.Text = comboBox.Text = value;
            }
        }

        public string[] choices
        {
            set
            {
                comboBox.Items.Clear();
                comboBox.Items.AddRange(value);                
            }
        }

        public override void Refresh()
        {
            base.Refresh();
            this.Focus();
            comboBox.Visible = textBox.Visible = true;
            comboBox.Select();
            comboBox.DroppedDown = true;
        }

        public event EventHandler OnTextChanged;

        protected override void OnPaint(PaintEventArgs e)
        {
            SetSize();
            base.OnPaint(e);
        }
        void SetSize()
        {
           IdLabel.Width = this.Width - 120;
        }

        string GetUrl(string provider, string id)
        {
            switch (provider)
            {
                case "themoviedb": return "http://www.themoviedb.org/movie/" + id;
                case "Imdb": return "http://www.imdb.com/title/" + id;
                case "Ciné-Passion": return "http://passion-xbmc.org/scraper/index2.php?Page=ViewMovie&ID=" + id;
                case "AlloCine": return "http://www.allocine.fr/film/fichefilm_gen_cfilm=" + id + ".html";
                case "TheTVDB": return "http://thetvdb.com/?tab=series&id=" + id;
                default: return "";
            }
        }

    }
}