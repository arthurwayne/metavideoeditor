using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using CustomControls;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class GeneralPanel : BasePanel
    {
        public GeneralPanel()
        {
           InitializeComponent();
           MainPanel = panel1;

           this.Click += new EventHandler(GeneralPanel_Click);
           panel1.Click += new EventHandler(GeneralPanel_Click);
           dataPanel.Click += new EventHandler(GeneralPanel_Click);

           titleBox = new TextBoxControl();
           titleBox.OnTextChanged += new EventHandler(OnChanged);
           titlePanel = new GeneralItemPanel(Kernel.Instance.GetString("TitleStr"), titleBox, 24);

           originalTitleBox = new TextBoxControl();
           originalTitleBox.OnTextChanged += new EventHandler(OnChanged);
           originalTitlePanel = new GeneralItemPanel(Kernel.Instance.GetString("OriginalTitleStr"), originalTitleBox, 24);

           sortTitleBox = new TextBoxControl();
           sortTitleBox.OnTextChanged += new EventHandler(OnChanged);
           sortTitlePanel = new GeneralItemPanel(Kernel.Instance.GetString("SortTitleStr"), sortTitleBox, 24);

           addedBox = new DateTimeControl();
           addedBox.OnValueChanged += new EventHandler(OnChanged);
           addedPanel = new GeneralItemPanel(Kernel.Instance.GetString("DateAddedStr"), addedBox, 24);

           yearBox = new TextBoxControl();
           yearBox.OnTextChanged += new EventHandler(OnChanged); 
           yearPanel = new GeneralItemPanel(Kernel.Instance.GetString("YearStr"), yearBox, 24);

           runTimeBox = new NumericBoxControl();
           runTimeBox.OnTextChanged += new EventHandler(OnChanged);
           runTimePanel = new GeneralItemPanel(Kernel.Instance.GetString("RuntimeStr"), runTimeBox, 24);

           ratingBox = new NumericBoxControl();
           ratingBox.decimals = 1;
            ratingBox.increment = (decimal)0.1;
           ratingBox.OnTextChanged += new EventHandler(OnChanged);
           ratingPanel = new GeneralItemPanel(Kernel.Instance.GetString("RatingStr"), ratingBox, 24);

           mpaaBox = new ComboBoxControl();
           mpaaBox.choices = new string[] { "G", "NC-17", "NR", "PG", "PG-13", "R" };
           mpaaBox.OnTextChanged += new EventHandler(OnChanged);
           mpaaPanel = new GeneralItemPanel(Kernel.Instance.GetString("MpaaStr"), mpaaBox, 24);

           overviewBox = new TextBoxControl();
           overviewBox.multiLine = true;
           overviewBox.OnTextChanged += new EventHandler(OnChanged);
           overviewPanel = new GeneralItemPanel(Kernel.Instance.GetString("OverviewStr"), overviewBox, 100);

           mediaTypeBox = new ComboBoxControl();
           mediaTypeBox.choices = new string[] { "AVI", "Blu-Ray", "DVD", "HD-DVD", "MKV" };
           mediaTypeBox.OnTextChanged += new EventHandler(OnChanged);
           mediaTypePanel = new GeneralItemPanel(Kernel.Instance.GetString("MediaTypeStr"), mediaTypeBox, 24);

           ratioBox = new ComboBoxControl();
           ratioBox.choices = new string[] { "1.33:1", "1.78:1", "1.85:1", "2.35:1", "2.40:1" };
           ratioBox.OnTextChanged += new EventHandler(OnChanged);
           ratioPanel = new GeneralItemPanel(Kernel.Instance.GetString("RatioStr"), ratioBox, 24);

           watchedBox = new ComboBoxControl();
           watchedBox.choices = new string[] { Kernel.Instance.GetString("YesStr"), Kernel.Instance.GetString("No") };
           watchedBox.OnTextChanged += new EventHandler(OnChanged);
           watchedPanel = new GeneralItemPanel(Kernel.Instance.GetString("WatchedStr"), watchedBox, 24);

           seriesBox = new TextBoxControl();
           seriesBox.OnTextChanged += new EventHandler(OnChanged);
           seriesPanel = new GeneralItemPanel(Kernel.Instance.GetString("SeriesStr"), seriesBox, 24);

           seasonBox = new TextBoxControl();
           seasonBox.OnTextChanged += new EventHandler(OnChanged);
           seasonPanel = new GeneralItemPanel(Kernel.Instance.GetString("SeasonStr"), seasonBox, 24);

           episodeBox = new TextBoxControl();
           episodeBox.OnTextChanged += new EventHandler(OnChanged);
           episodePanel = new GeneralItemPanel(Kernel.Instance.GetString("EpisodeStr"), episodeBox, 24);

           this.CreateGraphics();
        }

        void GeneralPanel_Click(object sender, EventArgs e)
        {
            this.panel1.Focus();
        }

        void OnChanged(object sender, EventArgs e)
        {
            if (SelectedItem.Title != titleBox.text)
            {
                SelectedItem.Title = titleBox.text;
                Kernel.Instance.ItemCollection.SelectedNode.Name = SelectedItem.Title;
            }
            SelectedItem.OriginalTitle = originalTitleBox.text;
            SelectedItem.SortTitle = sortTitleBox.text;
            SelectedItem.DateAdded = addedBox.dateTime;
            int year;
            if (Int32.TryParse(yearBox.text, out year))
                SelectedItem.Year = year;
            int? run = (int)runTimeBox.value;
            if (run.IsValidRunningTime())
            {
                SelectedItem.RunningTime = (int)runTimeBox.value;
                runTimeBox.TextDisplayed = Helper.ConvertToTime(runTimeBox.value);
            }
            else
            {
                SelectedItem.RunningTime = null;
                runTimeBox.TextDisplayed = "";
            }
            float? rate = (float)ratingBox.value;
            if (rate.IsValidRating())
            {
                SelectedItem.Rating = rate;
                ratingBox.TextDisplayed = Helper.ConvertToRating(ratingBox.value);
            }
            else
            {
                SelectedItem.Rating = null;
                ratingBox.TextDisplayed = "";
            }
            SelectedItem.MPAARating = mpaaBox.text;
            SelectedItem.Overview = overviewBox.text;
            SelectedItem.Mediatype = mediaTypeBox.text;
            SelectedItem.AspectRatio = ratioBox.text;
            if (watchedBox.text == Kernel.Instance.GetString("YesStr"))
                SelectedItem.Watched = true;
            else if (watchedBox.text == Kernel.Instance.GetString("NoStr"))
                SelectedItem.Watched = false;
            else
                SelectedItem.Watched = null;
            if (SelectedItem.Type == Entity.Season || SelectedItem.Type == Entity.Episode)
            {
                SelectedItem.SeriesName = seriesBox.text;
                SelectedItem.SeasonNumber = seasonBox.text;
                if (SelectedItem.Type == Entity.Episode) SelectedItem.EpisodeNumber = episodeBox.text;
            }
            HasChanged();
        }

        TextBoxControl titleBox;
        TextBoxControl originalTitleBox;
        TextBoxControl sortTitleBox;
        DateTimeControl addedBox;
        TextBoxControl yearBox;
        NumericBoxControl runTimeBox;
        NumericBoxControl ratingBox;
        ComboBoxControl mpaaBox;
        TextBoxControl overviewBox;
        ComboBoxControl mediaTypeBox;
        ComboBoxControl ratioBox;
        ComboBoxControl watchedBox;
        TextBoxControl seriesBox;
        TextBoxControl seasonBox;
        TextBoxControl episodeBox;

        GeneralItemPanel titlePanel;
        GeneralItemPanel originalTitlePanel;
        GeneralItemPanel sortTitlePanel;
        GeneralItemPanel addedPanel;
        GeneralItemPanel yearPanel;
        GeneralItemPanel runTimePanel;
        GeneralItemPanel ratingPanel;
        GeneralItemPanel mpaaPanel;
        GeneralItemPanel overviewPanel;
        GeneralItemPanel mediaTypePanel;
        GeneralItemPanel ratioPanel;
        GeneralItemPanel watchedPanel;
        GeneralItemPanel seriesPanel;
        GeneralItemPanel seasonPanel;
        GeneralItemPanel episodePanel;

        public override void UpdateData()
        {
            dataPanel.Controls.Clear();

            
            titleBox.text = SelectedItem.Title;
            AddControl(titlePanel);

            originalTitleBox.text = SelectedItem.OriginalTitle;
            AddControl(originalTitlePanel);

            sortTitleBox.text = SelectedItem.SortTitle;
            AddControl(sortTitlePanel);

            if (SelectedItem.Type == Entity.Season || SelectedItem.Type == Entity.Episode)
            {
                seriesBox.text = SelectedItem.SeriesName;
                AddControl(seriesPanel);

                seasonBox.text = SelectedItem.SeasonNumber;
                AddControl(seasonPanel);

                if (SelectedItem.Type == Entity.Episode)
                {
                    episodeBox.text = SelectedItem.EpisodeNumber;
                    AddControl(episodePanel);
                }
            }

            addedBox.dateTime = SelectedItem.DateAdded;
            AddControl(addedPanel);

            yearBox.text = SelectedItem.Year.ToString();
            AddControl(yearPanel);

            if (SelectedItem.RunningTime != null)
            {
                runTimeBox.value = (decimal)SelectedItem.RunningTime;
                runTimeBox.TextDisplayed = Helper.ConvertToTime(runTimeBox.value);
            }
            else
            {
                runTimeBox.value = 0;
                runTimeBox.TextDisplayed = "";
            }
            AddControl(runTimePanel);

            if (SelectedItem.Rating != null)
            {
                ratingBox.value = (decimal)SelectedItem.Rating;
                ratingBox.TextDisplayed = Helper.ConvertToRating(ratingBox.value);
            }
            else
            {
                ratingBox.value = 0;
                ratingBox.TextDisplayed = "";
            }
            AddControl(ratingPanel);

            mpaaBox.text = SelectedItem.MPAARating;
            AddControl(mpaaPanel);

            overviewBox.text = SelectedItem.Overview;
            AddControl(overviewPanel);

            mediaTypeBox.text = SelectedItem.Mediatype;
            AddControl(mediaTypePanel);

            ratioBox.text = SelectedItem.AspectRatio;
            AddControl(ratioPanel);

            if (SelectedItem.Watched != null)
            {
                if ((bool)SelectedItem.Watched) watchedBox.text = Kernel.Instance.GetString("YesStr");
                else watchedBox.text = Kernel.Instance.GetString("NoStr");
            }
            AddControl(watchedPanel);

            
        }

        private void AddControl(Control control)
        {
            int yLoc = 0;
            foreach (Control c in dataPanel.Controls)
                yLoc += c.Height + 10;
            control.Location = new Point(0, yLoc);
            dataPanel.Controls.Add(control);
        }

        
        





    }
}
