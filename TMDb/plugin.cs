using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace TMDb
{
    public class film : Item
    {
        public int Relevance { get; set; }
    }

    public class plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{BAC96A51-1FC8-4d60-B69C-7C9314E25352}"); }
        }

        public override string Name
        {
            get { return "themoviedb"; }
        }

        public override string Description
        {
            get
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les données de la base themoviedb.org";
                    default: return "Fetches movie metadata from themoviedb.org";
                }
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Provider; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.Movie; }
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

        public override void Init(Kernel kernel)
        {
            PluginOptions = new PluginConfiguration<PluginOptions>(kernel, this.GetType().Assembly);
            PluginOptions.Load();
        }

        private static readonly string APIKey = "ebb392e0588bc0b5ab9d9a6100711a8c";
        private static string Search = @"http://api.themoviedb.org/2.1/Movie.search/{0}/xml/{1}/{2}";
        private static string GetInfo = @"http://api.themoviedb.org/2.1/Movie.getInfo/{0}/xml/{1}/{2}";


        public override Item AutoFind(Item item)
        {
            if (item.ProvidersId != null)
            {
                DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name || p.Name == "Imdb");
                if (dp != null)
                    return Fetch(item);
            }
            Item f = new Item();
            f = AttemptFindId(item.Title, item.Year.ToString());

            if (f.ProvidersId == null || (f.ProvidersId.Count > 0 && item.Title.ToLower() != Helper.GetComparableName(item.Title)))
                f = AttemptFindId(Helper.GetComparableName(item.Title), item.Year.ToString());

            if (f.ProvidersId == null || f.ProvidersId.Count == 0) return null;
            else
            {
                return Fetch(f);
            }
        }

        private Item AttemptFindId(string name, string year)
        {
            if (year == null) year = "";
            List<film> filmsList = FindPossibleFilms(name);
            List<film> PossibleFilms = TryFindId(filmsList, name, year, false);

            if (!PossibleFilms.Exists(f => f.Relevance >= 5))
                PossibleFilms.AddRange(TryFindId(filmsList, name, year, true));

            if (PossibleFilms.Count == 0) return new Item();
            int max = PossibleFilms.Max(f => f.Relevance);
            return PossibleFilms.Find(f => f.Relevance == max);
        }

        private List<film> FindPossibleFilms(string name)
        {
            List<film> films = new List<film>();
            foreach (Item item in FindPossible(new Item { Title = name }))
                films.Add(new film
                {
                    Title = item.Title,
                    ProvidersId = item.ProvidersId,
                    OriginalTitle = item.OriginalTitle,
                    Year = item.Year,
                    PrimaryImage = item.PrimaryImage
                });
            return films;
        }

        public override List<Item> FindPossible(Item item)
        {
            XmlDocument doc = Helper.Fetch(string.Format(Search, lang, APIKey, HtmlUtil.UrlEncode(item.Title)));

            List<Item> filmsList = new List<Item>();
            if (doc == null) return filmsList;
            foreach (XmlNode node in doc.SelectNodes("//movies/movie"))
            {
                Item f = new Item();
                f.ProvidersId = new List<DataProviderId>();
                string id = node.SafeGetString("id");
                if (!string.IsNullOrEmpty(id))
                {
                    f.ProvidersId.Add(new DataProviderId
                    {
                        Name = this.Name,
                        Id = id,
                        Url = node.SafeGetString("url")
                    });
                }
                string imdb_id = node.SafeGetString("imdb_id");
                if (!string.IsNullOrEmpty(imdb_id))
                {
                    f.ProvidersId.Add(new DataProviderId { Name = "Imdb", Id = imdb_id });
                }

                f.Title = node.SafeGetString("name");
                f.Overview = node.SafeGetString("overview");
                try { f.Year = Int32.Parse(node.SafeGetString("released").Substring(0, 4)); }
                catch { }
                foreach (XmlNode n in node.SelectNodes("images/image[@type='poster']"))
                {
                    if (n.Attributes["size"].InnerText == "thumb")
                    {
                        f.PrimaryImage = new Poster { Image = n.Attributes["url"].InnerText };
                        break;
                    }
                }
                filmsList.Add(f);
            }

            return filmsList;
        }

        private static List<film> TryFindId(List<film> filmsList, string name, string year, bool compare)
        {
            List<film> PossibleFilms = new List<film>();
            int y;
            if (!Int32.TryParse(year, out y))
                y = 10;
            List<string> spacer = new List<string> { ":", "-", "–", "," };
            int index = filmsList.Count;
            foreach (film f in filmsList)
            {
                f.Relevance = index;
                index--;
                float tolerance = .8F;
                if (y == f.Year) tolerance = .75F;
                if (!compare && f.Title.ToLower() == name.ToLower()) { f.Relevance += 5; PossibleFilms.Add(f); continue; }
                if (compare && Helper.GetSimilarity(Helper.GetComparableName(f.Title), Helper.GetComparableName(name)) > tolerance)
                { f.Relevance += 4; PossibleFilms.Add(f); continue; }

                else if (f.OriginalTitle != null)
                {
                    if (f.OriginalTitle.Trim().ToLower() == name.ToLower()) { f.Relevance += 3; PossibleFilms.Add(f); continue; }
                }

                if (Math.Abs(y - (int)f.Year) < 3)
                {
                    string longest = name;
                    string shortest = f.Title;
                    if (name.Length < f.Title.Length) { longest = f.Title; shortest = name; }
                    foreach (string s in spacer)
                    {
                        if (longest.Contains(s))
                        {
                            string test = longest.Substring(0, longest.IndexOf(s)).Trim();
                            if (test.Length > 3 && Helper.GetSimilarity(Helper.GetComparableName(test), Helper.GetComparableName(shortest)) > .95)
                            { f.Relevance += 3; PossibleFilms.Add(f); }
                        }
                    }
                }
            }

            foreach (film f in PossibleFilms)
            {
                if (f.PrimaryImage != null) f.Relevance += 2;
                if (y == f.Year) f.Relevance += 2;
                else if (Math.Abs(y - (int)f.Year) < 3) f.Relevance += 1;
            }

            PossibleFilms.Sort(delegate(film f1, film f2) { return ((int)f2.Year).CompareTo((int)f1.Year); });
            return PossibleFilms;
        }

        public override Item Fetch(Item item)
        {
            Item movie = new Item();
            movie.ProvidersId = item.ProvidersId;
            DataProviderId dp = movie.ProvidersId.Find(p => p.Name == this.Name);
            if (dp == null) return null;
            movie.ProvidersId = new List<DataProviderId> { dp };
            XmlDocument InfosDoc = Helper.Fetch(string.Format(GetInfo, lang, APIKey, dp.Id));
            if (InfosDoc == null)
                return null;

            //Titre
            movie.Title = InfosDoc.SafeGetString("//name");

            //Url
            dp.Url = InfosDoc.SafeGetString("//url");

            //Année
            string release = InfosDoc.SafeGetString("//released");
            if (release != null && release.Length >= 4)
                movie.Year = Int32.Parse(release.Substring(0, 4));

            //Equipe
            movie.Crew = new List<CrewMember>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//cast/person[@job!='Actor']"))
            {
                movie.Crew.Add(new CrewMember { Name = n.Attributes["name"].InnerText, Activity = n.Attributes["job"].InnerText });
            }

            //Résumé
            movie.Overview = InfosDoc.SafeGetString("//overview");

            //TagLine
            string tagline = InfosDoc.SafeGetString("//tagline");
            if (!string.IsNullOrEmpty(tagline))
                movie.TagLines = new List<string> { tagline };

            //Durée
            try { movie.RunningTime = Int32.Parse(InfosDoc.SafeGetString("//runtime")); }
            catch { }

            //Trailer
            string trailer = InfosDoc.SafeGetString("trailer");
            if (!string.IsNullOrEmpty(trailer))
                movie.TrailerFiles = new List<string> { trailer };

            //Pays
            foreach (XmlNode n in InfosDoc.SelectNodes("//countries/country"))
            {
                if (movie.Countries == null) movie.Countries = new List<string>();
                movie.Countries.Add(n.Attributes["name"].InnerText);
            }

            //Genre
            movie.Genres = new List<string>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//categories/category[@type='genre']"))
            {
                    movie.Genres.Add(n.Attributes["name"].InnerText);
            }

            //Vignettes
            movie.ImagesPaths = new List<Poster>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//images/image[@type='poster']"))
            {
                string id = n.Attributes["id"].InnerText;
                if (!movie.ImagesPaths.Exists(ip => ip.Id == id))
                    movie.ImagesPaths.Add(new Poster { Id = id });
                if (n.Attributes["size"].InnerText == "original")
                {
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].Image = n.Attributes["url"].InnerText;
                }
                else if (n.Attributes["size"].InnerText == "thumb")
                {
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].Thumb = n.Attributes["url"].InnerText;
                }
            }

            //Backdrop
            movie.BackdropImagePaths = new List<Poster>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//images/image[@type='backdrop']"))
            {
                string id = n.Attributes["id"].InnerText;
                if (n.Attributes["size"].InnerText == "original")
                {
                    int size = 0;
                    Int32.TryParse(n.Attributes["width"].InnerText, out size);
                    if (size >= Config.Instance.MinBackdropWidth)
                    {
                        Poster poster = new Poster();
                        poster.Id = id;
                        poster.Checked = false;
                        poster.Image = n.Attributes["url"].InnerText;
                        movie.BackdropImagePaths.Add(poster);
                    }
                }
                else if (n.Attributes["size"].InnerText == "thumb" && movie.BackdropImagePaths.Exists(p => p.Id == id))
                {
                    movie.BackdropImagePaths[movie.BackdropImagePaths.FindIndex(ip => ip.Id == id)].Thumb = n.Attributes["url"].InnerText;
                }

            }
            if (movie.BackdropImagePaths.Count > 0)
            {
                for (int i = 0; i < Math.Min(movie.BackdropImagePaths.Count, Config.Instance.MaxBdSaved); i++)
                {
                    movie.BackdropImagePaths[i].Checked = true;
                }
            }

            //Note  
            movie.Rating = InfosDoc.SafeGetSingle("//rating", -1, 10); 

            //MPAA
            movie.MPAARating = InfosDoc.SafeGetString("//certification");

            // Studios
            movie.Studios = new List<string>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//studios/studio"))
            {
                movie.Studios.Add(n.Attributes["name"].InnerText);
            }

            //Acteurs
            movie.Actors = null;
            foreach (XmlNode n in InfosDoc.SelectNodes("//cast/person[@job='Actor']"))
            {
                if (movie.Actors == null)
                    movie.Actors = new List<Actor>();
                string name = n.Attributes["name"].InnerText;
                string role = n.Attributes["character"].InnerText;
                if (!string.IsNullOrEmpty(name))
                {
                    Actor actor = new Actor();
                    actor.Name = name;
                    actor.Role = role;
                    string ActorImg = n.Attributes["thumb"].InnerText;
                    if (!string.IsNullOrEmpty(ActorImg) && !ActorImg.EndsWith("Actor_Unknow.png"))
                        actor.ImagePath = ActorImg;
                    movie.Actors.Add(actor);
                }
            }

            return movie;
        }
    }
}
