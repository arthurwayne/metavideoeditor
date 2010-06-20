using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace MtnFrameGrabProvider
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{92A28804-7849-4599-A9F9-B284C04B1BB1}"); }
        }

        public override string Name
        {
            get { return "Mtn Extractor"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Extrait des vignettes à partir des fichiers vidéos";
                    default: return "Extracts thumb from video files";
                }
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Local; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.MovieAndSeries; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 0); }
        }

        public override Version RequiredMVEVersion
        {
            get { return new Version(2, 0, 0); }
        }

        public override bool IsConfigurable
        {
            get { return true; }
        }

        public static PluginConfiguration<PluginOptions> PluginOptions { get; set; }

        public override IPluginConfiguration PluginConfiguration
        {
            get
            {
                return PluginOptions;
            }
        }

        public override void Init(Kernel kernel)
        {
            PluginOptions = new PluginConfiguration<PluginOptions>(kernel, this.GetType().Assembly);
            PluginOptions.Load();
        }

        static string videoLocation = null;

        /*public override Item AutoFind(Item item)
        {
            return Fetch(item);
        }*/

        public override Item Read(Item item)
        {
            if (item.Type == Entity.Movie || item.Type == Entity.Episode)
            {
                if (item.Type == Entity.Movie && !PluginOptions.Instance.MoviesEnable) return null;
                if (item.Type == Entity.Episode && !PluginOptions.Instance.SeriesEnable) return null;
                if (item.ImagesPaths.IsNonEmpty() && PluginOptions.Instance.OnlyExtractMissing) return null;
                string videoLocation = null;
                if (File.Exists(item.Path))
                    videoLocation = item.Path;
                else if (item.VideoFiles.IsNonEmpty())
                {
                    videoLocation = item.VideoFiles[0];
                }
                else return null;
                Item i = new Item();
                string localFile = Path.Combine(ApplicationPaths.AppImagePath, Helper.GetMD5(videoLocation + "mtn") + ".jpg");
                if (File.Exists(localFile) || ThumbCreator.CreateThumb(videoLocation, localFile, 600))
                {
                    if (i.ImagesPaths == null) i.ImagesPaths = new List<Poster>();
                    i.ImagesPaths.AddDistinctPoster(new Poster { Image = localFile });
                }
                else
                    return null;
                return i;
            }
            
            return null;

        }
    }
}
