using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Globalization;
using mveEngine;

namespace AlloCine
{
    public class film : Item
    {
        public int Relevance { get; set; }
    }
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{B78EB5C6-BEDC-4ace-9C3A-020D6C3A8A33}"); }
        }

        public override string Name
        {
            get { return "AlloCine"; }
        }

        public override string Description
        {
            get
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Récupère les données de la base AlloCine.fr";
                    default: return "Fetches movie metadata from AlloCine.fr (french)";
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
            get { return new Version(1, 0, 1); }
        }

        public override Version RequiredMVEVersion
        {
            get { return new Version(2, 0, 0); }
        }

        public override bool IsConfigurable
        {
            get { return false; }
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

        private static string Search = @"http://api.allocine.fr/xml/search?partner=3&q={0}";
        private static string GetInfo = @"http://api.allocine.fr/xml/movie?partner=3&profile=large&code={0}";
        private static string GetTrailers = @"http://www.allocine.fr/skin/video/AcVisionData_xml.asp?media={0}";


        public override Item AutoFind(Item item)
        {
            if (item.ProvidersId != null)
            {
                DataProviderId dp = item.ProvidersId.Find(p => p.Name == this.Name || p.Name == "allocine");
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
            XmlDocument doc = Helper.Fetch(string.Format(Search, HtmlUtil.UrlEncode(item.Title)), Encoding.UTF8);
            List<Item> filmsList = new List<Item>();
            if (doc == null) return filmsList;
            //doc.Save(@"d:\test.xml");

            foreach (XmlNode node in doc.GetElementsByTagName("movie"))
            {
                Item f = new Item();
                f.ProvidersId = new List<DataProviderId>();
                string id = node.Attributes["code"].InnerText;
                if (!string.IsNullOrEmpty(id))
                {
                    f.ProvidersId.Add(new DataProviderId
                    {
                        Name = this.Name,
                        Id = id,
                        Url = string.Format("http://www.allocine.fr/film/fichefilm_gen_cfilm={0}.html", id)
                    });
                }

                f.Title = CleanAllocineTitle(SafeGetString(node, "title"));
                f.OriginalTitle = CleanAllocineTitle(SafeGetString(node, "originalTitle"));
                if (string.IsNullOrEmpty(f.Title) && string.IsNullOrEmpty(f.OriginalTitle)) continue;
                if (string.IsNullOrEmpty(f.Title)) f.Title = f.OriginalTitle;
                try { f.Year = Int32.Parse(SafeGetString(node, "productionYear")); }
                catch { }
                f.Overview = SafeGetString(node.GetNodeByName("castingShort"), "actors");
                XmlNode n = node.GetNodeByName("poster");
                if (n != null)
                {
                    try
                    {
                        string url = n.Attributes["href"].InnerText;
                        url = url.Replace(".fr/", ".fr/r_75_106/");
                        f.PrimaryImage = new Poster { Image = url };
                    }
                    catch { }
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
            XmlDocument InfosDoc = Helper.Fetch(string.Format(GetInfo, dp.Id), Encoding.UTF8);
            if (InfosDoc == null)
                return null;
            XmlNode node = InfosDoc.GetNodeByName("movie");
            //Titre
            movie.Title = CleanAllocineTitle(SafeGetString(node, "title"));

            //Titre original
            movie.OriginalTitle = CleanAllocineTitle(SafeGetString(node, "originalTitle"));

            //Année
            string release = SafeGetString(node, "productionYear");
            if (release != null && release.Length == 4)
                movie.Year = Int32.Parse(release);

            //Résumé
            movie.Overview = SafeGetString(node, "synopsis");

            //Durée
            try { movie.RunningTime = (int)(Int32.Parse(SafeGetString(node, "runtime")) / 60); }
            catch { }

            //Media
            XmlNode mediaNode = node.GetNodeByName("mediaList");
            foreach (XmlNode mNode in mediaNode.SelectChildren())
            {
                XmlNode typeNode = mNode.GetNodeByName("type");
                if (typeNode == null) continue;
                string typeCode = typeNode.Attributes["code"].InnerText;
                //Posters
                if (typeCode == "31001")
                {
                    if (movie.ImagesPaths == null) movie.ImagesPaths = new List<Poster>();
                    XmlNode thumbNode = mNode.GetNodeByName("thumbnail");
                    if (thumbNode == null) continue;
                    movie.ImagesPaths.Add(new Poster { Image = thumbNode.Attributes["href"].InnerText });
                }

                //Backdrops
                if (typeCode == "31006")
                {
                    if (movie.BackdropImagePaths == null) movie.BackdropImagePaths = new List<Poster>();
                    XmlNode thumbNode = mNode.GetNodeByName("thumbnail");
                    if (thumbNode == null) continue;
                    movie.BackdropImagePaths.Add(new Poster { Image = thumbNode.Attributes["href"].InnerText });
                }

                //Trailers
                if (PluginOptions.Instance.UseTrailers && (typeCode == "31003" || typeCode == "31016"))
                {
                    if (movie.TrailerFiles == null) movie.TrailerFiles = new List<string>();
                    string cmedia = mNode.Attributes["code"].InnerText;
                    XmlDocument trailerDoc = Helper.Fetch(string.Format(GetTrailers, cmedia), Encoding.UTF8);
                    if (trailerDoc != null)
                    {
                        XmlNode n = trailerDoc.SelectSingleNode("//AcVisionVideo");
                        if (n != null)
                        {
                            string url = n.Attributes["hd_path"].InnerText;
                            if (string.IsNullOrEmpty(url))
                                url = n.Attributes["md_path"].InnerText;
                            if (string.IsNullOrEmpty(url))
                                url = n.Attributes["ld_path"].InnerText;
                            if (!string.IsNullOrEmpty(url))
                            {
                                movie.TrailerFiles.Add(url);
                            }
                        }
                    }
                }
            }

            //Pays
            XmlNode countryNode = node.GetNodeByName("nationalityList");
            foreach (XmlNode n in countryNode.SelectChildren())
            {
                if (movie.Countries == null) movie.Countries = new List<string>();
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.Countries.Add(n.InnerText);
            }

            //Genre
            XmlNode genreNode = node.GetNodeByName("genreList");
            foreach (XmlNode n in genreNode.SelectChildren())
            {
                if (movie.Genres == null) movie.Genres = new List<string>();
                if (!string.IsNullOrEmpty(n.InnerText))
                    movie.Genres.Add(n.InnerText);
            }

            //Note            
            XmlNode statNode = node.GetNodeByName("statistics");
            if (statNode != null)
            {
                XmlNode ratingNode = statNode.GetNodeByName("ratingStats");
                float? rating = null;
                double vote = 0; double note = 0;
                foreach (XmlNode n in ratingNode.SelectChildren())                
                {
                        double V; double N;
                        try
                        {
                            V = Int32.Parse(n.InnerText);
                            N = double.Parse(n.Attributes["note"].InnerText, CultureInfo.InvariantCulture) * 2;
                        }
                        catch { continue; }
                        vote = vote + V;
                        note = note + (N * V);
                }
                    if (vote > 0)
                        rating = (float)Math.Round((double)(note / vote), 1);
                
                movie.Rating = rating;
            }

            //Acteurs - Equipe
            XmlNode castingNode = node.GetNodeByName("casting");
            foreach (XmlNode n in castingNode.SelectChildren())
            {
                string activity = SafeGetString(n, "activity");
                if (activity == "Acteur" || activity == "Actrice")
                {
                    if (movie.Actors == null) movie.Actors = new List<Actor>();
                    Actor actor = new Actor();
                    actor.Name = SafeGetString(n, "person");
                    actor.Role = SafeGetString(n, "role");
                    actor.ImagePath = SafeGetString(n, "picture");
                    movie.Actors.Add(actor);
                }
                else
                {
                    if (movie.Crew == null) movie.Crew = new List<CrewMember>();
                    CrewMember cm = new CrewMember();
                    cm.Name = SafeGetString(n, "person");
                    cm.Activity = activity;
                    movie.Crew.Add(cm);
                }
            }

            

            return movie;
        }

        private static string CleanAllocineTitle(string title)
        {
            if (string.IsNullOrEmpty(title)) return string.Empty;
            if (title.ToLower().EndsWith("(tv)")) return title.Substring(0, title.Length - 4).Trim();
            if (title.ToLower().EndsWith("(v)")) return title.Substring(0, title.Length - 3).Trim();
            return title;
        }

        static string SafeGetString(XmlNode doc, string path)
        {
            if (doc == null) return "";
            XmlNode rvalNode = doc.GetNodeByName(path);
            if (rvalNode != null && rvalNode.InnerText.Length > 0)
            {
                return rvalNode.InnerText;
            }
            return "";
        }

    }
}
