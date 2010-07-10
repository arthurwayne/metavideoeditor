using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;
using System.Threading;
using Microsoft.VisualBasic.FileIO;
using CustomControls;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class MetaVideoEditor : Form
    {
        static Config Config
        {
            get { return Config.Instance; }
        }

        List<Item> ItemsList
        {
            get { return Kernel.Instance.ItemCollection.ItemsList; }
        }

        Item SelectedItem
        {
            get { return Kernel.Instance.ItemCollection.SelectedItem; }
            set { Kernel.Instance.ItemCollection.SelectedItem = value; }
        }    


        public MetaVideoEditor()
        {
            this.Hide();
            Thread splashthread = new Thread(new ThreadStart(SplashScreen.ShowSplashScreen));
            splashthread.IsBackground = true;
            splashthread.Start();
            SplashScreen.UdpateProgressBar(0);
            SplashScreen.UdpateStatusText("Loading Kernel");
            mveEngine.Kernel.Init();
            Kernel.Instance.Message.OnMessageAdded += new GenerateMessage.MessageGeneratedEventHandler(Message_OnMessageAdded);
            Kernel.Instance.Message.OnProcessFails += new GenerateMessage.MessageGeneratedEventHandler(Message_OnProcessFails);
            Kernel.Instance.Message.OnProcessSuccess += new GenerateMessage.MessageGeneratedEventHandler(Message_OnProcessSuccess);
            Kernel.Instance.Message.OnProcessStart += new GenerateMessage.MessageGeneratedEventHandler(Message_OnProcessStart);
            Kernel.Instance.Message.OnRefreshNode += new GenerateMessage.RefreshNodeEventHandler(Message_OnRefreshNode);
            Logger.ReportVerbose("Start MetaVideoEditor");
            SplashScreen.UdpateProgressBar(50);
            SplashScreen.UdpateStatusText(Kernel.Instance.GetString("InitializeComponentsSS"));
            InitializeComponent();
            Initialize();

            SplashScreen.UdpateProgressBar(100);
            LoadData();
            
            
            this.Show();            
            this.Activate();
            SplashScreen.CloseSplashScreen();
        }

        private void Initialize()
        {            
            this.themeToolStripItem.DropDownClosed += new EventHandler(this.ribbonItem2.ClearBackground);
            this.saveToolStripItem1.DropDownClosed += new EventHandler(this.SaveButton.ClearBackground);

            //Localization
            tab1.Text = Kernel.Instance.GetString("HomeMW");
            ribbonButton1.Text = Kernel.Instance.GetString("SettingsMW");
            themeToolStripItem.Text = Kernel.Instance.GetString("ThemeMW");
            ToolStripItem1.Text = Kernel.Instance.GetString("AzurTheme");
            ToolStripItem2.Text = Kernel.Instance.GetString("MetalTheme");
            ToolStripItem3.Text = Kernel.Instance.GetString("DarkTheme");
            ToolStripItem4.Text = Kernel.Instance.GetString("NatureTheme");
            ToolStripItem5.Text = Kernel.Instance.GetString("DawnTheme");
            ToolStripItem6.Text = Kernel.Instance.GetString("CornTheme");
            ToolStripItem7.Text = Kernel.Instance.GetString("ChocolateTheme");
            ToolStripItem8.Text = Kernel.Instance.GetString("NavyTheme");
            ToolStripItem9.Text = Kernel.Instance.GetString("IceTheme");
            ToolStripItem10.Text = Kernel.Instance.GetString("VanillaTheme");
            ToolStripItem11.Text = Kernel.Instance.GetString("CanelaTheme");
            ToolStripItem12.Text = Kernel.Instance.GetString("CakeTheme");
            ribbonButton2.Text = Kernel.Instance.GetString("AboutMW");
            ribbonButton5.Text = Kernel.Instance.GetString("SearchMW");
            ribbonButton6.Text = Kernel.Instance.GetString("AutoMW");
            ribbonButton7.Text = Kernel.Instance.GetString("RestoreMW");
            saveToolStripItem1.Text = Kernel.Instance.GetString("SaveMW");
            SaveCurrentButton.Text = Kernel.Instance.GetString("SelectedItemMW");
            SaveCheckedButton.Text = Kernel.Instance.GetString("CheckedItemsMW");
            SaveModifiedButton.Text = Kernel.Instance.GetString("ModifiedItemsMW");
            ribbonButton10.Text = Kernel.Instance.GetString("RebuildMW");
            tabPanel5.Caption = Kernel.Instance.GetString("SettingsDisplayMW");
            tabPanel7.Caption = Kernel.Instance.GetString("SearchMW");
            tabPanel6.Caption = Kernel.Instance.GetString("MediaCollectionMW");
            Medialabel.Text = Kernel.Instance.GetString("MediaCollectionMW");
            SearchBox.Text = Kernel.Instance.GetString("SearchMW") + "...";
            tabPageOverview.Text = Kernel.Instance.GetString("TitleOvTab");
            tabPageGeneral.Text = Kernel.Instance.GetString("TitleGeTab");
            tabPageActors.Text = Kernel.Instance.GetString("TitleActTab");
            tabPageCrew.Text = Kernel.Instance.GetString("TitleCreTab");
            tabPageTrailers.Text = Kernel.Instance.GetString("TitleTraTab");
            tabPagePoster.Text = Kernel.Instance.GetString("TitlePoTab");
            tabPageBackdrop.Text = Kernel.Instance.GetString("TitleBdTab");
            tabPageBanners.Text = Kernel.Instance.GetString("TitleBaTab");
            tabPageGenres.Text = Kernel.Instance.GetString("TitleGenTab");
            tabPageStudios.Text = Kernel.Instance.GetString("TitleStTab");
            tabPageCountries.Text = Kernel.Instance.GetString("TitleCoTab");
            tabPageTagLines.Text = Kernel.Instance.GetString("TitleTagTab");

            
            if (Config.FullscreenMode)
                WindowHelper.GeometryFromString(Config.WindowGeometry, this);
            else
                this.WindowState = FormWindowState.Maximized;

            OverviewPanel overviewPanel = new OverviewPanel();
            GeneralPanel generalPanel = new GeneralPanel();
            PostersPanel postersPanel = new PostersPanel();
            BackdropsPanel backdropsPanel = new BackdropsPanel();
            ActorsPanel actorsPanel = new ActorsPanel();
            GenresPanel genresPanel = new GenresPanel();
            StudiosPanel studiosPanel = new StudiosPanel();
            CountriesPanel countriesPanel = new CountriesPanel();
            TagLinesPanel taglinesPanel = new TagLinesPanel();
            CrewPanel crewPanel = new CrewPanel();
            BannersPanel bannersPanel = new BannersPanel();
            TrailersPanel trailersPanel = new TrailersPanel();

            AddPanel(overviewPanel, tabPageOverview);
            AddPanel(generalPanel, tabPageGeneral);
            AddPanel(postersPanel, tabPagePoster);
            AddPanel(backdropsPanel, tabPageBackdrop);
            AddPanel(actorsPanel, tabPageActors);
            AddPanel(genresPanel, tabPageGenres);
            AddPanel(studiosPanel, tabPageStudios);
            AddPanel(countriesPanel, tabPageCountries);
            AddPanel(taglinesPanel, tabPageTagLines);
            AddPanel(crewPanel, tabPageCrew);
            AddPanel(bannersPanel, tabPageBanners);
            AddPanel(trailersPanel, tabPageTrailers);

            InitctMenu();
            SetStyle(Config.ColorTheme);
            itemsView.Select();
            RefreshCurrentTab();
        }

        private void AddPanel(BasePanel panel, TabPage page)
        {
            panel.Dock = DockStyle.Fill;
            page.Controls.Add(panel.MainPanel);
            page.Tag = panel;
        }

        private void LoadData()
        {
            Kernel.Instance.ItemCollection.CheckedItems.Clear();

            if (Config.IsFirstRun)
            {
                SplashScreen.HideSplashScreen();
                var form = new ConfigWizard();
                form.Owner = this;
                form.ShowDialog();
                SplashScreen.ReShowSplashScreen();
                Kernel.ReloadKernel();
            }
            SplashScreen.UdpateProgressBar(20);

            foreach (TreeNode tn in Kernel.Instance.ItemCollection.MainNodes)
                itemsView.Nodes.Add(tn);
            SplashScreen.UdpateProgressBar(60);
            SplashScreen.UdpateStatusText(Kernel.Instance.GetString("LoadingCacheSS"));
            foreach (Item item in Kernel.Instance.ItemCollection.ItemsList)
            {
                string path = Path.Combine(ApplicationPaths.AppCachePath, item.Id);
                if (File.Exists(path))
                {
                    try
                    {
                        Item i = (Item)Serializer.Deserialization(Path.Combine(ApplicationPaths.AppCachePath, item.Id));
                        ProvidersUtil.UpdateItem(item, i);
                    }
                    catch { continue; }

                }
            }
            SplashScreen.UdpateProgressBar(100);
            Async.Queue(Kernel.Instance.GetString("RefreshLibraryAsync"), () =>
            {
                Kernel.Instance.ItemCollection.RefreshData();
            });

            Async.Queue(Kernel.Instance.GetString("CheckUpdatesAsync"), () =>
                {
                    Updater.DownloadUpdateFile();
                }, SetUpdate);
        }

        private void MetaVideoEditor_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (messageCount > 0)
            {
                if (MessageBox.Show(Kernel.Instance.GetString("CloseConfirmMess"), "MetaVideoEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                {
                    e.Cancel = true;
                    return;
                }
                else
                    messageCount = 0;
            }
            Config.WindowGeometry = WindowHelper.GeometryToString(this);
            Application.Exit();
        }

        //Shortcut Keys
        private void MetaVideoEditor_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode.ToString() == "R")
                SearchButton_Click(sender, e);
            else if (e.Control && e.KeyCode.ToString() == "O")
                openItem(sender, e);
            else if (e.Control && e.KeyCode.ToString() == "A")
                AutoButton_Click(sender, e);
        }

        private void SetUpdate()
        {
            int updateCount = Updater.UpdateAvailable();
            if (updateCount > 0)
            {
                SetUpdateCount(updateCount);
            }
            else
                toolStripDropDownButton1.Visible = false;
        }

        delegate void SetUpdateCountDelegate(int count);
        void SetUpdateCount(int count)
        {
            if (InvokeRequired)
            {
                Invoke(new SetUpdateCountDelegate(SetUpdateCount), new object[] { count });
                return;
            }
            string text;
            if (count > 1) text = Kernel.Instance.GetString("AvailableUpdatesMW");
            else text = Kernel.Instance.GetString("AvailableUpdateMW");
            toolStripDropDownButton1.Text = string.Format(text, count.ToString());
        }

        private void toolStripDropDownButton1_Click(object sender, EventArgs e)
        {
            var form = new UpdateForm();
            form.Owner = this;
            DialogResult dr = form.ShowDialog();
            if (dr == DialogResult.OK)
            {
                Async.Queue(Kernel.Instance.GetString("CheckUpdatesAsync"), () =>
                {
                    Updater.DownloadUpdateFile();
                }, SetUpdate);
            }
        }

        #region Async messages
        int messageCount = 0;
        private void Message_OnMessageAdded(object sender, MessageEvents e)
        {
            messageCount++;
            SetMsgCount();
            SetButtonVisiblity();
            AddProcess(e.Message);
        }

        void Message_OnProcessStart(object sender, MessageEvents e)
        {
            StartProcess(e.Message);
        }

        void Message_OnProcessSuccess(object sender, MessageEvents e)
        {
            messageCount--;
            SetMsgCount();
            SetButtonVisiblity();
            RemoveProcess(e.Message, true);
        }
        void Message_OnProcessFails(object sender, MessageEvents e)
        {
            messageCount--;
            SetMsgCount();
            SetButtonVisiblity();
            RemoveProcess(e.Message, false);
        }
        
        delegate void SetMsgCountCallback();
        private void SetMsgCount()
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetMsgCountCallback(SetMsgCount), new object[] { });
                return;
            }
            string text = "";
            if (messageCount == 0)
                text = Kernel.Instance.GetString("NoActionMW");
            else if (messageCount == 1)
                text = string.Format(Kernel.Instance.GetString("ActionMW"), messageCount.ToString());
            else
                text = string.Format(Kernel.Instance.GetString("ActionsMW"), messageCount.ToString());
            messageDropDownButton.Text = text;
        }

        delegate void SetButtonVisiblityDelegate();
        private void SetButtonVisiblity()
        {
            if (this.InvokeRequired)
            {
                Invoke(new SetButtonVisiblityDelegate(SetButtonVisiblity), new object[] { });
                return;
            }
            if (messageCount == 0)
                messageDropDownButton.Image = null;
            else
                messageDropDownButton.Image = global::MetaVideoEditor.Properties.Resources.red_button;
        }

        delegate void AddProcessCallback(string text);
        private void AddProcess(string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new AddProcessCallback(AddProcess), new object[] { text });
                return;
            }
            messageDropDownButton.AddItem(text);
        }

        delegate void StartProcessCallback(string text);
        private void StartProcess(string text)
        {
            if (this.InvokeRequired)
            {
                Invoke(new StartProcessCallback(StartProcess), new object[] { text });
                return;
            }
            messageDropDownButton.StartProcess(text);
        }

        delegate void RemoveProcessCallback(string text, bool success);
        private void RemoveProcess(string text, bool success)
        {
            if (this.InvokeRequired)
            {
                Invoke(new RemoveProcessCallback(RemoveProcess), new object[] { text, success });
                return;
            }
            messageDropDownButton.RemoveItem(text, success);
        }

        #endregion

        #region TreeView

        private void itemsView_AfterSelect(object sender, TreeViewEventArgs e)
        {
            itemsView.Focus();
            if (Kernel.Instance.ItemCollection.SelectedNode != null)
                itemsView.Invalidate(Kernel.Instance.ItemCollection.SelectedNode.Bounds);
            Kernel.Instance.ItemCollection.SelectedNode = itemsView.SelectedNode;
            RefreshCurrentTab();
        }

        private void itemsView_AfterCheck(object sender, TreeViewEventArgs e)
        {
            if (e.Node.Tag != null)
            {
                if (e.Node.Checked)
                    Kernel.Instance.ItemCollection.CheckedItems.Add(Kernel.Instance.ItemCollection.FindById((string)e.Node.Tag));
                else
                    Kernel.Instance.ItemCollection.CheckedItems.RemoveAll(i => i.Id == ((string)e.Node.Tag));
            }
            foreach (TreeNode node in e.Node.Nodes)
            {
                node.Checked = e.Node.Checked;
                if (node.Checked && !Kernel.Instance.ItemCollection.CheckedItems.Exists(i => i.Id == (string)node.Tag))
                    Kernel.Instance.ItemCollection.CheckedItems.Add(Kernel.Instance.ItemCollection.FindById((string)node.Tag));
                else if (!node.Checked && Kernel.Instance.ItemCollection.CheckedItems.Exists(i => i.Id == (string)node.Tag))
                    Kernel.Instance.ItemCollection.CheckedItems.RemoveAll(i => i.Id == ((string)node.Tag));
            }
        }

        Icon minusIcon = Icon.FromHandle(new Bitmap(global::MetaVideoEditor.Properties.Resources.icon_minus).GetHicon());
        Icon plusIcon = Icon.FromHandle(new Bitmap(global::MetaVideoEditor.Properties.Resources.icon_plus).GetHicon());
        private void itemsView_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            int x = e.Bounds.X + e.Node.Level * 20;
            if (e.Node.IsSelected)
            {
                if (itemsView.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }

            if (e.Node.Nodes.Count > 0)
            {
                if (e.Node.IsExpanded)
                {
                    e.Graphics.DrawIcon(minusIcon, x, e.Bounds.Y + 5);
                }
                else
                {
                    e.Graphics.DrawIcon(plusIcon, x, e.Bounds.Y + 5);
                }
            }

            x += 25;
            if (e.Node.Checked)
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox_checked, x, e.Bounds.Y + 5);
            }
            else
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox, x, e.Bounds.Y + 5);
            }
            x += 15;
            e.Graphics.DrawLine(Pens.Black, 0, e.Bounds.Y, e.Node.TreeView.Width, e.Bounds.Y);

            Brush brush = Brushes.Black;
            Font font = itemsView.Font;
            if (Config.UseTreeColor)
            {
                Item item = Kernel.Instance.ItemCollection.FindById((string)e.Node.Tag);
                if (item != null)
                {
                    if (item.HasChanged)
                        font = new Font(font.FontFamily.Name, font.Size, FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
                    if (item.DataState < 21)
                        brush = Brushes.Red;
                    else if (item.DataState < 70)
                        brush = Brushes.Blue;
                }
            }
            e.Graphics.DrawString(e.Node.Name, font, brush, x, e.Bounds.Y + 5);
        }

        private void itemsView_MouseMove(object sender, MouseEventArgs e)
        {
            TreeNode theNode = this.itemsView.GetNodeAt(e.X, e.Y);
            if ((theNode != null))
            {
                if (theNode.ToolTipText != null)
                {
                    if (theNode.ToolTipText != this.toolTip1.GetToolTip(this.itemsView))
                    {
                        this.toolTip1.SetToolTip(this.itemsView, theNode.ToolTipText);
                    }
                }
                else
                {
                    this.toolTip1.SetToolTip(this.itemsView, "");
                }
            }
            else
            {
                this.toolTip1.SetToolTip(this.itemsView, "");
            }
        }

        delegate void refreshNodeDelegate(TreeNode node);
        private void SafeRefreshNode(TreeNode node)
        {
            if (itemsView.InvokeRequired)
                this.Invoke(new refreshNodeDelegate(RefreshNode), new object[] { node });
            else
                RefreshNode(node);
        }
        private void RefreshNode(TreeNode node)
        {
            node.TreeView.Refresh();
        }
        void Message_OnRefreshNode(object sender, NodeEvents e)
        {
            SafeRefreshNode(e.Node);            
        }
        

        ContextMenuStrip treeviewContextMenu;        
        private void InitctMenu()
        {
            treeviewContextMenu = new ContextMenuStrip();

            ContextMenuItem openMenu = new ContextMenuItem();
            openMenu.Text = Kernel.Instance.GetString("OpenMW");
            openMenu.Shortcut = "Ctrl+O";
            openMenu.Image = global::MetaVideoEditor.Properties.Resources.folder;
            openMenu.Click += new EventHandler(openItem);
            treeviewContextMenu.Items.Add(openMenu);

            ContextMenuItem saveMenu = new ContextMenuItem();
            saveMenu.Text = Kernel.Instance.GetString("SaveMW");
            saveMenu.Name = "saveMenu";
            saveMenu.Shortcut = "Ctrl+S";
            saveMenu.Image = global::MetaVideoEditor.Properties.Resources.save;
            saveMenu.Click += new EventHandler(saveMenu_Click);
            treeviewContextMenu.Items.Add(saveMenu);

            ContextMenuItem deleteMenu = new ContextMenuItem();
            deleteMenu.Text = Kernel.Instance.GetString("DeleteMW");
            deleteMenu.Name = "deleteMenu";
            deleteMenu.ShortcutKeys = (Keys)(Keys.Control | Keys.Delete);
            deleteMenu.Image = global::MetaVideoEditor.Properties.Resources.delete;
            deleteMenu.Click += new EventHandler(deleteItem);
            treeviewContextMenu.Items.Add(deleteMenu);

            itemsView.ContextMenuStrip = treeviewContextMenu;
        }
        
        private void itemsView_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Right)
            {
                Point p = new Point(e.X, e.Y); 
                TreeNode node = itemsView.GetNodeAt(p);
                if (node != null && node.Tag != null)
                {
                    itemsView.SelectedNode = node;
                    treeviewContextMenu.Show(itemsView, p);
                }
            }
        }


        #endregion

        #region actions

        void deleteItem(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Kernel.Instance.GetString("NoSelectedItemMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Directory.Exists(SelectedItem.Path) || File.Exists(SelectedItem.Path))
            {
                bool remove = true;
                try
                {
                    if (Directory.Exists(SelectedItem.Path))
                    {
                        FileSystem.DeleteDirectory(SelectedItem.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                    }
                    else if (File.Exists(SelectedItem.Path))
                        FileSystem.DeleteFile(SelectedItem.Path, UIOption.AllDialogs, RecycleOption.SendToRecycleBin, UICancelOption.ThrowException);
                }
                catch { remove = false; }
                if (remove)
                {
                    Kernel.Instance.ItemCollection.ItemsList.Remove(Kernel.Instance.ItemCollection.ItemsList.Find(i => i.Id == SelectedItem.Id));
                    itemsView.Nodes.Remove(Kernel.Instance.ItemCollection.SelectedNode);
                    itemsView.Refresh();
                }
            }
            else
            {
                MessageBox.Show(string.Format(Kernel.Instance.GetString("PathExistMess"), SelectedItem.Path), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
        }

        void openItem(object sender, EventArgs e)
        {
            if (SelectedItem == null) return;
            if (Directory.Exists(SelectedItem.Path))
                System.Diagnostics.Process.Start(SelectedItem.Path);
            else if (File.Exists(SelectedItem.Path))
                System.Diagnostics.Process.Start(Directory.GetParent(SelectedItem.Path).FullName);
        }

        void saveMenu_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Kernel.Instance.GetString("NoSelectedItemMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Kernel.Instance.ItemCollection.SaveItem(SelectedItem);
        }

        private void SaveCurrentButton_Click(object sender, EventArgs e)
        {
            saveMenu_Click(sender, e);
        }

        private void SaveCheckedButton_Click(object sender, EventArgs e)
        {
            if (Kernel.Instance.ItemCollection.CheckedItems.Count == 0)
            {
                MessageBox.Show(Kernel.Instance.GetString("NoCheckedItemMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (Item item in Kernel.Instance.ItemCollection.CheckedItems)
            {
                Kernel.Instance.ItemCollection.SaveItem(item);
            }
        }

        private void SaveModifiedButton_Click(object sender, EventArgs e)
        {
            if (Kernel.Instance.ItemCollection.ModifiedItems.Count == 0)
            {
                MessageBox.Show(Kernel.Instance.GetString("NoModifiedItemMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            foreach (Item item in Kernel.Instance.ItemCollection.ModifiedItems)
            {
                Kernel.Instance.ItemCollection.SaveItem(item);
            }
        }

        private void SearchButton_Click(object sender, EventArgs e)
        {
            if (SelectedItem == null)
            {
                MessageBox.Show(Kernel.Instance.GetString("NoSelectedItemMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            Search form = new Search(SelectedItem);
            form.Owner = this;
            DialogResult res = form.ShowDialog();
            if (res == DialogResult.OK)
            {
                itemsView.SelectedNode.Name = form.currentItem.Title;
                SelectedItem = form.currentItem;
                RefreshNode(itemsView.SelectedNode);
                if (SelectedItem.Type == Entity.Series)
                    Kernel.Instance.ItemCollection.SetSeriesID(itemsView.SelectedNode, SelectedItem.ProvidersId);
                RefreshCurrentTab();
                
            }
        }
        
        private void AutoButton_Click(object sender, EventArgs e)
        {            
            if (Kernel.Instance.ItemCollection.CheckedItems.Count > 0)
            {

                foreach (Item item in Kernel.Instance.ItemCollection.CheckedItems)
                {
                    Kernel.Instance.ItemCollection.AutoSearchItemAsync(item, () => 
                    { 
                        if (SelectedItem != null && SelectedItem.Id == item.Id)
                            RefreshCurrentTab();
                    });
                   
                }
            }
            else if (SelectedItem != null)
            {
                Cursor = Cursors.WaitCursor;
                if (Kernel.Instance.ItemCollection.AutoSearchItem(SelectedItem))
                {
                    Cursor = Cursors.Default;
                    RefreshCurrentTab();
                    itemsView.SelectedNode.Name = SelectedItem.Title;
                    RefreshNode(itemsView.SelectedNode);
                }
                else
                {
                    Cursor = Cursors.Default;
                    MessageBox.Show(Kernel.Instance.GetString("NoResultMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    if (SelectedItem.Type == Entity.Episode || SelectedItem.Type == Entity.Season)
                    {
                        TreeNode seriesNode = itemsView.SelectedNode.Parent;
                        if (ItemsList.Find(i => i.Id == (string)seriesNode.Tag).Type != Entity.Series)
                            seriesNode = seriesNode.Parent;
                        itemsView.SelectedNode = seriesNode;
                    }
                    SearchButton_Click(sender, e);
                }
            }
            else
                MessageBox.Show(Kernel.Instance.GetString("SelectItemsMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
        }

        private void InitButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Kernel.Instance.GetString("DeleteCacheMess"), "MetaVideoEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation)
                == DialogResult.Yes)
            {
                Cursor = Cursors.WaitCursor;                
                DirectoryInfo di = new DirectoryInfo(ApplicationPaths.AppCachePath);
                foreach (FileInfo fi in di.GetFiles())
                {
                    try
                    {
                        File.Delete(fi.FullName);
                    }
                    catch { }
                }
                ReInit();
                Cursor = Cursors.Default;
            }
        }

        private void ReInit()
        {
            itemsView.Nodes.Clear();
            Kernel.Instance.ItemCollection.SelectedNode = null;
            RefreshCurrentTab();
            Kernel.Instance.ItemCollection = new ItemCollection(Config.RootFolders);            
            LoadData();
        }

        private void CancelButton_Click(object sender, EventArgs e)
        {
            if (Kernel.Instance.ItemCollection.CheckedItems.Count > 0)
            {
                List<Item> checkedCopy = new List<Item>();
                foreach (Item i in Kernel.Instance.ItemCollection.CheckedItems)
                    checkedCopy.Add(i);
                foreach (Item i in checkedCopy)
                {
                    if (i.HasChanged)
                    {
                        Item retrievedItem;
                        try
                        {
                            retrievedItem = (Item)Serializer.Deserialization(Path.Combine(ApplicationPaths.AppCachePath, i.Id));
                        }
                        catch (Exception ex)
                        {
                            Logger.ReportException("Fails to deserialize file " + Path.Combine(ApplicationPaths.AppCachePath, i.Id), ex);
                            continue;
                        }
                        retrievedItem.HasChanged = false;
                        ItemsList[ItemsList.FindIndex(I => i.Id == I.Id)] = retrievedItem;

                        TreeNode tn = Kernel.Instance.ItemCollection.FindNodeById(i.Id);  
                        tn.Name = retrievedItem.Title;
                        tn.Checked = false;
                        RefreshNode(tn);
                    }
                }

                if (itemsView.SelectedNode.Tag != null)
                {
                    RefreshCurrentTab();
                }
            }
            else if (SelectedItem != null && SelectedItem.HasChanged)
            {
                Item retrievedItem;
                try
                {
                    retrievedItem = (Item)Serializer.Deserialization(Path.Combine(ApplicationPaths.AppCachePath, SelectedItem.Id));
                }
                catch (Exception ex)
                {
                    Logger.ReportException("Fails to deserialize file " + Path.Combine(ApplicationPaths.AppCachePath, SelectedItem.Id), ex);
                    return;
                }
                retrievedItem.HasChanged = false;
                SelectedItem = retrievedItem;
                itemsView.SelectedNode.Name = retrievedItem.Title;
                RefreshNode(itemsView.SelectedNode);
                RefreshCurrentTab();
            }
        }



        private void OptionButton_Click(object sender, EventArgs e)
        {
            var form = new Settings();
            form.Owner = this;
            DialogResult res = form.ShowDialog();
            SetStyle(Config.ColorTheme);
            if (res == DialogResult.OK)
                ReInit();
        }

        private void AproposButton_Click(object sender, EventArgs e)
        {
            Item i = SelectedItem;
            var form = new Apropos();
            form.Owner = this;
            form.ShowDialog();
        }


        #endregion

        #region Style

        private void Set_lB_Style(object sender, EventArgs e)
        {
            SetStyle((string)((CustomControls.ContextMenuItem)sender).Tag);
        }

        public void SetStyle(string Name)
        {
            Config.ColorTheme = Name;
            foreach (ToolStripMenuItem tsItem in themeToolStripItem.DropDownItems)
            {
                tsItem.Checked = ((string)tsItem.Tag == Name);
            }
            ColorsTheme colors = ColorStyle.GetColors(); 
            this.BackColor = colors.BackColor;
            foreach (TabPage tp in tabControl.TabPages)
            {
                tp.BackColor = colors.BackColor;
                foreach (Control c in tp.Controls)
                {
                    if (c.GetType() == typeof(CustomControls.DoubleBufferPanel))
                    {
                        CustomControls.DoubleBufferPanel panel = c as CustomControls.DoubleBufferPanel;
                        if (Config.Background == "wmc")
                            panel.BackgroundImage = ImageUtil.SetImgOpacity(global::MetaVideoEditor.Properties.Resources.BackgroundDefault, (float).5);                        
                        else
                            panel.BackgroundImage = null;
                    }
                }
                
            }
            SetBase(colors.BaseColor.R, colors.BaseColor.G, colors.BaseColor.B, colors.HaloColor); 
        }

        void SetBase(int R, int G, int B, Color HaloColor)
        {
            this.SuspendLayout();

            
            Medialabel.BackColor = Color.FromArgb(R, G, B);              
            SearchPanel.BackColor = Color.FromArgb(R - 16, G - 11, B - 6);
            SetLabelColor(Medialabel, R, G, B);
            
            foreach (Control control in this.panel1.Controls)
            {
                if (typeof(RibbonStyle.TabStrip) == control.GetType())
                {
                    ((RibbonStyle.TabStripProfessionalRenderer)((RibbonStyle.TabStrip)control).Renderer).HaloColor = HaloColor;
                    ((RibbonStyle.TabStripProfessionalRenderer)((RibbonStyle.TabStrip)control).Renderer).BaseColor = Color.FromArgb(R + 4, G + 3, B + 3);
                    for (int i = 0; i < ((RibbonStyle.TabStrip)control).Items.Count; i++)
                    {
                        RibbonStyle.Tab _tab = (RibbonStyle.Tab)((RibbonStyle.TabStrip)control).Items[i];

                        #region Set Tab Colors
                        if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
                        {
                            try
                            {
                                _tab.ForeColor = Color.FromArgb(R + 76, G + 71, B + 66);
                            }
                            catch
                            {
                                _tab.ForeColor = Color.FromArgb(250, 250, 250);
                            }
                        }
                        else
                        {
                            try
                            {
                                _tab.ForeColor = Color.FromArgb(R - 96, G - 91, B - 86);
                            }
                            catch
                            {
                                _tab.ForeColor = Color.FromArgb(10, 10, 10);
                            }
                        }
                        #endregion
                    }

                    control.BackColor = Color.FromArgb(R - 24, G - 8, B + 12);

                }
                if (typeof(RibbonStyle.TabPageSwitcher) == control.GetType())
                {
                    control.BackColor = Color.FromArgb(R - 24, G - 8, B + 12);

                    foreach (Control _control in control.Controls)
                    {
                        if (typeof(RibbonStyle.TabStripPage) == _control.GetType())
                        {
                            ((RibbonStyle.TabStripPage)_control).BaseColor = Color.FromArgb(R, G, B);
                            ((RibbonStyle.TabStripPage)_control).BaseColorOn = Color.FromArgb(R, G, B);

                            foreach (Control __control in _control.Controls)
                            {
                                if (typeof(RibbonStyle.TabPanel) == __control.GetType())
                                {
                                    #region Set TabPanel Colors
                                    if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
                                    {
                                        try
                                        {
                                            __control.ForeColor = Color.FromArgb(R + 76, G + 71, B + 66);
                                        }
                                        catch
                                        {
                                            __control.ForeColor = Color.FromArgb(250, 250, 250);
                                        }
                                    }
                                    else
                                    {
                                        try
                                        {
                                            __control.ForeColor = Color.FromArgb(R - 96, G - 91, B - 86);
                                        }
                                        catch
                                        {
                                            __control.ForeColor = Color.FromArgb(10, 10, 10);
                                        }
                                    }
                                    #endregion

                                    ((RibbonStyle.TabPanel)__control).BaseColor = Color.FromArgb(R, G, B);
                                    ((RibbonStyle.TabPanel)__control).BaseColorOn = Color.FromArgb(R + 16, G + 11, B + 6);

                                    foreach (Control ___control in __control.Controls)
                                    {
                                        if (typeof(RibbonStyle.RibbonButton) == ___control.GetType())
                                        {
                                            ((RibbonStyle.RibbonButton)___control).InfoColor = Color.FromArgb(R, G, B);

                                            RibbonStyle.RibbonButton _but = (RibbonStyle.RibbonButton)___control;

                                            #region Set Button Colors
                                            if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
                                            {
                                                try
                                                {
                                                    _but.ForeColor = Color.FromArgb(R + 76, G + 71, B + 66);
                                                }
                                                catch
                                                {
                                                    _but.ForeColor = Color.FromArgb(250, 250, 250);
                                                }
                                            }
                                            else
                                            {
                                                try
                                                {
                                                    _but.ForeColor = Color.FromArgb(R - 96, G - 91, B - 86);
                                                }
                                                catch
                                                {
                                                    _but.ForeColor = Color.FromArgb(10, 10, 10);
                                                }
                                            }
                                            #endregion

                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            }
            this.ResumeLayout(false);
        }

        void SetLabelColor(Label label, int R, int G, int B)
        {
            if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
            {
                try
                {
                    label.ForeColor = Color.FromArgb(R + 76, G + 71, B + 66);
                }
                catch
                {
                    label.ForeColor = Color.FromArgb(250, 250, 250);
                }
            }
            else
            {
                try
                {
                    label.ForeColor = Color.FromArgb(R - 96, G - 91, B - 86);
                }
                catch
                {
                    label.ForeColor = Color.FromArgb(10, 10, 10);
                }
            }
        }

        #endregion

        #region Local search
        private void SearchBox_TextChanged(object sender, EventArgs e)
        {

            if (SearchThread != null && SearchThread.IsAlive)
                SearchThread.Abort();
            SearchThread = new Thread(new ThreadStart(DisplaySeachrResults));
            SearchThread.IsBackground = true;
            SearchThread.Start();
        }

        private Thread SearchThread;
        private void DisplaySeachrResults()
        {
            this.SuspendLayout();
            if (this.itemsView.InvokeRequired)
            {
                ClearTreeCallback d = new ClearTreeCallback(ClearTree);
                this.Invoke(d, new object[] { });
            }
            else
                ClearTree();
            TreeNode nodes = new TreeNode();
            foreach (TreeNode tn in Kernel.Instance.ItemCollection.MainNodes)
            {
                TreeNode children = RecursiveSearch(tn, SearchBox.Text);

                if (children.Nodes.Count > 0)
                {
                    if (this.itemsView.InvokeRequired)
                    {
                        AddNodeCallback d = new AddNodeCallback(AddNode);
                        this.Invoke(d, new object[] { children });
                    }
                    else
                        AddNode(children);
                    if (SearchBox.Text != "")
                    {
                        if (this.itemsView.InvokeRequired)
                        {
                            ExpandNodeCallback d = new ExpandNodeCallback(ExpandNode);
                            this.Invoke(d, new object[] { children });
                        }
                        else
                            ExpandNode(children);
                    }
                }
            }
            this.ResumeLayout();
        }

        delegate void ClearTreeCallback();
        private void ClearTree()
        {
            itemsView.Nodes.Clear();
        }
        delegate void AddNodeCallback(TreeNode node);
        private void AddNode(TreeNode node)
        {
            itemsView.Nodes.Add(node);
        }
        delegate void ExpandNodeCallback(TreeNode node);
        private void ExpandNode(TreeNode node)
        {
            node.Expand();
        }



        private TreeNode RecursiveSearch(TreeNode node, string text)
        {
            TreeNode res = new TreeNode();
            res.Name = node.Name;
            foreach (TreeNode tn in node.Nodes)
            {
                if (tn.Name.ToLower().Contains(text.ToLower()))
                {
                    res.Nodes.Add(tn);
                }
                else if (tn.Nodes.Count > 0)
                {
                    TreeNode chidren = RecursiveSearch(tn, text);
                    if (chidren.Nodes.Count > 0)
                        res.Nodes.Add(chidren);
                }
            }
            return res;
        }

        private void SearchBox_Enter(object sender, EventArgs e)
        {
            if (SearchBox.Text == Kernel.Instance.GetString("SearchMW") + "...")
            {
                SearchBox.Text = "";
                SearchBox.ForeColor = Color.Black;
                SearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            }
            SearchPanel.BackgroundImage = global::MetaVideoEditor.Properties.Resources.B_click2;
        }

        private void SearchBox_Leave(object sender, EventArgs e)
        {
            SearchPanel.BackgroundImage = null;
        }

        #endregion

        #region TabControl

        private void tabControl_DrawItem(object sender, DrawItemEventArgs e)
        {
            Font font = e.Font;
            Brush back_brush;
            Brush fore_brush = new SolidBrush(e.ForeColor);
            ColorsTheme colors = ColorStyle.GetColors();
            Rectangle bounds = e.Bounds;
            //tabControl2.TabPages[e.Index].BackColor = Color.Silver;
            bounds = new Rectangle(bounds.X , -1, bounds.Width , bounds.Height + 5);

            int R = colors.BaseColor.R;
            int G = colors.BaseColor.G;
            int B = colors.BaseColor.B;
            if (e.Index == tabControl.SelectedIndex)
            {
                //font = new Font(e.Font, e.Font.Style);
                //back_brush = new SolidBrush(Color.DimGray);
                fore_brush = new SolidBrush(Color.Black);
                if (tabControl.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, bounds);
            }
            else
            {
                //font = new Font(e.Font, e.Font.Style & ~FontStyle.Bold);
                back_brush = new SolidBrush(colors.BaseColor);
                //fore_brush = new SolidBrush(tabControl2.TabPages[e.Index].ForeColor);
                //Rectangle r = e.Bounds;
                //r.Height += 5;
                e.Graphics.FillRectangle(back_brush, bounds.X, bounds.Y, bounds.Width, bounds.Height + 4);

                if (Color.FromArgb(R, G, B).GetBrightness() < 0.5)
                {
                    fore_brush = new SolidBrush(Color.FromArgb(R + 76, G + 71, B + 66));
                }
                else
                {
                    fore_brush = new SolidBrush(Color.FromArgb(R - 96, G - 91, B - 86));
                }
                
            }

            string tab_name = tabControl.TabPages[e.Index].Text;
            StringFormat sf = new StringFormat();
            sf.Alignment = StringAlignment.Center;
            sf.LineAlignment = StringAlignment.Center;

            e.Graphics.DrawString(tab_name, font, fore_brush, e.Bounds, sf);

            Brush background_brush = new SolidBrush(colors.BaseColor);
            Rectangle LastTabRect = tabControl.GetTabRect(tabControl.TabPages.Count - 1);
            Rectangle rect = new Rectangle();
            rect.Location = new Point(LastTabRect.Right  , 1);
            rect.Size = new Size(tabControl.Right - rect.Left + 5, 34);
            e.Graphics.FillRectangle(background_brush, rect);
        }

        BasePanel _selectedPanel;
        BasePanel SelectedPanel
        {
            get
            {
                if (_selectedPanel == null)
                    _selectedPanel = tabControl.TabPages[0].Tag as BasePanel;
                return _selectedPanel;
            }
            set
            {
                _selectedPanel = value;
                value.RefreshPanel();
            }
        }

        private void tabControl_Selected(object sender, TabControlEventArgs e)
        {
            SelectedPanel.DisposeControls();
            SelectedPanel = e.TabPage.Tag as BasePanel;            
        }

        delegate void refreshCurrentTabDelegate();
        void refreshCurrentTab()
        {
            SelectedPanel.RefreshPanel();
        }
        private void RefreshCurrentTab()
        {
            if (SelectedPanel != null)
            {
                if (this.InvokeRequired)
                {
                    refreshCurrentTabDelegate d = new refreshCurrentTabDelegate(refreshCurrentTab);
                    this.Invoke(d, new object[] { });
                }
                else
                SelectedPanel.RefreshPanel();
            }
        }

        #endregion

        

    }
}
