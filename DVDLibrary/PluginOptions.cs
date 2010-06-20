using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace DVDLibrary
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&DvdInfoCache folder:|fr&Dossier DvdInfoCache :")]
        public string InfoCacheFolder = Path.Combine(Path.Combine(Path.Combine(
            Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData), "Microsoft"), "eHome"), "DvdInfoCache");

        [Label("en&Save mve.dvdid.xml hidden:|fr&Enregistrer mve.dvdid.xml en fichier caché :")]
        public bool SaveHidden = false;
    }
}