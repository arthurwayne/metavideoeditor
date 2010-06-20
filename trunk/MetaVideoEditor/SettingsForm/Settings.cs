using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using mveEngine;

namespace MetaVideoEditor
{
    public partial class Settings : Form
    {
        public static Config Config
        {
            get { return Config.Instance; }
        }

        static string[,] Backgrounds = {
                                                 { "wmc", "Windows Media Center"},
                                                 { "grey", Kernel.Instance.GetString("ThemeMW") }
                                       };       

        public Settings()
        {
            InitializeComponent();
            Localize();
            SetStyle();

            DeleteButton.Enabled = false;
            foreach (string f in Config.RootFolders)
                Folders.Add(f);
            UpdateFolderList();

            UpdateProvidersTree(PluginType.Provider, ProvidersTree);
            UpdateProvidersTree(PluginType.Local, LocalTree);
            UpdateProvidersTree(PluginType.Saver, SaverTree);

            UpdateInstalledPlugin();
            UpdateAvailablePlugin();

            LogBox.Checked = Config.EnableTraceLogging;
            AddDataCheckBox.Checked = Config.AddMissingData;
            List<int> MinSizes = new List<int> { 0, 200, 400, 600, 800, 1000, 1200, 1600, 2000 };
            foreach (int n in MinSizes)
            {
                MinPixelBox.Items.Add(n);
                if (Config.MinBackdropWidth == n)
                    MinPixelBox.SelectedItem = n;
            }
            List<int> NbBdChoices = new List<int> { 0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 1000 };
            foreach (int n in NbBdChoices)
            {
                if (n<12) NbBdBox.Items.Add(n);
                else NbBdBox.Items.Add(Kernel.Instance.GetString("AllSet"));
                if (Config.MaxBdSaved == n)
                    NbBdBox.SelectedItem = n;
            }
            /*
            boProxyUse.Checked = Config.useProxy;
            strProxy.Text = Config.ProxyAdress;
            strProxyPort.Text = Config.ProxyPort;
            strProxyUser.Text = Config.ProxyUser;
            strProxyPass.Text = Config.ProxyPass;
            if (Config.useProxy)
            {
                boProxyUse.Checked = true;
                strProxy.Enabled = boProxyUse.Checked;
                strProxy.ReadOnly = !boProxyUse.Checked;
                strProxyPort.Enabled = boProxyUse.Checked;
                strProxyPort.ReadOnly = !boProxyUse.Checked;

            }*/
            strFilmExtension.Text = Config.FilmExtension;
            for (int i = 0; i <= Backgrounds.GetUpperBound(0); i++)
            {
                BackgroundBox.Items.Add(Backgrounds[i, 1]);
                if (Backgrounds[i, 0] == Config.Background)
                    BackgroundBox.SelectedIndex = i;
            }
            fullscreenBox.Checked = Config.FullscreenMode;
            UseColorBox.Checked = Config.UseTreeColor;
            RenameEpisodeBox.Checked = EpisodePatternBox.Enabled = Config.RenameEpisodes;
            RenameMovieBox.Checked = MoviePatternBox.Enabled = Config.RenameMovies;
            RenameSeasonBox.Checked = SeasonPatternBox.Enabled = Config.RenameSeasons;
            RenameSeriesBox.Checked = SeriesPatternBox.Enabled = Config.RenameSeries;
            MoviePatternBox.Text = Config.MoviePattern;
            SeriesPatternBox.Text = Config.SeriesPattern;
            SeasonPatternBox.Text = Config.SeasonPattern;
            EpisodePatternBox.Text = Config.EpisodePattern;
        }

