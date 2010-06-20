using System;
using System.Collections.Generic;
using System.Text;
using System.Collections.ObjectModel;
using System.IO;
using System.Collections;
using System.Xml;
using System.Text.RegularExpressions;
using System.Net;
using System.Web;
using System.Security.Cryptography;
using ICSharpCode.SharpZipLib.Zip;


namespace mveEngine
{
    public class Helper
    {    

        public static Item UpdateItem(Item OldItem, Item NewItem)
        {
            Item item = new Item();
            if (!Config.Instance.AddMissingData)
                item = NewItem;
            else
                item = OldItem;

            item.Path = OldItem.Path;
            item.VideoFiles = OldItem.VideoFiles;
            item.DateAdded = OldItem.DateAdded;
            item.MetadataLocation = OldItem.MetadataLocation;
            item.Id = OldItem.Id;
            item.Type = OldItem.Type;
            
            if (NewItem.ProvidersId != null)
            {
                List<DataProviderId> dp = new List<DataProviderId>(NewItem.ProvidersId);
                if (OldItem.ProvidersId == null) OldItem.ProvidersId = new List<DataProviderId>();
                item.ProvidersId = OldItem.ProvidersId;
                item.ProvidersId.RemoveAll(p => NewItem.ProvidersId.Exists(pid => p.Name == pid.Name));
                item.ProvidersId.AddRange(dp);
                item.ProvidersId = dp;
            }
            else item.ProvidersId = OldItem.ProvidersId;  
            
            item.EpisodeNumber = OldItem.EpisodeNumber;
            item.SeasonNumber = OldItem.SeasonNumber;
            item.Mediatype = OldItem.Mediatype;

            if (!string.IsNullOrEmpty(OldItem.SeriesName)) item.SeriesName = OldItem.SeriesName;
            else item.SeriesName = NewItem.SeriesName;

            if (Config.Instance.AddMissingData)
            {
                if (!string.IsNullOrEmpty(NewItem.Title)) item.Title = NewItem.Title;
                if (string.IsNullOrEmpty(item.OriginalTitle) && !string.IsNullOrEmpty(NewItem.OriginalTitle)) item.OriginalTitle = NewItem.OriginalTitle;
                if (string.IsNullOrEmpty(item.SortTitle) && !string.IsNullOrEmpty(NewItem.SortTitle)) item.SortTitle = NewItem.SortTitle;
                if (string.IsNullOrEmpty(item.MPAARating) && !string.IsNullOrEmpty(NewItem.MPAARating)) item.MPAARating = NewItem.MPAARating;
                if (string.IsNullOrEmpty(item.AspectRatio) && !string.IsNullOrEmpty(NewItem.AspectRatio)) item.AspectRatio = NewItem.AspectRatio;
                if (item.TagLines == null && NewItem.TagLines != null) item.TagLines = NewItem.TagLines;
                if (item.Year == null && NewItem.Year != null) item.Year = NewItem.Year;
                if (item.Rating == null && NewItem.Rating != null) item.Rating = NewItem.Rating;
                if (item.RunningTime == null && NewItem.RunningTime != null) item.RunningTime = NewItem.RunningTime;
                if (string.IsNullOrEmpty(item.Overview) && !string.IsNullOrEmpty(NewItem.Overview)) item.Overview = NewItem.Overview;
                if (NewItem.Actors != null)
                {
                    if (item.Actors == null) item.Actors = new List<Actor>();
                    foreach (Actor actor in NewItem.Actors)
                    {
                        Actor oldActor = item.Actors.Find(a => a.Name.ToLower() == actor.Name.ToLower());
                        if (oldActor == null)
                            item.Actors.Add(new Actor { Name = actor.Name, Role = actor.Role });
                    }
                }
                if (NewItem.BackdropImagePaths != null)
                {
                    if (item.BackdropImagePaths == null) item.BackdropImagePaths = new List<Poster>();
                    foreach (Poster s in NewItem.BackdropImagePaths)
                    {
                        if (!item.BackdropImagePaths.Exists(t => t.Image.ToLower() == s.Image.ToLower()))
                            item.BackdropImagePaths.Add(s);
                    }
                    for (int i = 0; i < item.BackdropImagePaths.Count; i++)
                        item.BackdropImagePaths[i].Checked = (i < Config.Instance.MaxBdSaved);
                }
                if (NewItem.ImagesPaths != null)
                {
                    if (item.ImagesPaths == null) item.ImagesPaths = new List<Poster>();
                    foreach (Poster s in NewItem.ImagesPaths)
                    {
                        if (!item.ImagesPaths.Exists(t => t.Image.ToLower() == s.Image.ToLower()))
                            item.ImagesPaths.Add(s);
                    }
                    if (item.ImagesPaths.Count > 0)
                    {
                        Poster pChecked = item.ImagesPaths.Find(p => p.Checked) ?? item.ImagesPaths[0];
                        foreach (Poster poster in item.ImagesPaths)
                            poster.Checked = (poster == pChecked);
                    }
                }
                if (NewItem.BannersPaths != null)
                {
                    if (item.BannersPaths == null) item.BannersPaths = new List<Poster>();
                    foreach (Poster s in NewItem.BannersPaths)
                    {
                        if (!item.BannersPaths.Exists(t => t.Image.ToLower() == s.Image.ToLower()))
                            item.BannersPaths.Add(s);
                    }
                    for (int i = 0; i < item.BannersPaths.Count; i++)
                        item.BannersPaths[i].Checked = (i == 0);
                }
                if (NewItem.TrailerFiles != null)
                {
                    if (item.TrailerFiles == null) item.TrailerFiles = new List<string>();
                    foreach (string s in NewItem.TrailerFiles)
                    {
                        if (!item.TrailerFiles.Exists(t => t.ToLower() == s.ToLower()))
                            item.TrailerFiles.Add(s);
                    }
                }
                if (NewItem.Crew != null)
                {
                    if (item.Crew == null) item.Crew = new List<CrewMember>();
                    foreach (CrewMember s in NewItem.Crew)
                    {
                        if (!item.Crew.Exists(t => t.Name.ToLower() == s.Name.ToLower()))
                            item.Crew.Add(s);
                    }
                }
                if (NewItem.Genres != null)
                {
                    if (item.Genres == null) item.Genres = new List<string>();
                    foreach (string s in NewItem.Genres)
                    {
                        if (!item.Genres.Exists(t => t.ToLower() == s.ToLower()))
                            item.Genres.Add(s);
                    }
                }
                if (NewItem.Studios != null)
                {
                    if (item.Studios == null) item.Studios = new List<string>();
                    foreach (string s in NewItem.Studios)
                    {
                        if (!item.Studios.Exists(t => t.ToLower() == s.ToLower()))
                            item.Studios.Add(s);
                    }
                }
                if (NewItem.Countries != null)
                {
                    if (item.Countries == null) item.Countries = new List<string>();
                    foreach (string s in NewItem.Countries)
                    {
                        if (!item.Countries.Exists(t => t.ToLower() == s.ToLower()))
                            item.Countries.Add(s);
                    }
                }
            }
            else
            {
                if (string.IsNullOrEmpty(item.Title)) item.Title = OldItem.Title;
                if (string.IsNullOrEmpty(item.OriginalTitle)) item.OriginalTitle = OldItem.OriginalTitle;
                if (string.IsNullOrEmpty(item.SortTitle)) item.SortTitle = OldItem.SortTitle;
                if (item.TagLines == null) item.TagLines = OldItem.TagLines;
                if (string.IsNullOrEmpty(item.MPAARating)) item.MPAARating = OldItem.MPAARating;
                if (string.IsNullOrEmpty(item.AspectRatio)) item.AspectRatio = OldItem.AspectRatio;
                if (item.Year == null) item.Year = OldItem.Year;
                if (item.Rating == null) item.Rating = OldItem.Rating;
                if (item.RunningTime == null) item.RunningTime = OldItem.RunningTime;
                if (string.IsNullOrEmpty(item.Overview)) item.Overview = OldItem.Overview;
                if (item.Actors == null) item.Actors = OldItem.Actors;
                if (item.BackdropImagePaths == null) item.BackdropImagePaths = OldItem.BackdropImagePaths;
                if (item.BannersPaths == null) item.BannersPaths = OldItem.BannersPaths;
                if (item.ImagesPaths == null) item.ImagesPaths = OldItem.ImagesPaths;
                if (item.TrailerFiles == null) item.TrailerFiles = OldItem.TrailerFiles;
                if (item.Crew == null) item.Crew = OldItem.Crew;
                if (item.Genres == null) item.Genres = OldItem.Genres;
                if (item.Studios == null) item.Studios = OldItem.Studios;
                if (item.Countries == null) item.Countries = OldItem.Countries;
            }
            return item;
        }

