using System;
using System.IO;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Web;
using System.Net;
using System.Diagnostics;
using System.Collections;
using System.Text.RegularExpressions;
using System.Globalization;
using System.Security.Cryptography;

namespace mveEngine
{
    public class ProvidersUtil
    { 
        public static void LocalFetch(Item item)
        {
            PluginEntities entity;
            if (item.Type == Entity.Movie || item.Type == Entity.Folder) entity = PluginEntities.Movie;
            else if (item.Type == Entity.Series || item.Type == Entity.Episode || item.Type == Entity.Season) entity = PluginEntities.Series;
            else return ;
            PluginFieldsOptions UpdatedOptions = new PluginFieldsOptions();
            foreach (Plugin p in Kernel.Instance.Plugins)
            {
                if (p.Type == PluginType.Local && (p.Entities == entity || p.Entities == PluginEntities.MovieAndSeries) && p.Options.Enable)
                {
                    Item res = null;
                    try { res = p.Read(item); }
                    catch (Exception ex) { Logger.ReportException("Error reading item  " + item.Title + " with provider " + p.Name, ex); continue; }
                    UpdateItem(item, p.Read(item), UpdatedOptions, p.Options);
                }
            }
        }      

        public static Item AutoFind(Item item)
        {
            Item Result = null;
            PluginEntities entity;
            if (item.Type == Entity.Movie || item.Type == Entity.Folder) entity = PluginEntities.Movie;
            else if (item.Type == Entity.Series || item.Type == Entity.Episode || item.Type == Entity.Season) entity = PluginEntities.Series;
            else return Result;
            PluginFieldsOptions UpdatedOptions = new PluginFieldsOptions();
            foreach (Plugin p in Kernel.Instance.Plugins)
            {
                if (p.Type == PluginType.Provider && (p.Entities == entity || p.Entities == PluginEntities.MovieAndSeries) && p.Options.Enable)
                {
                    Logger.ReportVerbose("Auto find item " + item.Title + " with provider " + p.Name);
                    Item res = null;
                    try
                    {
                        res = p.AutoFind(item);
                    }
                    catch (Exception ex) { Logger.ReportException("Error auto finding item  " + item.Title + " with provider " + p.Name, ex); continue; }
                    if (res != null)
                    {
                        if (Result == null) Result = new Item();
                        UpdateItem(Result, res, UpdatedOptions, p.Options);
                    }
                }
            }
            return Result;
        }

        public static List<Item> FindPossible(Item item)
        {
            List<Item> Results = new List<Item>();
            PluginEntities entity;
            if (item.Type == Entity.Movie || item.Type == Entity.Folder) entity = PluginEntities.Movie;
            else if (item.Type == Entity.Series) entity = PluginEntities.Series;
            else return Results;
            foreach (Plugin p in Kernel.Instance.Plugins)
            {
                if (p.Type == PluginType.Provider && (p.Entities == entity || p.Entities == PluginEntities.MovieAndSeries) && p.Options.Enable)
                {
                    Logger.ReportVerbose("Find possible for item " + item.Title + " with provider " + p.Name);
                    List<Item> items = null;
                    try { items = p.FindPossible(item); }
                    catch (Exception ex) { Logger.ReportException("Error finding results for item  " + item.Title + " with provider " + p.Name, ex); continue; }
                    
                    Results.AddRange(items);
                }
            }
            return Results;
        }

        public static Item Fetch(Item item)
        {
            Item Result = new Item();
            PluginEntities entity;
            if (item.Type == Entity.Movie || item.Type == Entity.Folder) entity = PluginEntities.Movie;
            else if (item.Type == Entity.Series || item.Type == Entity.Episode || item.Type == Entity.Season) entity = PluginEntities.Series;
            else return Result;
            PluginFieldsOptions UpdatedOptions = new PluginFieldsOptions();
            foreach (Plugin p in Kernel.Instance.Plugins)
            {
                if (p.Type == PluginType.Provider && (p.Entities == entity || p.Entities == PluginEntities.MovieAndSeries) && p.Options.Enable)
                {
                    Logger.ReportVerbose("Fetch item " + item.Title + " with provider " + p.Name);
                    Item res = null;
                    try { res = p.Fetch(item); }
                    catch (Exception ex) { Logger.ReportException("Error fetching item  " + item.Title + " with provider " + p.Name, ex); continue; }
                    UpdateItem(Result, res, UpdatedOptions, p.Options);
                }
            }
            return Result;
        }

