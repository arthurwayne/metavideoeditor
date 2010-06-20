using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Xml.Serialization;
using System.IO;
using System.Globalization;

namespace mveEngine
{
    [Serializable]
    public class BaseStrings
    {
        const string VERSION = "1.0000";
        const string ENFILE = "strings-en.xml";

        public string Version = VERSION; //this is used to see if we have changed and need to re-save

        //these are our strings keyed by property name
        //Main Window
        public string HomeMW = "Home";
        public string SettingsMW = "Settings";
        public string ThemeMW = "Theme";
        public string AboutMW = "About";
        public string SearchMW = "Search";
        public string AutoMW = "Auto";
        public string RestoreMW = "Restore";
        public string SaveMW = "Save";
        public string SelectedItemMW = "Selected item";
        public string CheckedItemsMW = "Checked items";
        public string ModifiedItemsMW = "Modified items";
        public string RebuildMW = "Rebuild";
        public string SettingsDisplayMW = "Settings & Display";
        public string MediaCollectionMW = "Media Collection";
        public string AvailableUpdateMW = "{0} update available";
        public string AvailableUpdatesMW = "{0} updates available";
        public string NoActionMW = "No action in progress";
        public string ActionMW = "{0} action in progress";
        public string ActionsMW = "{0} actions in progress";
        public string CurrentActionsMW = "Current actions";
        public string EditMW = "Edit";
        public string OpenMW = "Open";
        public string DeleteMW = "Delete";        

        //Theme
        public string AzurTheme = "Azur";
        public string MetalTheme = "Metal";
        public string DarkTheme = "Dark";
        public string NatureTheme = "Nature";
        public string DawnTheme = "Dawn";
        public string CornTheme = "Corn";
        public string ChocolateTheme = "Chocolate";
        public string NavyTheme = "Navy";
        public string IceTheme = "Ice";
        public string VanillaTheme = "Vanilla";
        public string CanelaTheme = "Canela";
        public string CakeTheme = "Cake";

        //Overview Tab
        public string TitleOvTab = "Overview";

        //General Tab
        public string TitleGeTab = "General";

        //Actors Tab
        public string TitleActTab = "Actors";
        public string EditNameActTab = "Edit name";
        public string EditRoleActTab = "Edit role";
        public string ChangeImageActTab = "Change image";
        public string AddActorActTab = "Double-click here to add an actor";

        //Crew Tab
        public string TitleCreTab = "Crew";
        public string EditActivityCreTab = "Edit activity";
        public string AddCrewMemberCreTab = "Double-click here to add a crew member";

        //Trailer Tab
        public string TitleTraTab = "Trailers";
        public string PlayTraTab = "Play";
        public string DownloadTraTab = "Download";
        public string NoTrailerTraTab = "No trailer";
        public string AddTrailerTraTab = "Double-click here to add a trailer";

        //Posters Tab
        public string TitlePoTab = "Posters";
        public string LoadingPoTab = "Loading";
        public string NoImagePoTab = "No image";
        public string CheckPoTab = "Check";
        public string UnCheckPoTab = "Uncheck";

        //Backdrops Tab
        public string TitleBdTab = "Backdrops";

        //Banners Tab
        public string TitleBaTab = "Banners";
        public string NoBannerBaTab = "No banner";
        public string AddBannerBaTab = "Double-click or drag a file here to add a banner";

        //Genres Tab
        public string TitleGenTab = "Genres";
        public string NoGenreGenTab = "No genre";
        public string AddGenreGenTab = "Double-click here to add a genre";

        //Studios Tab
        public string TitleStTab = "Studios";
        public string NoStudioStTab = "No studio";
        public string AddStudioStTab = "Double-click here to add a studio";

        //Countries Tab
        public string TitleCoTab = "Countries";
        public string NoCountryCoTab = "No country";
        public string AddCountryCoTab = "Double-click here to add a country";

        //TagLines Tab
        public string TitleTagTab = "Taglines";
        public string NoTaglineTagTab = "No tagline";
        public string AddTaglineTagTab = "Double-click here to add a tagline";

        //SpalshScreen
        public string InitializeComponentsSS = "Initialize Components";
        public string LoadingCacheSS = "Loading cache";

        //Search Window
        public string SearchSW = "Search";
        public string ResultsSW = "Results";
        public string NoResultW = "No result";
        public string ProvidedBy = "Provided by";

        //Async
        public string RefreshLibraryAsync = "Refresh Library";
        public string CheckUpdatesAsync = "Checking for updates";
        public string DownloadAsync = "Fetch";

        //MessageBox
        public string CloseConfirmMess = "Some actions are currently in progress.\nAre you sure you want to exit?";
        public string NoSelectedItemMess = "No selected item!";
        public string NoCheckedItemMess = "No checked item!";
        public string NoModifiedItemMess = "No modified item!";
        public string PathExistMess = "Path {0} doesn't exists!";
        public string NoResultMess = "No result";
        public string SelectItemsMess = "Please select one or more items";
        public string DeleteCacheMess = "This will clear the cache.\nAre you sure you want to continue?";
        public string SelectFolderMess = "Please select at least one folder";
        public string FolderModMess = "Folder list has been modified.\nNon saved data will be lost.\nDo you want to continue?";
        public string PlugInstMess = "This plugin is already installed";
        public string RemovePlugMess = "Do you really want to remove this plugin?";

