using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace MediaBrowserSaver
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{277EA96D-78B0-4440-9D49-8735EB27B601}"); }
        }

        public override string Name
        {
            get { return "MediaBrowser Saver"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Enregistre les backdrops et les infos gérées par MediaBrowser";
                    default: return "Saves metadata that are compatible with MediaBrowser";
                }
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Saver; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.MovieAndSeries; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 0); }
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

        public override bool Write(Item item)
        {
            bool res = true;
            location = item.MetadataLocation;
            switch (item.Type)
            {
                case Entity.Movie: WriteMovie(item);
                    break;
                case Entity.Series: WriteSeries(item);
                    break;
                case Entity.Episode: WriteEpisode(item);
                    break;
                default: break;
            }
            SaveImages(item);
            return res;
        }

        static string location;

        static bool WriteMovie(Item movie)
        {
            if (!Directory.Exists(location))
            {
                Directory.CreateDirectory(location);
                DirectoryInfo dirInfo = new DirectoryInfo(Directory.GetParent(location).FullName);
                dirInfo.Attributes = FileAttributes.Hidden;
            }
            string filename = Path.Combine(location, "mymovies.xml");
            bool res = true;
            if (File.Exists(filename))
                File.Delete(filename);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    xmlWriter.WriteStartElement("Title");
                    xmlWriter.Close();
                }

            }
            catch { res = false; }
            doc.Load(filename);

            XmlNode root = doc.DocumentElement;

            XmlElement title = doc.CreateElement("LocalTitle");
            title.InnerText = movie.Title;
            root.AppendChild(title);

            XmlElement origin = doc.CreateElement("OriginalTitle");
            origin.InnerText = movie.OriginalTitle;
            root.AppendChild(origin);

            XmlElement sort = doc.CreateElement("SortTitle");
            sort.InnerText = movie.SortTitle;
            root.AppendChild(sort);

            XmlElement added = doc.CreateElement("Added");
            added.InnerText = movie.DateAdded.ToString();
            root.AppendChild(added);

            if (movie.Year != null)
            {
                XmlElement year = doc.CreateElement("ProductionYear");
                year.InnerText = movie.Year.ToString();
                root.AppendChild(year);
            }

            if (movie.RunningTime != null)
            {
                XmlElement run = doc.CreateElement("RunningTime");
                run.InnerText = movie.RunningTime.ToString();
                root.AppendChild(run);
            }

            if (movie.Rating != null)
            {
                XmlElement rate = doc.CreateElement("IMDBrating");
                rate.InnerText = movie.Rating.Value.ToString(new CultureInfo("en-US"));
                root.AppendChild(rate);
            }

            XmlElement mpaa = doc.CreateElement("MPAARating");
            mpaa.InnerText = movie.MPAARating;
            root.AppendChild(mpaa);

            XmlElement desc = doc.CreateElement("Description");
            desc.InnerText = movie.Overview;
            root.AppendChild(desc);

            XmlElement mtype = doc.CreateElement("Type");
            mtype.InnerText = movie.Mediatype;
            root.AppendChild(mtype);

            XmlElement ratio = doc.CreateElement("AspectRatio");
            ratio.InnerText = movie.AspectRatio;
            root.AppendChild(ratio);            

            XmlElement pers = doc.CreateElement("Persons");

            if (movie.Crew != null)
            {
                foreach (CrewMember s in movie.Crew)
                {
                    XmlElement dir = doc.CreateElement("Person");
                    XmlElement name = doc.CreateElement("Name");
                    name.InnerText = s.Name;
                    XmlElement type = doc.CreateElement("Type");
                    type.InnerText = s.Activity;
                    dir.AppendChild(name);
                    dir.AppendChild(type);
                    pers.AppendChild(dir);
                }
            }

            if (movie.Actors != null)
            {
                foreach (Actor a in movie.Actors)
                {
                    XmlElement act = doc.CreateElement("Person");
                    XmlElement name = doc.CreateElement("Name");
                    name.InnerText = a.Name;
                    XmlElement role = doc.CreateElement("Role");
                    role.InnerText = a.Role;
                    XmlElement type = doc.CreateElement("Type");
                    type.InnerText = "Actor";
                    act.AppendChild(name);
                    act.AppendChild(type);
                    act.AppendChild(role);
                    pers.AppendChild(act);
                }
            }
            root.AppendChild(pers);

            if (movie.Genres != null)
            {
                XmlElement genres = doc.CreateElement("Genres");
                foreach (string g in movie.Genres)
                {
                    XmlElement genre = doc.CreateElement("Genre");
                    genre.InnerText = g;
                    genres.AppendChild(genre);
                }
                root.AppendChild(genres);
            }

            if (movie.Studios != null)
            {
                XmlElement studios = doc.CreateElement("Studios");
                foreach (string g in movie.Studios)
                {
                    XmlElement studio = doc.CreateElement("Studio");
                    studio.InnerText = g;
                    studios.AppendChild(studio);
                }
                root.AppendChild(studios);
            }

            if (movie.Countries != null)
            {
                XmlElement countries = doc.CreateElement("Countries");
                foreach (string c in movie.Countries)
                {
                    XmlElement country = doc.CreateElement("Country");
                    country.InnerText = c;
                    countries.AppendChild(country);
                }
                root.AppendChild(countries);
            }

            if (movie.TagLines != null)
            {
                XmlElement tags = doc.CreateElement("Taglines");
                foreach (string t in movie.TagLines)
                {
                    XmlElement tag = doc.CreateElement("Tagline");
                    tag.InnerText = t;
                    tags.AppendChild(tag);
                }
                root.AppendChild(tags);
            }

            try
            {
                doc.Save(filename);
                if (PluginOptions.Instance.SaveXmlHidden)
                    File.SetAttributes(filename, FileAttributes.Hidden);
            }
            catch { res = false; }

            if (movie.TrailerFiles.IsNonEmpty())
            {
                foreach (string s in movie.TrailerFiles)
                {
                    if (File.Exists(s))
                    {
                        string trailersPath = Path.Combine(location, "Trailers");
                        if (!Directory.Exists(trailersPath))
                            Directory.CreateDirectory(trailersPath);
                        string dest = Path.Combine(trailersPath, Path.GetFileName(s));
                        if (!Path.Equals(s, dest))
                        {
                            try { File.Copy(s, dest); }
                            catch { continue; }
                            if (PluginOptions.Instance.DeleteTrailer)
                            {
                                try { File.Delete(s); }
                                catch (Exception ex) { Logger.ReportError("Unable to delete file " + s, ex); }
                            }
                        }
                    }

                }
            }

            return res;
        }

        static void WriteSeries(Item series)
        {
            string filename = Path.Combine(series.Path, "series.xml");

            if (File.Exists(filename))
                File.Delete(filename);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartElement("Series");
                    xmlWriter.Close();
                }

            }
            catch { }
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;

            if (series.ProvidersId != null)
            {
                DataProviderId dp = series.ProvidersId.Find(p => p.Name == "thetvdb");
                if (dp != null)
                {
                    XmlElement id = doc.CreateElement("id");
                    id.InnerText = dp.Id;
                    root.AppendChild(id);
                }
            }

            XmlElement ban = doc.CreateElement("banner");
            if (!string.IsNullOrEmpty(ImageUtil.GetLocalImagePath(series.PrimaryBanner.Image))) ban.InnerText = "banner.jpg";
            root.AppendChild(ban);
            

            XmlElement ov = doc.CreateElement("Overview");
            if (series.Overview != null) ov.InnerText = series.Overview;
            root.AppendChild(ov);

            XmlElement name = doc.CreateElement("SeriesName");
            if (series.SeriesName != null) name.InnerText = series.SeriesName;
            root.AppendChild(name);

            string actors = "";
            if (series.Actors.IsNonEmpty())
            {
                actors = "|";
                foreach (Actor a in series.Actors)
                    actors += a.Name + "|";
            }
            XmlElement act = doc.CreateElement("Actors");
            act.InnerText = actors;
            root.AppendChild(act);

            string genres = "";
            if (series.Genres.IsNonEmpty())
            {
                genres = "|";
                foreach (string g in series.Genres)
                    genres += g + "|";
            }
            XmlElement gen = doc.CreateElement("Genre");
            gen.InnerText = genres;
            root.AppendChild(gen);

            XmlElement mpaa = doc.CreateElement("ContentRating");
            mpaa.InnerText = series.MPAARating;
            root.AppendChild(mpaa);

            XmlElement run = doc.CreateElement("Runtime");
            if (series.RunningTime.IsValidRunningTime()) run.InnerText = series.RunningTime.ToString();
            root.AppendChild(run);

            XmlElement rate = doc.CreateElement("Rating");
            if (series.Rating.IsValidRating()) rate.InnerText = ((float)series.Rating).ToString(new CultureInfo("en-US"));
            root.AppendChild(rate);

            string studios = "";
            if (series.Studios.IsNonEmpty())
            {
                studios = "|";
                foreach (string s in series.Studios)
                    studios += s + "|";
            }
            XmlElement stu = doc.CreateElement("Network");
            stu.InnerText = studios;
            root.AppendChild(stu);

            try
            {
                doc.Save(filename);
                if (PluginOptions.Instance.SaveXmlHidden)
                    File.SetAttributes(filename, FileAttributes.Hidden);
            }
            catch { }
        }
       

        static void WriteEpisode(Item episode)
        {
            if (!Directory.Exists(episode.MetadataLocation))
            {
                Directory.CreateDirectory(episode.MetadataLocation);
                DirectoryInfo dirInfo = new DirectoryInfo(episode.MetadataLocation);
                dirInfo.Attributes = FileAttributes.Hidden;
            }
            string filename = Path.Combine(episode.MetadataLocation, Path.GetFileNameWithoutExtension(episode.Path) + ".xml");

            if (File.Exists(filename))
                File.Delete(filename);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteStartElement("Item");
                    xmlWriter.Close();
                }

            }
            catch { }
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;

            XmlElement fi = doc.CreateElement("filename");
            if (episode.PrimaryImage != null) fi.InnerText = Path.GetFileName(episode.PrimaryImage.Image);
            root.AppendChild(fi);

            XmlElement ov = doc.CreateElement("Overview");
            if (episode.Overview != null) ov.InnerText = episode.Overview;
            root.AppendChild(ov);

            XmlElement epn = doc.CreateElement("EpisodeNumber");
            if (episode.EpisodeNumber != null) epn.InnerText = episode.EpisodeNumber;
            root.AppendChild(epn);

            XmlElement name = doc.CreateElement("EpisodeName");
            if (episode.Title != null)
            {
                string n = episode.Title;
                if (n.Contains("-"))
                    n = n.Substring(n.IndexOf("-") + 1).Trim();
                name.InnerText = n;
            }
            root.AppendChild(name);

            XmlElement sea = doc.CreateElement("SeasonNumber");
            if (episode.SeasonNumber != null) sea.InnerText = episode.SeasonNumber;
            root.AppendChild(sea);

            XmlElement rate = doc.CreateElement("Rating");
            if (episode.Rating != null) rate.InnerText = ((float)episode.Rating).ToString(new CultureInfo("en-US"));
            root.AppendChild(rate);

            string actors = "";
            if (episode.Actors != null)
            {
                actors = "|";
                foreach (Actor a in episode.Actors)
                    actors += a.Name + "|";
            }
            XmlElement act = doc.CreateElement("GuestStars");
            act.InnerText = actors;
            root.AppendChild(act);

            string directors = "";
            if (episode.Crew != null && episode.Crew.Exists(c => Helper.IsDirectorName(c.Activity)))
            {
                directors = "|";
                foreach (CrewMember d in episode.Crew.FindAll(c => Helper.IsDirectorName(c.Activity)))
                    directors += d.Name + "|";
            }
            XmlElement dir = doc.CreateElement("Directors");
            dir.InnerText = directors;
            root.AppendChild(dir);

            string writers = "";
            if (episode.Crew != null && episode.Crew.Exists(c => Helper.IsWriterName(c.Activity)))
            {
                writers = "|";
                foreach (CrewMember w in episode.Crew.FindAll(c => Helper.IsWriterName(c.Activity)))
                    writers += w.Name + "|";
            }
            XmlElement wri = doc.CreateElement("Writer");
            wri.InnerText = writers;
            root.AppendChild(wri);

            try
            {
                doc.Save(filename);
                if (PluginOptions.Instance.SaveXmlHidden)
                    File.SetAttributes(filename, FileAttributes.Hidden);
            }
            catch { }
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
                        actors.Add(new Actor { Name = name.Trim() });
                }
            }
            return actors;
        }

        const string Banner = "banner";
        const string Backdrop = "backdrop";

        public static bool SaveImages(Item item)
        {
            bool res = true;
            try
            {
                //Banner
                if (item.BannersPaths.IsNonEmpty())
                {
                    string localBanner = ImageUtil.GetLocalImagePath(ImageUtil.GetLocalImagePath(item.PrimaryBanner.Image));
                    if (!string.IsNullOrEmpty(localBanner) && localBanner.ToLower() != Path.Combine(item.MetadataLocation, "banner.jpg").ToLower())
                    {

                        try { File.Delete(FindImage(Banner, item.MetadataLocation)); }
                        catch { }
                        try
                        {
                            File.Copy(localBanner, Path.Combine(item.MetadataLocation, "banner.jpg"), true);
                            if (PluginOptions.Instance.SaveBdHidden)
                                File.SetAttributes(Path.Combine(item.MetadataLocation, "banner.jpg"), FileAttributes.Hidden);
                        }
                        catch { res = false; }
                    }
                    else if (string.IsNullOrEmpty(localBanner))
                    {
                        try { File.Delete(FindImage(Banner, item.MetadataLocation)); }
                        catch { }
                    }
                }

                //Backdrop
                if (PluginOptions.Instance.UseBackdrop)
                {
                    List<string> OldBd = FindImages(Backdrop, item.MetadataLocation);
                    if (!item.BackdropImagePaths.IsNonEmpty())
                    {
                        foreach (string s in OldBd)
                        {
                            try { File.Delete(s); }
                            catch { }
                        }
                    }
                    else
                    {
                        if (OldBd.Count > item.BackdropImagePaths.Count)
                        {
                            for (int i = item.BackdropImagePaths.Count; i < OldBd.Count; i++)
                            {
                                try { File.Delete(OldBd[i]); }
                                catch { }
                            }
                        }
                        int index = 1;
                        string postfix = "";
                        foreach (Poster s in item.BackdropImagePaths.FindAll(p => p.Checked))
                        {
                            string localBd = ImageUtil.GetLocalImagePath(s.Image);
                            if (!string.IsNullOrEmpty(localBd) && localBd.ToLower() != Path.Combine(item.MetadataLocation, "backdrop" + postfix + ".jpg").ToLower())
                            {
                                try { File.Delete(Path.Combine(item.MetadataLocation, "backdrop" + postfix + ".jpg")); }
                                catch { res = false; }
                                try
                                {
                                    File.Copy(localBd, Path.Combine(item.MetadataLocation, "backdrop" + postfix + ".jpg"), true);
                                    if (PluginOptions.Instance.SaveBdHidden)
                                        File.SetAttributes(Path.Combine(item.MetadataLocation, "backdrop" + postfix + ".jpg"), FileAttributes.Hidden);
                                }
                                catch { }
                            }
                            postfix = index.ToString();
                            index++;
                        }
                    }
                }

                //Actors Images
                if (PluginOptions.Instance.UseCasting && item.Actors.IsNonEmpty() && Directory.Exists(PluginOptions.Instance.ImagesByName))
                {
                    string ibnPath = PluginOptions.Instance.ImagesByName;
                    if (!ibnPath.ToLower().Contains("people"))
                    {
                        ibnPath = Path.Combine(ibnPath, "People");
                        if (!Directory.Exists(ibnPath))
                            Directory.CreateDirectory(ibnPath);
                    }
                    foreach (Actor actor in item.Actors)
                    {
                        if (!string.IsNullOrEmpty(actor.ImagePath))
                        {
                            string actorImgPath = Path.Combine(ibnPath, actor.Name);
                            string existingImg = FindImage("folder", actorImgPath);
                            if (string.IsNullOrEmpty(existingImg) || existingImg.ToLower() != actor.ImagePath.ToLower())
                            {
                                string localImg = ImageUtil.GetLocalImagePath(actor.ImagePath);
                                if (!string.IsNullOrEmpty(localImg))
                                {
                                    if (!Directory.Exists(actorImgPath))
                                        Directory.CreateDirectory(actorImgPath);
                                    if (File.Exists(existingImg))
                                        File.Delete(existingImg);
                                    File.Copy(localImg, Path.Combine(actorImgPath, "folder.jpg"));
                                }
                            }
                        }
                    }
                }
            }
            catch { res = false; }
            return res;
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

        static string FindImage(string name, string Location)
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
