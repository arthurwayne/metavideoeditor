using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace MediaBrowserSaver
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&ImagesByName folder:|fr&Dossier ImagesByName :")]
        public string ImagesByName = Plugin.GetImagesByNameFolder;

        [Label("en&Save mymovies.xml hidden:|fr&Enregistrer mymovies.xml en fichier caché :")]
        public bool SaveXmlHidden = false;

        [Label("en&Save backdrops hidden|fr&Enregistrer les backdrops en fichier caché :")]
        public bool SaveBdHidden = false;

        [Label("en&Delete trailer from cache after move|fr&Supprimer trailer du cache après copie :")]
        public bool DeleteTrailer = true;
    }
}