using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace mveEngine
{
    [Serializable]
    public class ConfigData
    {
        public bool EnableTraceLogging = false;
        public bool IsFirstRun = true;
        public List<string> RootFolders = new List<string>();
        public bool AddMissingData = true;
        public DateTime LastUpdateCheck = DateTime.Now;
        public int MinBackdropWidth = 0;
        public int MaxBdSaved = 3;
        public bool useProxy = false;
        public string ProxyAdress = null;
        public string ProxyPort = null;
        public string ProxyUser = null;
        public string ProxyPass = null;
        public string FilmExtension = ".rmvb,.mov,.avi,.mpg,.mpeg,.wmv,.mp4,.mkv,.divx,.dvr-ms,.ogm,.iso,.flv";
        public string Background = "wmc";
        public bool FullscreenMode = true;
        public bool RenameMovies = false;
        public bool RenameSeries = false;
        public bool RenameSeasons = false;
        public bool RenameEpisodes = false;
        public string MoviePattern = "%t (%y)";
        public string SeriesPattern = "%t";
        public string EpisodePattern = "%sn - S%sE%e - %t";
        public string SeasonPattern = "Saison %s";
        public string WindowGeometry = "";
        public string ColorTheme = "Azur";
        public bool UseTreeColor = true;
        public string TrailerPath = Path.Combine(ApplicationPaths.AppConfigPath, "Trailers");
        public bool RemoveAccents = false;

        // for the serializer
        public ConfigData()
        {
        }


        public ConfigData(string file)
        {
            this.file = file;
            this.settings = XmlSettings<ConfigData>.Bind(this, file);
        }

        [SkipField]
        string file;

        [SkipField]
        XmlSettings<ConfigData> settings;


        public static ConfigData FromFile(string file)
        {
            return new ConfigData(file);
        }

        public void Save()
        {
            this.settings.Write();
        } 

    }
}