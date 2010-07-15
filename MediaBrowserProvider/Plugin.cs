using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace MediaBrowserProvider
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{E5FAC70B-6628-4e5f-9E58-10C92F306B98}"); }
        }

        public override string Name
        {
            get { return "MediaBrowser Reader"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les backdrops et les infos gérées par MediaBrowser";
                    default: return "Get metadata that are compatible with MediaBrowser";
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
            get { return new Version(1, 0, 2); }
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

        public override Item Read(Item item)
        {
            Item i = new Item();
            location = item.MetadataLocation;
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

        public static string GetImagesByNameFolder
        {
            get
            {                
                string path = "";
                string MBPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MediaBrowser");
                if (File.Exists(Path.Combine(MBPath, "MediaBrowserXml.config")))
                {
                    try
                    {
                        XmlDocument doc = new XmlDocument();
                        doc.Load(Path.Combine(MBPath, "MediaBrowserXml.config"));
                        path = doc.SafeGetString("Settings/ImageByNameLocation");
                    }
                    catch { return ""; }
                }
                if (Directory.Exists(path)) return path;
                return "";
            }
        }

        private static void FetchXmlMovie(Item movie)
        {            
            string mfile = Path.Combine(location, "mymovies.xml");

            if (File.Exists(mfile))
            {
                XmlDocument doc = new XmlDocument();
                doc.Load(mfile);

                
                movie.Title = doc.SafeGetString("Title/LocalTitle");
                movie.OriginalTitle = doc.SafeGetString("Title/OriginalTitle");
                movie.SortTitle = doc.SafeGetString("Title/SortTitle");
                DateTime added;
                DateTime.TryParse(doc.SafeGetString("Title/Added"), out added);
                if (added != null) movie.DateAdded = added;

                if (movie.Year == null)
                {
                    int y = doc.SafeGetInt32("Title/ProductionYear", 0);
                    if (y > 1900)
                        movie.Year = y;
                }
                if (movie.RunningTime == null)
                {
                    int rt = doc.SafeGetInt32("Title/RunningTime", 0);
                    if (rt > 0)
                        movie.RunningTime = rt;
                }
                movie.Rating = doc.SafeGetSingle("Title/IMDBrating", (float)-1, (float)10);
                movie.MPAARating = doc.SafeGetString("Title/MPAARating");
                movie.Overview = doc.SafeGetString("Title/Description");
                movie.Mediatype = doc.SafeGetString("Title/Type");
                movie.AspectRatio = doc.SafeGetString("Title/AspectRatio");
                
                if (movie.Overview != null)
                    movie.Overview = movie.Overview.Replace("\n\n", "\n");

                foreach (XmlNode node in doc.SelectNodes("Title/Persons/Person[Type='Actor']"))
                {
                    try
                    {
                        if (movie.Actors == null)
                            movie.Actors = new List<Actor>();
                        Actor actor = new Actor();
                        actor.Name = node.SelectSingleNode("Name").InnerText;
                        actor.Role = node.SelectSingleNode("Role").InnerText;
                        if (Directory.Exists(PluginOptions.Instance.ImagesByName))
                        {
                            string ibnPath = PluginOptions.Instance.ImagesByName;
                            if (!ibnPath.ToLower().Contains("people"))
                                ibnPath = Path.Combine(ibnPath, "People");
                            string actorImage = FindImage("folder", Path.Combine(ibnPath, actor.Name));
                            if (File.Exists(actorImage))
                                actor.ImagePath = actorImage;
                        }
                        movie.Actors.Add(actor);
                    }
                    catch
                    {
                        // fall through i dont care, one less actor
                    }
                }


                foreach (XmlNode node in doc.SelectNodes("Title/Persons/Person"))
                {
                    if (node.SafeGetString("Type") != "Actor")
                    {
                        if (movie.Crew == null)
                            movie.Crew = new List<CrewMember>();
                        movie.Crew.Add(new CrewMember { Name = node.SafeGetString("Name"), Activity = node.SafeGetString("Type") });
                    }
                }


                foreach (XmlNode node in doc.SelectNodes("Title/Genres/Genre"))
                {
                    try
                    {
                        if (movie.Genres == null)
                            movie.Genres = new List<string>();
                        movie.Genres.Add(node.InnerText);
                    }
                    catch
                    {
                        // fall through i dont care, one less genre
                    }
                }


                foreach (XmlNode node in doc.SelectNodes("Title/Studios/Studio"))
                {
                    try
                    {
                        if (movie.Studios == null)
                            movie.Studios = new List<string>();
                        movie.Studios.Add(node.InnerText);
                        //movie.Studios.Add(new Studio { Name = node.InnerText });                        
                    }
                    catch
                    {
                        // fall through i dont care, one less actor
                    }
                }

                foreach (XmlNode node in doc.SelectNodes("Title/Countries/Country"))
                {
                    try
                    {
                        if (movie.Countries == null)
                            movie.Countries = new List<string>();
                        movie.Countries.Add(node.InnerText);                       
                    }
                    catch
                    {
                        // fall through i dont care, one less actor
                    }
                }

                foreach (XmlNode node in doc.SelectNodes("Title/Taglines/Tagline"))
                {
                    try
                    {
                        if (movie.TagLines == null)
                            movie.TagLines = new List<string>();
                        movie.TagLines.Add(node.InnerText);
                        //movie.Studios.Add(new Studio { Name = node.InnerText });                        
                    }
                    catch
                    {
                        // fall through i dont care, one less actor
                    }
                }

                foreach (XmlNode node in doc.SelectNodes("Title/providerId"))
                {
                    if (movie.ProvidersId == null)
                        movie.ProvidersId = new List<DataProviderId>();
                    movie.ProvidersId.Add(new DataProviderId
                    {
                        Name = node.Attributes["name"].InnerText,
                        Id = node.Attributes["id"].InnerText,
                        Url = node.Attributes["url"].InnerText
                    });
                }

                string trailersPath = Path.Combine(location, "Trailers");
                if (Directory.Exists(trailersPath))
                {
                    DirectoryInfo di = new DirectoryInfo(trailersPath);
                    foreach (FileInfo fi in di.GetFiles())
                    {
                        if (FileUtil.IsVideo(fi.Name))
                        {
                            if (movie.TrailerFiles == null) movie.TrailerFiles = new List<string>();
                            movie.TrailerFiles.Add(fi.FullName);
                        }
                    }
                }
            }
        }

        private static void FetchSeries(Item series)
        {
            string mfile = Path.Combine(location, "series.xml");

            if (File.Exists(mfile))
            {
                XmlDocument metadataDoc = new XmlDocument();
                metadataDoc.Load(mfile);

                var seriesNode = metadataDoc.SelectSingleNode("Series");
                if (seriesNode == null)
                {
                    // support for sams metadata scraper 
                    seriesNode = metadataDoc.SelectSingleNode("Item");
                }

                // exit if we have no data. 
                if (seriesNode == null)
                {
                    return;
                }

                string id = seriesNode.SafeGetString("id");
                if (!string.IsNullOrEmpty(id))
                {
                    series.ProvidersId = new List<DataProviderId>();
                    series.ProvidersId.Add(new DataProviderId { Id = id, Name = "TheTVDB", Url = "http://thetvdb.com/?tab=series&id=" + id });
                }

                var p = seriesNode.SafeGetString("banner");
                if (p != null)
                {
                    string bannerFile = Path.Combine(location, Path.GetFileName(p));
                    if (File.Exists(bannerFile))
                        series.PrimaryBanner = new Poster { Image = bannerFile };
                }
                
                series.Overview = seriesNode.SafeGetString("Overview");
                series.SeriesName = series.Title = seriesNode.SafeGetString("SeriesName");

                string actors = seriesNode.SafeGetString("Actors");
                if (actors != null)
                {

                    series.Actors = new List<Actor>();
                    foreach (string n in actors.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(n.Trim()))
                        {
                            Actor actor = new Actor();
                            actor.Name = n;
                            if (Directory.Exists(PluginOptions.Instance.ImagesByName))
                            {
                                string ibnPath = PluginOptions.Instance.ImagesByName;
                                if (!ibnPath.ToLower().Contains("people"))
                                    ibnPath = Path.Combine(ibnPath, "People");
                                string actorImage = FindImage("folder", Path.Combine(ibnPath, actor.Name));
                                if (File.Exists(actorImage))
                                    actor.ImagePath = actorImage;
                            }
                            series.Actors.Add(actor);
                        }
                    }
                }


                string genres = seriesNode.SafeGetString("Genre");
                if (genres != null)
                    series.Genres = new List<string>(genres.Trim('|').Split('|'));

                series.MPAARating = seriesNode.SafeGetString("ContentRating");

                string runtimeString = seriesNode.SafeGetString("Runtime");
                if (!string.IsNullOrEmpty(runtimeString))
                {

                    int runtime;
                    if (int.TryParse(runtimeString.Split(' ')[0], out runtime))
                        series.RunningTime = runtime;
                }


                string ratingString = seriesNode.SafeGetString("Rating");
                if (ratingString != null)
                {
                    float imdbRating;
                    if (float.TryParse(ratingString, NumberStyles.AllowDecimalPoint, new CultureInfo("en-US"), out imdbRating))
                    {
                        series.Rating = imdbRating;
                    }
                }

                string studios = seriesNode.SafeGetString("Network");
                if (studios != null)
                {
                    series.Studios = new List<string>(studios.Split('|'));
                }
                
            }
        }

        private static void FetchEpisode(Item episode)
        {
            var mfile = Path.Combine(location, Path.GetFileNameWithoutExtension(episode.Path) + ".xml");
            if (File.Exists(mfile))
            {
                XmlDocument metadataDoc = new XmlDocument();
                metadataDoc.Load(mfile);

                var p = metadataDoc.SafeGetString("Item/filename");
                if (p != null && p.Length > 0)
                {
                    string image = Path.Combine(episode.MetadataLocation, Path.GetFileName(p));
                    if (File.Exists(image))
                        episode.PrimaryImage = new Poster { Image = image };
                }

                episode.Overview = metadataDoc.SafeGetString("Item/Overview");
                episode.EpisodeNumber = metadataDoc.SafeGetString("Item/EpisodeNumber");
                episode.Title = metadataDoc.SafeGetString("Item/EpisodeName");
                episode.SeasonNumber = metadataDoc.SafeGetString("Item/SeasonNumber");
                episode.Rating = metadataDoc.SafeGetSingle("Item/Rating", (float)-1, 10);

                string writers = metadataDoc.SafeGetString("Item/Writer");
                if (writers != null)
                {
                    episode.Crew = new List<CrewMember>();
                    foreach (string s in writers.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(s.Trim()))
                            episode.Crew.Add(new CrewMember { Name = s.Trim(), Activity = "Writer" });
                    }
                }


                string directors = metadataDoc.SafeGetString("Item/Director");
                if (directors != null)
                {
                    if (episode.Crew == null) episode.Crew = new List<CrewMember>();
                    foreach (string s in directors.Split('|'))
                    {
                        if (!string.IsNullOrEmpty(s.Trim()))
                            episode.Crew.Add(new CrewMember { Name = s.Trim(), Activity = "Director" });
                    }
                }


                var actors = ActorListFromString(metadataDoc.SafeGetString("Item/GuestStars"));
                if (actors != null)
                {
                    if (episode.Actors == null)
                        episode.Actors = new List<Actor>();
                    episode.Actors = actors;
                }
            }
        }

        private static List<Actor> ActorListFromString(string unsplit)
        {

            List<Actor> actors = null;
            if (unsplit != null)
            {
                actors = new List<Actor>();
                foreach (string name in unsplit.Split('|'))
                {
                    if (!string.IsNullOrEmpty(name.Trim()))
                    {
                        Actor actor = new Actor();
                        actor.Name = name.Trim();
                        if (Directory.Exists(PluginOptions.Instance.ImagesByName))
                        {
                            string ibnPath = PluginOptions.Instance.ImagesByName;
                            if (!ibnPath.ToLower().Contains("people"))
                                ibnPath = Path.Combine(ibnPath, "People");
                            string actorImage = FindImage("folder", Path.Combine(ibnPath, actor.Name));
                            if (File.Exists(actorImage))
                                actor.ImagePath = actorImage;
                        }
                        actors.Add(actor);
                    }
                }
            }
            return actors;
        }

        const string Backdrop = "backdrop";
        const string Banner = "banner";
        public static void FetchImages(Item item)
        {
            if (location == null) return;

            if (item.BackdropImagePaths == null)
                item.BackdropImagePaths = new List<Poster>();
            List<string> backdropPaths = FindImages(Backdrop, location);
            backdropPaths.Reverse();
            if (backdropPaths.Count > 0)
            {
                foreach (string b in backdropPaths)
                {
                    item.BackdropImagePaths.Insert(0, new Poster { Image = b, Checked = false });
                }
            }

            if (item.BannersPaths == null)
                item.BannersPaths = new List<Poster>();
            List<string> bannerPaths = FindImages(Banner, location);

            if (bannerPaths.Count > 0)
            {
                foreach (string b in bannerPaths)
                    item.BannersPaths.AddDistinct(new List<Poster> { new Poster { Image = b }});
            }
        }

        static List<string> FindImages(string name, string Location)
        {
            var paths = new List<string>();

            string postfix = "";
            int index = 1;

            do
            {
                string currentImage = FindImage(name + postfix, Location);
                if (currentImage == null) break;
                paths.Add(currentImage);
                postfix = index.ToString();
                index++;

            } while (true);

            return paths;
        }

        public static string FindImage(string name, string Location)
        {
            string file = Path.Combine(Location, name + ".jpg");
            if (File.Exists(file))
                return file;

            file = Path.Combine(Location, name + ".png");
            if (File.Exists(file))
                return file;

            if (name == "folder") // we will also look for images that match by name in the same location for the primary image
            {
                var dir = Path.GetDirectoryName(Location);
                var filename_without_extension = Path.GetFileNameWithoutExtension(Location);

                // dir was null for \\10.0.0.4\dvds - workaround
                if (dir != null && filename_without_extension != null)
                {
                    file = Path.Combine(dir, filename_without_extension);
                    if (File.Exists(file + ".jpg"))
                        return file + ".jpg";
                    if (File.Exists(file + ".png"))
                        return file + ".png";
                }
            }
            return null;
        }
        
    }
}
