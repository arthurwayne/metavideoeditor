using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;

namespace mveEngine
{
    [Serializable]
    public class Data
    {
        public List<Item> Items { get; set; }
    }

    [Serializable]
    public class Item
    {
        public string Path { get; set; }
        public string Id { get; set; }
        public DateTime DateAdded { get; set; }
        public Entity Type { get; set; }
        public bool HasChanged { get; set; }
        public List<string> VideoFiles { get; set; }
        public List<DataProviderId> ProvidersId { get; set; }
        public bool? Watched { get; set; }

        public string Title { get; set; }
        public string OriginalTitle { get; set; }
        public string SortTitle { get; set; }
        public List<string> TagLines { get; set; }
        public int? Year { get; set; }
        public string Overview { get; set; }       
        public List<Poster> ImagesPaths { get; set; }
        public Poster PrimaryImage
        {
            get
            {
                Poster poster = null;
                if (ImagesPaths.IsNonEmpty())
                    poster = ImagesPaths.Find(p => p.Checked) ?? ImagesPaths[0];
                return poster;
            }
            set
            {
                if (value != null)
                {
                    if (ImagesPaths == null)
                        ImagesPaths = new List<Poster>();
                    if (!ImagesPaths.Exists(p => p.Image == value.Image))
                        ImagesPaths.Add(value);
                    foreach (Poster p in ImagesPaths)
                        p.Checked = (p.Image == value.Image);
                }
            }
        }
        public List<Poster> BackdropImagePaths { get; set; }
        public Poster PrimaryBackdrop
        {
            get
            {
                Poster poster = null;
                if (BackdropImagePaths.IsNonEmpty())
                    poster = BackdropImagePaths.Find(p => p.Checked) ?? BackdropImagePaths[0];
                return poster;
            }
            set
            {
                if (value != null)
                {
                    if (BackdropImagePaths == null)
                        BackdropImagePaths = new List<Poster>();
                    if (!BackdropImagePaths.Exists(p => p.Image == value.Image))
                        BackdropImagePaths.Add(value);
                    foreach (Poster p in BackdropImagePaths)
                        p.Checked = (p.Image == value.Image);
                }
            }
        }
        public List<string> Genres { get; set; }
        public List<Actor> Actors { get; set; }
        public List<string> Studios { get; set; }
        public List<string> Countries { get; set; }
        public List<CrewMember> Crew { get; set; }
        public int? RunningTime { get; set; }
        public Single? Rating { get; set; }        
        public List<Poster> BannersPaths { get; set; }
        public Poster PrimaryBanner
        {
            get
            {
                Poster poster = null;
                if (BannersPaths != null && BannersPaths.Count > 0)
                    poster = BannersPaths.Find(p => p.Checked) ?? BannersPaths[0];
                return poster;
            }
            set
            {
                if (value != null)
                {
                    if (BannersPaths == null)
                        BannersPaths = new List<Poster>();
                    if (!BannersPaths.Exists(p => p.Image == value.Image))
                        BannersPaths.Add(value);
                    foreach (Poster p in BannersPaths)
                        p.Checked = (p.Image == value.Image);
                }
            }
        }
        public string MetadataLocation { get; set; } 
        public string MPAARating { get; set; } 
        public List<string> TrailerFiles { get; set; }
        public string Mediatype { get; set; }
        public string AspectRatio { get; set; }

        public string SeriesName { get; set; }
        public string SeasonNumber { get; set; }
        public string EpisodeNumber { get; set; }
        public int DataState 
        {
            get
            {
                int r = 0;
                if (ImagesPaths.IsNonEmpty())
                    r += 25;
                if (BackdropImagePaths.IsNonEmpty() || Type == Entity.Episode || Type == Entity.Season)
                    r += 25;
                if (Type == Entity.Season && BannersPaths.IsNonEmpty())
                    r += 25;
                if (!string.IsNullOrEmpty(Overview))
                    r += 10;
                if (Rating.IsValidRating() && RunningTime.IsValidRunningTime())
                    r += 10;
                if (Year != null)
                    r += 10;
                if (Actors.IsNonEmpty() && Crew.IsNonEmpty())
                    r += 10;
                if (Genres != null && Genres.Count > 0)
                    r += 10;
                return r;
            }
        }

        //Constructor
        public Item(Item i)
        {
            this.Title = i.Title;
            this.OriginalTitle = i.OriginalTitle;
            this.SortTitle = i.SortTitle;
            this.TagLines = i.TagLines;
            this.Path = i.Path;
            this.VideoFiles = i.VideoFiles;
            this.DateAdded = i.DateAdded;
            this.Overview = i.Overview;
            if (i.ProvidersId != null) this.ProvidersId = new List<DataProviderId>(i.ProvidersId);
            if (i.Actors != null) this.Actors = new List<Actor>(i.Actors);
            if (i.Studios != null) this.Studios = new List<string>(i.Studios);
            if (i.Countries != null) this.Countries = new List<string>(i.Countries);
            if (i.BackdropImagePaths != null) this.BackdropImagePaths = new List<Poster>(i.BackdropImagePaths);
            if (i.ImagesPaths != null) this.ImagesPaths = new List<Poster>(i.ImagesPaths);
            if (i.Genres != null) this.Genres = new List<string>(i.Genres);
            if (i.TrailerFiles != null) this.TrailerFiles = new List<string>();
            this.HasChanged = i.HasChanged;
            this.Id = i.Id;
            this.Rating = i.Rating;
            this.RunningTime = i.RunningTime;
            this.Type = i.Type;
            this.Year = i.Year;
            this.SeriesName = i.SeriesName;
            this.SeasonNumber = i.SeasonNumber;
            this.EpisodeNumber = i.EpisodeNumber;
            this.MetadataLocation = i.MetadataLocation;
            this.BannersPaths = i.BannersPaths;
            this.MPAARating = i.MPAARating;
            this.Mediatype = i.Mediatype;
            this.AspectRatio = i.AspectRatio;
            this.Watched = i.Watched;
            this.Crew = i.Crew;


        }
        public Item()
        {
        }
    }

    [Serializable]
    public class Actor
    {
        public string Name { get; set; }
        public string Role { get; set; }
        public string ImagePath { get; set; }
    }

    [Serializable]
    public class CrewMember
    {
        public string Name { get; set; }
        public string Activity { get; set; }
    }

    [Serializable]
    public class DataProviderId
    {
        public string Name { get; set; }
        public string Id { get; set; }
        public string Url { get; set; }
    }

    [Serializable]
    public enum Entity
    {
        Movie,
        Series,
        Season,
        Episode,
        Folder
    }

    [Serializable]
    public class Poster
    {
        public string Thumb { get; set; }
        public string Image { get; set; }
        public string Id { get; set; }
        public string width { get; set; }
        public string height { get; set; }
        public bool Checked { get; set; }

        public Poster()
        { }

        //Constructor
        public Poster(Poster p)
        {
            this.Checked = p.Checked;
            this.height = p.height;
            this.Id = p.Id;
            this.Image = p.Image;
            this.Thumb = p.Thumb;
            this.width = p.width;
        }
    }
}
