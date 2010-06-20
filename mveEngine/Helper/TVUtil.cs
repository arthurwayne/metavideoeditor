using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;
using System.Xml;
using System.Diagnostics;
using System.Web;

namespace mveEngine
{
    public static class TVUtils
    {

        private static readonly Regex[] episodeExpressions = new Regex[] {
                        new Regex(@".*\\[s|S]?(?<seasonnumber>\d{1,2})[x|X](?<epnumber>\d{1,3})[^\\]*$"),   // 01x02 blah.avi S01x01 balh.avi
                        new Regex(@".*\\[s|S](?<seasonnumber>\d{1,2})x?[e|E](?<epnumber>\d{1,3})[^\\]*$"), // S01E02 blah.avi, S01xE01 blah.avi
                        new Regex(@".*\\(?<seriesname>[^\\]*)[s|S]?(?<seasonnumber>\d{1,2})[x|X](?<epnumber>\d{1,3})[^\\]*$"),   // 01x02 blah.avi S01x01 balh.avi
                        new Regex(@".*\\(?<seriesname>[^\\]*)[s|S](?<seasonnumber>\d{1,2})[x|X|\.]?[e|E](?<epnumber>\d{1,3})[^\\]*$") // S01E02 blah.avi, S01xE01 blah.avi
        };
        /// <summary>
        /// To avoid the following matching movies they are only valid when contained in a folder which has been matched as a being season
        /// </summary>
        private static readonly Regex[] episodeExpressionsInASeasonFolder = new Regex[] {
                        new Regex(@".*\\(?<epnumber>\d{1,2})\s?-\s?[^\\]*$"), // 01 - blah.avi, 01-blah.avi
                        new Regex(@".*\\(?<epnumber>\d{1,2})[^\d\\]*[^\\]*$"), // 01.avi, 01.blah.avi "01 - 22 blah.avi" 
                        new Regex(@".*\\(?<seasonnumber>\d)(?<epnumber>\d{1,2})[^\d\\]+[^\\]*$"), // 01.avi, 01.blah.avi
                        new Regex(@".*\\\D*\d+(?<epnumber>\d{2})") // hell0 - 101 -  hello.avi

        };

        private static readonly Regex[] seasonPathExpressions = new Regex[] {
                        new Regex(@".+\\[s|S]eason\s?(?<seasonnumber>\d{1,2})$"),
                        new Regex(@".+\\[s|S]æson\s?(?<seasonnumber>\d{1,2})$"),
                        new Regex(@".+\\[t|T]emporada\s?(?<seasonnumber>\d{1,2})$"),
                        new Regex(@".+\\[s|S]aison\s?(?<seasonnumber>\d{1,2})[^\\]*$"),
                        new Regex(@".+\\[s|S]taffel\s?(?<seasonnumber>\d{1,2})$"),
                        new Regex(@".+\\[s|S](?<seasonnumber>\d{1,2})$"),
                        new Regex(@".+\\[s|S]eason\s?(?<seasonnumber>\d{1,2})[^\\]*$")

        };

        public static bool IsEpisode(string fullPath)
        {
            bool isInSeason = IsSeasonFolder(Path.GetDirectoryName(fullPath));
            if (isInSeason)
                return true;
            else if (EpisodeNumberFromFile(fullPath, isInSeason) != null)
                return true;
            return false;
        }

        /// <summary>
        /// Takes the full path and filename
        /// </summary>
        /// <param name="filename"></param>
        /// <returns></returns>
        public static string EpisodeNumberFromFile(string fullPath)
        {
            return EpisodeNumberFromFile(fullPath, IsSeasonFolder(Path.GetDirectoryName(fullPath)));
        }

        public static string EpisodeNumberFromFile(string fullPath, bool isInSeason)
        {

            string fl = fullPath.ToLower();
            foreach (Regex r in episodeExpressions)
            {
                Match m = r.Match(fl);
                if (m.Success)
                    return m.Groups["epnumber"].Value;
            }
            if (isInSeason)
            {
                foreach (Regex r in episodeExpressionsInASeasonFolder)
                {
                    Match m = r.Match(fl);
                    if (m.Success)
                        return m.Groups["epnumber"].Value;
                }

            }

            return null;
        }

        public static bool IsSeasonFolder(string path)
        {
            foreach (Regex r in seasonPathExpressions)
                if (r.IsMatch(path.ToLower()))
                    return true;
            return false;
        }

        public static bool IsSeriesFolder(DirectoryInfo di)
        {
            string path = di.FullName;
            string[] files = Directory.GetFiles(path);
            
            if (IsSeasonFolder(path))
                return false;

            int i = 0;

            foreach (DirectoryInfo subdi in di.GetDirectories())
            {
                string folder = subdi.FullName;
                if (IsSeasonFolder(folder))
                    return true; // we have found at least one season folder
                else
                    i++;
                if (i >= 3)
                    return false; // a folder with more than 3 non-season folders in will not becounted as a series
            }

            if (files == null)
                files = Directory.GetFiles(path);
            foreach (string file in files)
            {
                if (EpisodeNumberFromFile(file, false) != null)
                    return true;
            }
            return false;
        }

        public static string SeasonNumberFromEpisodeFile(string fullPath)
        {
            string fl = fullPath.ToLower();
            foreach (Regex r in episodeExpressions)
            {
                Match m = r.Match(fl);
                if (m.Success)
                {
                    Group g = m.Groups["seasonnumber"];
                    if (g != null)
                        return g.Value;
                    else
                        return null;
                }
            }
            return null;
        }

        /// <summary>
        /// Takes the single folder name not the full path
        /// </summary>
        /// <param name="folder"></param>
        /// <returns></returns>
        public static string SeasonNumberFromFolderName(string fullpath)
        {
            string p = fullpath.ToLower();
            foreach (Regex r in seasonPathExpressions)
            {
                Match m = r.Match(p);
                if (m.Success)
                    return m.Groups["seasonnumber"].Value;
            }
            return null;
        }   
        
    }
}