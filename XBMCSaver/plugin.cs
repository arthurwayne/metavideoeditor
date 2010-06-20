using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace XBMCSaver
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{6DA3CE1B-FF70-46c3-B856-E8E957B69FC5}"); }
        }

        public override string Name
        {
            get { return "XBMC Saver"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Enregistre les infos gérées par XBMC";
                    default: return "Saves metadata that are compatible with XBMC";
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

        public override bool Write(Item item)
        {
            bool res = true;
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
            else return false;
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
        static string videofilename;

        static bool WriteMovie(Item movie)
        {
            string filename = Path.Combine(location, videofilename + ".nfo");
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
                    xmlWriter.WriteStartElement("movie");
                    xmlWriter.Close();
                }

            }
            catch { res = false; }
            doc.Load(filename);

            XmlNode root = doc.DocumentElement;

            XmlElement title = doc.CreateElement("title");
            title.InnerText = movie.Title;
            root.AppendChild(title);

            XmlElement origin = doc.CreateElement("originaltitle");
            origin.InnerText = movie.OriginalTitle;
            root.AppendChild(origin);

            XmlElement sort = doc.CreateElement("sorttitle");
            sort.InnerText = movie.SortTitle;
            root.AppendChild(sort);

            XmlElement added = doc.CreateElement("added");
            added.InnerText = movie.DateAdded.ToString();
            root.AppendChild(added);

            if (movie.Year != null)
            {
                XmlElement year = doc.CreateElement("year");
                year.InnerText = movie.Year.ToString();
                root.AppendChild(year);
            }

            if (movie.RunningTime != null)
            {
                XmlElement run = doc.CreateElement("runtime");
                run.InnerText = movie.RunningTime.ToString();
                root.AppendChild(run);
            }

            if (movie.Rating != null)
            {
                XmlElement rate = doc.CreateElement("rating");
                rate.InnerText = movie.Rating.Value.ToString(new CultureInfo("en-US"));
                root.AppendChild(rate);
            }

            XmlElement mpaa = doc.CreateElement("mpaa");
            mpaa.InnerText = movie.MPAARating;
            root.AppendChild(mpaa);

            XmlElement desc = doc.CreateElement("outline");
            desc.InnerText = movie.Overview;
            root.AppendChild(desc);

            XmlElement plot = doc.CreateElement("plot");
            plot.InnerText = movie.Overview;
            root.AppendChild(plot);

            XmlElement mtype = doc.CreateElement("type");
            mtype.InnerText = movie.Mediatype;
            root.AppendChild(mtype);

            XmlElement ratio = doc.CreateElement("aspectratio");
            ratio.InnerText = movie.AspectRatio;
            root.AppendChild(ratio);

            if (movie.Crew != null)
            {
                foreach (CrewMember s in movie.Crew)
                {
                    if (Helper.IsDirectorName(s.Activity))
                    {
                        XmlElement dir = doc.CreateElement("director");
                        dir.InnerText = s.Name;
                        root.AppendChild(dir);
                    }                    
                }
            }

            if (movie.Actors != null)
            {
                foreach (Actor a in movie.Actors)
                {
                    XmlElement act = doc.CreateElement("actor");
                    XmlElement name = doc.CreateElement("name");
                    name.InnerText = a.Name;
                    XmlElement role = doc.CreateElement("role");
                    role.InnerText = a.Role;
                    
                    act.AppendChild(name);
                    act.AppendChild(role);
                    if (!string.IsNullOrEmpty(a.ImagePath))
                    {
                        XmlElement type = doc.CreateElement("thumb");
                        type.InnerText = a.ImagePath;
                        act.AppendChild(type);
                    }
                    root.AppendChild(act);
                }
            }

            if (movie.Genres != null)
            {
                foreach (string g in movie.Genres)
                {
                    XmlElement genre = doc.CreateElement("genre");
                    genre.InnerText = g;
                    root.AppendChild(genre);
                }
            }

            if (movie.Studios != null)
            {
                foreach (string g in movie.Studios)
                {
                    XmlElement studio = doc.CreateElement("studio");
                    studio.InnerText = g;
                    root.AppendChild(studio);
                }
            }

            if (movie.Countries != null)
            {
                foreach (string c in movie.Countries)
                {
                    XmlElement country = doc.CreateElement("country");
                    country.InnerText = c;
                    root.AppendChild(country);
                }
            }

            if (movie.TagLines != null)
            {
                foreach (string t in movie.TagLines)
                {
                    XmlElement tag = doc.CreateElement("tagline");
                    tag.InnerText = t;
                    root.AppendChild(tag);
                }
            }

            if (movie.TrailerFiles != null)
            {
                foreach (string t in movie.TrailerFiles)
                {
                    XmlElement trailer = doc.CreateElement("trailer");
                    trailer.InnerText = t;
                    root.AppendChild(trailer);
                }
            }

            if (movie.ImagesPaths.IsNonEmpty())
            {                
                foreach (Poster p in movie.ImagesPaths )
                {
                    XmlElement poster = doc.CreateElement("thumb");
                    poster.InnerText = p.Image;
                    root.AppendChild(poster);
                }
            }

            if (movie.BannersPaths.IsNonEmpty())
            {
                foreach (Poster p in movie.BannersPaths)
                {
                    XmlElement poster = doc.CreateElement("thumb");
                    poster.InnerText = p.Image;
                    root.AppendChild(poster);
                }
            }

            if (movie.BackdropImagePaths.IsNonEmpty())
            {
                int index = 0;
                XmlElement fanart = doc.CreateElement("fanart");
                foreach (Poster p in movie.BackdropImagePaths)
                {
                    XmlElement poster = doc.CreateElement("thumb");
                    poster.InnerText = p.Image;
                    fanart.AppendChild(poster);
                    index++;
                    if (index < Config.Instance.MaxBdSaved) break;
                }
                root.AppendChild(fanart);
            }

            try
            {
                doc.Save(filename);
            }
            catch { res = false; }

            return res;
        }

        static void WriteSeries(Item series)
        {
            string filename = Path.Combine(series.Path, "tvshow.nfo");

            if (File.Exists(filename))
                File.Delete(filename);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    xmlWriter.WriteStartElement("tvshow");
                    xmlWriter.Close();
                }

            }
            catch { }
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;

            XmlElement title = doc.CreateElement("title");
            if (series.SeriesName != null) title.InnerText = series.SeriesName;
            root.AppendChild(title);

            XmlElement rate = doc.CreateElement("rating");
            if (series.Rating.IsValidRating()) rate.InnerText = ((float)series.Rating).ToString(new CultureInfo("en-US"));
            root.AppendChild(rate);

            XmlElement season = doc.CreateElement("season");
            season.InnerText = "-1";
            root.AppendChild(season);

            XmlElement displayseason = doc.CreateElement("displayseason");
            displayseason.InnerText = "-1";
            root.AppendChild(displayseason);

            XmlElement displayepisode = doc.CreateElement("displayepisode");
            displayepisode.InnerText = "-1";
            root.AppendChild(displayepisode);

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

            XmlElement ov = doc.CreateElement("plot");
            if (series.Overview != null) ov.InnerText = series.Overview;
            root.AppendChild(ov);

            if (series.Actors != null)
            {
                foreach (Actor a in series.Actors)
                {
                    XmlElement act = doc.CreateElement("actor");
                    XmlElement name = doc.CreateElement("name");
                    name.InnerText = a.Name;
                    XmlElement role = doc.CreateElement("role");
                    role.InnerText = a.Role;

                    act.AppendChild(name);
                    act.AppendChild(role);
                    if (!string.IsNullOrEmpty(a.ImagePath))
                    {
                        XmlElement type = doc.CreateElement("thumb");
                        type.InnerText = a.ImagePath;
                        act.AppendChild(type);
                    }
                    root.AppendChild(act);
                }
            }

            if (series.Genres != null)
            {
                foreach (string g in series.Genres)
                {
                    XmlElement genre = doc.CreateElement("genre");
                    genre.InnerText = g;
                    root.AppendChild(genre);
                }
            }

            XmlElement mpaa = doc.CreateElement("mpaa");
            mpaa.InnerText = series.MPAARating;
            root.AppendChild(mpaa);

            XmlElement run = doc.CreateElement("runtime");
            if (series.RunningTime.IsValidRunningTime()) run.InnerText = series.RunningTime.ToString();
            root.AppendChild(run);

            if (series.Studios != null)
            {
                foreach (string g in series.Studios)
                {
                    XmlElement studio = doc.CreateElement("studio");
                    studio.InnerText = g;
                    root.AppendChild(studio);
                }
            }

            if (series.TagLines != null)
            {
                foreach (string t in series.TagLines)
                {
                    XmlElement tag = doc.CreateElement("tagline");
                    tag.InnerText = t;
                    root.AppendChild(tag);
                }
            }

            if (series.ImagesPaths.IsNonEmpty())
            {
                foreach (Poster p in series.ImagesPaths)
                {
                    XmlElement poster = doc.CreateElement("thumb");
                    poster.InnerText = p.Image;
                    root.AppendChild(poster);
                }
            }

            if (series.BannersPaths.IsNonEmpty())
            {
                foreach (Poster p in series.BannersPaths)
                {
                    XmlElement poster = doc.CreateElement("thumb");
                    poster.InnerText = p.Image;
                    root.AppendChild(poster);
                }
            }

            try
            {
                doc.Save(filename);
            }
            catch { }
        }


        static void WriteEpisode(Item episode)
        {
            string filename = Path.Combine(location, videofilename + ".nfo");

            if (File.Exists(filename))
                File.Delete(filename);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(filename, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8'");
                    xmlWriter.WriteStartElement("episodedetails");
                    xmlWriter.Close();
                }

            }
            catch { }
            doc.Load(filename);
            XmlNode root = doc.DocumentElement;

            XmlElement ov = doc.CreateElement("plot");
            if (episode.Overview != null) ov.InnerText = episode.Overview;
            root.AppendChild(ov);

            XmlElement epn = doc.CreateElement("episode");
            if (episode.EpisodeNumber != null) epn.InnerText = episode.EpisodeNumber;
            root.AppendChild(epn);

            XmlElement title = doc.CreateElement("title");
            if (episode.Title != null) title.InnerText = episode.Title;            
            root.AppendChild(title);

            XmlElement sea = doc.CreateElement("season");
            if (episode.SeasonNumber != null) sea.InnerText = episode.SeasonNumber;
            root.AppendChild(sea);

            XmlElement rate = doc.CreateElement("rating");
            if (episode.Rating != null) rate.InnerText = ((float)episode.Rating).ToString(new CultureInfo("en-US"));
            root.AppendChild(rate);

            if (episode.Actors != null)
            {
                foreach (Actor a in episode.Actors)
                {
                    XmlElement act = doc.CreateElement("actor");
                    XmlElement name = doc.CreateElement("name");
                    name.InnerText = a.Name;
                    XmlElement role = doc.CreateElement("role");
                    role.InnerText = a.Role;

                    act.AppendChild(name);
                    act.AppendChild(role);
                    if (!string.IsNullOrEmpty(a.ImagePath))
                    {
                        XmlElement type = doc.CreateElement("thumb");
                        type.InnerText = a.ImagePath;
                        act.AppendChild(type);
                    }
                    root.AppendChild(act);
                }
            }            

            if (episode.Crew != null && episode.Crew.Exists(c => Helper.IsDirectorName(c.Activity)))
            {
                foreach (CrewMember d in episode.Crew.FindAll(c => Helper.IsDirectorName(c.Activity)))
                {
                    XmlElement dir = doc.CreateElement("director");
                    dir.InnerText = d.Name;
                    root.AppendChild(dir);
                }
            }            

            if (episode.Crew != null && episode.Crew.Exists(c => Helper.IsWriterName(c.Activity)))
            {
                foreach (CrewMember w in episode.Crew.FindAll(c => Helper.IsWriterName(c.Activity)))
                {
                    XmlElement wri = doc.CreateElement("credits");
                    wri.InnerText = w.Name;
                    root.AppendChild(wri);
                }
            }            

            try
            {
                doc.Save(filename);
            }
            catch { }
        }



        public static bool SaveImages(Item item)
        {
            bool res = true;
            try
            {
                //Poster
                if (PluginOptions.Instance.UsePoster && item.ImagesPaths.IsNonEmpty())
                {
                    string localPoster = ImageUtil.GetLocalImagePath(item.PrimaryImage.Image);
                    string dest = Path.Combine(location, videofilename + ".tbn");

                    if (!string.IsNullOrEmpty(localPoster) && localPoster.ToLower() != dest.ToLower())
                    {                        
                        if (File.Exists(dest))
                        {
                            try { File.Delete(dest); }
                            catch { }
                        }
                        try { File.Copy(localPoster, dest, true); }
                        catch { }
                    }
                }

                //Backdrop
                if (PluginOptions.Instance.UseBackdrop && item.BackdropImagePaths.IsNonEmpty())
                {
                    string localPoster = ImageUtil.GetLocalImagePath(item.PrimaryBackdrop.Image);
                    string dest = Path.Combine(location, videofilename + "-fanart.jpg");

                    if (!string.IsNullOrEmpty(localPoster) && localPoster.ToLower() != dest.ToLower())
                    {                        
                        if (File.Exists(dest))
                        {
                            try { File.Delete(dest); }
                            catch { }
                        }
                        try { File.Copy(localPoster, dest, true); }
                        catch { }
                    }
                }

                //Banner
                if (PluginOptions.Instance.UseBanner && item.BannersPaths.IsNonEmpty())
                {
                    string localPoster = ImageUtil.GetLocalImagePath(item.PrimaryBanner.Image);
                    string dest = Path.Combine(location, videofilename + ".tbn");

                    if (!string.IsNullOrEmpty(localPoster) && localPoster.ToLower() != dest.ToLower())
                    {                        
                        if (File.Exists(dest))
                        {
                            try { File.Delete(dest); }
                            catch { }
                        }
                        try { File.Copy(localPoster, dest, true); }
                        catch { }
                    }
                }

                //Actors Images
                if (PluginOptions.Instance.UseCasting && item.Actors.IsNonEmpty() && Directory.Exists(PluginOptions.Instance.ActorsThumbPath))
                {                    
                    foreach (Actor actor in item.Actors)
                    {
                        if (!string.IsNullOrEmpty(actor.ImagePath))
                        {
                            string actorImgPath = Path.Combine(PluginOptions.Instance.ActorsThumbPath, actor.Name + ".tbn");                            
                            if (!Path.Equals(actorImgPath, actor.ImagePath))
                            {
                                string localImg = ImageUtil.GetLocalImagePath(actor.ImagePath);
                                if (!string.IsNullOrEmpty(localImg))
                                {
                                    if (File.Exists(actorImgPath))
                                        File.Delete(actorImgPath);
                                    File.Copy(localImg, actorImgPath);
                                }
                            }
                        }
                    }
                }
            }
            catch { res = false; }
            return res;
        }

      
    }
}
