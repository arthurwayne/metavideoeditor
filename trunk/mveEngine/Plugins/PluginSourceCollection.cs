using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections.ObjectModel;
using System.Xml;
using System.IO;

namespace mveEngine
{
    public class PluginSourceCollection
    {

        public static PluginSourceCollection Instance = new PluginSourceCollection();

        public IEnumerable<IPlugin> AvailablePlugins
        {
            get
            {
                List<IPlugin> plugins = new List<IPlugin>();
                //foreach (var source in this)
                {
                    plugins.AddRange(DiscoverRemotePlugins());
                }
                return plugins;
            }
        }

        public List<IPlugin> PluginsToUpdate
        {
            get
            {
                List<IPlugin> available = (List<IPlugin>)AvailablePlugins;
                List<IPlugin> ToUpdate = new List<IPlugin>();
                foreach (IPlugin plugin in Kernel.Instance.Plugins)
                {
                    IPlugin p = available.Find(q => q.Name == plugin.Name);
                    if (p != null && p.Version > plugin.Version && p.RequiredMVEVersion <= Updater.CurrentVersion)
                    {
                        ToUpdate.Add(p);
                    }
                }
                return ToUpdate;
            }
        }

        private List<IPlugin> DiscoverRemotePlugins()
        {
            var list = new List<IPlugin>();
            XmlDocument doc = Updater.DownloadUpdateFile();
            
            if (doc != null)
            {
                foreach (XmlNode pluginRoot in doc.SelectNodes("Update/Plugin"))
                {
                    list.Add(new RemotePlugin()
                    {
                        Description = pluginRoot.SafeGetString("Description"),
                        FileName = pluginRoot.SafeGetString("Filename"),
                        Version = new System.Version(pluginRoot.SafeGetString("Version")),
                        RequiredMVEVersion = new Version(pluginRoot.SafeGetString("RequiredMVEVersion") ?? "2.0.0"),
                        Name = pluginRoot.SafeGetString("Name"),
                        Type = (PluginType)Enum.Parse(typeof(PluginType), pluginRoot.SafeGetString("Type"))
                    });
                }
            }
            else
            {
                Logger.ReportWarning("There appears to be no network connection. Plugin can not be installed.");
            }
            return list;
        }

        private string GetPath(string source)
        {
            var index = source.LastIndexOf("\\");
            if (index <= 0)
            {
                index = source.LastIndexOf("/");
            }
            return source.Substring(0, index);
        }
    }
}
