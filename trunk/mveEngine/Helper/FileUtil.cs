using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using Microsoft.Win32;
using System.Windows.Forms;
using System.Collections;

namespace mveEngine
{
    class DirectorySort : IComparer
    {
        public int Compare(object x, object y)
        {
            DirectoryInfo d1 = x as DirectoryInfo;
            DirectoryInfo d2 = y as DirectoryInfo;
            return d1.Name.CompareTo(d2.Name);
        }
    }

    public class FileUtil
    {
        static Config Config
        {
            get { return Config.Instance; }
        }

        public static List<TreeNode> ReadFolder(string folderPath, List<Item> items)
        {
            List<TreeNode> nodes = new List<TreeNode>();
            if (!Directory.Exists(folderPath))
                return null;
            
            DirectoryInfo dirInfo = new DirectoryInfo(folderPath);

            DirectoryInfo[] dis = dirInfo.GetDirectories();
            Array.Sort(dis, new DirectorySort());
            foreach (DirectoryInfo di in dis)
            {
                if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || di.Attributes == FileAttributes.System || di.Name.EndsWith("$RECYCLE.BIN"))
                    continue;
                try
                {
                    Item item = new Item();
                    GetTitle(item, di.Name);
                    item.Path = di.FullName;
                    item.VideoFiles = GetVideoFiles(di);
                    item.Id = Helper.GetMD5(di.FullName);
                    item.MetadataLocation = item.Path;
                    item.DateAdded = di.CreationTime;
                    TreeNode n = new TreeNode();
                    n.ToolTipText = di.FullName;
                    n.Name = item.Title;
                    n.Tag = item.Id;
                    
                    if (TVUtils.IsSeriesFolder(di))
                    {
                        item.Type = Entity.Series;
                        item.SeriesName = item.Title;
                    }
                    else if (TVUtils.IsSeasonFolder(item.Path))
                    {
                        item.Type = Entity.Season;
                        item.SeriesName = di.Parent.Name;
                        item.SeasonNumber = TVUtils.SeasonNumberFromFolderName(di.FullName);
                        foreach (FileInfo epInfo in di.GetFiles())
                        {
                            if (!IsVideo(epInfo.FullName))
                                continue;
                            Item ep = new Item();
                            GetTitle(ep, Path.GetFileNameWithoutExtension(epInfo.Name));
                            ep.Path = epInfo.FullName;
                            ep.MetadataLocation = Path.Combine(Directory.GetParent(ep.Path).FullName, "metadata");
                            ep.DateAdded = epInfo.CreationTime;
                            ep.Id = Helper.GetMD5(ep.Path);
                            ep.Type = Entity.Episode;
                            ep.SeriesName = item.SeriesName;
                            ep.SeasonNumber = item.SeasonNumber;
                            ep.EpisodeNumber = TVUtils.EpisodeNumberFromFile(ep.Path, true);
                            items.Add(ep);
                            TreeNode epn = new TreeNode();
                            epn.Name = string.Format("S{0}E{1}", ep.SeasonNumber, ep.EpisodeNumber);
                            epn.Tag = ep.Id;
                            epn.ToolTipText = epInfo.FullName;
                            n.Nodes.Add(epn);
                        }
                    }
                    else if (IsVideoFolder(di))
                    {
                        item.Type = Entity.Movie;
                    }
                    else
                    {
                        item.Type = Entity.Folder;
                    }

                    if (item.Type != Entity.Movie && item.Type != Entity.Season)
                    {
                        foreach (TreeNode tn in ReadFolder(di.FullName, items))
                            n.Nodes.Add(tn);
                    }
                    items.Add(item);
                    nodes.Add(n);
                }
                catch { }
            }

            foreach (FileInfo fi in dirInfo.GetFiles())
            {
                if (IsVideo(fi.FullName))
                {
                    Item item = new Item();
                    GetTitle(item, Path.GetFileNameWithoutExtension(fi.Name));
                    item.Path = fi.FullName;
                    item.Id = Helper.GetMD5(fi.FullName);
                    item.MetadataLocation = Path.Combine(Path.Combine(Directory.GetParent(fi.FullName).FullName, "metadata"), item.Title);
                    item.DateAdded = fi.CreationTime;
                    TreeNode n = new TreeNode();
                    n.Name = item.Title;
                    n.Tag = item.Id;
                    n.ToolTipText = fi.FullName;
                    item.Type = Entity.Movie;
                    
                    if (TVUtils.IsEpisode(fi.FullName))
                    {
                        item.Type = Entity.Episode;
                        item.MetadataLocation = Path.Combine(Directory.GetParent(item.Path).FullName, "metadata");
                        item.SeasonNumber = TVUtils.SeasonNumberFromEpisodeFile(item.Path);
                        item.EpisodeNumber = TVUtils.EpisodeNumberFromFile(item.Path);
                        if (TVUtils.IsSeriesFolder(dirInfo))
                            item.SeriesName = dirInfo.Name;
                        item.Title = n.Name = string.Format("S{0}E{1}", item.SeasonNumber, item.EpisodeNumber);
                    }                    
                    items.Add(item);
                    nodes.Add(n);
                }
            }