        static MD5CryptoServiceProvider md5Provider = new MD5CryptoServiceProvider();

        public static string GetMD5(string str)
        {
            lock (md5Provider)
            {
                return new Guid(md5Provider.ComputeHash(Encoding.Unicode.GetBytes(str))).ToString();
            }
        }

        public static bool IsDirectorName(string dir)
        {
            switch (dir.ToLower())
            {
                case "director": return true;
                case "producteur": return true;
                default: return false;
            }
        }

        public static bool IsWriterName(string dir)
        {
            switch (dir.ToLower())
            {
                case "writer": return true;
                case "scénariste": return true;
                default: return false;
            }
        }

        public static string ConvertToTime(decimal d)
        {
            string result;
            Int32 h = (Int32)(d / 60);
            Int32 m = (Int32)(d % 60);
            if (h == 0)
                result = string.Format("{0} min.", m.ToString());
            else
            {
                if (m == 0)
                    result = string.Format("{0}h", h.ToString());
                else
                {
                    string min = m.ToString();
                    if (min.Length == 1)
                        min = "0" + min;
                    result = string.Format("{0}h{1}", h.ToString(), min);
                }
            }
            return result;
        }

        public static string ConvertToRating(decimal d)
        {
            return string.Format(Kernel.Instance.GetString("RateConvert"), d.ToString());
        }

