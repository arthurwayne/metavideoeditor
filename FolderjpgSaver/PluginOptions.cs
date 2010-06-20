using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace FolderjpgSaver
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&Save hidden:|fr&Enregistrer en fichier caché :")]
        public bool SaveHidden = false;
    }
}