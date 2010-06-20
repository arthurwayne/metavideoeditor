using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace mveEngine
{
    public class RemotePlugin : IPlugin
    {

        public void Init(Kernel kernel)
        {
        }

        public string FileName
        {
            get;
            set;
        }

        public Guid Id 
        { 
            get; 
            set; 
        }

        public string Name
        {
            get;
            set;
        }

        public string Description
        {
            get;
            set;
        }

        public System.Version Version
        {
            get;
            set;
        }

        public Version RequiredMVEVersion
        {
            get;
            set;
        }

        public PluginType Type
        {
            get;
            set;
        }

        public PluginEntities Entities 
        {
            get; 
            set; 
        }

        public virtual bool IsConfigurable
        {
            get
            {
                return false;
            }
        }

        public virtual PluginConfigurationOptions Options
        {
            get;
            set;
        }


        public virtual void Configure()
        {
        }

        public virtual Item AutoFind(Item item) { return null; }
        public virtual Item Fetch(Item item) { return null; }
        public virtual List<Item> FindPossible(Item item) { return null; }
        public virtual bool Write(Item item) { return false; }
        public virtual Item Read(Item item) { return null; }
        public void Save()
        {
        }
    }
}
