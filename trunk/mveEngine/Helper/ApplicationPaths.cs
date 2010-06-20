using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace mveEngine
{
    public static class ApplicationPaths
    {
        static Dictionary<string, string> pathMap;

        static string[,] tree = { 
                    { "AppConfigPath",       "app_data",         "MetaVideoEditor"  }, 
                    { "AppCachePath",        "AppConfigPath",    "Cache"         },
                    { "AppImagePath",        "AppConfigPath",    "ImageCache"},
                    { "AppDataPath",         "AppConfigPath",    "Data"         },
                    { "AppPluginPath",       "AppConfigPath",    "Plugins" },
                    { "AppLogPath",          "AppConfigPath",    "Logs"},
                    { "AppLocalizationPath", "AppConfigPath",    "Localization" }
            };


        static ApplicationPaths()
        {
            pathMap = new Dictionary<string, string>();
            pathMap["app_data"] = System.Environment.GetFolderPath(System.Environment.SpecialFolder.CommonApplicationData);

            BuildTree();
        }

        static void BuildTree()
        {
            for (int i = 0; i <= tree.GetUpperBound(0); i++)
            {
                var e = Path.Combine(pathMap[tree[i, 1]], tree[i, 2]);
                if (!Directory.Exists(e))
                {
                    Directory.CreateDirectory(e);
                }
                pathMap[tree[i, 0]] = e;
            }
        }

        public static string AppLogPath
        {
            get
            {
                return pathMap["AppLogPath"];
            }
        }

        public static string AppPluginPath
        {
            get
            {
                return pathMap["AppPluginPath"];
            }
        }

        public static string AppImagePath
        {
            get
            {
                return pathMap["AppImagePath"];
            }
        }

        public static string AppConfigPath
        {
            get
            {
                return pathMap["AppConfigPath"];
            }
        }

        public static string AppCachePath
        {
            get
            {
                return pathMap["AppCachePath"];
            }
        }

        public static string AppDataPath
        {
            get
            {
                return pathMap["AppDataPath"];
            }
        }

        public static string AppLocalizationPath
        {
            get
            {
                return pathMap["AppLocalizationPath"];
            }
        }

        public static string ConfigFile
        {
            get
            {
                var path = AppConfigPath;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return Path.Combine(path, "config.xml");
            }
        }

        public static string UpdateFile
        {
            get
            {
                var path = AppConfigPath;
                if (!Directory.Exists(path))
                    Directory.CreateDirectory(path);
                return Path.Combine(path, "update.xml");
            }
        }

    }
}
