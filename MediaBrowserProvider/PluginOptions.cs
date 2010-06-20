using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace MediaBrowserProvider
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&ImagesByName folder:|fr&Dossier ImagesByName :")]
        public string ImagesByName = Plugin.GetImagesByNameFolder;
        
    }
}