using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using mveEngine;

namespace FolderjpgSaver
{
    public class Plugin : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{7409615A-56EB-4388-8DBE-6A5798F05441}"); }
        }

        public override string Name
        {
            get { return "Folder.jpg Saver"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Enregistre les fichiers folder.jpg";
                    default: return "Saves the first poster as folder.jpg";
                } 
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Saver; }
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

        public override bool Write(Item item)
        {
            if (!PluginOptions.Instance.UsePoster) return true;
            bool res = true;
            string localPoster = ImageUtil.GetLocalImagePath(item.PrimaryImage.Image ?? "");
            string dstPoster = Path.Combine(item.MetadataLocation, "folder.jpg");
            if (item.Type == Entity.Episode && item.PrimaryImage != null)
                dstPoster = Path.Combine(item.MetadataLocation, Path.GetFileName(item.PrimaryImage.Image));
            if (!string.IsNullOrEmpty(localPoster) && localPoster.ToLower() != Path.Combine(item.MetadataLocation, "folder.jpg").ToLower()
                && localPoster.ToLower() != Path.Combine(item.MetadataLocation, "folder.png").ToLower())
            {

                try { File.Delete(FindImage("folder", item.MetadataLocation)); }
                catch { }
                try { 
                    File.Copy(localPoster, dstPoster, true); 
                    if (PluginOptions.Instance.SaveHidden)
                        File.SetAttributes(dstPoster, FileAttributes.Hidden); 
                }
                catch { res = false; }
            }
            return res;
        }

        static string FindImage(string name, string Location)
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
