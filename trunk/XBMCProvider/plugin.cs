using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace XBMCProvider
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{C8471D46-04EE-4926-89B3-B5C00C9AA409}"); }
        }

        public override string Name
        {
            get { return "XBMC Reader"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les infos gérées par XBMC";
                    default: return "Get metadata that are compatible with XBMC";
                } 
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Local; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.MovieAndSeries; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 1); }
        }

        public override Version RequiredMVEVersion
        {
            get { return new Version(2, 0, 0); }
        }

        public override bool IsConfigurable
        {
            get { return true; }
        }

        public static PluginConfiguration<PluginOptions> PluginOptions { get; set; }

        public override IPluginConfiguration PluginConfiguration
        {
            get
            {
                return PluginOptions;
            }
        }

        public override void Init(Kernel kernel)
        {
            PluginOptions = new PluginConfiguration<PluginOptions>(kernel, this.GetType().Assembly);
            PluginOptions.Load();
        }

        private static string location;
        static string videofilename;

        public override Item Read(Item item)
        {
            Item i = new Item();
            if (Directory.Exists(item.Path))
            {
                location = item.Path;
                if (item.VideoFiles.IsNonEmpty())
                    videofilename = Path.GetFileNameWithoutExtension(item.VideoFiles[0]);
            }
            else if (File.Exists(item.Path))
            {
                location = Directory.GetParent(item.Path).FullName;
                videofilename = Path.GetFileNameWithoutExtension(item.Path);
            }
            else
                return i;
            switch (item.Type)
            {
                case Entity.Movie: FetchXmlMovie(i);
                    break;
                case Entity.Series: FetchSeries(i);
                    break;
                case Entity.Episode: FetchEpisode(i);
                    break;
                default: break;
            }
            FetchImages(i);
            return i;
        }

        private static void FetchXmlMovie(Item movie)
        {
            string mfile = Path.Combine(location, videofilename + ".nfo");

            if (File.Exists(mfile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mfile);

                movie.Title = doc.SafeGetString("movie/title");
                movie.OriginalTitle = doc.SafeGetString("movie/originaltitle");
                movie.SortTitle = doc.SafeGetString("movie/sorttitle");
                DateTime added;
                DateTime.TryParse(doc.SafeGetString("movie/added"), out added);
                if (added != null) movie.DateAdded = added;

                int? y = doc.SafeGetInt32("movie/year", 0);
                if (y.IsValidYear()) movie.Year = y;
               
                int? rt = doc.SafeGetInt32("movie/runtime", 0);
                if (rt.IsValidRunningTime())  movie.RunningTime = rt;

                movie.Rating = doc.SafeGetSingle("movie/rating", (float)-1, (float)10);
                movie.MPAARating = doc.SafeGetString("movie/mpaa");
                movie.Overview = doc.SafeGetString("movie/plot");
                movie.Mediatype = doc.SafeGetString("movie/type");
                movie.AspectRatio = doc.SafeGetString("movie/aspectratio");

                foreach (XmlNode node in doc.SelectNodes("movie/actor"))
                {
                    if (movie.Actors == null)
                        movie.Actors = new List<Actor>();
                    Actor actor = new Actor();
                    actor.Name = node.SafeGetString("name");
                    actor.Role = node.SafeGetString("role");
                    actor.ImagePath = node.SafeGetString("thumb");
                    if (string.IsNullOrEmpty(actor.ImagePath) && Directory.Exists(PluginOptions.Instance.ActorsThumbPath))
                    {
                        string actorImage = Path.Combine(PluginOptions.Instance.ActorsThumbPath, actor.Name + ".tbn");
                        if (File.Exists(actorImage))
                            actor.ImagePath = actorImage;
                    }
                    movie.Actors.Add(actor);
                }


                foreach (XmlNode node in doc.SelectNodes("movie/director"))
                {
                    if (movie.Crew == null) movie.Crew = new List<CrewMember>();
                    movie.Crew.Add(new CrewMember { Name = node.InnerText, Activity = "Director" });
                }

                foreach (XmlNode node in doc.SelectNodes("movie/genre"))
                {
                    if (movie.Genres == null) movie.Genres = new List<string>();
                    movie.Genres.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("movie/studio"))
                {
                    if (movie.Studios == null) movie.Studios = new List<string>();
                    movie.Studios.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("movie/country"))
                {
                    if (movie.Countries == null) movie.Countries = new List<string>();
                    movie.Countries.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("movie/tagline"))
                {
                    if (movie.TagLines == null) movie.TagLines = new List<string>();
                    movie.TagLines.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("movie/trailer"))
                {
                    if (movie.TrailerFiles == null) movie.TrailerFiles = new List<string>();
                    movie.TrailerFiles.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("movie/thumb"))
                {
                    if (movie.ImagesPaths == null) movie.ImagesPaths = new List<Poster>();
                    movie.ImagesPaths.AddDistinctPoster(new Poster { Image = node.InnerText });
                }

                foreach (XmlNode node in doc.SelectNodes("movie/fanart/thumb"))
                {
                    if (movie.BackdropImagePaths == null) movie.BackdropImagePaths = new List<Poster>();
                    movie.BackdropImagePaths.AddDistinctPoster(new Poster { Image = node.InnerText });
                }

            }
        }

        private static void FetchSeries(Item series)
        {
            string mfile = Path.Combine(location, "tvshow.nfo");

            if (File.Exists(mfile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mfile);

                series.SeriesName = series.Title = doc.SafeGetString("tvshow/title");
                series.Rating = doc.SafeGetSingle("tvshow/rating", (float)-1, (float)10);

                string id = doc.SafeGetString("tvshow/id");
                if (!string.IsNullOrEmpty(id))
                {
                    series.ProvidersId = new List<DataProviderId>();
                    series.ProvidersId.Add(new DataProviderId { Id = id, Name = "TheTVDB", Url = "http://thetvdb.com/?tab=series&id=" + id });
                }

                series.Overview = doc.SafeGetString("tvshow/overview");

                foreach (XmlNode node in doc.SelectNodes("tvshow/actor"))
                {
                    if (series.Actors == null)
                        series.Actors = new List<Actor>();
                    Actor actor = new Actor();
                    actor.Name = node.SafeGetString("name");
                    actor.Role = node.SafeGetString("role");
                    actor.ImagePath = node.SafeGetString("thumb");
                    if (string.IsNullOrEmpty(actor.ImagePath) && Directory.Exists(PluginOptions.Instance.ActorsThumbPath))
                    {
                        string actorImage = Path.Combine(PluginOptions.Instance.ActorsThumbPath, actor.Name + ".tbn");
                        if (File.Exists(actorImage))
                            actor.ImagePath = actorImage;
                    }
                    series.Actors.Add(actor);
                }

                foreach (XmlNode node in doc.SelectNodes("tvshow/genre"))
                {
                    if (series.Genres == null) series.Genres = new List<string>();
                    series.Genres.Add(node.InnerText);
                }

                series.MPAARating = doc.SafeGetString("mpaa");

                int? rt = doc.SafeGetInt32("tvshow/runtime", 0);
                if (rt.IsValidRunningTime()) series.RunningTime = rt;

                series.Rating = doc.SafeGetSingle("tvshow/rating", (float)-1, (float)10);

                foreach (XmlNode node in doc.SelectNodes("tvshow/studio"))
                {
                    if (series.Studios == null) series.Studios = new List<string>();
                    series.Studios.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("tvshow/tagline"))
                {
                    if (series.TagLines == null) series.TagLines = new List<string>();
                    series.TagLines.Add(node.InnerText);
                }

                foreach (XmlNode node in doc.SelectNodes("tvshow/thumb"))
                {
                    if (series.BannersPaths == null) series.BannersPaths = new List<Poster>();
                    series.BannersPaths.AddDistinctPoster(new Poster { Image = node.InnerText });
                }
            }
        }

        private static void FetchEpisode(Item episode)
        {
            var mfile = Path.Combine(location, videofilename + ".nfo");
            if (File.Exists(mfile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mfile);

                episode.Overview = doc.SafeGetString("episodedetails/plot");
                episode.EpisodeNumber = doc.SafeGetString("episodedetails/episode");
                episode.Title = doc.SafeGetString("episodedetails/title");
                episode.SeasonNumber = doc.SafeGetString("episodedetails/season");
                episode.Rating = doc.SafeGetSingle("episodedetails/rating", (float)-1, 10);

                foreach (XmlNode node in doc.SelectNodes("episodedetails/actor"))
                {
                    if (episode.Actors == null)
                        episode.Actors = new List<Actor>();
                    Actor actor = new Actor();
                    actor.Name = node.SafeGetString("name");
                    actor.Role = node.SafeGetString("role");
                    actor.ImagePath = node.SafeGetString("thumb");
                    if (string.IsNullOrEmpty(actor.ImagePath) && Directory.Exists(PluginOptions.Instance.ActorsThumbPath))
                    {
                        string actorImage = Path.Combine(PluginOptions.Instance.ActorsThumbPath, actor.Name + ".tbn");
                        if (File.Exists(actorImage))
                            actor.ImagePath = actorImage;
                    }
                    episode.Actors.Add(actor);
                }


                foreach (XmlNode node in doc.SelectNodes("episodedetails/director"))
                {
                    if (episode.Crew == null) episode.Crew = new List<CrewMember>();
                    episode.Crew.Add(new CrewMember { Name = node.InnerText, Activity = "Director" });
                }

                foreach (XmlNode node in doc.SelectNodes("episodedetails/credit"))
                {
                    if (episode.Crew == null) episode.Crew = new List<CrewMember>();
                    episode.Crew.Add(new CrewMember { Name = node.InnerText, Activity = "Writer" });
                }
            }
        }
        
        public static void FetchImages(Item item)
        {
            string fanart = Path.Combine(location, videofilename + "-fanart.jpg");
            if (File.Exists(fanart))
            {
                if (item.BackdropImagePaths == null) item.BackdropImagePaths = new List<Poster>();
                item.BackdropImagePaths.AddDistinctPoster(new Poster { Image = fanart, Checked = true });
            }

            string poster = Path.Combine(location, videofilename + ".tbn");
            if (File.Exists(poster))
            {
                if (item.Type != Entity.Series)
                {
                    if (item.ImagesPaths == null) item.ImagesPaths = new List<Poster>();
                    item.ImagesPaths.AddDistinctPoster(new Poster { Image = poster, Checked = true });
                }
                else
                {
                    if (item.BannersPaths == null) item.BannersPaths = new List<Poster>();
                    item.BannersPaths.AddDistinctPoster(new Poster { Image = poster, Checked = true });
                }
            }
        }      

    }
}