        public static bool UnZip(string SrcFile, string DstPath)
        {
            try
            {
                if (!Directory.Exists(DstPath))
                    Directory.CreateDirectory(DstPath);
                FileStream fileStreamIn = new FileStream(SrcFile, FileMode.Open, FileAccess.Read);
                ZipInputStream zipInStream = new ZipInputStream(fileStreamIn);
                ZipEntry entry = zipInStream.GetNextEntry();
                while (entry != null)
                {
                    FileStream fileStreamOut = new FileStream(Path.Combine(DstPath, entry.Name), FileMode.Create, FileAccess.Write);

                    int size;
                    byte[] buffer = new byte[4096];
                    do
                    {
                        size = zipInStream.Read(buffer, 0, buffer.Length);
                        fileStreamOut.Write(buffer, 0, size);
                    } while (size > 0);
                    fileStreamOut.Close();
                    entry = zipInStream.GetNextEntry();
                }
                zipInStream.Close();

                fileStreamIn.Close();
                return true;
            }
            catch { return false; }
        }

        public static float GetSimilarity(string string1, string string2)
        {
            float dis = ComputeDistance(string1, string2);
            float maxLen = string1.Length;
            if (maxLen < string2.Length)
                maxLen = string2.Length;
            if (maxLen == 0.0F)
                return 1.0F;
            else
                return 1.0F - dis / maxLen;
        }

        private static int ComputeDistance(string s, string t)
        {
            int n = s.Length;
            int m = t.Length;
            int[,] distance = new int[n + 1, m + 1]; // matrix

            int cost = 0;
            if (n == 0) return m;
            if (m == 0) return n;
            //init1

            for (int i = 0; i <= n; distance[i, 0] = i++) ;
            for (int j = 0; j <= m; distance[0, j] = j++) ;
            //find min distance

            for (int i = 1; i <= n; i++)
            {
                for (int j = 1; j <= m; j++)
                {
                    cost = (t.Substring(j - 1, 1) ==
                        s.Substring(i - 1, 1) ? 0 : 1);
                    distance[i, j] = Math.Min(Math.Min(distance[i - 1, j] + 1,
                    distance[i, j - 1] + 1),
                    distance[i - 1, j - 1] + cost);
                }
            }
            return distance[n, m];
        }

        static string remove = "\"'!`?";
        // "Face/Off" support.
        static string spacers = "/,.:;\\(){}[]+-_=–*";  // (there are not actually two - in the they are different char codes)

        public static string GetComparableName(string name)
        {
            if (name == null)
                return "";
            name = name.ToLower();
            name = name.Normalize(NormalizationForm.FormKD);

            int i = name.IndexOf("(");
            int j = -1;
            if (i > -1) j = name.IndexOf(")", i);
            while (i > -1 && j > -1)
            {
                name = name.Replace(name.Substring(i, j - i + 1), "");
                i = name.IndexOf("(");
                if (i > -1) j = name.IndexOf(")", i);
            }
            name = name.Trim();

            StringBuilder sb = new StringBuilder();
            foreach (char c in name)
            {
                if ((int)c >= 0x2B0 && (int)c <= 0x0333)
                {
                    // skip char modifier and diacritics 
                }
                else if (remove.IndexOf(c) > -1)
                {
                    // skip chars we are removing
                }
                else if (spacers.IndexOf(c) > -1)
                {
                    sb.Append(" ");
                }
                else if (c == '&')
                {
                    sb.Append(" and ");
                }
                else
                {
                    sb.Append(c);
                }
            }
            name = sb.ToString();
            name = name.Replace("the", " ");
            List<string> marks = new List<string> { "dvdrip", "hdrip", "tvrip", "ts", "r5", "screener", "french", "divx", "xvid", "1080p", "x264", "mp3", "proper" };
            foreach (string mark in marks)
            {
                name = name.Replace(mark, "");
            }
            if (name.EndsWith("1")) name = name.Remove(name.Length - 1);
            string prev_name;
            do
            {
                prev_name = name;
                name = name.Replace("  ", " ");
            } while (name.Length != prev_name.Length);

            return name.Trim();
        }

        public static XmlDocument Fetch(string url)
        {
            return Fetch(url, Encoding.UTF8);
        }
        public static XmlDocument Fetch(string url, Encoding encoding)
        {
            int attempt = 0;
            while (attempt < 2)
            {
                attempt++;
                try
                {
                    HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(url);  
                    req.UserAgent = "MetaVideoEditor";
                    req.Timeout = 23000;
                    WebResponse resp = req.GetResponse();
                    try
                    {
                        using (Stream s = resp.GetResponseStream())
                        {
                            StreamReader sr = new StreamReader(s, encoding);
                            XmlDocument doc = new XmlDocument();
                            doc.Load(sr);
                            resp.Close();
                            s.Close();
                            return doc;
                        }
                    }
                    finally
                    {
                        resp.Close();
                    }
                }
                catch (Exception e)
                {
                    Logger.ReportException("Exception fetching XML file " + url, e);
                }
            }
            return null;
        }
    }
}