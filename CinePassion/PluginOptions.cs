﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace CinePassion
{
    public class PluginOptions : PluginConfigurationOptions
    {
        [Label("en&User name :|fr&Nom d'utilisateur :")]
        public string username = "";

        [Label("en&Password :|fr&Mot de passe :")]
        public string password = "";

        [Label("en&Language:|fr&Langage :")]
        [Items("en,fr")]
        public string Language = Plugin.GetLanguage;

        [Label("en&Rating:|fr&Notes :")]
        [Items("allocine,imdb,cinepassion,moyenne des trois")]
        public string Rating = "allocine";
        
    }
}