        public static bool Write(Item item)
        {
            bool Result = true;
            PluginEntities entity;
            if (item.Type == Entity.Movie || item.Type == Entity.Folder) entity = PluginEntities.Movie;
            else if (item.Type == Entity.Series || item.Type == Entity.Episode || item.Type == Entity.Season) entity = PluginEntities.Series;
            else return Result;
            PluginFieldsOptions UpdatedOptions = new PluginFieldsOptions();
            foreach (Plugin p in Kernel.Instance.Plugins)
            {
                if (p.Type == PluginType.Saver && (p.Entities == entity || p.Entities == PluginEntities.MovieAndSeries) && p.Options.Enable)
                {
                    Logger.ReportVerbose("Write item " + item.Title + " with provider " + p.Name);
                    try { Result &= p.Write(item); }
                    catch (Exception ex) { Logger.ReportException("Error writting data for " + item.Title + " with provider " + p.Name, ex); }
                }
            }
            return Result;
        }


        #region UpdateItem
        public static void UpdateItem(Item item, Item FetchedItem)
        {
            Plugin p = Kernel.Instance.Plugins[0] as Plugin;
            UpdateItem(item, FetchedItem, new PluginFieldsOptions(), p.Options);
        }
        private static void UpdateItem(Item item, Item FetchedItem, PluginFieldsOptions UpdatedOptions, PluginConfigurationOptions options)
        {
            if (FetchedItem == null) return;
            if (options.UseTitle && !string.IsNullOrEmpty(FetchedItem.Title) && !UpdatedOptions.UseTitle)
            {
                item.Title = FetchedItem.Title;
                UpdatedOptions.UseTitle = true;
            }
            if (options.UseOriginalTitle && !string.IsNullOrEmpty(FetchedItem.OriginalTitle) && !UpdatedOptions.UseOriginalTitle)
            {
                item.OriginalTitle = FetchedItem.OriginalTitle;
                UpdatedOptions.UseOriginalTitle = true;
            }
            if (options.UseSortTitle && !string.IsNullOrEmpty(FetchedItem.SortTitle) && !UpdatedOptions.UseSortTitle)
            {
                item.SortTitle = FetchedItem.SortTitle;
                UpdatedOptions.UseSortTitle = true;
            }
            if (options.UseTagLines)
            {
                if (item.TagLines == null) item.TagLines = new List<string>();
                item.TagLines.AddDistinct(FetchedItem.TagLines);
            }
            if (options.UseProductionYear && FetchedItem.Year.IsValidYear() && !UpdatedOptions.UseProductionYear)
            {
                item.Year = FetchedItem.Year;
                UpdatedOptions.UseProductionYear = true;
            }
            if (options.UseRuntime && FetchedItem.RunningTime.IsValidRunningTime() && !UpdatedOptions.UseRuntime)
            {
                item.RunningTime = FetchedItem.RunningTime;
                UpdatedOptions.UseRuntime = true;
            }
            if (options.UseRating && FetchedItem.Rating.IsValidRating() && !UpdatedOptions.UseRating)
            {
                item.Rating = FetchedItem.Rating;
                UpdatedOptions.UseRating = true;
            }
            if (options.UseMPAARating && !string.IsNullOrEmpty(FetchedItem.MPAARating) && !UpdatedOptions.UseMPAARating)
            {
                item.MPAARating = FetchedItem.MPAARating;
                UpdatedOptions.UseMPAARating = true;
            }
            if (options.UseOverview && !string.IsNullOrEmpty(FetchedItem.Overview) && !UpdatedOptions.UseOverview)
            {
                item.Overview = FetchedItem.Overview;
                UpdatedOptions.UseOverview = true;
            }
            if (options.UseAspectRatio && !string.IsNullOrEmpty(FetchedItem.AspectRatio) && !UpdatedOptions.UseAspectRatio)
            {
                item.AspectRatio = FetchedItem.AspectRatio;
                UpdatedOptions.UseAspectRatio = true;
            }
            if (options.UseCasting)
            {
                if (FetchedItem.Actors.IsNonEmpty())
                {
                    if (item.Actors == null) item.Actors = new List<Actor>();
                    item.Actors.AddDistinct(FetchedItem.Actors);
                }
                if (FetchedItem.Crew.IsNonEmpty())
                {
                    if (item.Crew == null) item.Crew = new List<CrewMember>();
                    item.Crew.AddDistinct(FetchedItem.Crew);
                }
            }
            if (options.UseGenres)
            {
                if (item.Genres == null) item.Genres = new List<string>();
                item.Genres.AddDistinct(FetchedItem.Genres);
            }
            if (options.UseStudios)
            {
                if (item.Studios == null) item.Studios = new List<string>();
                item.Studios.AddDistinct(FetchedItem.Studios);
            }
            if (options.UseCountries)
            {
                if (item.Countries == null) item.Countries = new List<string>();
                item.Countries.AddDistinct(FetchedItem.Countries);
            }
            if (options.UsePoster && FetchedItem.ImagesPaths.IsNonEmpty())
            {
                if (item.ImagesPaths == null) item.ImagesPaths = new List<Poster>();
                if (UpdatedOptions.UsePoster)
                {
                    foreach (Poster p in FetchedItem.ImagesPaths)
                        p.Checked = false;
                }
                else
                {
                    FetchedItem.ImagesPaths[0].Checked = true;
                }
                UpdatedOptions.UsePoster = true;
                item.ImagesPaths.AddDistinct(FetchedItem.ImagesPaths);
            }
            if (options.UseBackdrop && FetchedItem.BackdropImagePaths.IsNonEmpty())
            {
                if (item.BackdropImagePaths == null) item.BackdropImagePaths = new List<Poster>();
                if (UpdatedOptions.UseBackdrop)
                {
                    foreach (Poster p in FetchedItem.BackdropImagePaths)
                        p.Checked = false;
                }
                else
                {
                    for (int i = 0; i < Math.Min(FetchedItem.BackdropImagePaths.Count, Config.Instance.MaxBdSaved); i++)
                        FetchedItem.BackdropImagePaths[i].Checked = true;
                }
                UpdatedOptions.UseBackdrop = true;
                item.BackdropImagePaths.AddDistinct(FetchedItem.BackdropImagePaths);
            }
            if (options.UseBanner && FetchedItem.BannersPaths.IsNonEmpty())
            {
                if (item.BannersPaths == null) item.BannersPaths = new List<Poster>();
                if (UpdatedOptions.UseBanner)
                {
                    foreach (Poster p in FetchedItem.BannersPaths)
                        p.Checked = false;
                }
                else
                {
                    FetchedItem.BannersPaths[0].Checked = true;
                }
                UpdatedOptions.UseBanner = true;
                item.BannersPaths.AddDistinct(FetchedItem.BannersPaths);
            }
            if (options.UseTrailers)
            {
                if (item.TrailerFiles == null) item.TrailerFiles = new List<string>();
                item.TrailerFiles.AddDistinct(FetchedItem.TrailerFiles);
            }

            if (FetchedItem.ProvidersId != null)
            {
                if (item.ProvidersId == null) item.ProvidersId = new List<DataProviderId>();
                item.ProvidersId.AddRange(FetchedItem.ProvidersId);
            }
            if (FetchedItem.Watched != null) item.Watched = FetchedItem.Watched;
            if (FetchedItem.DateAdded != null && FetchedItem.DateAdded != DateTime.MinValue) item.DateAdded = FetchedItem.DateAdded;
        }

        #endregion
        

        
    }
}