            return nodes;
        }

        static void GetTitle(Item item, string name)
        {
            Regex re = new Regex(@"(?<name>.*)\((?<year>\d{4})\)");
            Match m = re.Match(name);
            if (m.Success)
            {
                item.Title = m.Groups["name"].Value.Trim();
                string year = m.Groups["year"] != null ? m.Groups["year"].Value : null;
                if (year != null)
                {
                    int? y = Int32.Parse(year);
                    if (y.IsValidYear())
                        item.Year = y;
                }
            }
            else
                item.Title = name;
        }

        static bool IsVideoFolder(DirectoryInfo di)
        {
            foreach (DirectoryInfo subdir in di.GetDirectories())
            {
                if (subdir.FullName.ToLower().EndsWith("video_ts") || subdir.FullName.ToLower().EndsWith("bdmv"))
                    return true;
            }
            List<FileInfo> fi = di.GetFiles().ToList();
            int n = fi.FindAll(f => IsVideo(f.Name)).Count;
            return (n > 0 && n < 5) ;
        }

        public static Dictionary<string, bool> perceivedTypeCache = new Dictionary<string, bool>();
        public static bool IsVideo(string filename)
        {
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            // On vérifie si l'extension est présente dans la liste des extensions déclarées
            if (Config.FilmExtension.IndexOf(extension)>0)
                // Si oui, alors le fichier est bien une vidéo
                return true;
            switch (extension)
            {
                // special case so DVD files are never considered videos
                case ".vob":
                case ".bup":
                case ".ifo":
                    return false;
                default:

                    bool isVideo;
                    lock (perceivedTypeCache)
                    {
                        if (perceivedTypeCache.TryGetValue(extension, out isVideo))
                        {
                            return isVideo;
                        }
                    }

                    string pt = null;
                    RegistryKey key = Registry.ClassesRoot;
                    key = key.OpenSubKey(extension);
                    if (key != null)
                    {
                        pt = key.GetValue("PerceivedType") as string;
                    }
                    if (pt == null) pt = "";
                    pt = pt.ToLower();

                    lock (perceivedTypeCache)
                    {
                        perceivedTypeCache[extension] = (pt == "video");
                    }

                    return perceivedTypeCache[extension];
            }

        }

        private static List<string> GetVideoFiles(DirectoryInfo di)
        {
            List<string> videosFiles = new List<string>();

            foreach (FileInfo fi in di.GetFiles())
            {
                if (IsVideo(fi.FullName))
                    videosFiles.Add(fi.FullName);
            }
            return videosFiles;
        }

        public static List<string> GetMBfolders()
        {
            try
            {
                string vfPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.CommonApplicationData), "MediaBrowser");
                vfPath = Path.Combine(vfPath, "StartUpFolder");
                if (!Directory.Exists(vfPath)) return new List<string>();