        //Fields strings
        public string TitleStr = "Title";
        public string OriginalTitleStr = "Original title";
        public string SortTitleStr = "Sort title";
        public string PosterStr = "Posters";
        public string BackdropStr = "Backdrops";
        public string BannersStr = "Banners";
        public string TrailersStr = "Trailers";
        public string YearStr = "Production Year";
        public string RuntimeStr = "Runtime";
        public string RatingStr = "Rating";
        public string MpaaStr = "MPAA Rating";
        public string OverviewStr = "Overview";
        public string RatioStr = "Aspect Ratio";
        public string CastingStr = "Casting";
        public string GenresStr = "Genres";
        public string StudiosStr = "Studios";
        public string CountriesStr = "Coutries";
        public string TaglinesStr = "Taglines";
        public string MediaTypeStr = "Media Type";
        public string SeriesStr = "Series";
        public string SeasonStr = "Season";
        public string EpisodeStr = "Episode";
        public string MoviesStr = "Movies";
        public string DateAddedStr = "Date Added";
        public string WatchedStr = "Watched";
        public string YesStr = "Yes";
        public string NoStr = "No";
        public string OKStr = "OK";
        public string CancelStr = "Cancel";
        public string VersionStr = "Version";        

        public string RateConvert = "{0} on 10";

        //About
        public string CurrentVersionAb = "Current version:";
        public string DownloadAb = "Download:";
        public string FrSupportAb = "French support:";

        //Settings
        public string FoldersSet = "Folders";
        public string GeneralSet = "General";
        public string DisplaySet = "Display";
        public string RenamingSet = "Renaming";
        public string LocalSet = "Local";
        public string ProvidersSet = "Providers";
        public string SaversSet = "Savers";
        public string PluginsSet = "Plugins";
        public string AnalyzedFoldersSet = "Analyzed Folders";
        public string GeneralSettingsSet = "General Settings";
        public string DisplaySettingsSet = "Display Settings";
        public string RenameItemsSet = "Renaming Items";
        public string LocalPluginsSet = "Local Plugins";
        public string ProvidersPluginsSet = "Providers Plugins";
        public string SaversPluginsSet = "Savers Plugins";
        public string AddRemovePluginsSet = "Add or remove plugins";
        public string AddSet = "Add";
        public string RemoveSet = "Remove";
        public string MBFoldersSet = "MediaBrowser Folders";
        public string DebugSet = "Debug";
        public string ActDebugSet = "Activate debug";
        public string DataSet = "Data";
        public string AddMissingDataSet = "Only add missing data";
        public string ExtensionsSet = "Extensions";
        public string VidExtSet = "Video extensions";
        public string BdWidthSet = "Min Backdrop Width (pixel)";
        public string BDToSaveSet = "Backdrops to save";
        public string BackgroundImgSet = "Detailled Panel Background";
        public string SaveGeomSet = "Save Window Geometry";
        public string TreeColorsSet = "Tree colors";
        public string UserColorsSet = "Use colors in the tree";
        public string RedColorSet = "Red: items with empty data";
        public string BlueColorSet = "Blue: items with a few data";
        public string BlackColorSet = "Black: items with complete data";
        public string BoldColorSet = "Bold: modified items";
        public string RenameMoviesSet = "Enable movies renaming";
        public string RenameSeriesSet = "Enable series renaming";
        public string RenameSeasonSet = "Enable seasons renaming";
        public string RenameEpisodeSet = "Enable episode renaming";
        public string PatternSet = "Patern";
        public string ValuesSet = "Values";
        public string SeriesNameSet = "Series name";
        public string SeasonNumSet = "Season number";
        public string EpisodeNum = "Episode number";
        public string TypeSet = "Type";
        public string DescSet = "Description";
        public string AvailablePluginsSet = "Available plugins";
        public string InstalledPluginsSet = "Installed plugins";
        public string AllSet = "All (not recommended)";
        public string PluginOptionsSet = "Plugin options";

        //Updates
        public string UpdatesUp = "Updates";
        public string UpdateUp = "Update";
        public string DownloadUp = "Download updates";
        public string PluginInstallUp = "Pluings install";

        //Wizard
        public string TitleWiz = "Configuration wizard";
        public string NextWiz = "Next";
        public string BackWiz = "Back";
        public string FinishWiz = "Finish";
        public string WelcomeWiz = "Welcome to MetaVideoEditor";
        public string WelcSubWiz = "This wizard will help you to configure MetaVideoEditor for the first time";
        public string MCSoftWiz = "Media Center software";
        public string MCSoftQuestionWiz = "What media center software do you use?";
        public string DVDlibWiz = "Windows DVD Library";
        public string FoldersConfWiz = "Folders configuration";
        public string FoldersQuestionWiz = "Select folders containing your video files";
        public string ProvidersWiz = "Providers";
        public string ProvQuestionWiz = "Select the providers you want to use";
        public string QuitWiz = "Do you really want to quit the configuration wizard?";



        public BaseStrings() //for the serializer
        {
        }

        public static BaseStrings FromFile(string file)
        {
            BaseStrings s = new BaseStrings();
            XmlSettings<BaseStrings> settings = XmlSettings<BaseStrings>.Bind(s, file);

            Logger.ReportInfo("Using String Data from " + file);

            if (VERSION != s.Version && Path.GetFileName(file).ToLower() == ENFILE)
            {
                //only re-save the english version as that is the one defined internally
                File.Delete(file);
                s = new BaseStrings();
                settings = XmlSettings<BaseStrings>.Bind(s, file);
            }
            return s;
        }
    }
}
