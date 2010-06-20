using System;
using System.Text;
using System.IO;
using System.Collections.Generic;
using System.Windows.Forms;

namespace mveEngine
{
    [Serializable]
    public class Folder : Item
    {
        public Folder()
        {
            children = GetChildren(false);
        }

        public Folder(string path)
        {
            this.Path = path;
            this.Id = Helper.GetMD5(path);
            children = GetChildren(false);
        }

        public Folder(List<string> path)
        {
            this.IsRootFolder = true;
            this.Path = "root folder";
            this.Id = Helper.GetMD5(this.Path);
            RootFolders = path;
            children = GetChildren(true);
        }

        bool IsRootFolder = false;

        List<string> RootFolders;

        List<Item> children;

        List<Item> GetChildren(bool allowCache)
        {

            List<Item> items = null;
            //if (allowCache)
            {
                items = GetCachedChildren();
            }
            
            if (items == null)
            {
                items = GetNonCachedChildren();

                if (allowCache)
                {
                    SaveChildren(items);
                }
            }

            SetParent(items);
            return items;
        }

        public List<Item> Children
        {
            get
            {
                return children;
            }
        }

        public IEnumerable<Item> RecursiveChildren
        {
            get
            {
                foreach (var item in Children)
                {
                    var folder = item as Folder;
                    if (folder != null)
                    {
                        foreach (var subitem in folder.RecursiveChildren)
                        {
                            yield return subitem;
                        }
                    }
                    if (!IsRootFolder)
                        yield return item;
                }
            }
        }

        void SetParent(List<Item> items)
        {
            if (items == null || this.IsRootFolder) return;
            foreach (var item in items)
            {
                item.Parent = this;
            }
        }

        void SaveChildren(List<Item> items)
        {
            if (items == null || IsRootFolder == false) return;
            //Serializer.Serialization(System.IO.Path.Combine(ApplicationPaths.AppCachePath, Id), items);
            //Kernel.Instance.ItemRepository.SaveChildren(Id, items.Select(i => i.Id));
            foreach (var item in items)
            {
                //Kernel.Instance.ItemRepository.SaveItem(item);

                Serializer.Serialization(System.IO.Path.Combine(ApplicationPaths.AppCachePath, item.Id), item);
            }
        }

        public void Save()
        {
            SaveChildren(Children);
        }

        List<Item> GetCachedChildren()
        {
            List<Item> items = null;

            Folder cached = null;
            try
            {
                cached = (Folder)Serializer.Deserialization(System.IO.Path.Combine(ApplicationPaths.AppCachePath, Id)); // Kernel.Instance.ItemRepository.RetrieveChildren(Id);
            }
            catch (Exception ex) { Logger.ReportException("failed to deserialize ", ex); return null; }

            /*if (cached != null)
            {
                items = new List<Item>();
                foreach (var guid in cached)
                {
                    Item item = null;
                    try
                    {
                        item = (Item)Serializer.Deserialization(System.IO.Path.Combine(ApplicationPaths.AppCachePath, guid.Id));// Kernel.Instance.ItemRepository.RetrieveItem(guid);
                    }
                    catch { item = null; }

                    if (item != null)
                    {
                        items.Add(item);
                    }
                }
            }*/
            if (cached != null)
            {
                items = new List<Item>();
                foreach (Item item in cached.RecursiveChildren)
                    items.Add(item);
            }
            return items;
        }

        protected virtual List<Item> GetNonCachedChildren()
        {
            if (this.IsRootFolder)
            {
                List<Item> childrenFolders = new List<Item>();
                foreach (string folder in RootFolders)
                {
                    Folder f = new Folder(folder);
                    f.Title = folder;
                    childrenFolders.Add(f);
                }
                return childrenFolders;
            }
            else
                return EntityResolver.GetChildren(this);

        }

    }
}