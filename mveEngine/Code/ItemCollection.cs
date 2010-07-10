using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Windows.Forms;

namespace mveEngine
{
    public class ItemCollection
    {
        public List<TreeNode> MainNodes = new List<TreeNode>();
        public List<Item> ItemsList = new List<Item>();
        public List<Item> CheckedItems = new List<Item>();

        private Item _selectedItem;
        public Item SelectedItem
        {
            get
            {
                if (SelectedNode != null && SelectedNode.Tag != null)
                    _selectedItem = FindById((string)SelectedNode.Tag);
                else
                    _selectedItem = null;
                return _selectedItem;
            }
            set
            {
                if (value == null)
                {
                    _selectedItem = null;
                    return;
                }
                int index = ItemsList.FindIndex(i => i.Id == value.Id);
                if (index > -1)
                    ItemsList[ItemsList.FindIndex(i => i.Id == value.Id)] = value;
            }
        }

        public List<Item> ModifiedItems
        {
            get
            {
                return ItemsList.FindAll(i => i.HasChanged);
            }
        }

        private TreeNode _selectedNode;
        public TreeNode SelectedNode
        {
            get
            {
                return _selectedNode;
            }
            set
            {
                _selectedNode = value;
            }
        }


        //Constructor
        public ItemCollection(List<string> RootFolders)
        {
            ItemsList.Clear();
            MainNodes.Clear();
            foreach (string RootFolder in RootFolders)
            {
                TreeNode RootNode = new TreeNode();
                if (!Directory.Exists(RootFolder))
                {
                    continue;
                }
                if (RootFolder.Length == 3)
                    RootNode.Name = RootFolder;
                else
                    RootNode.Name = RootFolder.Substring(RootFolder.LastIndexOf("\\") + 1);
                RootNode.ToolTipText = RootFolder;
                foreach (TreeNode n in FileUtil.ReadFolder(RootFolder, ItemsList))
                    RootNode.Nodes.Add(n);
                MainNodes.Add(RootNode);
                //SplashScreen.UdpateProgressBar(20 + (int)Math.Round((double)(index * 40 / Config.RootFolders.Count)));
            }
        }

        public Item FindById(string id)
        {
            return ItemsList.Find(i => i.Id == id);
        }

        public void RefreshData()
        {
            List<Item> items = new List<Item>();
            foreach (Item i in ItemsList)
                items.Add(i);
            foreach (Item item in items)
            {
                if (!item.HasChanged)
                {
                    string path = Path.Combine(ApplicationPaths.AppCachePath, item.Id);
                    try
                    {
                        Item i = FindById(item.Id);
                        ProvidersUtil.LocalFetch(i);
                    }
                    catch (Exception e) { Logger.ReportException("Fails to fetch local data for " + item.Path, e); continue; }
                    Serializer.Serialization(path, item);
                }
            }
        }

        public void SaveItem(Item item)
        {
            if (item == null) return;
            Async.Queue("SaveItem", () =>
                {
                    FileUtil.Rename(item, ItemsList);
                    if (ProvidersUtil.Write(item))
                        item.HasChanged = false;
                    Serializer.Serialization(Path.Combine(ApplicationPaths.AppCachePath, item.Id), item);
                    TreeNode tn = FindNodeById(item.Id);
                    if (tn != null)
                    {
                        tn.Checked = false;
                        RefreshNode(tn);
                    }
                }, true, "Enregistre " + item.Title);
        }

        public void RefreshNode(TreeNode node)
        {
            Kernel.Instance.Message.RefreshNode(node);
        }

        public TreeNode FindNodeById(string id)
        {
            List<TreeNode> Nodes = new List<TreeNode>();
            foreach (TreeNode tn in MainNodes)
                Nodes.AddRange(FindNodes(tn, id));
            if (Nodes.Count == 0)
                return null;
            return Nodes[0];
        }

        private List<TreeNode> FindNodes(TreeNode node, string id)
        {
            List<TreeNode> Nodes = new List<TreeNode>();
            foreach (TreeNode n in node.Nodes)
            {
                if (n.Tag != null && (string)n.Tag == id)
                    Nodes.Add(n);
                Nodes.AddRange(FindNodes(n, id));
            }
            return Nodes;
        }

        public void AutoSearchItemAsync(Item item, Action done)
        {
            if (item == null) return;
            Async.Queue("AutoSearchItem", () =>
                {
                    if (!AutoSearchItem(item))
                        throw new Exception();

                }, done, false, 0, "Recherche données pour " + item.Title);
        }

        public bool AutoSearchItem(Item item)
        {
            Item resultItem = ProvidersUtil.AutoFind(item);
            if (resultItem != null)
            {
                int index = ItemsList.FindIndex(i => i.Id == item.Id);
                ItemsList[index] = Helper.UpdateItem(item, resultItem);
                ItemsList[index].HasChanged = true;
                TreeNode tn = FindNodeById(item.Id);
                if (tn != null)
                {
                    if (item.Type != Entity.Season)
                    {
                        tn.Name = resultItem.Title;
                    }
                    if (item.Type == Entity.Series)
                    {
                        SetSeriesID(tn, resultItem.ProvidersId);
                    }
                    tn.Checked = true;
                    tn.Checked = false;
                    RefreshNode(tn);
                }
                return true;
            }
            else
                return false;
        }

        public void SetSeriesID(TreeNode node, List<DataProviderId> dp)
        {
            if (dp == null) return;
            foreach (TreeNode n in node.Nodes)
            {
                Item i = ItemsList[ItemsList.FindIndex(item => item.Id == (string)n.Tag)];
                if (i.ProvidersId == null)
                    i.ProvidersId = new List<DataProviderId>();
                i.ProvidersId.RemoveAll(pi => dp.Exists(p => p.Name == pi.Name));
                i.ProvidersId.AddRange(dp);
                if (n.Nodes.Count > 0)
                    SetSeriesID(n, dp);
            }
        }


    }


}