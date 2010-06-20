using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using mveEngine;

namespace FolderjpgProvider
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{859F7FC1-93AF-4f0b-A652-00BFBE3C3103}"); }
        }

        public override string Name
        {
            get { return "Folder.jpg Reader"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Lit les fichiers folder.jpg et folder.png";
                    default: return "Get folder.jpg and folder.png files";
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
            get { return false; }
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

        public override Item Read(Item item)
        {
            Item i = new Item();
            string poster = FindImage("folder", item.MetadataLocation);
            if (!string.IsNullOrEmpty(poster))
            {
                if (i.ImagesPaths == null) i.ImagesPaths = new List<Poster>();
                Poster p = new Poster();
                p.Image = poster;
                p.Checked = true;
                try
                {
                    Image img = Image.FromFile(poster);
                    p.width = ((int)img.Width).ToString();
                    p.height = ((int)img.Height).ToString();
                }
                catch { }
                i.ImagesPaths.AddDistinctPoster(p);
            }
            return i;
        }

        public static string FindImage(string name, string Location)
        {
            string file = Path.Combine(Location, name + ".jpg");
            if (File.Exists(file))
                return file;

            file = Path.Combine(Location, name + ".png");
            if (File.Exists(file))
                return file;

            if (name == "folder") // we will also look for images that match by name in the same location for the primary image
            {
                var dir = Path.GetDirectoryName(Location);
                var filename_without_extension = Path.GetFileNameWithoutExtension(Location);

                // dir was null for \\10.0.0.4\dvds - workaround
                if (dir != null && filename_without_extension != null)
                {
                    file = Path.Combine(dir, filename_without_extension);
                    if (File.Exists(file + ".jpg"))
                        return file + ".jpg";
                    if (File.Exists(file + ".png"))
                        return file + ".png";
                }
            }
            return null;
        }

    }
}
