using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Globalization;
using mveEngine;

namespace CinePassion
{
    public class film : Item
    {
        public int Relevance { get; set; }        
    }
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{7BBB3E29-BDD0-44d8-8687-7334248A3C60}"); }
        }

        public override string Name
        {
            get { return "Ciné-Passion"; }
        }

        public override string Description
        {
            get
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les données de la base Ciné-Passion";
                    default: return "Fetches movie metadata from Cine-Passion";
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
            get { return new Version(1, 0, 8); }
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

        public static string GetLanguage
        {
            get
            {
                if (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString() == "fr")
                    return "fr";
                return "en";
            }
        }

        public override void Init(Kernel kernel)
        {
            PluginOptions = new PluginConfiguration<PluginOptions>(kernel, this.GetType().Assembly);
            PluginOptions.Load();
        }

        private static readonly string XbmcKey = "2952351097998ac1240cb2ab7333a3d2";
        private static string Search = @"http://passion-xbmc.org/scraper/API/1/Movie.Search/{0}/{1}/Title/fr/XML/{2}/{3}";
        private static string GetInfo = @"http://passion-xbmc.org/scraper/API/1/Movie.GetInfo/{0}/{1}/ID/{2}/XML/{3}/{4}";
        /*private static string GetQuota = @"http://passion-xbmc.org/scraper/API/1/User.GetQuota/{0}/{1}/fr/XML/{2}";

        public static string Quota
        {
            get
            {

                XmlDocument fetchquota = Helper.Fetch(string.Format(GetQuota, PluginOptions.Instance.username, PluginOptions.Instance.password, XbmcKey));
                if (fetchquota == null) return null;
                XmlNode QuotaNode = fetchquota.SelectSingleNode("//userquota/quota");

                string QuotaAvailable = QuotaNode.Attributes["authorize"].InnerText;
                return " - Quota Restant :" + QuotaAvailable;

            }
        }*/
        
        public override Item AutoFind(Item item)
        {
            if (item.ProvidersId != null)
            {
                DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name || p.Name == "AlloCine");
                if (dp != null)
                    return Fetch(item);
            }
            Item f = new Item();
            f = AttemptFindId(item.Title, item.Year.ToString());

            if (f.ProvidersId == null)
                f = AttemptFindId(Helper.GetComparableName(item.Title), item.Year.ToString());

            if (f.ProvidersId == null || f.ProvidersId.Count == 0)  return null;
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
            XmlDocument doc = Helper.Fetch(string.Format(Search, PluginOptions.Instance.username, PluginOptions.Instance.password, XbmcKey, HttpUtility.UrlEncode(item.Title)));

            List<Item> filmsList = new List<Item>();
            if (doc == null) return filmsList;
            string Quota = "";
            try
            {
                XmlNode qNode = doc.SelectSingleNode("//results/quota");
                int qUse = Int32.Parse(qNode.Attributes["use"].InnerText);
                int qAuth = Int32.Parse(qNode.Attributes["authorize"].InnerText);
                Quota = " - Quota Restant :" + (qAuth - qUse).ToString();
            }
            catch { }
            foreach (XmlNode node in doc.SelectNodes("//results/movie"))
            {
                Item f = new Item();
                f.ProvidersId = new List<DataProviderId>();
                string id = node.SafeGetString("id");
                if (!string.IsNullOrEmpty(id))
                {
                    f.ProvidersId.Add(new DataProviderId {
                        Name = this.Name + Quota, 
                        Id = id, 
                        Url = "http://passion-xbmc.org/scraper/index2.php?Page=ViewMovie&ID=" + id });
                }
                string allocine_id = node.SafeGetString("id_allocine");
                if (!string.IsNullOrEmpty(allocine_id))
                {
                    f.ProvidersId.Add(new DataProviderId { Name = "AlloCine", Id = allocine_id });
                }
                string imdb_id = node.SafeGetString("id_imdb");
                if (!string.IsNullOrEmpty(imdb_id))
                {
                    f.ProvidersId.Add(new DataProviderId { Name = "Imdb", Id = imdb_id });
                }
                
                f.Title = CleanAllocineTitle(node.SafeGetString("title"));
                f.OriginalTitle = CleanAllocineTitle(node.SafeGetString("originaltitle"));
                f.Overview = node.SafeGetString("plot");
                try { f.Year = Int32.Parse(node.SafeGetString("year")); }
                catch { }
                foreach (XmlNode n in node.SelectNodes("images/image[@type='Poster']"))
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
            DataProviderId dp = movie.ProvidersId.Find(p => p.Name == this.Name || p.Name == "AlloCine");
            if (dp == null) return null;
            movie.ProvidersId = new List<DataProviderId>{dp};
            XmlDocument InfosDoc = Helper.Fetch(string.Format(GetInfo, PluginOptions.Instance.username, PluginOptions.Instance.password, PluginOptions.Instance.Language, XbmcKey, dp.Id));
            if (InfosDoc == null)
                return null;

            //Id
            string allocine = InfosDoc.SafeGetString("//id_allocine");
            if (!string.IsNullOrEmpty(allocine)) movie.ProvidersId.Add(new DataProviderId
            {
                Name = "AlloCine",
                Id = allocine,
                Url = "http://www.allocine.fr/film/fichefilm_gen_cfilm=" + allocine + ".html"
            });
            string imdb = InfosDoc.SafeGetString("//id_imdb");
            if (!string.IsNullOrEmpty(imdb)) movie.ProvidersId.Add(new DataProviderId
            {
                Name = "Imdb",
                Id = "tt0" + imdb,
                Url = "http://www.imdb.com/title/tt0" + imdb
            });

            //Titre
            movie.Title = CleanAllocineTitle(InfosDoc.SafeGetString("//title"));

            //Titre original
            movie.OriginalTitle = CleanAllocineTitle(InfosDoc.SafeGetString("//originaltitle"));

            //Url
            dp.Url = InfosDoc.SafeGetString("//url");

            //Année
            string release = InfosDoc.SafeGetString("//year");
            if (release != null && release.Length == 4)
                movie.Year = Int32.Parse(release);

            //Producteur
            movie.Crew = new List<CrewMember>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//directors/director"))
            {
                movie.Crew.Add(new CrewMember { Name = n.InnerText, Activity = "Producteur" });
            }

            //Résumé
            movie.Overview = InfosDoc.SafeGetString("//plot");

            //TagLine
            movie.TagLines = new List<string>{ InfosDoc.SafeGetString("//tagline")};

            //Durée
            try { movie.RunningTime = Int32.Parse(InfosDoc.SafeGetString("//runtime")); }
            catch { }

            //Trailer
            foreach (XmlNode n in InfosDoc.SelectNodes("//trailers/trailer"))
            {
                if (movie.TrailerFiles == null) movie.TrailerFiles = new List<string>();
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.TrailerFiles.Add(n.InnerText);
            }

            //Pays
            foreach (XmlNode n in InfosDoc.SelectNodes("//countries/country"))
            {
                if (movie.Countries == null) movie.Countries = new List<string>();
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.Countries.Add(n.InnerText);
            }

            //Genre
            movie.Genres = new List<string>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//genres/genre"))
            {
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.Genres.Add(n.InnerText);
            }

            //Vignettes
            movie.ImagesPaths = new List<Poster>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//images/image[@type='Poster']"))
            {
                string id = n.Attributes["id"].InnerText;
                if (!movie.ImagesPaths.Exists(ip => ip.Id == id))
                    movie.ImagesPaths.Add(new Poster { Id = id });
                if (n.Attributes["size"].InnerText == "original")
                {
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].Image = n.Attributes["url"].InnerText;
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].width = n.Attributes["width"].InnerText;
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].height = n.Attributes["height"].InnerText;
                }
                else if (n.Attributes["size"].InnerText == "preview")
                {
                    movie.ImagesPaths[movie.ImagesPaths.FindIndex(ip => ip.Id == id)].Thumb = n.Attributes["url"].InnerText;
                }
            }  

            //Backdrop
            movie.BackdropImagePaths = new List<Poster>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//images/image[@type='Fanart']"))
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
                        poster.width = n.Attributes["width"].InnerText;
                        poster.height = n.Attributes["height"].InnerText;
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
            float? rating = null;
            if (PluginOptions.Instance.Rating != "moyenne des trois")
            {
                try
                {
                    rating = (float)double.Parse(InfosDoc.SelectSingleNode("//ratings/rating[@type='" + PluginOptions.Instance.Rating + "']").InnerText);
                }
                catch { }
            }
            else
            {
                double vote = 0; double note = 0;
                foreach (XmlNode n in InfosDoc.SelectNodes("//ratings/rating"))
                {
                    double V; double N;
                    try
                    {
                        N = double.Parse(n.InnerText, new CultureInfo("fr-FR"));
                        V = Int32.Parse(n.Attributes["votes"].InnerText);
                    }
                    catch { continue; }
                    vote = vote + V;
                    note = note + (N * V);
                }
                if (vote > 0)
                    rating = (float)Math.Round((double)(note / vote), 1);
            }
            movie.Rating = rating;


            // Studios
            movie.Studios = new List<string>();
            foreach (XmlNode n in InfosDoc.SelectNodes("//studios/studio"))
            {
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.Studios.Add(n.InnerText);
            }

            //Acteurs
            movie.Actors = null;
            foreach (XmlNode n in InfosDoc.SelectNodes("//casting/person"))
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

        private static string CleanAllocineTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;
            string res = title;
            if (title.ToLower().EndsWith("(tv)")) res = title.Substring(0, title.Length - 4).Trim();
            if (title.ToLower().EndsWith("(v)")) res = title.Substring(0, title.Length - 3).Trim();
            return HttpUtility.HtmlDecode(res);
        }

    }
}
