using System;
using System.Collections.Generic;

namespace mveEngine
{
    public static class Extensions
    {
        public static bool IsValidYear(this int? year)
        {
            if (year == null) return false;
            else if (year > 1900 && year < 2020)
                return true;
            return false;
        }

        public static bool IsValidRunningTime(this int? run)
        {
            if (run == null) return false;
            else if (run > 0)
                return true;
            return false;
        }

        public static bool IsValidRating(this float? rate)
        {
            if (rate == null) return false;
            else if (rate > 0 && rate <= 10)
                return true;
            return false; 
        }

        public static bool IsNonEmpty(this List<string> list)
        {
            return (list != null && list.Count > 0);
        }

        public static bool IsNonEmpty(this List<Poster> list)
        {
            return (list != null && list.Count > 0);
        }

        public static bool IsNonEmpty(this List<Actor> list)
        {
            return (list != null && list.Count > 0);
        }

        public static bool IsNonEmpty(this List<CrewMember> list)
        {
            return (list != null && list.Count > 0);
        }

        public static void AddDistinct(this List<string> list, List<string> newList)
        {
            if (!newList.IsNonEmpty()) return;
            if (list == null) list = new List<string>();
            list.RemoveAll(s => newList.Contains(s));
            list.AddRange(newList);
        }

        public static void AddDistinct(this List<Poster> list, List<Poster> newList)
        {            
            if (!newList.IsNonEmpty() || list == null) return;
            foreach (Poster p in newList)
                list.AddDistinctPoster(p);
        }

        public static void AddDistinct(this List<Actor> list, List<Actor> newList)
        {
            if (!newList.IsNonEmpty() || list == null) return;
            list.RemoveAll(s => newList.Exists(p => p.Name == s.Name));
            list.AddRange(newList);
        }

        public static void AddDistinct(this List<CrewMember> list, List<CrewMember> newList)
        {
            if (!newList.IsNonEmpty() || list == null) return;
            list.RemoveAll(s => newList.Exists(p => p.Name == s.Name));
            list.AddRange(newList);
        }

        public static void AddDistinctPoster(this List<Poster> list, Poster newPoster)
        {
            if (list == null) return;
            bool add = true;
            foreach (Poster p in list)
            {
                if (p.Image.ToLower() == newPoster.Image.ToLower()) { add = false; continue; }
                string path;
                if (ImageUtil.LocalFileExists(p.Image, out path) && FileUtil.Compare(p.Image, newPoster.Image))
                    add = false;
            }
            if (add) list.Add(newPoster);
        }
    }
}