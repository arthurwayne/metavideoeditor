using System;
namespace mveEngine
{
    public interface IPluginConfiguration
    {
        bool? BuildUI();
        void Load();
        void Save();
        PluginConfigurationOptions Options();
        
    }
}
