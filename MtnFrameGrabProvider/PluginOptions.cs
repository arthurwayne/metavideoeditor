using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace MtnFrameGrabProvider
{
    public class PluginOptions : PluginConfigurationOptions
    {
        
        [Label("en&Enable for movies:|fr&Activer pour les films :")]
        public bool MoviesEnable = false;

        [Label("en&Enable for series:|fr&Activer pour les séries :")]
        public bool SeriesEnable = false;

        [Label("en&Only extract if no poster:|fr&Extraire uniquement si aucune jaquette")]
        public bool OnlyExtractMissing = true;

        [Label("en&Capture Time (seconds):|fr&Moment de la capture (secondes)")]
        public string MtnCaptureTime = "600";
    }
}