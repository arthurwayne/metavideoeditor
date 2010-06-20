using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace XBMCSaver
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&Actors thumbs folder:|fr&Dossier vignettes acteurs :")]
        public string ActorsThumbPath = "";
        
    }
}