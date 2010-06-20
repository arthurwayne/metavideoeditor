using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace mveEngine
{
    public abstract class BasePlugin : IPlugin
    {
        public abstract void Init(Kernel kernel);
        public abstract Guid Id { get; }
        public string FileName { get { throw new NotSupportedException(); } }
        public abstract string Name { get; }
        public abstract string Description { get; }
        public abstract PluginType Type { get; }
        public abstract PluginEntities Entities { get; }
        public abstract Version Version { get; }
        public abstract Version RequiredMVEVersion { get; }
        public virtual Item AutoFind(Item item) { return null; }
        public virtual Item Fetch(Item item) { return null; }
        public virtual List<Item> FindPossible(Item item) { return null; }
        public virtual bool Write(Item item) { return false; }
        public virtual Item Read(Item item) { return null; }
        public abstract bool IsConfigurable { get; }        
        public virtual IPluginConfiguration PluginConfiguration { get { return null; } }
        public virtual PluginConfigurationOptions Options
        {
            get
            {
                if (PluginConfiguration != null)
                    return PluginConfiguration.Options();
                return null;
            }
        }

        public virtual void Configure()
        {            
            if (PluginConfiguration != null)
            {
                if (PluginConfiguration.BuildUI() == true)
                    PluginConfiguration.Save();
                else
                    PluginConfiguration.Load();
            }
        }

        public void Save()
        {
            if (PluginConfiguration != null)
                PluginConfiguration.Save();
        }
    }

    public interface IPlugin
    {
        void Init(Kernel kernel);
        Guid Id { get; }
        string FileName { get; }
        string Name { get; }
        string Description { get; }
        PluginType Type { get; }
        PluginEntities Entities { get; }
        Version Version { get; }
        Version RequiredMVEVersion { get; }
        bool IsConfigurable { get; }
        void Configure();
        Item Fetch(Item item);
        Item AutoFind(Item item);
        List<Item> FindPossible(Item item);
        Item Read(Item item);
        bool Write(Item item);
        void Save();
        PluginConfigurationOptions Options { get; }
    }

    public enum PluginType
    {
        Provider,
        Local,
        Saver
    }

    public enum PluginEntities
    {
        Movie,
        Series,
        MovieAndSeries
    }
}