        private void Localize()
        {
            this.Text = Kernel.Instance.GetString("SettingsMW");
            OKButton.Text = Kernel.Instance.GetString("OKStr");
            CancelButton.Text = Kernel.Instance.GetString("CancelStr");
            tab1.Text = Kernel.Instance.GetString("FoldersSet");
            foldersTitle.Text = Kernel.Instance.GetString("AnalyzedFoldersSet");
            AddButton.Text = Kernel.Instance.GetString("AddSet");
            DeleteButton.Text = Kernel.Instance.GetString("RemoveSet");
            ImportButton.Text = Kernel.Instance.GetString("MBFoldersSet");
            tab2.Text = Kernel.Instance.GetString("GeneralSet");
            generalTitle.Text = Kernel.Instance.GetString("GeneralSettingsSet");
            groupBox10.Text = Kernel.Instance.GetString("DebugSet");
            LogBox.Text = Kernel.Instance.GetString("ActDebugSet");
            groupBox11.Text = Kernel.Instance.GetString("DataSet");
            AddDataCheckBox.Text = Kernel.Instance.GetString("AddMissingDataSet");
            groupBox12.Text = Kernel.Instance.GetString("ExtensionsSet");
            label14.Text = Kernel.Instance.GetString("VidExtSet");
            groupBox9.Text = Kernel.Instance.GetString("TitleBdTab");
            label9.Text = Kernel.Instance.GetString("BdWidthSet");
            label10.Text = Kernel.Instance.GetString("BDToSaveSet");
            tab3.Text = Kernel.Instance.GetString("DisplaySet");
            displayTitle.Text = Kernel.Instance.GetString("DisplaySettingsSet");
            groupBox13.Text = Kernel.Instance.GetString("DisplaySet");
            label15.Text = Kernel.Instance.GetString("BackgroundImgSet");
            fullscreenBox.Text = Kernel.Instance.GetString("SaveGeomSet");
            groupBox14.Text = Kernel.Instance.GetString("TreeColorsSet");
            UseColorBox.Text = Kernel.Instance.GetString("UserColorsSet");
            label11.Text = Kernel.Instance.GetString("RedColorSet");
            label12.Text = Kernel.Instance.GetString("BlueColorSet");
            label13.Text = Kernel.Instance.GetString("BlackColorSet");
            label16.Text = Kernel.Instance.GetString("BoldColorSet");
            tab8.Text = Kernel.Instance.GetString("RenamingSet");
            RenameTitle.Text = Kernel.Instance.GetString("RenameItemsSet");
            groupBox4.Text = Kernel.Instance.GetString("MoviesStr");
            groupBox5.Text = Kernel.Instance.GetString("SeriesStr");
            groupBox6.Text = Kernel.Instance.GetString("SeasonStr");
            groupBox7.Text = Kernel.Instance.GetString("EpisodeStr");
            RenameMovieBox.Text = Kernel.Instance.GetString("RenameMoviesSet");
            RenameSeriesBox.Text = Kernel.Instance.GetString("RenameSeriesSet");
            RenameSeasonBox.Text = Kernel.Instance.GetString("RenameSeasonSet");
            RenameEpisodeBox.Text = Kernel.Instance.GetString("RenameEpisodeSet");
            label1.Text = label4.Text = label5.Text = label8.Text = Kernel.Instance.GetString("PatternSet");
            groupBox8.Text = Kernel.Instance.GetString("ValuesSet");
            label3.Text = string.Format("{0}\r\n\r\n{1}\r\n\r\n{2}\r\n\r\n{3}\r\n\r\n{4}\r\n\r\n{5}\r\n\r\n{6}", 
                Kernel.Instance.GetString("TitleStr"),
                Kernel.Instance.GetString("OriginalTitleStr"),
                Kernel.Instance.GetString("SortTitleStr"),
                Kernel.Instance.GetString("YearStr"),
                Kernel.Instance.GetString("SeriesNameSet"),
                Kernel.Instance.GetString("SeasonNumSet"),
                Kernel.Instance.GetString("EpisodeNum"));
            tab7.Text = Kernel.Instance.GetString("LocalSet");
            groupBox2.Text = Kernel.Instance.GetString("LocalSet");
            LocalTitle.Text = Kernel.Instance.GetString("LocalPluginsSet");
            tab4.Text = Kernel.Instance.GetString("ProvidersSet");
            groupBox1.Text = Kernel.Instance.GetString("ProvidersSet");
            ProvidersTitle.Text = Kernel.Instance.GetString("ProvidersPluginsSet");
            tab5.Text = Kernel.Instance.GetString("SaversSet");
            groupBox3.Text = Kernel.Instance.GetString("SaversSet");
            SaversTitle.Text = Kernel.Instance.GetString("SaversPluginsSet");
            tab9.Text = Kernel.Instance.GetString("PluginsSet");
            groupBox15.Text = Kernel.Instance.GetString("PluginsSet");
            PluginsTitle.Text = Kernel.Instance.GetString("AddRemovePluginsSet");
            label17.Text = Kernel.Instance.GetString("AvailablePluginsSet");
            label18.Text = Kernel.Instance.GetString("InstalledPluginsSet");
            addPluginButton.Text = Kernel.Instance.GetString("AddSet") + " >>";
            deletePluginButton.Text = Kernel.Instance.GetString("RemoveSet");
            pluginVersionLabel.Text = Kernel.Instance.GetString("VersionStr");
            pluginTypeLabel.Text = Kernel.Instance.GetString("TypeSet");
            pluginDescLabel.Text = Kernel.Instance.GetString("DescSet");

        }

