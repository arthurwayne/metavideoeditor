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
    public class DataConfig
    {
        public bool EnableTraceLogging = false;     
        public List<string> RootFolders = new List<string>();
        public bool AddMissingData = false;
        public DateTime LastUpdateCheck = DateTime.Now;
        public int CheckUpdateDays = 10;
        public int MinBackdropWidth = 0;
        public int MaxBdSaved = 3;
        public bool useProxy = false;
        public string ProxyAdress = null;
        public string ProxyPort = null;
        public string ProxyUser = null;
        public string ProxyPass = null;
        public string FilmExtension = null;
        public string Background = "backdrop";
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
        // for the serializer
        public DataConfig()
        {
        }

        public DataConfig(string file)
        {
            this.file = file;
        }

        string file;

        public static DataConfig FromFile(string file)
        {

            if (!File.Exists(file))
            {
                DataConfig d = new DataConfig(file);
                d.Save();
            }
            return (DataConfig)Serializer.Deserialization(file);

        }

        private void Save()
        {
            Save(file);
        }

        /// <summary>
        /// Write current config to file
        /// </summary>
        public void Save(string file)
        {
           Serializer.Serialization(file, this);           
        }
    }

    public class Configuration
    {
        private DataConfig data;
        private DataConfig DefaultData = new DataConfig();

        #region Constructor  
      
        public bool EnableTraceLogging
        {
            get { return this.data.EnableTraceLogging; }
            set { if (this.data.EnableTraceLogging != value) { this.data.EnableTraceLogging = value; Save(); } }
        }

        public List<string> RootFolders
        {
            get
            {
                if (this.data.RootFolders == null) { this.data.RootFolders = DefaultData.RootFolders; Save(); } 
                return this.data.RootFolders;
            }
            set { if (this.data.RootFolders != value) { this.data.RootFolders = value; Save(); } }
        }

        public bool AddMissingData
        {
            get { return this.data.AddMissingData; }
            set { if (this.data.AddMissingData != value) { this.data.AddMissingData = value; Save(); } }
        }

        public DateTime LastUpdateCheck
        {
            get { return this.data.LastUpdateCheck; }
            set { if (this.data.LastUpdateCheck != value) { this.data.LastUpdateCheck = value; Save(); } }
        }

        public int CheckUpdateDays
        {
            get { if (this.data.CheckUpdateDays == 0) this.data.CheckUpdateDays = DefaultData.CheckUpdateDays; return this.data.CheckUpdateDays; }
            set { if (this.data.CheckUpdateDays != value) { this.data.CheckUpdateDays = value; Save(); } }
        }

        public int MinBackdropWidth
        {
            get { return this.data.MinBackdropWidth; }
            set { if (this.data.MinBackdropWidth != value) { this.data.MinBackdropWidth = value; Save(); } }
        }

        public int MaxBdSaved
        {
            get { return this.data.MaxBdSaved; }
            set { if (this.data.MaxBdSaved != value) { this.data.MaxBdSaved = value; Save(); } }
        }

        public bool useProxy
        {
            get { return this.data.useProxy; }
            set { if (this.data.useProxy != value) { this.data.useProxy = value; Save(); } }
        }

        public string ProxyAdress
        {
            get { return this.data.ProxyAdress; }
            set { if (this.data.ProxyAdress != value) { this.data.ProxyAdress = value; Save(); } }
        }

        public string ProxyPort
        {
            get { return this.data.ProxyPort; }
            set { if (this.data.ProxyPort != value) { this.data.ProxyPort = value; Save(); } }
        }
        public string ProxyUser
        {
            get { return this.data.ProxyUser; }
            set { if (this.data.ProxyUser != value) { this.data.ProxyUser = value; Save(); } }
        }
        public string ProxyPass
        {
            get { return this.data.ProxyPass; }
            set { if (this.data.ProxyPass != value) { this.data.ProxyPass = value; Save(); } }
        }

        public string FilmExtension
        {
            get { return this.data.FilmExtension; }
            set { if (this.data.FilmExtension != value) { this.data.FilmExtension = value; Save(); } }
        }

        public string Background
        {
            get { return this.data.Background; }
            set { if (this.data.Background != value) { this.data.Background = value; Save(); } }
        }

        public bool FullscreenMode
        {
            get { return this.data.FullscreenMode; }
            set { if (this.data.FullscreenMode != value) { this.data.FullscreenMode = value; Save(); } }
        }

        public bool RenameMovies
        {
            get { return this.data.RenameMovies; }
            set { if (this.data.RenameMovies != value) { this.data.RenameMovies = value; Save(); } }
        }

        public bool RenameSeries
        {
            get { return this.data.RenameSeries; }
            set { if (this.data.RenameSeries != value) { this.data.RenameSeries = value; Save(); } }
        }

        public bool RenameSeasons
        {
            get { return this.data.RenameSeasons; }
            set { if (this.data.RenameSeasons != value) { this.data.RenameSeasons = value; Save(); } }
        }

        public bool RenameEpisodes
        {
            get { return this.data.RenameEpisodes; }
            set { if (this.data.RenameEpisodes != value) { this.data.RenameEpisodes = value; Save(); } }
        }

        public string MoviePattern
        {
            get { if (string.IsNullOrEmpty(this.data.MoviePattern)) this.data.MoviePattern = DefaultData.MoviePattern; return this.data.MoviePattern; }
            set { if (this.data.MoviePattern != value) { this.data.MoviePattern = value; Save(); } }
        }

        public string EpisodePattern
        {
            get { if (string.IsNullOrEmpty(this.data.EpisodePattern)) this.data.EpisodePattern = DefaultData.EpisodePattern; return this.data.EpisodePattern; }
            set { if (this.data.EpisodePattern != value) { this.data.EpisodePattern = value; Save(); } }
        }

        public string SeasonPattern
        {
            get { if (string.IsNullOrEmpty(this.data.SeasonPattern)) this.data.SeasonPattern = DefaultData.SeasonPattern; return this.data.SeasonPattern; }
            set { if (this.data.SeasonPattern != value) { this.data.SeasonPattern = value; Save(); } }
        }

        public string SeriesPattern
        {
            get { if (string.IsNullOrEmpty(this.data.SeriesPattern)) this.data.SeriesPattern = DefaultData.SeriesPattern; return this.data.SeriesPattern; }
            set { if (this.data.SeriesPattern != value) { this.data.SeriesPattern = value; Save(); } }
        }

        public string WindowGeometry
        {
            get { if (string.IsNullOrEmpty(this.data.WindowGeometry)) this.data.WindowGeometry = DefaultData.WindowGeometry; return this.data.WindowGeometry; }
            set { if (this.data.WindowGeometry != value) { this.data.WindowGeometry = value; Save(); } }
        }

        public string ColorTheme
        {
            get { if (string.IsNullOrEmpty(this.data.ColorTheme)) this.data.ColorTheme = DefaultData.ColorTheme; return this.data.ColorTheme; }
            set { if (this.data.ColorTheme != value) { this.data.ColorTheme = value; Save(); } }
        }

        public bool UseTreeColor
        {
            get { return this.data.UseTreeColor; }
            set { if (this.data.UseTreeColor != value) { this.data.UseTreeColor = value; Save(); } }
        }

        #endregion

        private static Configuration _instance = new Configuration();
        public static Configuration Instance
        {
            get
            {
                return _instance;
            }
        }

        bool isValid;
        public Configuration()
        {
            isValid = Load();
            if (useProxy)
            {
                ICredentials cred;
                cred = new NetworkCredential(ProxyUser, ProxyPass);
                System.Net.GlobalProxySelection.Select = new System.Net.WebProxy("http://"+ProxyAdress+":"+ProxyPort,true,null, cred);
            }
            if (string.IsNullOrEmpty(FilmExtension))
            {
                FilmExtension = ".rmvb,.mov,.avi,.mpg,.mpeg,.wmv,.mp4,.mkv,.divx,.dvr-ms,.ogm,.iso";
            }
        }

        public bool IsValid
        {
            get
            {
                return isValid;
            }
        }

        private void Save()
        {
            lock (this)
                this.data.Save(ApplicationPaths.ConfigFile);
        }

        private bool Load()
        {
            try
            {
                this.data = DataConfig.FromFile(ApplicationPaths.ConfigFile);
                return true;
            }
            catch
            {
                this.data = new DataConfig();
                Save();
                return true;
            }
        }

    }
}