using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using mveEngine;

namespace DVDLibrary
{
    public class DVDLibrary : BasePlugin
    {
        public override Guid Id
        {
            get { return new Guid("{4FB8DD89-A443-4dc2-939D-19F6F6F705FB}"); }
        }

        public override string Name
        {
            get { return "DvD-ID"; }
        }

        public override string Description
        {
            get 
            {
                switch (System.Globalization.CultureInfo.CurrentCulture.Parent.ToString())
                {
                    case "fr": return "Enregistre les infos compatibles avec la filmothèque Media Center";
                    default: return "Saves metadata for use with Windows DVD Library";
                } 
            }
        }

        public override PluginType Type
        {
            get { return PluginType.Saver; }
        }

        public override PluginEntities Entities
        {
            get { return PluginEntities.Movie; }
        }

        public override Version Version
        {
            get { return new Version(1, 0, 1); }
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
            if (!Directory.Exists(PluginOptions.Instance.InfoCacheFolder))
            {
                try
                {
                    Directory.CreateDirectory(PluginOptions.Instance.InfoCacheFolder);
                }
                catch { return false; }
            }

            return WriteDvdId(item);
        }

        static bool WriteDvdId(Item movie)
        {
            string file = Path.Combine(movie.Path, "mve.dvdid.xml");

            if (File.Exists(file))
                File.Delete(file);

            XmlDocument doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8' standalone='yes'");
                    xmlWriter.WriteStartElement("DISC");
                    xmlWriter.Close();
                }

            }
            catch { return false; }
            doc.Load(file);

            XmlNode root = doc.DocumentElement;

            XmlElement title = doc.CreateElement("NAME");
            title.InnerText = movie.Title;
            root.AppendChild(title);

            XmlElement Id = doc.CreateElement("ID");
            Id.InnerText = movie.Id;
            root.AppendChild(Id);
            try
            {
                doc.Save(file);
                if (PluginOptions.Instance.SaveHidden)
                    File.SetAttributes(file, FileAttributes.Hidden);
            }
            catch { return false; }

            file = Path.Combine(PluginOptions.Instance.InfoCacheFolder, movie.Id + ".xml");
            if (File.Exists(file))
                File.Delete(file);
            doc = new XmlDocument();
            try
            {
                using (XmlTextWriter xmlWriter = new XmlTextWriter(file, System.Text.Encoding.UTF8))
                {
                    xmlWriter.Formatting = Formatting.Indented;
                    xmlWriter.WriteProcessingInstruction("xml", "version='1.0' encoding='UTF-8' standalone='yes'");
                    xmlWriter.WriteStartElement("METADATA");
                    xmlWriter.Close();
                }

            }
            catch { return false; }
            doc.Load(file);

            root = doc.DocumentElement;
            XmlElement metadata = doc.CreateElement("MDR-DVD");

            XmlElement expire = doc.CreateElement("MetadataExpires");
            expire.InnerText = "0001-01-01T00:00:00";
            metadata.AppendChild(expire);

            XmlElement version = doc.CreateElement("version");
            version.InnerText = "5.0";
            metadata.AppendChild(version);

            XmlElement dvdtitle = doc.CreateElement("dvdTitle");
            dvdtitle.InnerText = movie.Title;
            metadata.AppendChild(dvdtitle);

            XmlElement studio = doc.CreateElement("studio");
            string st = "";
            if (movie.Studios.IsNonEmpty())
            {
                foreach (string s in movie.Studios)
                    st += s + ";";
                if (st.Length > 1)
                {
                    st = st.Remove(st.Length - 2);
                    studio.InnerText = st;
                }
            }
            metadata.AppendChild(studio);

            XmlElement actors = doc.CreateElement("leadPerformer");
            string act = "";
            if (movie.Actors != null)
            {
                foreach (Actor a in movie.Actors)
                    act += a.Name + ";";
                if (act.Length > 1)
                {
                    act = act.Remove(act.Length - 2);
                    actors.InnerText = act;
                }
            }
            metadata.AppendChild(actors);

            XmlElement dir = doc.CreateElement("director");
            if (movie.Crew.IsNonEmpty() && movie.Crew.Exists(c => Helper.IsDirectorName(c.Activity)))
                dir.InnerText = movie.Crew.Find(c => Helper.IsDirectorName(c.Activity)).Name;
            metadata.AppendChild(dir);

            XmlElement rate = doc.CreateElement("MPAARating");
            rate.InnerText = movie.MPAARating;
            metadata.AppendChild(rate);

            XmlElement date = doc.CreateElement("releaseDate");
            if (movie.Year != null)
            {
                date.InnerText = movie.Year.ToString() + " 01 01";
            }
            metadata.AppendChild(date);

            XmlElement genre = doc.CreateElement("genre");
            string ge = "";
            if (movie.Genres != null)
            {
                foreach (string s in movie.Genres)
                    ge += s + ", ";
                if (ge.Length > 1)
                {
                    ge = ge.Remove(ge.Length - 2);
                    genre.InnerText = ge;
                }
            }
            metadata.AppendChild(genre);

            XmlElement duration = doc.CreateElement("duration");
            if (movie.RunningTime != null)
                duration.InnerText = movie.RunningTime.ToString();
            metadata.AppendChild(duration);

            XmlElement TitleElmt = doc.CreateElement("title");

            XmlElement titleTitle = doc.CreateElement("titleTitle");
            titleTitle.InnerText = movie.Title;
            TitleElmt.AppendChild(titleTitle);

            XmlElement syn = doc.CreateElement("synopsis");
            syn.InnerText = movie.Overview;
            TitleElmt.AppendChild(syn);

            metadata.AppendChild(TitleElmt);
            root.AppendChild(metadata);

            XmlElement NeedsAttribution = doc.CreateElement("NeedsAttribution");
            NeedsAttribution.InnerText = "true";
            root.AppendChild(NeedsAttribution);

            XmlElement ID = doc.CreateElement("DvdId");
            ID.InnerText = movie.Id;
            root.AppendChild(ID);

            try
            {
                doc.Save(file);
            }
            catch { return false; }
            return true;
        }


    }
}
