using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace TMDb
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&Language:|fr&Langage :")]
        [Items(plugin.langString)]
        public string Language = plugin.DefaultLang;
        
    }
}