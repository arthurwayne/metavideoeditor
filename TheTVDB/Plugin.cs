using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using mveEngine;

namespace TheTVDB
{
    public class TheTVDB : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{9A9B0D48-D0D5-499a-BCDA-F6DA40979653}"); }
        }

        public override string Name
        {
            get { return "TheTVDB"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les données de la base thetvdb.com";
                    default: return "Fetches TV show metadata from thetvdb.com";
                } 
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Provider; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.Series; }
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

        public static readonly string TVDBApiKey = "B89CE93890E9419B";
        public static readonly string BannerUrl = "http://www.thetvdb.com/banners/";
        private static readonly string rootUrl = "http://www.thetvdb.com/api/";
        //private static readonly string seriesQuery = "GetSeries.php?language=all&seriesname={0}";
        private static readonly string seriesQuery = "GetSeries.php?seriesname={0}";
        private static readonly string seriesLoad = "http://www.thetvdb.com/api/{0}/series/{1}/all/{2}.zip";


        private static string XmlPath
        {
            get
            {
                string path = Path.Combine(ApplicationPaths.AppDataPath, "TheTvDbXml");
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return path;
            }
        }

        static string[,] langList =
        {
                    { "Dansk", "da"},
                    { "Suomeksi", "fi"},
                    { "Nederlands", "nl"},
                    { "Deutsch", "de"},
                    { "Italiano", "it"},
                    { "Español", "es"},
                    { "Français", "fr"},
                    { "Polski", "pl"},
                    { "Magyar", "hu"},
                    { "Ελληνικά", "el"},
                    { "Türkçe", "tr"},
                    { "русский язык", "ru"},
                    { "עברית", "he"},
                    { "日本語", "ja"},
                    { "Português", "pt"},
                    { "中文", "zh"},
                    { "čeština", "cs"},
                    { "Slovenski", "sl"},
                    { "Hrvatski", "hr"},
                    { "한국어", "ko"},
                    { "English", "en"},
                    { "Svenska", "sv"},
                    { "Norsk", "no"}
        };

        public const string langString = "Dansk,Suomeksi,Nederlands,Deutsch,Italiano,Español,Français,Polski,Magyar,Ελληνικά,Türkçe,русский язык,עברית,日本語,Português,中文,čeština,Slovenski,Hrvatski,한국어,English,Svenska,Norsk";

        public static string DefaultLang
        {
            get
            {
                for (int i = 0; i <= langList.GetUpperBound(0); i++)
                {
                    if (langList[i, 1] == System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                        return langList[i, 0];
                }
                return "English";
            }
        }

        private static string lang
        {
            get
            {
                for (int i = 0; i <= langList.GetUpperBound(0); i++)
                {
                    if (langList[i, 0] == PluginOptions.Instance.Language)
                        return langList[i, 1];
                }
                return "en";
            }
        }

        public override List<Item> FindPossible(Item item)
        {            
            if (item.Type != Entity.Series) return null;
            string url = string.Format(rootUrl + seriesQuery, HtmlUtil.UrlEncode(item.Title));
            XmlDocument doc = Helper.Fetch(url);
            if (doc == null) return null;
            List<Item> series = null;
            XmlNodeList nodes = doc.SelectNodes("//Series");
            foreach (XmlNode node in nodes)
            {
                Item s = new Item();
                s.Type = Entity.Series;
                s.Title = node.SafeGetString("./SeriesName");
                s.ProvidersId = new List<DataProviderId>();
                string id = node.SafeGetString("./seriesid");
                s.Overview = node.SafeGetString("./Overview");
                if (!string.IsNullOrEmpty(id))
                    s.ProvidersId.Add(new DataProviderId { Name = this.Name, Id = id, Url = "http://thetvdb.com/?tab=series&id=" + id });
                else
                    continue;
                if (series == null) series = new List<Item>();
                series.Add(s);
            }
            return series;
        }

        public override Item Fetch(Item item)
        {
            switch (item.Type)
            {
                case Entity.Series:
                    return GetSeriesDetails(item);
                case Entity.Season:
                    return GetSeasonDetails(item);
                case Entity.Episode:
                    return GetEpisodeDetails(item);
                default: 
                    return null;
            }
        }

        public override Item AutoFind(Item item)
        {
            if (item.ProvidersId != null)
            {
                DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name);
                if (dp != null)
                    return Fetch(item);
            }
            List<Item> Possible = FindPossible(item);
            if (Possible == null) return null;
            string comparableName = Helper.GetComparableName(item.Title);
            foreach (Item s in Possible)
            {
                if (Helper.GetComparableName(s.Title) == comparableName)
                {
                    return Fetch(s);
                }
            }
            return null;
        }        

        private static bool LoadSeriesDetails(string seriesId)
        {
            string path = Path.Combine(XmlPath, seriesId);
            if (Directory.Exists(path))
                return true;
            Directory.CreateDirectory(path);
            string tmp = Path.Combine(path, lang + ".zip");
            if (HtmlUtil.Download(tmp, string.Format(seriesLoad, TVDBApiKey, seriesId, lang)) && Helper.UnZip(tmp, path))
            {
                try { File.Delete(tmp); }
                catch { }
                return true;
            }
            return false;
        }

        public Item GetSeriesDetails(Item item)
        {
            DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name);
            if (dp == null) return null;
            if (!LoadSeriesDetails(dp.Id))
                return null;
            Item series = new Item();
            series.ProvidersId = new List<DataProviderId> { dp };

            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(Path.Combine(XmlPath, dp.Id), lang + ".xml"));
            if (doc == null) return null;

            series.Title = series.SeriesName = doc.SafeGetString("//SeriesName");
            series.Overview = doc.SafeGetString("//Overview");
            series.Rating = doc.SafeGetSingle("//Rating", 0, 10);
            series.RunningTime = doc.SafeGetInt32("//Runtime");
            series.MPAARating = doc.SafeGetString("//ContentRating");

            string g = doc.SafeGetString("//Genre");
            if (g != null)
            {
                string[] genres = g.Trim('|').Split('|');
                if (g.Length > 0)
                {
                    series.Genres = new List<string>();
                    series.Genres.AddRange(genres);
                }
            }

            XmlDocument actorsDoc = new XmlDocument();
            actorsDoc.Load(Path.Combine(Path.Combine(XmlPath, dp.Id), "actors.xml"));
            if (actorsDoc != null)
            {
                series.Actors = new List<Actor>();
                foreach (XmlNode actorNode in actorsDoc.SelectNodes("//Actor"))
                {
                    string name = actorNode.SafeGetString("Name");
                    if (!string.IsNullOrEmpty(name))
                    {
                        Actor actor = new Actor();
                        actor.Name = name;
                        actor.Role = actorNode.SafeGetString("Role");
                        actor.ImagePath = BannerUrl + actorNode.SafeGetString("Image");
                        if (series.Actors == null) series.Actors = new List<Actor>();
                        series.Actors.Add(actor);
                    }
                }
            }
            else
            {
                string actors = doc.SafeGetString("//Actors");
                if (actors != null)
                {
                    string[] a = actors.Trim('|').Split('|');
                    if (a.Length > 0)
                    {
                        series.Actors = new List<Actor>();
                        series.Actors.AddRange(
                            a.Select(actor => new Actor { Name = actor }));
                    }
                }
            }

            XmlDocument banners = new XmlDocument();
            banners.Load(Path.Combine(Path.Combine(XmlPath, dp.Id), "banners.xml"));
            if (banners != null)
            {
                series.BackdropImagePaths = new List<Poster>();
                series.ImagesPaths = new List<Poster>();
                series.BannersPaths = new List<Poster>();
                foreach (XmlNode node in banners.SelectNodes("//Banner"))
                {
                    if (node.SafeGetString("BannerType") == "poster")
                    {
                        Poster poster = new Poster();
                        string path = node.SafeGetString("BannerPath");
                        string thumb = node.SafeGetString("ThumbnailPath");
                        if (!string.IsNullOrEmpty(thumb))
                            poster.Thumb = BannerUrl + thumb;
                        poster.Image = BannerUrl + path;

                        if (!string.IsNullOrEmpty(path))
                            series.ImagesPaths.Add(poster);
                    }
                    else if (node.SafeGetString("BannerType") == "fanart")
                    {
                        Poster poster = new Poster();
                        poster.Checked = false;
                        string path = node.SafeGetString("BannerPath");
                        string thumb = node.SafeGetString("ThumbnailPath");
                        string size = node.SafeGetString("BannerType2");
                        if (size.Contains("x"))
                        {
                            poster.width = size.Substring(0, size.IndexOf("x"));
                            poster.height = size.Substring(size.IndexOf("x") + 1);
                        }
                        if (!string.IsNullOrEmpty(thumb))
                            poster.Thumb = BannerUrl + thumb;
                        poster.Image = BannerUrl + path;
                        if (!string.IsNullOrEmpty(path))
                            series.BackdropImagePaths.Add(poster);
                    }
                    else if (node.SafeGetString("BannerType") == "series")
                    {
                        Poster poster = new Poster();
                        string path = node.SafeGetString("BannerPath");
                        poster.Image = BannerUrl + path;
                        if (!string.IsNullOrEmpty(path))
                            series.BannersPaths.Add(poster);
                    }
                }

            }

            return series;
        }


        public Item GetSeasonDetails(Item item)
        {
            DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name);
            if (dp == null) return null;
            if (!LoadSeriesDetails(dp.Id))
                return null;
            Item season = new Item();
            season.ProvidersId = new List<DataProviderId> { dp };

            if (string.IsNullOrEmpty(item.SeasonNumber)) return null;
            season.SeasonNumber = item.SeasonNumber;
            season.Title = "Saison " + season.SeasonNumber;
            season.SeriesName = item.SeriesName;

            XmlDocument banners = new XmlDocument();
            banners.Load(Path.Combine(Path.Combine(XmlPath, dp.Id), "banners.xml"));
            if (banners == null) return null;

            season.ImagesPaths = new List<Poster>();
            season.BannersPaths = new List<Poster>();
            foreach (XmlNode node in banners.SelectNodes("//Banner[BannerType='season'][Season='" + season.SeasonNumber + "']"))
            {
                if (node.SafeGetString("BannerType2") == "season")
                {
                    Poster poster = new Poster();
                    string path = node.SafeGetString("BannerPath");
                    poster.Image = BannerUrl + path;
                    if (!string.IsNullOrEmpty(path))
                        season.ImagesPaths.Add(poster);
                }
                else if (node.SafeGetString("BannerType2") == "seasonwide")
                {
                    Poster poster = new Poster();
                    string path = node.SafeGetString("BannerPath");
                    poster.Image = BannerUrl + path;
                    if (!string.IsNullOrEmpty(path))
                        season.BannersPaths.Add(poster);
                }
            }

            return season;
        }


        public Item GetEpisodeDetails(Item item)
        {
            DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name);
            if (dp == null) return null;
            if (!LoadSeriesDetails(dp.Id))
                return null;
            if (string.IsNullOrEmpty(item.EpisodeNumber) || string.IsNullOrEmpty(item.SeasonNumber))
                return null;
            Item episode = new Item();

            episode.SeasonNumber = item.SeasonNumber;
            episode.EpisodeNumber = item.EpisodeNumber;
            episode.SeriesName = item.SeriesName;
            if (episode.SeasonNumber != "0")
                episode.SeasonNumber = episode.SeasonNumber.TrimStart('0');
            episode.EpisodeNumber = episode.EpisodeNumber.TrimStart('0');

            bool UsingAbsoluteData = false;
            XmlDocument doc = new XmlDocument();
            doc.Load(Path.Combine(Path.Combine(XmlPath, dp.Id), lang + ".xml"));
            if (doc == null) return null;
            XmlNode epNode = doc.SelectSingleNode("//Episode[SeasonNumber='" + episode.SeasonNumber + "'][EpisodeNumber='" + episode.EpisodeNumber + "']");
            if (epNode == null)
            {
                Logger.ReportWarning("Episode S" + episode.EpisodeNumber + "E" + episode.SeasonNumber + " not found in xml file");
                return null;
            }

            var f = epNode.SafeGetString("filename");
            if (f != null)
                episode.PrimaryImage = new Poster { Image = BannerUrl + f };

            episode.Overview = epNode.SafeGetString("Overview");
            if (UsingAbsoluteData)
                episode.EpisodeNumber = epNode.SafeGetString("absolute_number");
            if (episode.EpisodeNumber == null)
                episode.EpisodeNumber = epNode.SafeGetString("EpisodeNumber");

            episode.Title = epNode.SafeGetString("EpisodeName");
            episode.SeasonNumber = epNode.SafeGetString("SeasonNumber");
            episode.Rating = epNode.SafeGetSingle("Rating", (float)-1, 10);
            if (episode.Rating == -1)
                episode.Rating = null;

            string actors = epNode.SafeGetString("GuestStars");
            if (actors != null)
            {
                episode.Actors = new List<Actor>(actors.Trim('|').Split('|')
                    .Select(str => new Actor() { Name = str })
                    );
            }

            string directors = epNode.SafeGetString("Director");
            if (directors != null)
            {
                episode.Crew = new List<CrewMember>();
                foreach (string crew in directors.Trim('|').Split('|'))
                    episode.Crew.Add(new CrewMember { Name = crew, Activity = "Director" });
            }

            string writers = epNode.SafeGetString("Writer");
            if (writers != null)
            {
                if (episode.Crew == null) episode.Crew = new List<CrewMember>();
                foreach (string crew in writers.Trim('|').Split('|'))
                    episode.Crew.Add(new CrewMember { Name = crew, Activity = "Writer" });
            }

            return episode;
        }
       

    }
}
