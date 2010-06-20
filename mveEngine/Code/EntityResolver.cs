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

    public class EntityResolver
    {
        static Configuration Config
        {
            get { return Configuration.Instance; }
        }

        public static List<Item> GetChildren(Item item)
        {
            Logger.ReportInfo("analyze " + item.Path);
            List<Item> children = new List<Item>();
            if (!Directory.Exists(item.Path))
                return null;
            DirectoryInfo dirInfo = new DirectoryInfo(item.Path);

            DirectoryInfo[] dis = dirInfo.GetDirectories();
            Array.Sort(dis, new DirectorySort());
            foreach (DirectoryInfo di in dis)
            {
                if ((di.Attributes & FileAttributes.Hidden) == FileAttributes.Hidden || di.Attributes == FileAttributes.System || di.Name.EndsWith("$RECYCLE.BIN"))
                    continue;
                try
                {
                    var child = new Item();

                    if (TVUtils.IsSeriesFolder(di))
                    {
                        child = new Folder(di.FullName);
                        child.Type = Entity.Series;
                    }
                    else if (TVUtils.IsSeasonFolder(di.FullName))
                    {
                        child = new Folder(di.FullName);
                        child.Type = Entity.Season;
                        child.SeriesName = di.Parent.Name;
                        child.SeasonNumber = TVUtils.SeasonNumberFromFolderName(di.FullName);
                    }
                    else if (IsVideoFolder(di))
                    {
                        child = new Item();
                        child.Type = Entity.Movie;
                    }
                    else
                    {
                        child = new Folder(di.FullName);
                        child.Type = Entity.Folder;
                    }
                    child.Title = di.Name;
                    child.Path = di.FullName;
                    child.Id = Helper.GetMD5(di.FullName);
                    child.MetadataLocation = child.Path;
                    children.Add(child);
                }
                catch (Exception ex) { Logger.ReportException("Error while analizing " + item.Path, ex); }
            }
            
            foreach (FileInfo fi in dirInfo.GetFiles())
            {
                if (IsVideo(fi.FullName))
                {
                    Item child = new Item();
                    child.Title = Path.GetFileNameWithoutExtension(fi.Name);
                    child.Path = fi.FullName;
                    child.Id = Helper.GetMD5(fi.FullName);
                    child.MetadataLocation = Path.Combine(Path.Combine(Directory.GetParent(fi.FullName).FullName, "metadata"), child.Title);

                    child.Type = Entity.Movie;

                    if (TVUtils.IsEpisode(fi.FullName))
                    {
                        child.Type = Entity.Episode;
                        child.MetadataLocation = Path.Combine(Directory.GetParent(child.Path).FullName, "metadata");
                        child.SeasonNumber = TVUtils.SeasonNumberFromEpisodeFile(child.Path);
                        child.EpisodeNumber = TVUtils.EpisodeNumberFromFile(child.Path);
                        if (TVUtils.IsSeriesFolder(dirInfo))
                            child.SeriesName = dirInfo.Name;
                    }
                    children.Add(child);
                }
            }


            return children;
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
            return (n > 0 && n < 5);
        }

        public static Dictionary<string, bool> perceivedTypeCache = new Dictionary<string, bool>();
        public static bool IsVideo(string filename)
        {
            string extension = System.IO.Path.GetExtension(filename).ToLower();
            // On vérifie si l'extension est présente dans la liste des extensions déclarées
            if (Config.FilmExtension.IndexOf(extension) > 0)
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

    }
}