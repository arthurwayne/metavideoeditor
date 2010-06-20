using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace mveEngine
{
    public class Plugin : IPlugin
    {
        string filename;
        Assembly assembly;
        IPlugin pluginInterface;

        internal Plugin(string filename) {
            this.filename = filename;
            assembly = Assembly.Load(System.IO.File.ReadAllBytes(filename));
            pluginInterface = FindPluginInterface(assembly);
        }

        public static Plugin FromFile(string filename, bool forceShadow)
        {
            return new Plugin(filename);
        }

        public IPlugin FindPluginInterface(Assembly assembly)
        {

            IPlugin pluginInterface = null;

            var plugin = assembly.GetTypes().Where(type => typeof(IPlugin).IsAssignableFrom(type)).FirstOrDefault();
            if (plugin != null)
            {
                try
                {
                    pluginInterface = plugin.GetConstructor(System.Type.EmptyTypes).Invoke(null) as IPlugin;
                }
                catch 
                {
                    throw;
                }
            }

            if (pluginInterface == null)
            {
                throw new ApplicationException("The following assembly is not a valid Plugin : " + assembly.FullName);
            }

            return pluginInterface;
        }

        public void Delete()
        {
            System.IO.File.Delete(filename);
        }

        public void Init(Kernel config)
        {
            pluginInterface.Init(config); 
        }
        public Guid Id { get { return pluginInterface.Id; } }
        public string FileName { get { return filename; } }
        public string Name { get { return pluginInterface.Name; } }
        public string Description { get { return pluginInterface.Description; } }
        public PluginType Type { get { return pluginInterface.Type; } }
        public PluginEntities Entities { get { return pluginInterface.Entities; } }
        public System.Version Version { get { return pluginInterface.Version; } }
        public Version RequiredMVEVersion { get { return pluginInterface.RequiredMVEVersion; } }
        public virtual Item AutoFind(Item item) { return pluginInterface.AutoFind(item); }
        public virtual Item Fetch(Item item) { return pluginInterface.Fetch(item); }
        public virtual List<Item> FindPossible(Item item) { return pluginInterface.FindPossible(item); }
        public virtual Item Read(Item item) { return pluginInterface.Read(item); }
        public virtual bool Write(Item item) { return pluginInterface.Write(item); }
        public bool IsConfigurable { get { return pluginInterface.IsConfigurable; } }
        public virtual void Configure() { pluginInterface.Configure(); }
        public PluginConfigurationOptions Options { get { return pluginInterface.Options; } }        
        public void Save() {  pluginInterface.Save(); }
        
        
    }
}