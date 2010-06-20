using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using CustomControls;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class OverviewPanel : BasePanel
    {
        public OverviewPanel()
        {
            InitializeComponent();
            generalGroup.Text = Kernel.Instance.GetString("TitleGeTab");
            peopleGroup.Text = Kernel.Instance.GetString("CastingStr");
            overviewGroup.Text = Kernel.Instance.GetString("OverviewStr");
            MainPanel = DetailsPanel;
            posterBox.ShowResolution = bd1Box.ShowResolution = bd2Box.ShowResolution = bannerBox.ShowResolution = false;
                   
        }        

        public override void UpdateData()
        {
            SetDetailsPanelVisibility();            

            posterBox.ImgPoster = SelectedItem.PrimaryImage;
            
            if (SelectedItem.BackdropImagePaths.IsNonEmpty())
            {
                bd1Box.ImgPoster = SelectedItem.BackdropImagePaths[0];
                if (SelectedItem.BackdropImagePaths.Count > 1)
                {
                    bd2Box.ImgPoster = SelectedItem.BackdropImagePaths[1];
                }
                else
                    bd2Box.ImgPoster = null;
            }
            else
            {
                bd1Box.ImgPoster = bd2Box.ImgPoster = null;
            }
            bannerBox.ImgPoster = SelectedItem.PrimaryBanner;          

            if (SelectedItem.Type == Entity.Folder)
            {
                generalGroup.Visible = overviewGroup.Visible = peopleGroup.Visible = false;
                return;
            }
            generalGroup.Visible = overviewGroup.Visible = peopleGroup.Visible = true;
            
            UpdateTitle();
            UpdateOverview();
            UpdateGenres();
            UpdateStudios();
            UpdateCrew();
            UpdateActors();
            UpdateRating();
            UpdateRunTime();
        }

        void UpdateTitle()
        {
            if (SelectedItem.Year != null)
                titleBox.text = string.Format("{0} ({1})", SelectedItem.Title, SelectedItem.Year.ToString());
            else
                titleBox.text = SelectedItem.Title;            
        }

        void UpdateOverview()
        {
            overviewBox.text = SelectedItem.Overview;
        }

        void UpdateRunTime()
        {
            if (SelectedItem.RunningTime.IsValidRunningTime())
            {
                runtimeBox.value = (decimal)SelectedItem.RunningTime;
                runtimeBox.TextDisplayed = Helper.ConvertToTime(runtimeBox.value);
            }
            else
                runtimeBox.TextDisplayed = Kernel.Instance.GetString("RuntimeStr");
        }

        void UpdateRating()
        {
            Star1Picture.Image = Star2Picture.Image = Star3Picture.Image = Star4Picture.Image = Star5Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Empty;

            if (SelectedItem.Rating.IsValidRating())
            {
                if (SelectedItem.Rating > .7 && SelectedItem.Rating < 1.8)
                    Star1Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Half;
                else if (SelectedItem.Rating > 1.7)
                {
                    Star1Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Full;
                    if (SelectedItem.Rating > 2.7 && SelectedItem.Rating < 3.8)
                        Star2Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Half;
                    else if (SelectedItem.Rating > 3.7)
                    {
                        Star2Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Full;
                        if (SelectedItem.Rating > 4.7 && SelectedItem.Rating < 5.8)
                            Star3Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Half;
                        else if (SelectedItem.Rating > 5.7)
                        {
                            Star3Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Full;
                            if (SelectedItem.Rating > 6.7 && SelectedItem.Rating < 7.8)
                                Star4Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Half;
                            else if (SelectedItem.Rating > 7.7)
                            {
                                Star4Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Full;
                                if (SelectedItem.Rating > 8.7 && SelectedItem.Rating < 9.8)
                                    Star5Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Half;
                                else if (SelectedItem.Rating > 9.7)
                                    Star5Picture.Image = global::MetaVideoEditor.Properties.Resources.Star_Full;
                            }
                        }
                    }
                }

                ratingBox.value = (decimal)SelectedItem.Rating;
                ratingBox.TextDisplayed = Helper.ConvertToRating(ratingBox.value);
            }
            else
            {
                ratingBox.value = 0;
                ratingBox.TextDisplayed = Kernel.Instance.GetString("RatingStr");
            }
        }

        void UpdateGenres()
        {
            if (SelectedItem.Genres.IsNonEmpty())
            {
                string genreText = "";
                string genreLabel = "";
                foreach (string s in SelectedItem.Genres)
                {
                    genreLabel += s + " ● ";
                    genreText += s + ", ";
                }
                genreText = genreText.Remove(genreText.Length - 2);
                genreLabel = genreLabel.Remove(genreLabel.Length - 2);
                genresBox.text = genreText;
                genresBox.label.Text = genreLabel;

            }
            else
            {
                genresBox.text = "";
                genresBox.label.Text = Kernel.Instance.GetString("GenresStr");
            }
        }

        void UpdateStudios()
        {
            if (SelectedItem.Studios.IsNonEmpty())
            {
                string studioText = "";
                string studioLabel = "";
                foreach (string s in SelectedItem.Studios)
                {
                    studioLabel += s + " ● ";
                    studioText += s + ", ";
                }
                studioText = studioText.Remove(studioText.Length - 2);
                studioLabel = studioLabel.Remove(studioLabel.Length - 2);
                studiosBox.text = studioText;
                studiosBox.label.Text = studioLabel;

            }
            else
            {
                studiosBox.text = "";
                studiosBox.label.Text = Kernel.Instance.GetString("StudiosStr");
            }
        }

        void UpdateCrew()
        {
            if (SelectedItem.Crew.IsNonEmpty())
            {
                string crewText = "";
                string crewLabel = Kernel.Instance.GetString("TitleCreTab") + " :";
                foreach (CrewMember c in SelectedItem.Crew)
                {
                    if (string.IsNullOrEmpty(c.Activity))
                    {
                        crewLabel += c.Name + " ● ";
                        crewText += c.Name + ", ";
                    }
                    else
                    {
                        crewLabel += string.Format("{0} ({1})", c.Name, c.Activity) + " ● ";
                        crewText += string.Format("{0} ({1})", c.Name, c.Activity) + ", ";
                    }
                }
                crewText = crewText.Remove(crewText.Length - 2);
                crewLabel = crewLabel.Remove(crewLabel.Length - 2);
                crewBox.text = crewText;
                crewBox.label.Text = crewLabel;
            }
            else
            {
                crewBox.text = "";
                crewBox.label.Text = Kernel.Instance.GetString("TitleCreTab");
            }
        }

        void UpdateActors()
        {
            if (SelectedItem.Actors.IsNonEmpty())
            {
                string actorsText = "";
                string actorsLabel = Kernel.Instance.GetString("TitleActTab") + " :";
                foreach (Actor c in SelectedItem.Actors)
                {
                    if (string.IsNullOrEmpty(c.Role))
                    {
                        actorsLabel += c.Name + " ● ";
                        actorsText += c.Name + ", ";
                    }
                    else
                    {
                        actorsLabel += string.Format("{0} ({1})", c.Name, c.Role) + " ● ";
                        actorsText += string.Format("{0} ({1})", c.Name, c.Role) + ", ";
                    }
                }
                actorsText = actorsText.Remove(actorsText.Length - 2);
                actorsLabel = actorsLabel.Remove(actorsLabel.Length - 2);
                actorsBox.text = actorsText;
                actorsBox.label.Text = actorsLabel;
            }
            else
            {
                actorsBox.text = "";
                actorsBox.label.Text = Kernel.Instance.GetString("TitleActTab");
            }
        }

        private void SetDetailsPanelVisibility()
        {
            if (SelectedItem.Type == Entity.Folder)
            {
                InfosPanel.Visible = bannerBox.Visible = false;
                posterBox.Visible = bd1Box.Visible = bd2Box.Visible = true;
            }
            else if (SelectedItem.Type == Entity.Movie || SelectedItem.Type == Entity.Episode)
            {
                InfosPanel.Visible = posterBox.Visible = bd1Box.Visible = bd2Box.Visible = true;
                bannerBox.Visible = false;
            }
            else
            {
                InfosPanel.Visible = bannerBox.Visible = posterBox.Visible = bd1Box.Visible = bd2Box.Visible = true;
            }
        }

        private void posterBox_OnPictureChanged(object sender, EventArgs e)
        {
            ImageBox ib = (ImageBox)sender;
            HasChanged();
            if (!SelectedItem.ImagesPaths.IsNonEmpty())
            {
                SelectedItem.ImagesPaths = new List<Poster>();
                SelectedItem.ImagesPaths.Add(ib.ImgPoster);
            }           
            ib.RefreshBox();
        }

        private void posterBox_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.ImagesPaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            ib.ImgPoster = null;
        }

        private void posterBox_OnCheckChanged(object sender, EventArgs e)
        {
            ImageBox ib = (ImageBox)sender;
            HasChanged();
            ib.ImgPoster.Checked = ib.IsChecked;
        }

        private void bd1Box_OnPictureChanged(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            HasChanged();
            if (!SelectedItem.BackdropImagePaths.IsNonEmpty())
            {
                SelectedItem.BackdropImagePaths = new List<Poster>();
                SelectedItem.BackdropImagePaths.Add(ib.ImgPoster);
            }
            ib.RefreshBox();
        }

        private void bd1Box_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.BackdropImagePaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            ib.ImgPoster = null;
        }

        private void bannerBox_OnPictureChanged(object sender, EventArgs e)
        {
            ImageBox ib = (ImageBox)sender;
            HasChanged();
            if (!SelectedItem.BannersPaths.IsNonEmpty())
            {
                SelectedItem.BannersPaths = new List<Poster>();
                SelectedItem.BannersPaths.Add(ib.ImgPoster);
            }
            ib.RefreshBox();
        }

        private void bannerBox_OnPictureDeleted(object sender, EventArgs e)
        {
            ImageBox ib = sender as ImageBox;
            SelectedItem.BannersPaths.RemoveAll(i => i.Image == ib.ImgPoster.Image);
            HasChanged();
            ib.ImgPoster = null;
        }

        private void DetailsPanel_Click(object sender, EventArgs e)
        {
            MainPanel.Focus();
        }

        private void overviewBox_OnTextChanged(object sender, EventArgs e)
        {
            SelectedItem.Overview = overviewBox.text;
            HasChanged();
        }

        private void titleBox_OnTextChanged(object sender, EventArgs e)
        {
            Regex[] titleExpressions = new Regex[] {
                new Regex(@"^(?<title>.+),\s?(?<year>\d{4})$"),
                new Regex(@"^(?<title>.+)\((?<year>\d{4})\)$")
            };
            bool match = false;
            foreach (Regex exp in titleExpressions)
            {
                Match m = exp.Match(titleBox.text);
                if (m.Success)
                {
                    match = true;
                    SelectedItem.Title = m.Groups["title"].Value.Trim();
                    int? y = Int32.Parse(m.Groups["year"].Value);
                    if (y.IsValidYear())
                        SelectedItem.Year = y;
                }
            }
            if (!match)
                SelectedItem.Title = titleBox.text;
            Kernel.Instance.ItemCollection.SelectedNode.Name = SelectedItem.Title;
            UpdateTitle();
            HasChanged();
        }

        private void runtimeBox_OnTextChanged(object sender, EventArgs e)
        {
            int? run = (int)runtimeBox.value;
            if (run.IsValidRunningTime())
            {
                SelectedItem.RunningTime = (int)runtimeBox.value;
                runtimeBox.TextDisplayed = Helper.ConvertToTime(runtimeBox.value);
            }
            else
            {
                SelectedItem.RunningTime = null;
                runtimeBox.TextDisplayed = Kernel.Instance.GetString("RuntimeStr");
            }
            HasChanged();
        }

        private void ratingBox_OnTextChanged(object sender, EventArgs e)
        {
            float? rate = (float)ratingBox.value;
            if (rate.IsValidRating())
            {
                SelectedItem.Rating = (float)ratingBox.value;
                ratingBox.TextDisplayed = Helper.ConvertToRating(ratingBox.value);
            }
            else
            {
                SelectedItem.Rating = null;
                ratingBox.TextDisplayed = Kernel.Instance.GetString("RatingStr");
            }
            UpdateRating();
            HasChanged();
        }

        private void genresBox_OnTextChanged(object sender, EventArgs e)
        {
            SelectedItem.Genres = new List<string>();
            foreach (string s in genresBox.text.Split(','))
                SelectedItem.Genres.Add(s.Trim());
            UpdateGenres();
            HasChanged();
        }

        private void studiosBox_OnTextChanged(object sender, EventArgs e)
        {
            SelectedItem.Studios = new List<string>();
            foreach (string s in studiosBox.text.Split(','))
                SelectedItem.Studios.Add(s.Trim());
            UpdateStudios();
            HasChanged();
        }

        private void crewBox_OnTextChanged(object sender, EventArgs e)
        {
            SelectedItem.Crew = new List<CrewMember>();
            foreach (string s in crewBox.text.Split(','))
            {
                CrewMember crew = new CrewMember();
                Regex r = new Regex(@"^(?<name>[^\(]+)\((?<activity>.+)\)$");
                Match m = r.Match(s.Trim());
                if (m.Success)
                {
                    crew.Activity = m.Groups["activity"].Value;
                    crew.Name = m.Groups["name"].Value;
                }
                else
                    crew.Name = s.Trim();
                SelectedItem.Crew.Add(crew);
            }
            UpdateCrew();
            HasChanged();
        }

        private void actorsBox_OnTextChanged(object sender, EventArgs e)
        {
            List<Actor> tmp = new List<Actor>(SelectedItem.Actors);
            SelectedItem.Actors = new List<Actor>();
            foreach (string s in actorsBox.text.Split(','))
            {
                Actor actor = new Actor();
                Regex r = new Regex(@"^(?<name>[^\(]+)\((?<role>.+)\)$");
                Match m = r.Match(s.Trim());
                if (m.Success)
                {
                    actor.Role = m.Groups["role"].Value.Trim();
                    actor.Name = m.Groups["name"].Value.Trim();
                }
                else
                    actor.Name = s.Trim();
                Actor a = tmp.Find(A => A.Name == actor.Name || A.Role == actor.Role);
                if (a != null && !string.IsNullOrEmpty(a.ImagePath))
                    actor.ImagePath = a.ImagePath;
                SelectedItem.Actors.Add(actor);
            }
            UpdateActors();
            HasChanged();
        }

    }
}
