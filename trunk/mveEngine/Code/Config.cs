using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Net;

namespace mveEngine
{

    public class Config
    {
        private ConfigData data;

        #region Constructor

        public bool EnableTraceLogging
        {
            get { return this.data.EnableTraceLogging; }
            set { if (this.data.EnableTraceLogging != value) { this.data.EnableTraceLogging = value; Save(); } }
        }

        public bool IsFirstRun
        {
            get { return this.data.IsFirstRun; }
            set { if (this.data.IsFirstRun != value) { this.data.IsFirstRun = value; Save(); } }
        }

        public List<string> RootFolders
        {
            get
            {
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
            get { return this.data.MoviePattern; }
            set { if (this.data.MoviePattern != value) { this.data.MoviePattern = value; Save(); } }
        }

        public string EpisodePattern
        {
            get { return this.data.EpisodePattern; }
            set { if (this.data.EpisodePattern != value) { this.data.EpisodePattern = value; Save(); } }
        }

        public string SeasonPattern
        {
            get { return this.data.SeasonPattern; }
            set { if (this.data.SeasonPattern != value) { this.data.SeasonPattern = value; Save(); } }
        }

        public string SeriesPattern
        {
            get { return this.data.SeriesPattern; }
            set { if (this.data.SeriesPattern != value) { this.data.SeriesPattern = value; Save(); } }
        }

        public string WindowGeometry
        {
            get { return this.data.WindowGeometry; }
            set { if (this.data.WindowGeometry != value) { this.data.WindowGeometry = value; Save(); } }
        }

        public string ColorTheme
        {
            get { return this.data.ColorTheme; }
            set { if (this.data.ColorTheme != value) { this.data.ColorTheme = value; Save(); } }
        }

        public bool UseTreeColor
        {
            get { return this.data.UseTreeColor; }
            set { if (this.data.UseTreeColor != value) { this.data.UseTreeColor = value; Save(); } }
        }

        public string TrailerPath
        {
            get { return this.data.TrailerPath; }
            set { if (this.data.TrailerPath != value) { this.data.TrailerPath = value; Save(); } }
        }

        public bool RemoveAccents
        {
            get { return this.data.RemoveAccents; }
            set { if (this.data.RemoveAccents != value) { this.data.RemoveAccents = value; Save(); } }
        }

        #endregion

        private static Config _instance = new Config();
        public static Config Instance
        {
            get
            {
                return _instance;
            }
        }

        bool isValid;
        private Config()
        {
            isValid = Load();
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
                this.data.Save();
        }

        public void Reset()
        {
            lock (this)
            {
                this.data = new ConfigData();
                Save();
            }
        }

        private bool Load()
        {
            try
            {
                this.data = ConfigData.FromFile(ApplicationPaths.ConfigFile);
                return true;
            }
            catch (Exception ex)
            {
                /*DialogResult r = ev.Dialog(ex.Message + "\n" + Application.CurrentInstance.StringData("ConfigErrorDial"), Application.CurrentInstance.StringData("ConfigErrorCapDial"), DialogButtons.Yes | DialogButtons.No, 600, true);
                if (r == DialogResult.Yes)
                {
                    this.data = new ConfigData();
                    Save();
                    return true;
                }
                else*/
                    return false;
            }
        }

    }



}