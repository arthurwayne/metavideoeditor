using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace TheTVDB
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&Language:|fr&Langage :")]
        [Items(TheTVDB.langString)]
        public string Language = TheTVDB.DefaultLang;
        
    }
}