        private void UpdateFolderList()
        {
            FolderList.Clear();
            foreach (string b in Folders)
                FolderList.Items.Add(b);
        }

        private List<string> Folders = new List<string>();

        private void CancelButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void OKButton_Click(object sender, EventArgs e)
        {
            List<string> rf = new List<string>();
            foreach (string s in Folders)
                rf.Add(s);
            if (Folders.Count == 0)
            {
                MessageBox.Show(Kernel.Instance.GetString("SelectFolderMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                return;
            }
            if (Config.RootFolders.Count > 0 && !CompareFolders(Config.RootFolders, Folders))
            {
                DialogResult res = MessageBox.Show(Kernel.Instance.GetString("FolderModMess"), "MetaVideoEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                if (res == DialogResult.No)
                    return;
                else
                    this.DialogResult = DialogResult.OK;
            }
            Config.EnableTraceLogging = LogBox.Checked;
            Config.RootFolders = Folders;
            Config.AddMissingData = AddDataCheckBox.Checked;
            Config.MinBackdropWidth = (int)MinPixelBox.SelectedItem;            

            int maxbd = 0;
            if (!Int32.TryParse(NbBdBox.Text, out maxbd))
                maxbd = 1000;
            Config.MaxBdSaved = maxbd;
            /*
            Config.useProxy = boProxyUse.Checked;
            Config.ProxyAdress = strProxy.Text;
            Config.ProxyPort = strProxyPort.Text;
            Config.ProxyUser = strProxyUser.Text;
            Config.ProxyPass = strProxyPass.Text;*/
            Config.FilmExtension = strFilmExtension.Text;
            try { Config.Background = Backgrounds[BackgroundBox.SelectedIndex, 0]; }
            catch { Config.Background = Backgrounds[0, 0]; }
            Config.FullscreenMode = fullscreenBox.Checked;
            Config.UseTreeColor = UseColorBox.Checked;
            Config.RenameMovies = RenameMovieBox.Checked;
            Config.RenameSeries = RenameSeriesBox.Checked;
            Config.RenameSeasons = RenameSeasonBox.Checked;
            Config.RenameEpisodes = RenameEpisodeBox.Checked;
            Config.MoviePattern = MoviePatternBox.Text;
            Config.SeriesPattern = SeriesPatternBox.Text;
            Config.SeasonPattern = SeasonPatternBox.Text;
            Config.EpisodePattern = EpisodePatternBox.Text;
            foreach (var plugin in Kernel.Instance.Plugins.ToList())
                plugin.Save();
            foreach (IPlugin plugin in PluginsToRemove)
            {
                Kernel.Instance.DeletePlugin(plugin);
            }
            if (PluginsToAdd.Count > 0)
            {
                var form = new InstallPlugins();                
                form.Owner = this.Owner;
                form.BeginInstall(PluginsToAdd);
                form.ShowDialog();
            }
            this.Close();
        }

        private void AddButton_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            DialogResult res = fb.ShowDialog();
            if (res == DialogResult.OK)
            {
                Folders.Add(fb.SelectedPath);
                UpdateFolderList();
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (FolderList.SelectedItems.Count > 0)
            {
                foreach (ListViewItem lv in FolderList.SelectedItems)
                    Folders.Remove(lv.Text);
                UpdateFolderList();
            }
        }

        private void ImportButton_Click(object sender, EventArgs e)
        {
            List<string> MBfolders = FileUtil.GetMBfolders();
            foreach (string f in MBfolders)
            {
                if (!Folders.Contains(f))
                    Folders.Add(f);
            }
            UpdateFolderList();
        }

        private void FolderList_SelectedIndexChanged(object sender, EventArgs e)
        {
            DeleteButton.Enabled = true;
        }

        private bool CompareFolders(List<string> f1, List<string> f2)
        {
            if (f1.Count != f2.Count)
                return false;
            foreach (string s in f1)
            {
                if (!f2.Contains(s))
                    return false;
            }
            return true;
        }

        private void SetStyle()
        {
            ColorsTheme colors = ColorStyle.GetColors();
            this.BackColor = colors.BackColor;
            int R = colors.BaseColor.R;
            int G = colors.BaseColor.G;
            int B = colors.BaseColor.B;
            foreach (Control cont in this.tabPageSwitcher1.Controls)
            {
                foreach (Control control in cont.Controls)
                {
                    if (control.GetType() == typeof(Label) && control.Name.EndsWith("Title"))
                    {

                        Label label = (Label)control;
                        label.BackColor = colors.BaseColor;
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
                }
            }
        }

        private void UpdateProvidersTree(PluginType type, TreeView tree)
        {
            UpdateProvidersTree(type, tree, 0);
        }
        private void UpdateProvidersTree(PluginType type, TreeView tree, int SelectedIndex)
        {
            tree.Nodes.Clear();
            Kernel.Instance.Plugins.Sort(delegate(IPlugin p1, IPlugin p2) { return p1.Options.Order.CompareTo(p2.Options.Order); });
            int index = 0;
            
            foreach (Plugin plugin in Kernel.Instance.Plugins)
            {
                if (plugin.Type == type)
                {
                    TreeNode node = new TreeNode();
                    node.Tag = plugin;
                    node.Checked = plugin.Options.Enable;                    
                    tree.Nodes.Add(node);
                    if (index == SelectedIndex) { tree.SelectedNode = node;  }
                    index++;
                }
            }
        }

        private void providersTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            Plugin plugin = (Plugin)e.Node.Tag;
            Brush brush = Brushes.Black;
            if (e.Node.IsSelected)
            {
                if (SelectedTree.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }
            if (e.Node.Checked)
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox_checked, e.Bounds.X + 2, e.Bounds.Y + 5);
            }
            else
            {
                brush = Brushes.Gray;
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.checkbox, e.Bounds.X + 2, e.Bounds.Y + 5);
            }
            e.Graphics.DrawString(plugin.Name + " - v" + plugin.Version.ToString(), ProvidersTree.Font, brush, e.Bounds.X+20, e.Bounds.Y + 4);
            e.Graphics.DrawString(plugin.Description, ProvidersTree.Font, brush, e.Bounds.X+25, e.Bounds.Y + 20);
            if (e.Node.Index > 0)
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.up, e.Bounds.X + 395, e.Bounds.Y + 9);
            int maxIndex = 0;
            if (SelectedTree != null) maxIndex = SelectedTree.Nodes.Count - 1;
            if (e.Node.Index < maxIndex)
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.down, e.Bounds.X + 430, e.Bounds.Y + 9);
            if (plugin.IsConfigurable)
            {
                e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.Config, e.Bounds.X + 470, e.Bounds.Y + 5);
            }

        }

        TreeView SelectedTree;

        private void providersTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            
            int y = e.Y %  ProvidersTree.ItemHeight;
            //MessageBox.Show(e.X.ToString() + " - " + y.ToString());
            Plugin plugin = (Plugin)e.Node.Tag;
            if (plugin.IsConfigurable && e.X > 476 && e.X < 495 && y > 5 && y < 30)
            {                
                plugin.Configure();
            }
            if (e.X > 3 && e.X < 14 && y > 9 && y < 21)
            {
                e.Node.Checked = !e.Node.Checked;
                plugin.Options.Enable = e.Node.Checked;
                plugin.Save(); 
            }
            if (e.Node.Index > 0 && e.X > 399 && e.X < 411 && y > 10 && y < 25)
            {
                int tmp = ((Plugin)SelectedTree.Nodes[e.Node.Index].Tag).Options.Order;
                ((Plugin)SelectedTree.Nodes[e.Node.Index].Tag).Options.Order = ((Plugin)SelectedTree.Nodes[e.Node.Index - 1].Tag).Options.Order;
                ((Plugin)SelectedTree.Nodes[e.Node.Index -1].Tag).Options.Order = tmp;                
                UpdateProvidersTree(plugin.Type, SelectedTree, e.Node.Index-1);
            }
            else if (e.Node.Index < SelectedTree.Nodes.Count - 1 && e.X > 431 && e.X < 448 && y > 10 && y < 25)
            {
                int tmp = ((Plugin)SelectedTree.Nodes[e.Node.Index].Tag).Options.Order;
                ((Plugin)SelectedTree.Nodes[e.Node.Index].Tag).Options.Order  = ((Plugin)SelectedTree.Nodes[e.Node.Index + 1].Tag).Options.Order;
                ((Plugin)SelectedTree.Nodes[e.Node.Index + 1].Tag).Options.Order = tmp;
                UpdateProvidersTree(plugin.Type, SelectedTree, e.Node.Index+1);                
            }
        }

        private void providersTree_MouseMove(object sender, MouseEventArgs e)
        {
            bool CanClick = false;
            try
            {
                int index = (int)Math.Round((double)(e.Y / SelectedTree.ItemHeight));
                Plugin plugin = (Plugin)SelectedTree.Nodes[index].Tag;
                CanClick = plugin.IsConfigurable;
            }
            catch { }
            if (CanClick)
            {
                int y = e.Y % ProvidersTree.ItemHeight;
                if (e.X > 476 && e.X < 495 && y > 5 && y < 30)
                {
                    Cursor = Cursors.Hand;
                }
                else
                    Cursor = Cursors.Default;
            }
        }

        private void fieldsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            //if (SelectedTree != null) SelectedTree.SelectedNode = e.Node;
            Plugin plugin = (Plugin)e.Node.Tag;
            TreeView tree = LocalFieldsTree;
            if (plugin.Type == PluginType.Provider) tree = ProviderFieldsTree;
            else if (plugin.Type == PluginType.Saver) tree = SaverFieldsTree;
            if (tree.Nodes.Count == 0)
            {
                tree.Nodes.Add(Kernel.Instance.GetString("TitleStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("OriginalTitleStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("SortTitleStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("PosterStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("BackdropStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("BannersStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("TrailersStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("YearStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("RuntimeStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("RatingStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("MpaaStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("OverviewStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("RatioStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("CastingStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("GenresStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("StudiosStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("CountriesStr"));
                tree.Nodes.Add(Kernel.Instance.GetString("TaglinesStr"));
            }
            tree.Nodes[0].Checked = plugin.Options.UseTitle;
            tree.Nodes[1].Checked = plugin.Options.UseOriginalTitle;
            tree.Nodes[2].Checked = plugin.Options.UseSortTitle;
            tree.Nodes[3].Checked = plugin.Options.UsePoster;
            tree.Nodes[4].Checked = plugin.Options.UseBackdrop;
            tree.Nodes[5].Checked = plugin.Options.UseBanner;
            tree.Nodes[6].Checked = plugin.Options.UseTrailers;
            tree.Nodes[7].Checked = plugin.Options.UseProductionYear;
            tree.Nodes[8].Checked = plugin.Options.UseRuntime;
            tree.Nodes[9].Checked = plugin.Options.UseRating;
            tree.Nodes[10].Checked = plugin.Options.UseMPAARating;
            tree.Nodes[11].Checked = plugin.Options.UseOverview;
            tree.Nodes[12].Checked = plugin.Options.UseAspectRatio;
            tree.Nodes[13].Checked = plugin.Options.UseCasting;
            tree.Nodes[14].Checked = plugin.Options.UseGenres;
            tree.Nodes[15].Checked = plugin.Options.UseStudios;
            tree.Nodes[16].Checked = plugin.Options.UseCountries;
            tree.Nodes[17].Checked = plugin.Options.UseTagLines;

        }

        private void fieldsTree_NodeMouseClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            if (e.X > 15)
            e.Node.Checked = !e.Node.Checked;
            Plugin plugin;
            try
            {
                plugin = (Plugin)SelectedTree.SelectedNode.Tag;
            }
            catch { return; }
            switch (e.Node.Index)
            {
                case 0: plugin.Options.UseTitle = e.Node.Checked; break;
                case 1: plugin.Options.UseOriginalTitle = e.Node.Checked; break;
                case 2: plugin.Options.UseSortTitle = e.Node.Checked; break;
                case 3: plugin.Options.UsePoster = e.Node.Checked; break;
                case 4: plugin.Options.UseBackdrop = e.Node.Checked; break;
                case 5: plugin.Options.UseBanner = e.Node.Checked; break;
                case 6: plugin.Options.UseTrailers = e.Node.Checked; break;
                case 7: plugin.Options.UseProductionYear = e.Node.Checked; break;
                case 8: plugin.Options.UseRuntime = e.Node.Checked; break;
                case 9: plugin.Options.UseRating = e.Node.Checked; break;
                case 10: plugin.Options.UseMPAARating = e.Node.Checked; break;
                case 11: plugin.Options.UseOverview = e.Node.Checked; break;
                case 12: plugin.Options.UseAspectRatio = e.Node.Checked; break;
                case 13: plugin.Options.UseCasting = e.Node.Checked; break;
                case 14: plugin.Options.UseGenres = e.Node.Checked; break;
                case 15: plugin.Options.UseStudios = e.Node.Checked; break;
                case 16: plugin.Options.UseCountries = e.Node.Checked; break;
                case 17: plugin.Options.UseTagLines = e.Node.Checked; break;
            }
        }

        private void tab4_Click(object sender, EventArgs e)
        {
            SelectedTree = ProvidersTree;
            ProvidersTree.Select();
        }

        private void tab7_Click(object sender, EventArgs e)
        {
            SelectedTree = LocalTree;
            LocalTree.Select();
        }

        private void tab5_Click(object sender, EventArgs e)
        {
            SelectedTree = SaverTree;
            SaverTree.Select();
        }

        private void RenameMovieBox_CheckedChanged(object sender, EventArgs e)
        {
            MoviePatternBox.Enabled = RenameMovieBox.Checked;
        }

        private void RenameSeriesBox_CheckedChanged(object sender, EventArgs e)
        {
            SeriesPatternBox.Enabled = RenameSeriesBox.Checked;
        }

        private void RenameSeasonBox_CheckedChanged(object sender, EventArgs e)
        {
            SeasonPatternBox.Enabled = RenameSeasonBox.Checked;
        }

        private void RenameEpisodeBox_CheckedChanged(object sender, EventArgs e)
        {
            EpisodePatternBox.Enabled = RenameEpisodeBox.Checked;
        }

        
        List<IPlugin> PluginsToAdd = new List<IPlugin>();
        List<IPlugin> PluginsToRemove = new List<IPlugin>();
        List<IPlugin> InstalledPlugin = new List<IPlugin>(Kernel.Instance.Plugins);
        TreeNode SelectedInstalledNode;
        TreeNode SelectedAvailableNode;

        private void UpdateInstalledPlugin()
        {
            installedPluginsTree.Nodes.Clear();
            foreach (IPlugin plugin in InstalledPlugin)
            {
                TreeNode node = new TreeNode();
                node.Tag = plugin;
                node.Text = plugin.Name;
                installedPluginsTree.Nodes.Add(node);
            }
        }

        private void UpdateAvailablePlugin()
        {
            availablePluginsTree.Nodes.Clear();
            foreach (IPlugin plugin in PluginSourceCollection.Instance.AvailablePlugins)
            {
                TreeNode node = new TreeNode();
                node.Tag = plugin;
                node.Text = plugin.Name;
                availablePluginsTree.Nodes.Add(node);
            }
            availablePluginsTree.Select();
        }

        private void addPluginButton_Click(object sender, EventArgs e)
        {
            if (InstalledPlugin.Exists(p => p.Name == ((IPlugin)SelectedAvailableNode.Tag).Name))
            {
                MessageBox.Show(Kernel.Instance.GetString("PlugInstMess"), "MetaVideoEditor", MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }
            PluginsToAdd.Add((IPlugin)SelectedAvailableNode.Tag);
            InstalledPlugin.Add((IPlugin)SelectedAvailableNode.Tag);
            UpdateInstalledPlugin();
        }

        private void availablePluginsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedAvailableNode = e.Node;
            deletePluginButton.Enabled = false;
            IPlugin plugin = (IPlugin)SelectedAvailableNode.Tag;
            pluginDescLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("DescSet"), plugin.Description);
            pluginTypeLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("TypeSet"), plugin.Type.ToString());
            pluginVersionLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("VersionStr"), plugin.Version.ToString());
        }

        private void installedPluginsTree_AfterSelect(object sender, TreeViewEventArgs e)
        {
            SelectedInstalledNode = e.Node;
            deletePluginButton.Enabled = true;
            IPlugin plugin = (IPlugin)SelectedInstalledNode.Tag;
            pluginDescLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("DescSet"), plugin.Description);
            pluginTypeLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("TypeSet"), plugin.Type.ToString());
            pluginVersionLabel.Text = string.Format("{0} : {1}", Kernel.Instance.GetString("VersionStr"), plugin.Version.ToString());
        }

        private void deletePluginButton_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show(Kernel.Instance.GetString("RemovePlugMess"), "MetaVideoEditor", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                IPlugin plugin = (IPlugin)SelectedInstalledNode.Tag;
                if (PluginsToAdd.Contains(plugin))
                {
                    PluginsToAdd.Remove(plugin);
                    UpdateInstalledPlugin();
                }
                else
                {
                    InstalledPlugin.Remove(plugin);
                    PluginsToRemove.Add(plugin);
                    UpdateInstalledPlugin();
                }
            }
        }

        private void availablePluginsTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                if (availablePluginsTree.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }
            e.Graphics.DrawString(e.Node.Text, availablePluginsTree.Font, Brushes.Black, e.Bounds.X + 5, e.Bounds.Y + 4);

        }

        private void installedPluginsTree_DrawNode(object sender, DrawTreeNodeEventArgs e)
        {
            if (e.Node.IsSelected)
            {
                if (installedPluginsTree.Focused)
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_click2, e.Bounds);
                else
                    e.Graphics.DrawImage(global::MetaVideoEditor.Properties.Resources.B_on2, e.Bounds);
            }
            e.Graphics.DrawString(e.Node.Text, installedPluginsTree.Font, Brushes.Black, e.Bounds.X + 5, e.Bounds.Y + 4);
        }

        private void tab9_Click(object sender, EventArgs e)
        {
            availablePluginsTree.Select();
        }




    }

}