                List<string> Folders = new List<string>();
                DirectoryInfo di = new DirectoryInfo(vfPath);
                foreach (FileInfo fi in di.GetFiles("*.vf"))
                {
                    using (StreamReader s = new StreamReader(fi.FullName, true))
                    {
                        string line = s.ReadLine();
                        while (line != null)
                        {
                            var colonPos = line.IndexOf(':');
                            if (colonPos <= 0)
                            {
                                continue;
                            }

                            var type = line.Substring(0, colonPos).Trim();
                            var data = line.Substring(colonPos + 1).Trim();

                            if (type == "folder" && Directory.Exists(data))
                            {
                                Folders.Add(data);
                            }
                            line = s.ReadLine();
                        }
                    }
                }
                return Folders;
            }
            catch { return new List<string>(); }
        }

        public static void Rename(Item item, List<Item> items)
        {
            if (!RenameItem(item)) return;
            
            string name = "";
            if (item.Type == Entity.Movie)
                name = Config.MoviePattern;
            else if (item.Type == Entity.Series)
                name = Config.SeriesPattern;
            else if (item.Type == Entity.Season)
                name = Config.SeasonPattern;
            else if (item.Type == Entity.Episode)
                name = Config.EpisodePattern;

            if (!string.IsNullOrEmpty(item.Title)) name = name.Replace("%t", item.Title);
            else name = name.Replace("%t", "");
            if (!string.IsNullOrEmpty(item.OriginalTitle)) name = name.Replace("%o", item.OriginalTitle);
            else name = name.Replace("%o", "");
            if (!string.IsNullOrEmpty(item.SortTitle)) name = name.Replace("%st", item.SortTitle);
            else name = name.Replace("%st", "");
            if (item.Year.IsValidYear()) name = name.Replace("%y", item.Year.ToString());
            else name = name.Replace("%y", "");
            if (!string.IsNullOrEmpty(item.SeriesName)) name = name.Replace("%sn", item.SeriesName);
            else name = name.Replace("%sn", "");
            if (!string.IsNullOrEmpty(item.SeasonNumber)) name = name.Replace("%s", item.SeasonNumber);
            else name = name.Replace("%s", "");
            if (!string.IsNullOrEmpty(item.EpisodeNumber)) name = name.Replace("%e", item.EpisodeNumber);
            else name = name.Replace("%e", "");

            foreach (char lDisallowed in Path.GetInvalidFileNameChars()) name = name.Replace(lDisallowed.ToString(), "");

            if (Directory.Exists(item.Path))
            {
                string newPath = Path.Combine(Directory.GetParent(item.Path).FullName, name);
                if (!Directory.Exists(newPath))
                {
                    try { Directory.Move(item.Path, newPath); }
                    catch (Exception ex) { Logger.ReportException("Error renaming item " + item.Title, ex); return; }
                    ChangePath(item.Path, newPath, items);
                }
            }
            else if (File.Exists(item.Path))
            {
                string newPath = Path.Combine(Directory.GetParent(item.Path).FullName, name + Path.GetExtension(item.Path));
                string srtPath = Path.Combine(Directory.GetParent(item.Path).FullName, Path.GetFileNameWithoutExtension(item.Path) + ".srt");
                string newSrtPath = Path.Combine(Directory.GetParent(item.Path).FullName, name + ".srt");
                if (!File.Exists(newPath))
                {
                    try { File.Move(item.Path, newPath); }
                    catch { return; }
                    item.Path = newPath;
                    item.MetadataLocation = Path.Combine(Directory.GetParent(item.Path).FullName, "metadata");
                    if (File.Exists(srtPath))
                    {
                        try { File.Move(srtPath, newSrtPath); }
                        catch (Exception ex) { Logger.ReportException("Error renaming item " + item.Title, ex); return; }
                    }
                }
            }
        }
           

        static bool RenameItem(Item item)
        {
            switch (item.Type)
            {
                case Entity.Movie: return Config.RenameMovies;
                case Entity.Series: return Config.RenameSeries;
                case Entity.Season: return Config.RenameSeasons;
                case Entity.Episode: return Config.RenameEpisodes;
                default: return false;
            }
        }

        static void ChangePath(string oldPath, string newPath, List<Item> items)
        {
            foreach (Item i in items)
            {
                if (i.Path.Contains(oldPath))
                {
                    i.Path = i.Path.Replace(oldPath, newPath);
                    i.MetadataLocation = i.MetadataLocation.Replace(oldPath, newPath);
                    if (i.ImagesPaths != null)
                    {
                        foreach (Poster p in i.ImagesPaths)
                            p.Image = p.Image.Replace(oldPath, newPath);
                    }
                    if (i.BackdropImagePaths != null)
                    {
                        foreach (Poster p in i.BackdropImagePaths)
                            p.Image = p.Image.Replace(oldPath, newPath);
                    }
                    if (i.BannersPaths != null)
                    {
                        foreach (Poster p in i.BannersPaths)
                            p.Image = p.Image.Replace(oldPath, newPath);
                    } 
                    if (i.TrailerFiles != null)
                    {
                        List<string> trailers = new List<string>();
                        foreach (string s in i.TrailerFiles)
                            trailers.Add(s.Replace(oldPath, newPath));
                        i.TrailerFiles = trailers;
                    }
                }
            }
        }

        public static bool Compare(string path1, string path2)
        {
            if (path1.ToLower() == path2.ToLower()) return true;
            int i = 0, j = 0;
            FileStream f1;
            FileStream f2;

            try
            {
                f1 = new FileStream(path1, FileMode.Open);
                f2 = new FileStream(path2, FileMode.Open);
            }
            catch { return false; }

            try
            {
                do
                {
                    i = f1.ReadByte();
                    j = f2.ReadByte();
                    if (i != j) break;
                } while (i != -1 && j != -1);
            }
            catch { return false; }
            f1.Close();
            f2.Close();
            if (i != j)
                return false;
            else
                return true;
        }

    }
}