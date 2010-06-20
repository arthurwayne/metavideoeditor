using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;

namespace MetaVideoEditor
{
    partial class MetaVideoEditor
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MetaVideoEditor));
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.Medialabel = new System.Windows.Forms.Label();
            this.itemsView = new System.Windows.Forms.TreeView();
            this.SearchPanel = new System.Windows.Forms.Panel();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.SearchBox = new System.Windows.Forms.TextBox();
            this.panel3 = new System.Windows.Forms.Panel();
            this.tabControl = new CustomControls.DraggableTabControl();
            this.tabPageOverview = new System.Windows.Forms.TabPage();
            this.tabPageGeneral = new System.Windows.Forms.TabPage();
            this.tabPageActors = new System.Windows.Forms.TabPage();
            this.tabPageCrew = new System.Windows.Forms.TabPage();
            this.tabPageTrailers = new System.Windows.Forms.TabPage();
            this.tabPagePoster = new System.Windows.Forms.TabPage();
            this.tabPageBackdrop = new System.Windows.Forms.TabPage();
            this.tabPageBanners = new System.Windows.Forms.TabPage();
            this.tabPageGenres = new System.Windows.Forms.TabPage();
            this.tabPageStudios = new System.Windows.Forms.TabPage();
            this.tabPageCountries = new System.Windows.Forms.TabPage();
            this.tabPageTagLines = new System.Windows.Forms.TabPage();
            this.toolTip1 = new System.Windows.Forms.ToolTip(this.components);
            this.panel1 = new System.Windows.Forms.Panel();
            this.tabPageSwitcher1 = new RibbonStyle.TabPageSwitcher();
            this.tabStripPage1 = new RibbonStyle.TabStripPage();
            this.tabPanel6 = new RibbonStyle.TabPanel();
            this.ribbonButton10 = new RibbonStyle.RibbonButton();
            this.SaveButton = new RibbonStyle.RibbonItem();
            this.saveToolStripItem1 = new System.Windows.Forms.ToolStripMenuItem();
            this.SaveCurrentButton = new CustomControls.ContextMenuItem();
            this.SaveCheckedButton = new CustomControls.ContextMenuItem();
            this.SaveModifiedButton = new CustomControls.ContextMenuItem();
            this.tabPanel7 = new RibbonStyle.TabPanel();
            this.ribbonButton7 = new RibbonStyle.RibbonButton();
            this.ribbonButton6 = new RibbonStyle.RibbonButton();
            this.ribbonButton5 = new RibbonStyle.RibbonButton();
            this.tabPanel5 = new RibbonStyle.TabPanel();
            this.ribbonButton2 = new RibbonStyle.RibbonButton();
            this.ribbonItem2 = new RibbonStyle.RibbonItem();
            this.themeToolStripItem = new System.Windows.Forms.ToolStripMenuItem();
            this.ToolStripItem1 = new CustomControls.ContextMenuItem();
            this.ToolStripItem2 = new CustomControls.ContextMenuItem();
            this.ToolStripItem3 = new CustomControls.ContextMenuItem();
            this.ToolStripItem4 = new CustomControls.ContextMenuItem();
            this.ToolStripItem5 = new CustomControls.ContextMenuItem();
            this.ToolStripItem6 = new CustomControls.ContextMenuItem();
            this.ToolStripItem7 = new CustomControls.ContextMenuItem();
            this.ToolStripItem8 = new CustomControls.ContextMenuItem();
            this.ToolStripItem9 = new CustomControls.ContextMenuItem();
            this.ToolStripItem10 = new CustomControls.ContextMenuItem();
            this.ToolStripItem11 = new CustomControls.ContextMenuItem();
            this.ToolStripItem12 = new CustomControls.ContextMenuItem();
            this.ribbonButton1 = new RibbonStyle.RibbonButton();
            this.tabStripPage3 = new RibbonStyle.TabStripPage();
            this.tabPanel11 = new RibbonStyle.TabPanel();
            this.tabPanel12 = new RibbonStyle.TabPanel();
            this.tabPanel3 = new RibbonStyle.TabPanel();
            this.tabStripPage2 = new RibbonStyle.TabStripPage();
            this.tabPanel10 = new RibbonStyle.TabPanel();
            this.ribbonButton21 = new RibbonStyle.RibbonButton();
            this.ribbonButton20 = new RibbonStyle.RibbonButton();
            this.ribbonButton14 = new RibbonStyle.RibbonButton();
            this.tabPanel8 = new RibbonStyle.TabPanel();
            this.ribbonButton19 = new RibbonStyle.RibbonButton();
            this.ribbonButton18 = new RibbonStyle.RibbonButton();
            this.ribbonButton17 = new RibbonStyle.RibbonButton();
            this.tabPanel9 = new RibbonStyle.TabPanel();
            this.ribbonButton16 = new RibbonStyle.RibbonButton();
            this.ribbonButton15 = new RibbonStyle.RibbonButton();
            this.ribbonButton13 = new RibbonStyle.RibbonButton();
            this.ribbonButton12 = new RibbonStyle.RibbonButton();
            this.tabStripPage4 = new RibbonStyle.TabStripPage();
            this.tabPanel4 = new RibbonStyle.TabPanel();
            this.tabPanel1 = new RibbonStyle.TabPanel();
            this.tabPanel2 = new RibbonStyle.TabPanel();
            this.tabStrip1 = new RibbonStyle.TabStrip();
            this.tab1 = new RibbonStyle.Tab();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.messageDropDownButton = new CustomControls.ToolStripQueue();
            this.messageTextBox = new System.Windows.Forms.ToolStripStatusLabel();
            this.toolStripDropDownButton1 = new System.Windows.Forms.ToolStripDropDownButton();
            this.testToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.item1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.item2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            this.SearchPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.panel3.SuspendLayout();
            this.tabControl.SuspendLayout();
            this.panel1.SuspendLayout();
            this.tabPageSwitcher1.SuspendLayout();
            this.tabStripPage1.SuspendLayout();
            this.tabPanel6.SuspendLayout();
            this.SaveButton.SuspendLayout();
            this.tabPanel7.SuspendLayout();
            this.tabPanel5.SuspendLayout();
            this.ribbonItem2.SuspendLayout();
            this.tabStripPage3.SuspendLayout();
            this.tabStripPage2.SuspendLayout();
            this.tabPanel10.SuspendLayout();
            this.tabPanel8.SuspendLayout();
            this.tabPanel9.SuspendLayout();
            this.tabStripPage4.SuspendLayout();
            this.tabStrip1.SuspendLayout();
            this.statusStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer1
            // 
            this.splitContainer1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.splitContainer1.FixedPanel = System.Windows.Forms.FixedPanel.Panel1;
            this.splitContainer1.Location = new System.Drawing.Point(4, 117);
            this.splitContainer1.Name = "splitContainer1";
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.Medialabel);
            this.splitContainer1.Panel1.Controls.Add(this.itemsView);
            this.splitContainer1.Panel1.Controls.Add(this.SearchPanel);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.panel3);
            this.splitContainer1.Panel2MinSize = 75;
            this.splitContainer1.Size = new System.Drawing.Size(1256, 825);
            this.splitContainer1.SplitterDistance = 254;
            this.splitContainer1.SplitterWidth = 10;
            this.splitContainer1.TabIndex = 42;
            // 
            // Medialabel
            // 
            this.Medialabel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.Medialabel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.Medialabel.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.Medialabel.Location = new System.Drawing.Point(3, 1);
            this.Medialabel.Name = "Medialabel";
            this.Medialabel.Size = new System.Drawing.Size(248, 23);
            this.Medialabel.TabIndex = 37;
            this.Medialabel.Text = "Media Collection";
            this.Medialabel.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // itemsView
            // 
            this.itemsView.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.itemsView.CheckBoxes = true;
            this.itemsView.DrawMode = System.Windows.Forms.TreeViewDrawMode.OwnerDrawAll;
            this.itemsView.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F);
            this.itemsView.FullRowSelect = true;
            this.itemsView.HideSelection = false;
            this.itemsView.ItemHeight = 30;
            this.itemsView.Location = new System.Drawing.Point(3, 52);
            this.itemsView.Name = "itemsView";
            this.itemsView.ShowLines = false;
            this.itemsView.Size = new System.Drawing.Size(248, 773);
            this.itemsView.TabIndex = 33;
            this.itemsView.AfterCheck += new System.Windows.Forms.TreeViewEventHandler(this.itemsView_AfterCheck);
            this.itemsView.DrawNode += new System.Windows.Forms.DrawTreeNodeEventHandler(this.itemsView_DrawNode);
            this.itemsView.MouseUp += new System.Windows.Forms.MouseEventHandler(this.itemsView_MouseUp);
            this.itemsView.AfterSelect += new System.Windows.Forms.TreeViewEventHandler(this.itemsView_AfterSelect);
            this.itemsView.MouseMove += new System.Windows.Forms.MouseEventHandler(this.itemsView_MouseMove);
            // 
            // SearchPanel
            // 
            this.SearchPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.SearchPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.SearchPanel.Controls.Add(this.pictureBox1);
            this.SearchPanel.Controls.Add(this.SearchBox);
            this.SearchPanel.Location = new System.Drawing.Point(3, 23);
            this.SearchPanel.Name = "SearchPanel";
            this.SearchPanel.Size = new System.Drawing.Size(248, 30);
            this.SearchPanel.TabIndex = 39;
            // 
            // pictureBox1
            // 
            this.pictureBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.pictureBox1.BackColor = System.Drawing.Color.White;
            this.pictureBox1.BackgroundImage = global::MetaVideoEditor.Properties.Resources.search2;
            this.pictureBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.pictureBox1.Location = new System.Drawing.Point(223, 7);
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.Size = new System.Drawing.Size(20, 18);
            this.pictureBox1.TabIndex = 39;
            this.pictureBox1.TabStop = false;
            // 
            // SearchBox
            // 
            this.SearchBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.SearchBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic);
            this.SearchBox.ForeColor = System.Drawing.Color.Silver;
            this.SearchBox.Location = new System.Drawing.Point(5, 3);
            this.SearchBox.Name = "SearchBox";
            this.SearchBox.Size = new System.Drawing.Size(238, 24);
            this.SearchBox.TabIndex = 38;
            this.SearchBox.Text = "Search...";
            this.SearchBox.TextChanged += new System.EventHandler(this.SearchBox_TextChanged);
            this.SearchBox.Leave += new System.EventHandler(this.SearchBox_Leave);
            this.SearchBox.Enter += new System.EventHandler(this.SearchBox_Enter);
            // 
            // panel3
            // 
            this.panel3.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.panel3.Controls.Add(this.tabControl);
            this.panel3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel3.Location = new System.Drawing.Point(0, 0);
            this.panel3.Margin = new System.Windows.Forms.Padding(0);
            this.panel3.Name = "panel3";
            this.panel3.Size = new System.Drawing.Size(992, 825);
            this.panel3.TabIndex = 41;
            // 
            // tabControl
            // 
            this.tabControl.AllowDrop = true;
            this.tabControl.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl.Controls.Add(this.tabPageOverview);
            this.tabControl.Controls.Add(this.tabPageGeneral);
            this.tabControl.Controls.Add(this.tabPageActors);
            this.tabControl.Controls.Add(this.tabPageCrew);
            this.tabControl.Controls.Add(this.tabPageTrailers);
            this.tabControl.Controls.Add(this.tabPagePoster);
            this.tabControl.Controls.Add(this.tabPageBackdrop);
            this.tabControl.Controls.Add(this.tabPageBanners);
            this.tabControl.Controls.Add(this.tabPageGenres);
            this.tabControl.Controls.Add(this.tabPageStudios);
            this.tabControl.Controls.Add(this.tabPageCountries);
            this.tabControl.Controls.Add(this.tabPageTagLines);
            this.tabControl.DrawMode = System.Windows.Forms.TabDrawMode.OwnerDrawFixed;
            this.tabControl.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F);
            this.tabControl.Location = new System.Drawing.Point(-7, -3);
            this.tabControl.Margin = new System.Windows.Forms.Padding(0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(998, 829);
            this.tabControl.TabIndex = 0;
            this.tabControl.Selected += new System.Windows.Forms.TabControlEventHandler(this.tabControl_Selected);
            this.tabControl.DrawItem += new System.Windows.Forms.DrawItemEventHandler(this.tabControl_DrawItem);
            // 
            // tabPageOverview
            // 
            this.tabPageOverview.BackColor = System.Drawing.Color.Transparent;
            this.tabPageOverview.Location = new System.Drawing.Point(4, 27);
            this.tabPageOverview.Name = "tabPageOverview";
            this.tabPageOverview.Size = new System.Drawing.Size(990, 798);
            this.tabPageOverview.TabIndex = 0;
            this.tabPageOverview.Text = "Overview";
            this.tabPageOverview.UseVisualStyleBackColor = true;
            // 
            // tabPageGeneral
            // 
            this.tabPageGeneral.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.tabPageGeneral.Location = new System.Drawing.Point(4, 27);
            this.tabPageGeneral.Name = "tabPageGeneral";
            this.tabPageGeneral.Size = new System.Drawing.Size(989, 798);
            this.tabPageGeneral.TabIndex = 1;
            this.tabPageGeneral.Text = "General";
            this.tabPageGeneral.UseVisualStyleBackColor = true;
            // 
            // tabPageActors
            // 
            this.tabPageActors.Location = new System.Drawing.Point(4, 27);
            this.tabPageActors.Name = "tabPageActors";
            this.tabPageActors.Size = new System.Drawing.Size(989, 798);
            this.tabPageActors.TabIndex = 2;
            this.tabPageActors.Text = "Acteurs";
            this.tabPageActors.UseVisualStyleBackColor = true;
            // 
            // tabPageCrew
            // 
            this.tabPageCrew.Location = new System.Drawing.Point(4, 27);
            this.tabPageCrew.Name = "tabPageCrew";
            this.tabPageCrew.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCrew.Size = new System.Drawing.Size(989, 798);
            this.tabPageCrew.TabIndex = 11;
            this.tabPageCrew.Text = "Equipe";
            this.tabPageCrew.UseVisualStyleBackColor = true;
            // 
            // tabPageTrailers
            // 
            this.tabPageTrailers.Location = new System.Drawing.Point(4, 27);
            this.tabPageTrailers.Name = "tabPageTrailers";
            this.tabPageTrailers.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTrailers.Size = new System.Drawing.Size(989, 798);
            this.tabPageTrailers.TabIndex = 3;
            this.tabPageTrailers.Text = "Bandes annonces";
            this.tabPageTrailers.UseVisualStyleBackColor = true;
            // 
            // tabPagePoster
            // 
            this.tabPagePoster.Location = new System.Drawing.Point(4, 27);
            this.tabPagePoster.Name = "tabPagePoster";
            this.tabPagePoster.Padding = new System.Windows.Forms.Padding(3);
            this.tabPagePoster.Size = new System.Drawing.Size(989, 798);
            this.tabPagePoster.TabIndex = 4;
            this.tabPagePoster.Text = "Jaquettes";
            this.tabPagePoster.UseVisualStyleBackColor = true;
            // 
            // tabPageBackdrop
            // 
            this.tabPageBackdrop.Location = new System.Drawing.Point(4, 27);
            this.tabPageBackdrop.Name = "tabPageBackdrop";
            this.tabPageBackdrop.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBackdrop.Size = new System.Drawing.Size(989, 798);
            this.tabPageBackdrop.TabIndex = 5;
            this.tabPageBackdrop.Text = "Backdrops";
            this.tabPageBackdrop.UseVisualStyleBackColor = true;
            // 
            // tabPageBanners
            // 
            this.tabPageBanners.Location = new System.Drawing.Point(4, 27);
            this.tabPageBanners.Name = "tabPageBanners";
            this.tabPageBanners.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageBanners.Size = new System.Drawing.Size(989, 798);
            this.tabPageBanners.TabIndex = 6;
            this.tabPageBanners.Text = "Bannières";
            this.tabPageBanners.UseVisualStyleBackColor = true;
            // 
            // tabPageGenres
            // 
            this.tabPageGenres.Location = new System.Drawing.Point(4, 27);
            this.tabPageGenres.Name = "tabPageGenres";
            this.tabPageGenres.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageGenres.Size = new System.Drawing.Size(989, 798);
            this.tabPageGenres.TabIndex = 7;
            this.tabPageGenres.Text = "Genres";
            this.tabPageGenres.UseVisualStyleBackColor = true;
            // 
            // tabPageStudios
            // 
            this.tabPageStudios.Location = new System.Drawing.Point(4, 27);
            this.tabPageStudios.Name = "tabPageStudios";
            this.tabPageStudios.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageStudios.Size = new System.Drawing.Size(989, 798);
            this.tabPageStudios.TabIndex = 8;
            this.tabPageStudios.Text = "Studios";
            this.tabPageStudios.UseVisualStyleBackColor = true;
            // 
            // tabPageCountries
            // 
            this.tabPageCountries.Location = new System.Drawing.Point(4, 27);
            this.tabPageCountries.Name = "tabPageCountries";
            this.tabPageCountries.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageCountries.Size = new System.Drawing.Size(989, 798);
            this.tabPageCountries.TabIndex = 9;
            this.tabPageCountries.Text = "Pays";
            this.tabPageCountries.UseVisualStyleBackColor = true;
            // 
            // tabPageTagLines
            // 
            this.tabPageTagLines.Location = new System.Drawing.Point(4, 27);
            this.tabPageTagLines.Name = "tabPageTagLines";
            this.tabPageTagLines.Padding = new System.Windows.Forms.Padding(3);
            this.tabPageTagLines.Size = new System.Drawing.Size(989, 798);
            this.tabPageTagLines.TabIndex = 10;
            this.tabPageTagLines.Text = "Accroches";
            this.tabPageTagLines.UseVisualStyleBackColor = true;
            // 
            // toolTip1
            // 
            this.toolTip1.AutomaticDelay = 300;
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.tabPageSwitcher1);
            this.panel1.Controls.Add(this.tabStrip1);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(1264, 114);
            this.panel1.TabIndex = 35;
            // 
            // tabPageSwitcher1
            // 
            this.tabPageSwitcher1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage1);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage3);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage2);
            this.tabPageSwitcher1.Controls.Add(this.tabStripPage4);
            this.tabPageSwitcher1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabPageSwitcher1.Location = new System.Drawing.Point(0, 26);
            this.tabPageSwitcher1.Name = "tabPageSwitcher1";
            this.tabPageSwitcher1.SelectedTabStripPage = this.tabStripPage1;
            this.tabPageSwitcher1.Size = new System.Drawing.Size(1264, 88);
            this.tabPageSwitcher1.TabIndex = 1;
            this.tabPageSwitcher1.TabStrip = this.tabStrip1;
            this.tabPageSwitcher1.Text = "tabPageSwitcher1";
            // 
            // tabStripPage1
            // 
            this.tabStripPage1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage1.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage1.Caption = "";
            this.tabStripPage1.Controls.Add(this.tabPanel6);
            this.tabStripPage1.Controls.Add(this.tabPanel7);
            this.tabStripPage1.Controls.Add(this.tabPanel5);
            this.tabStripPage1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage1.Location = new System.Drawing.Point(4, 0);
            this.tabStripPage1.Name = "tabStripPage1";
            this.tabStripPage1.Opacity = 255;
            this.tabStripPage1.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabStripPage1.Size = new System.Drawing.Size(1256, 86);
            this.tabStripPage1.Speed = 8;
            this.tabStripPage1.TabIndex = 0;
            // 
            // tabPanel6
            // 
            this.tabPanel6.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel6.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel6.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel6.Caption = "Media Collection";
            this.tabPanel6.Controls.Add(this.ribbonButton10);
            this.tabPanel6.Controls.Add(this.SaveButton);
            this.tabPanel6.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel6.Location = new System.Drawing.Point(379, 3);
            this.tabPanel6.Name = "tabPanel6";
            this.tabPanel6.Opacity = 255;
            this.tabPanel6.Size = new System.Drawing.Size(172, 83);
            this.tabPanel6.Speed = 1;
            this.tabPanel6.TabIndex = 7;
            // 
            // ribbonButton10
            // 
            this.ribbonButton10.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton10.filename = null;
            this.ribbonButton10.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton10.FlatAppearance.BorderSize = 0;
            this.ribbonButton10.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton10.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton10.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton10.folder = null;
            this.ribbonButton10.Image = global::MetaVideoEditor.Properties.Resources.refresh;
            this.ribbonButton10.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton10.img = global::MetaVideoEditor.Properties.Resources.refresh;
            this.ribbonButton10.img_back = null;
            this.ribbonButton10.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton10.img_heigth = 32;
            this.ribbonButton10.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton10.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton10.InfoComment = "Réinitialise les données de la bibliothèques. Attention : toutes les données non " +
                "enregistrées seront écrasées !";
            this.ribbonButton10.InfoImage = "";
            this.ribbonButton10.InfoTitle = "Réinitialisation de la bibliothèque";
            this.ribbonButton10.Location = new System.Drawing.Point(83, 6);
            this.ribbonButton10.MenuItems = null;
            this.ribbonButton10.Name = "ribbonButton10";
            this.ribbonButton10.Size = new System.Drawing.Size(72, 58);
            this.ribbonButton10.TabIndex = 13;
            this.ribbonButton10.Text = "Rebuild";
            this.ribbonButton10.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton10.UseVisualStyleBackColor = true;
            this.ribbonButton10.Click += new System.EventHandler(this.InitButton_Click);
            // 
            // SaveButton
            // 
            this.SaveButton.AutoSize = false;
            this.SaveButton.BackColor = System.Drawing.Color.Transparent;
            this.SaveButton.Dock = System.Windows.Forms.DockStyle.None;
            this.SaveButton.img = global::MetaVideoEditor.Properties.Resources.save;
            this.SaveButton.img_back = null;
            this.SaveButton.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.SaveButton.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.SaveButton.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.saveToolStripItem1});
            this.SaveButton.Location = new System.Drawing.Point(6, 6);
            this.SaveButton.Name = "SaveButton";
            this.SaveButton.Size = new System.Drawing.Size(76, 58);
            this.SaveButton.TabIndex = 14;
            this.SaveButton.Text = "ribbonItem1";
            // 
            // saveToolStripItem1
            // 
            this.saveToolStripItem1.AutoSize = false;
            this.saveToolStripItem1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.saveToolStripItem1.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.SaveCurrentButton,
            this.SaveCheckedButton,
            this.SaveModifiedButton});
            this.saveToolStripItem1.Name = "saveToolStripItem1";
            this.saveToolStripItem1.Size = new System.Drawing.Size(0, 54);
            this.saveToolStripItem1.Text = "Save";
            // 
            // SaveCurrentButton
            // 
            this.SaveCurrentButton.Name = "SaveCurrentButton";
            this.SaveCurrentButton.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.SaveCurrentButton.Size = new System.Drawing.Size(222, 22);
            this.SaveCurrentButton.Text = "Selected item";
            this.SaveCurrentButton.Click += new System.EventHandler(this.SaveCurrentButton_Click);
            // 
            // SaveCheckedButton
            // 
            this.SaveCheckedButton.Name = "SaveCheckedButton";
            this.SaveCheckedButton.Size = new System.Drawing.Size(222, 22);
            this.SaveCheckedButton.Text = "Checked items";
            this.SaveCheckedButton.Click += new System.EventHandler(this.SaveCheckedButton_Click);
            // 
            // SaveModifiedButton
            // 
            this.SaveModifiedButton.Name = "SaveModifiedButton";
            this.SaveModifiedButton.ShortcutKeys = ((System.Windows.Forms.Keys)(((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.Shift)
                        | System.Windows.Forms.Keys.S)));
            this.SaveModifiedButton.Size = new System.Drawing.Size(222, 22);
            this.SaveModifiedButton.Text = "Modified items";
            this.SaveModifiedButton.Click += new System.EventHandler(this.SaveModifiedButton_Click);
            // 
            // tabPanel7
            // 
            this.tabPanel7.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel7.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel7.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel7.Caption = "Search";
            this.tabPanel7.Controls.Add(this.ribbonButton7);
            this.tabPanel7.Controls.Add(this.ribbonButton6);
            this.tabPanel7.Controls.Add(this.ribbonButton5);
            this.tabPanel7.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel7.Location = new System.Drawing.Point(189, 3);
            this.tabPanel7.Name = "tabPanel7";
            this.tabPanel7.Opacity = 255;
            this.tabPanel7.Size = new System.Drawing.Size(190, 83);
            this.tabPanel7.Speed = 1;
            this.tabPanel7.TabIndex = 8;
            // 
            // ribbonButton7
            // 
            this.ribbonButton7.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton7.filename = null;
            this.ribbonButton7.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton7.FlatAppearance.BorderSize = 0;
            this.ribbonButton7.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton7.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton7.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton7.folder = null;
            this.ribbonButton7.Image = global::MetaVideoEditor.Properties.Resources.cancel;
            this.ribbonButton7.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton7.img = global::MetaVideoEditor.Properties.Resources.cancel;
            this.ribbonButton7.img_back = null;
            this.ribbonButton7.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton7.img_heigth = 32;
            this.ribbonButton7.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton7.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton7.InfoComment = "Annuler les modifications apportées sur le ou les éléments sélectionnés";
            this.ribbonButton7.InfoImage = "";
            this.ribbonButton7.InfoTitle = "Rétablir";
            this.ribbonButton7.Location = new System.Drawing.Point(135, 6);
            this.ribbonButton7.MenuItems = null;
            this.ribbonButton7.Name = "ribbonButton7";
            this.ribbonButton7.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton7.TabIndex = 10;
            this.ribbonButton7.Text = "Restore";
            this.ribbonButton7.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton7.UseVisualStyleBackColor = true;
            this.ribbonButton7.Click += new System.EventHandler(this.CancelButton_Click);
            // 
            // ribbonButton6
            // 
            this.ribbonButton6.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton6.filename = null;
            this.ribbonButton6.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton6.FlatAppearance.BorderSize = 0;
            this.ribbonButton6.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton6.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton6.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton6.folder = null;
            this.ribbonButton6.Image = global::MetaVideoEditor.Properties.Resources.autosearch;
            this.ribbonButton6.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton6.img = global::MetaVideoEditor.Properties.Resources.autosearch;
            this.ribbonButton6.img_back = null;
            this.ribbonButton6.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton6.img_heigth = 32;
            this.ribbonButton6.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton6.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton6.InfoComment = "Laissez MetaVideoEditor chercher les informations concernant le ou les éléments s" +
                "électionnés";
            this.ribbonButton6.InfoImage = "";
            this.ribbonButton6.InfoTitle = "Recherche automatique";
            this.ribbonButton6.Location = new System.Drawing.Point(82, 6);
            this.ribbonButton6.MenuItems = null;
            this.ribbonButton6.Name = "ribbonButton6";
            this.ribbonButton6.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton6.TabIndex = 9;
            this.ribbonButton6.Text = "Auto";
            this.ribbonButton6.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton6.UseVisualStyleBackColor = true;
            this.ribbonButton6.Click += new System.EventHandler(this.AutoButton_Click);
            // 
            // ribbonButton5
            // 
            this.ribbonButton5.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton5.filename = null;
            this.ribbonButton5.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton5.FlatAppearance.BorderSize = 0;
            this.ribbonButton5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton5.folder = null;
            this.ribbonButton5.Image = global::MetaVideoEditor.Properties.Resources.search;
            this.ribbonButton5.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton5.img = global::MetaVideoEditor.Properties.Resources.search;
            this.ribbonButton5.img_back = null;
            this.ribbonButton5.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton5.img_heigth = 32;
            this.ribbonButton5.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton5.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton5.InfoComment = "Recherchez les informations concernant l\'élément en cours";
            this.ribbonButton5.InfoImage = "";
            this.ribbonButton5.InfoTitle = "Recherche";
            this.ribbonButton5.Location = new System.Drawing.Point(6, 6);
            this.ribbonButton5.MenuItems = null;
            this.ribbonButton5.Name = "ribbonButton5";
            this.ribbonButton5.Size = new System.Drawing.Size(71, 58);
            this.ribbonButton5.TabIndex = 8;
            this.ribbonButton5.Text = "Search";
            this.ribbonButton5.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton5.UseVisualStyleBackColor = true;
            this.ribbonButton5.Click += new System.EventHandler(this.SearchButton_Click);
            // 
            // tabPanel5
            // 
            this.tabPanel5.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel5.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel5.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel5.Caption = "Options & Affichage";
            this.tabPanel5.Controls.Add(this.ribbonButton2);
            this.tabPanel5.Controls.Add(this.ribbonItem2);
            this.tabPanel5.Controls.Add(this.ribbonButton1);
            this.tabPanel5.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel5.Location = new System.Drawing.Point(0, 3);
            this.tabPanel5.Name = "tabPanel5";
            this.tabPanel5.Opacity = 255;
            this.tabPanel5.Size = new System.Drawing.Size(189, 83);
            this.tabPanel5.Speed = 1;
            this.tabPanel5.TabIndex = 6;
            // 
            // ribbonButton2
            // 
            this.ribbonButton2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton2.filename = null;
            this.ribbonButton2.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton2.FlatAppearance.BorderSize = 0;
            this.ribbonButton2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton2.folder = null;
            this.ribbonButton2.Image = global::MetaVideoEditor.Properties.Resources.apropos;
            this.ribbonButton2.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton2.img = global::MetaVideoEditor.Properties.Resources.apropos;
            this.ribbonButton2.img_back = null;
            this.ribbonButton2.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton2.img_heigth = 32;
            this.ribbonButton2.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton2.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton2.InfoComment = "";
            this.ribbonButton2.InfoImage = "";
            this.ribbonButton2.InfoTitle = "A propos de MetaVideoEditor";
            this.ribbonButton2.Location = new System.Drawing.Point(122, 6);
            this.ribbonButton2.MenuItems = null;
            this.ribbonButton2.Name = "ribbonButton2";
            this.ribbonButton2.Size = new System.Drawing.Size(50, 58);
            this.ribbonButton2.TabIndex = 5;
            this.ribbonButton2.Text = "About";
            this.ribbonButton2.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton2.UseVisualStyleBackColor = true;
            this.ribbonButton2.Click += new System.EventHandler(this.AproposButton_Click);
            // 
            // ribbonItem2
            // 
            this.ribbonItem2.AutoSize = false;
            this.ribbonItem2.BackColor = System.Drawing.Color.Transparent;
            this.ribbonItem2.Dock = System.Windows.Forms.DockStyle.None;
            this.ribbonItem2.img = global::MetaVideoEditor.Properties.Resources.colors;
            this.ribbonItem2.img_back = null;
            this.ribbonItem2.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonItem2.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonItem2.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.themeToolStripItem});
            this.ribbonItem2.Location = new System.Drawing.Point(64, 6);
            this.ribbonItem2.Name = "ribbonItem2";
            this.ribbonItem2.Size = new System.Drawing.Size(55, 58);
            this.ribbonItem2.TabIndex = 16;
            this.ribbonItem2.Text = "ribbonItem2";
            // 
            // themeToolStripItem
            // 
            this.themeToolStripItem.AutoSize = false;
            this.themeToolStripItem.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.None;
            this.themeToolStripItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.ToolStripItem1,
            this.ToolStripItem2,
            this.ToolStripItem3,
            this.ToolStripItem4,
            this.ToolStripItem5,
            this.ToolStripItem6,
            this.ToolStripItem7,
            this.ToolStripItem8,
            this.ToolStripItem9,
            this.ToolStripItem10,
            this.ToolStripItem11,
            this.ToolStripItem12});
            this.themeToolStripItem.Name = "themeToolStripItem";
            this.themeToolStripItem.Size = new System.Drawing.Size(0, 54);
            this.themeToolStripItem.Text = "Theme";
            // 
            // ToolStripItem1
            // 
            this.ToolStripItem1.Name = "ToolStripItem1";
            this.ToolStripItem1.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem1.Tag = "Azur";
            this.ToolStripItem1.Text = "Azur";
            this.ToolStripItem1.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem2
            // 
            this.ToolStripItem2.Name = "ToolStripItem2";
            this.ToolStripItem2.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem2.Tag = "Metal";
            this.ToolStripItem2.Text = "Metal";
            this.ToolStripItem2.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem3
            // 
            this.ToolStripItem3.Name = "ToolStripItem3";
            this.ToolStripItem3.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem3.Tag = "Dark";
            this.ToolStripItem3.Text = "Dark";
            this.ToolStripItem3.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem4
            // 
            this.ToolStripItem4.Name = "ToolStripItem4";
            this.ToolStripItem4.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem4.Tag = "Nature";
            this.ToolStripItem4.Text = "Nature";
            this.ToolStripItem4.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem5
            // 
            this.ToolStripItem5.Name = "ToolStripItem5";
            this.ToolStripItem5.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem5.Tag = "Dawn";
            this.ToolStripItem5.Text = "Dawn";
            this.ToolStripItem5.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem6
            // 
            this.ToolStripItem6.Name = "ToolStripItem6";
            this.ToolStripItem6.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem6.Tag = "Corn";
            this.ToolStripItem6.Text = "Corn";
            this.ToolStripItem6.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem7
            // 
            this.ToolStripItem7.Name = "ToolStripItem7";
            this.ToolStripItem7.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem7.Tag = "Chocolate";
            this.ToolStripItem7.Text = "Chocolate";
            this.ToolStripItem7.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem8
            // 
            this.ToolStripItem8.Name = "ToolStripItem8";
            this.ToolStripItem8.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem8.Tag = "Navy";
            this.ToolStripItem8.Text = "Navy";
            this.ToolStripItem8.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem9
            // 
            this.ToolStripItem9.Name = "ToolStripItem9";
            this.ToolStripItem9.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem9.Tag = "Ice";
            this.ToolStripItem9.Text = "Ice";
            this.ToolStripItem9.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem10
            // 
            this.ToolStripItem10.Name = "ToolStripItem10";
            this.ToolStripItem10.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem10.Tag = "Vanilla";
            this.ToolStripItem10.Text = "Vanilla";
            this.ToolStripItem10.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem11
            // 
            this.ToolStripItem11.Name = "ToolStripItem11";
            this.ToolStripItem11.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem11.Tag = "Canela";
            this.ToolStripItem11.Text = "Canela";
            this.ToolStripItem11.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ToolStripItem12
            // 
            this.ToolStripItem12.Name = "ToolStripItem12";
            this.ToolStripItem12.Size = new System.Drawing.Size(128, 22);
            this.ToolStripItem12.Tag = "Cake";
            this.ToolStripItem12.Text = "Cake";
            this.ToolStripItem12.Click += new System.EventHandler(this.Set_lB_Style);
            // 
            // ribbonButton1
            // 
            this.ribbonButton1.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton1.filename = null;
            this.ribbonButton1.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton1.FlatAppearance.BorderSize = 0;
            this.ribbonButton1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton1.folder = null;
            this.ribbonButton1.Image = global::MetaVideoEditor.Properties.Resources.settings;
            this.ribbonButton1.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton1.img = global::MetaVideoEditor.Properties.Resources.settings;
            this.ribbonButton1.img_back = null;
            this.ribbonButton1.img_click = global::MetaVideoEditor.Properties.Resources.B_click1;
            this.ribbonButton1.img_heigth = 32;
            this.ribbonButton1.img_on = global::MetaVideoEditor.Properties.Resources.B_on1;
            this.ribbonButton1.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton1.InfoComment = "Modifiez ici les paramètres généraux de MetaVideoEditor";
            this.ribbonButton1.InfoImage = "";
            this.ribbonButton1.InfoTitle = "Options";
            this.ribbonButton1.Location = new System.Drawing.Point(8, 6);
            this.ribbonButton1.MenuItems = null;
            this.ribbonButton1.Name = "ribbonButton1";
            this.ribbonButton1.Size = new System.Drawing.Size(50, 58);
            this.ribbonButton1.TabIndex = 4;
            this.ribbonButton1.Text = "Settings";
            this.ribbonButton1.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton1.UseVisualStyleBackColor = true;
            this.ribbonButton1.Click += new System.EventHandler(this.OptionButton_Click);
            // 
            // tabStripPage3
            // 
            this.tabStripPage3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage3.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage3.Caption = "";
            this.tabStripPage3.Controls.Add(this.tabPanel11);
            this.tabStripPage3.Controls.Add(this.tabPanel12);
            this.tabStripPage3.Controls.Add(this.tabPanel3);
            this.tabStripPage3.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage3.Location = new System.Drawing.Point(4, 0);
            this.tabStripPage3.Name = "tabStripPage3";
            this.tabStripPage3.Opacity = 255;
            this.tabStripPage3.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabStripPage3.Size = new System.Drawing.Size(1256, 86);
            this.tabStripPage3.Speed = 8;
            this.tabStripPage3.TabIndex = 2;
            // 
            // tabPanel11
            // 
            this.tabPanel11.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel11.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel11.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel11.Caption = "";
            this.tabPanel11.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel11.Location = new System.Drawing.Point(359, 3);
            this.tabPanel11.Name = "tabPanel11";
            this.tabPanel11.Opacity = 255;
            this.tabPanel11.Size = new System.Drawing.Size(229, 83);
            this.tabPanel11.Speed = 1;
            this.tabPanel11.TabIndex = 6;
            // 
            // tabPanel12
            // 
            this.tabPanel12.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel12.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel12.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel12.Caption = "";
            this.tabPanel12.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel12.Location = new System.Drawing.Point(217, 3);
            this.tabPanel12.Name = "tabPanel12";
            this.tabPanel12.Opacity = 255;
            this.tabPanel12.Size = new System.Drawing.Size(142, 83);
            this.tabPanel12.Speed = 1;
            this.tabPanel12.TabIndex = 7;
            // 
            // tabPanel3
            // 
            this.tabPanel3.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel3.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel3.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel3.Caption = "";
            this.tabPanel3.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel3.Location = new System.Drawing.Point(0, 3);
            this.tabPanel3.Name = "tabPanel3";
            this.tabPanel3.Opacity = 255;
            this.tabPanel3.Size = new System.Drawing.Size(217, 83);
            this.tabPanel3.Speed = 1;
            this.tabPanel3.TabIndex = 5;
            // 
            // tabStripPage2
            // 
            this.tabStripPage2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage2.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage2.Caption = "";
            this.tabStripPage2.Controls.Add(this.tabPanel10);
            this.tabStripPage2.Controls.Add(this.tabPanel8);
            this.tabStripPage2.Controls.Add(this.tabPanel9);
            this.tabStripPage2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage2.Location = new System.Drawing.Point(4, 0);
            this.tabStripPage2.Name = "tabStripPage2";
            this.tabStripPage2.Opacity = 255;
            this.tabStripPage2.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabStripPage2.Size = new System.Drawing.Size(1256, 86);
            this.tabStripPage2.Speed = 8;
            this.tabStripPage2.TabIndex = 1;
            // 
            // tabPanel10
            // 
            this.tabPanel10.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel10.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel10.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel10.Caption = "Mailing";
            this.tabPanel10.Controls.Add(this.ribbonButton21);
            this.tabPanel10.Controls.Add(this.ribbonButton20);
            this.tabPanel10.Controls.Add(this.ribbonButton14);
            this.tabPanel10.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel10.Location = new System.Drawing.Point(426, 3);
            this.tabPanel10.Name = "tabPanel10";
            this.tabPanel10.Opacity = 255;
            this.tabPanel10.Size = new System.Drawing.Size(154, 83);
            this.tabPanel10.Speed = 1;
            this.tabPanel10.TabIndex = 6;
            // 
            // ribbonButton21
            // 
            this.ribbonButton21.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton21.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton21.filename = null;
            this.ribbonButton21.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton21.FlatAppearance.BorderSize = 0;
            this.ribbonButton21.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton21.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton21.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton21.folder = null;
            this.ribbonButton21.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton21.img = null;
            this.ribbonButton21.img_back = null;
            this.ribbonButton21.img_click = null;
            this.ribbonButton21.img_heigth = 0;
            this.ribbonButton21.img_on = null;
            this.ribbonButton21.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton21.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton21.InfoImage = "diagram.png";
            this.ribbonButton21.InfoTitle = "PDAs";
            this.ribbonButton21.Location = new System.Drawing.Point(80, 15);
            this.ribbonButton21.MenuItems = null;
            this.ribbonButton21.Name = "ribbonButton21";
            this.ribbonButton21.Size = new System.Drawing.Size(28, 29);
            this.ribbonButton21.TabIndex = 19;
            this.ribbonButton21.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton21.UseVisualStyleBackColor = true;
            // 
            // ribbonButton20
            // 
            this.ribbonButton20.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton20.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton20.filename = null;
            this.ribbonButton20.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton20.FlatAppearance.BorderSize = 0;
            this.ribbonButton20.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton20.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton20.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton20.folder = null;
            this.ribbonButton20.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton20.img = null;
            this.ribbonButton20.img_back = null;
            this.ribbonButton20.img_click = null;
            this.ribbonButton20.img_heigth = 0;
            this.ribbonButton20.img_on = null;
            this.ribbonButton20.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton20.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton20.InfoImage = "diagram.png";
            this.ribbonButton20.InfoTitle = "Sent";
            this.ribbonButton20.Location = new System.Drawing.Point(114, 15);
            this.ribbonButton20.MenuItems = null;
            this.ribbonButton20.Name = "ribbonButton20";
            this.ribbonButton20.Size = new System.Drawing.Size(28, 28);
            this.ribbonButton20.TabIndex = 18;
            this.ribbonButton20.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton20.UseVisualStyleBackColor = true;
            // 
            // ribbonButton14
            // 
            this.ribbonButton14.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton14.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton14.filename = null;
            this.ribbonButton14.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton14.FlatAppearance.BorderSize = 0;
            this.ribbonButton14.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton14.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton14.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton14.folder = null;
            this.ribbonButton14.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton14.img = null;
            this.ribbonButton14.img_back = null;
            this.ribbonButton14.img_click = null;
            this.ribbonButton14.img_heigth = 0;
            this.ribbonButton14.img_on = null;
            this.ribbonButton14.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton14.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton14.InfoImage = "diagram.png";
            this.ribbonButton14.InfoTitle = "Sent";
            this.ribbonButton14.Location = new System.Drawing.Point(6, 6);
            this.ribbonButton14.MenuItems = null;
            this.ribbonButton14.Name = "ribbonButton14";
            this.ribbonButton14.Size = new System.Drawing.Size(55, 58);
            this.ribbonButton14.TabIndex = 17;
            this.ribbonButton14.Text = "Greetings";
            this.ribbonButton14.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton14.UseVisualStyleBackColor = true;
            // 
            // tabPanel8
            // 
            this.tabPanel8.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel8.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel8.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel8.Caption = "Reports";
            this.tabPanel8.Controls.Add(this.ribbonButton19);
            this.tabPanel8.Controls.Add(this.ribbonButton18);
            this.tabPanel8.Controls.Add(this.ribbonButton17);
            this.tabPanel8.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel8.Location = new System.Drawing.Point(235, 3);
            this.tabPanel8.Name = "tabPanel8";
            this.tabPanel8.Opacity = 255;
            this.tabPanel8.Size = new System.Drawing.Size(191, 83);
            this.tabPanel8.Speed = 1;
            this.tabPanel8.TabIndex = 6;
            // 
            // ribbonButton19
            // 
            this.ribbonButton19.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton19.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton19.filename = null;
            this.ribbonButton19.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton19.FlatAppearance.BorderSize = 0;
            this.ribbonButton19.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton19.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton19.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton19.folder = null;
            this.ribbonButton19.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton19.img = null;
            this.ribbonButton19.img_back = null;
            this.ribbonButton19.img_click = null;
            this.ribbonButton19.img_heigth = 0;
            this.ribbonButton19.img_on = null;
            this.ribbonButton19.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton19.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton19.InfoImage = "diagram.png";
            this.ribbonButton19.InfoTitle = "PDAs";
            this.ribbonButton19.Location = new System.Drawing.Point(112, 6);
            this.ribbonButton19.MenuItems = null;
            this.ribbonButton19.Name = "ribbonButton19";
            this.ribbonButton19.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton19.TabIndex = 21;
            this.ribbonButton19.Text = "Graphs";
            this.ribbonButton19.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton19.UseVisualStyleBackColor = true;
            // 
            // ribbonButton18
            // 
            this.ribbonButton18.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton18.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton18.filename = null;
            this.ribbonButton18.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton18.FlatAppearance.BorderSize = 0;
            this.ribbonButton18.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton18.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton18.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton18.folder = null;
            this.ribbonButton18.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton18.img = null;
            this.ribbonButton18.img_back = null;
            this.ribbonButton18.img_click = null;
            this.ribbonButton18.img_heigth = 0;
            this.ribbonButton18.img_on = null;
            this.ribbonButton18.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton18.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton18.InfoImage = "diagram.png";
            this.ribbonButton18.InfoTitle = "PDAs";
            this.ribbonButton18.Location = new System.Drawing.Point(59, 6);
            this.ribbonButton18.MenuItems = null;
            this.ribbonButton18.Name = "ribbonButton18";
            this.ribbonButton18.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton18.TabIndex = 20;
            this.ribbonButton18.Text = "Edit";
            this.ribbonButton18.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton18.UseVisualStyleBackColor = true;
            // 
            // ribbonButton17
            // 
            this.ribbonButton17.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton17.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton17.filename = null;
            this.ribbonButton17.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton17.FlatAppearance.BorderSize = 0;
            this.ribbonButton17.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton17.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton17.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton17.folder = null;
            this.ribbonButton17.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton17.img = null;
            this.ribbonButton17.img_back = null;
            this.ribbonButton17.img_click = null;
            this.ribbonButton17.img_heigth = 0;
            this.ribbonButton17.img_on = null;
            this.ribbonButton17.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton17.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton17.InfoImage = "diagram.png";
            this.ribbonButton17.InfoTitle = "PDAs";
            this.ribbonButton17.Location = new System.Drawing.Point(6, 6);
            this.ribbonButton17.MenuItems = null;
            this.ribbonButton17.Name = "ribbonButton17";
            this.ribbonButton17.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton17.TabIndex = 19;
            this.ribbonButton17.Text = "Daily";
            this.ribbonButton17.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton17.UseVisualStyleBackColor = true;
            // 
            // tabPanel9
            // 
            this.tabPanel9.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel9.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel9.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel9.Caption = "Salesmans";
            this.tabPanel9.Controls.Add(this.ribbonButton16);
            this.tabPanel9.Controls.Add(this.ribbonButton15);
            this.tabPanel9.Controls.Add(this.ribbonButton13);
            this.tabPanel9.Controls.Add(this.ribbonButton12);
            this.tabPanel9.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel9.Location = new System.Drawing.Point(0, 3);
            this.tabPanel9.Name = "tabPanel9";
            this.tabPanel9.Opacity = 255;
            this.tabPanel9.Size = new System.Drawing.Size(235, 83);
            this.tabPanel9.Speed = 1;
            this.tabPanel9.TabIndex = 6;
            // 
            // ribbonButton16
            // 
            this.ribbonButton16.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton16.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton16.filename = null;
            this.ribbonButton16.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton16.FlatAppearance.BorderSize = 0;
            this.ribbonButton16.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton16.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton16.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton16.folder = null;
            this.ribbonButton16.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton16.img = null;
            this.ribbonButton16.img_back = null;
            this.ribbonButton16.img_click = null;
            this.ribbonButton16.img_heigth = 0;
            this.ribbonButton16.img_on = null;
            this.ribbonButton16.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton16.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton16.InfoImage = "diagram.png";
            this.ribbonButton16.InfoTitle = "PDAs";
            this.ribbonButton16.Location = new System.Drawing.Point(170, 6);
            this.ribbonButton16.MenuItems = null;
            this.ribbonButton16.Name = "ribbonButton16";
            this.ribbonButton16.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton16.TabIndex = 18;
            this.ribbonButton16.Text = "PDAs";
            this.ribbonButton16.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton16.UseVisualStyleBackColor = true;
            // 
            // ribbonButton15
            // 
            this.ribbonButton15.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton15.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton15.filename = null;
            this.ribbonButton15.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton15.FlatAppearance.BorderSize = 0;
            this.ribbonButton15.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton15.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton15.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton15.folder = null;
            this.ribbonButton15.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton15.img = null;
            this.ribbonButton15.img_back = null;
            this.ribbonButton15.img_click = null;
            this.ribbonButton15.img_heigth = 0;
            this.ribbonButton15.img_on = null;
            this.ribbonButton15.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton15.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton15.InfoImage = "diagram.png";
            this.ribbonButton15.InfoTitle = "Sent";
            this.ribbonButton15.Location = new System.Drawing.Point(117, 6);
            this.ribbonButton15.MenuItems = null;
            this.ribbonButton15.Name = "ribbonButton15";
            this.ribbonButton15.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton15.TabIndex = 17;
            this.ribbonButton15.Text = "Benefits";
            this.ribbonButton15.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton15.UseVisualStyleBackColor = true;
            // 
            // ribbonButton13
            // 
            this.ribbonButton13.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton13.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton13.filename = null;
            this.ribbonButton13.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton13.FlatAppearance.BorderSize = 0;
            this.ribbonButton13.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton13.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton13.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton13.folder = null;
            this.ribbonButton13.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton13.img = null;
            this.ribbonButton13.img_back = null;
            this.ribbonButton13.img_click = null;
            this.ribbonButton13.img_heigth = 0;
            this.ribbonButton13.img_on = null;
            this.ribbonButton13.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton13.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton13.InfoImage = "diagram.png";
            this.ribbonButton13.InfoTitle = "Sent";
            this.ribbonButton13.Location = new System.Drawing.Point(64, 6);
            this.ribbonButton13.MenuItems = null;
            this.ribbonButton13.Name = "ribbonButton13";
            this.ribbonButton13.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton13.TabIndex = 16;
            this.ribbonButton13.Text = "Routes";
            this.ribbonButton13.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton13.UseVisualStyleBackColor = true;
            // 
            // ribbonButton12
            // 
            this.ribbonButton12.BackColor = System.Drawing.Color.Transparent;
            this.ribbonButton12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.ribbonButton12.filename = null;
            this.ribbonButton12.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton12.FlatAppearance.BorderSize = 0;
            this.ribbonButton12.FlatAppearance.MouseDownBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton12.FlatAppearance.MouseOverBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(100)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.ribbonButton12.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.ribbonButton12.folder = null;
            this.ribbonButton12.ImageAlign = System.Drawing.ContentAlignment.TopCenter;
            this.ribbonButton12.img = null;
            this.ribbonButton12.img_back = null;
            this.ribbonButton12.img_click = null;
            this.ribbonButton12.img_heigth = 0;
            this.ribbonButton12.img_on = null;
            this.ribbonButton12.InfoColor = System.Drawing.Color.FromArgb(((int)(((byte)(201)))), ((int)(((byte)(217)))), ((int)(((byte)(239)))));
            this.ribbonButton12.InfoComment = "loosdfosdf dsofd df dfodf dofd fod afosdf dof do";
            this.ribbonButton12.InfoImage = "diagram.png";
            this.ribbonButton12.InfoTitle = "Sent";
            this.ribbonButton12.Location = new System.Drawing.Point(11, 6);
            this.ribbonButton12.MenuItems = null;
            this.ribbonButton12.Name = "ribbonButton12";
            this.ribbonButton12.Size = new System.Drawing.Size(47, 58);
            this.ribbonButton12.TabIndex = 15;
            this.ribbonButton12.Text = "Clients";
            this.ribbonButton12.TextAlign = System.Drawing.ContentAlignment.BottomCenter;
            this.ribbonButton12.UseVisualStyleBackColor = true;
            // 
            // tabStripPage4
            // 
            this.tabStripPage4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage4.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabStripPage4.Caption = "";
            this.tabStripPage4.Controls.Add(this.tabPanel4);
            this.tabStripPage4.Controls.Add(this.tabPanel1);
            this.tabStripPage4.Controls.Add(this.tabPanel2);
            this.tabStripPage4.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabStripPage4.Location = new System.Drawing.Point(4, 0);
            this.tabStripPage4.Name = "tabStripPage4";
            this.tabStripPage4.Opacity = 255;
            this.tabStripPage4.Padding = new System.Windows.Forms.Padding(0, 3, 0, 0);
            this.tabStripPage4.Size = new System.Drawing.Size(1256, 86);
            this.tabStripPage4.Speed = 8;
            this.tabStripPage4.TabIndex = 3;
            // 
            // tabPanel4
            // 
            this.tabPanel4.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel4.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel4.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel4.Caption = "";
            this.tabPanel4.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel4.Location = new System.Drawing.Point(426, 3);
            this.tabPanel4.Name = "tabPanel4";
            this.tabPanel4.Opacity = 255;
            this.tabPanel4.Size = new System.Drawing.Size(142, 83);
            this.tabPanel4.Speed = 1;
            this.tabPanel4.TabIndex = 6;
            // 
            // tabPanel1
            // 
            this.tabPanel1.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel1.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel1.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel1.Caption = "";
            this.tabPanel1.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel1.Location = new System.Drawing.Point(142, 3);
            this.tabPanel1.Name = "tabPanel1";
            this.tabPanel1.Opacity = 255;
            this.tabPanel1.Size = new System.Drawing.Size(284, 83);
            this.tabPanel1.Speed = 1;
            this.tabPanel1.TabIndex = 4;
            // 
            // tabPanel2
            // 
            this.tabPanel2.BackColor = System.Drawing.Color.Transparent;
            this.tabPanel2.BaseColor = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel2.BaseColorOn = System.Drawing.Color.FromArgb(((int)(((byte)(215)))), ((int)(((byte)(227)))), ((int)(((byte)(242)))));
            this.tabPanel2.Caption = "";
            this.tabPanel2.Dock = System.Windows.Forms.DockStyle.Left;
            this.tabPanel2.Location = new System.Drawing.Point(0, 3);
            this.tabPanel2.Name = "tabPanel2";
            this.tabPanel2.Opacity = 255;
            this.tabPanel2.Size = new System.Drawing.Size(142, 83);
            this.tabPanel2.Speed = 1;
            this.tabPanel2.TabIndex = 5;
            // 
            // tabStrip1
            // 
            this.tabStrip1.AutoSize = false;
            this.tabStrip1.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(191)))), ((int)(((byte)(219)))), ((int)(((byte)(255)))));
            this.tabStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Hidden;
            this.tabStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tab1});
            this.tabStrip1.Location = new System.Drawing.Point(0, 0);
            this.tabStrip1.Name = "tabStrip1";
            this.tabStrip1.Padding = new System.Windows.Forms.Padding(60, 3, 30, 0);
            this.tabStrip1.SelectedTab = null;
            this.tabStrip1.ShowItemToolTips = false;
            this.tabStrip1.Size = new System.Drawing.Size(1264, 26);
            this.tabStrip1.TabIndex = 0;
            this.tabStrip1.TabOverlap = 0;
            this.tabStrip1.Text = "tabStrip1";
            // 
            // tab1
            // 
            this.tab1.AutoSize = false;
            this.tab1.Checked = true;
            this.tab1.CheckState = System.Windows.Forms.CheckState.Checked;
            this.tab1.Font = new System.Drawing.Font("Segoe UI", 9F);
            this.tab1.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(44)))), ((int)(((byte)(90)))), ((int)(((byte)(154)))));
            this.tab1.Margin = new System.Windows.Forms.Padding(6, 1, 0, 2);
            this.tab1.Name = "tab1";
            this.tab1.Size = new System.Drawing.Size(63, 23);
            this.tab1.TabStripPage = this.tabStripPage1;
            this.tab1.Text = "Home";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.messageDropDownButton,
            this.messageTextBox,
            this.toolStripDropDownButton1});
            this.statusStrip1.LayoutStyle = System.Windows.Forms.ToolStripLayoutStyle.HorizontalStackWithOverflow;
            this.statusStrip1.Location = new System.Drawing.Point(0, 953);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(1264, 22);
            this.statusStrip1.TabIndex = 36;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // messageDropDownButton
            // 
            this.messageDropDownButton.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.messageDropDownButton.Name = "messageDropDownButton";
            this.messageDropDownButton.Size = new System.Drawing.Size(26, 20);
            this.messageDropDownButton.Text = "0";
            // 
            // messageTextBox
            // 
            this.messageTextBox.AutoSize = false;
            this.messageTextBox.BackColor = System.Drawing.Color.Transparent;
            this.messageTextBox.Name = "messageTextBox";
            this.messageTextBox.Size = new System.Drawing.Size(600, 17);
            this.messageTextBox.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // toolStripDropDownButton1
            // 
            this.toolStripDropDownButton1.Alignment = System.Windows.Forms.ToolStripItemAlignment.Right;
            this.toolStripDropDownButton1.DisplayStyle = System.Windows.Forms.ToolStripItemDisplayStyle.Text;
            this.toolStripDropDownButton1.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this.toolStripDropDownButton1.ImageTransparentColor = System.Drawing.Color.Magenta;
            this.toolStripDropDownButton1.Name = "toolStripDropDownButton1";
            this.toolStripDropDownButton1.ShowDropDownArrow = false;
            this.toolStripDropDownButton1.Size = new System.Drawing.Size(4, 20);
            this.toolStripDropDownButton1.Click += new System.EventHandler(this.toolStripDropDownButton1_Click);
            // 
            // testToolStripMenuItem
            // 
            this.testToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.item1ToolStripMenuItem,
            this.item2ToolStripMenuItem});
            this.testToolStripMenuItem.Name = "testToolStripMenuItem";
            this.testToolStripMenuItem.Size = new System.Drawing.Size(12, 47);
            // 
            // item1ToolStripMenuItem
            // 
            this.item1ToolStripMenuItem.Name = "item1ToolStripMenuItem";
            this.item1ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.item1ToolStripMenuItem.Text = "item1";
            // 
            // item2ToolStripMenuItem
            // 
            this.item2ToolStripMenuItem.Name = "item2ToolStripMenuItem";
            this.item2ToolStripMenuItem.Size = new System.Drawing.Size(104, 22);
            this.item2ToolStripMenuItem.Text = "item2";
            // 
            // MetaVideoEditor
            // 
            this.AllowDrop = true;
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Inherit;
            this.ClientSize = new System.Drawing.Size(1264, 975);
            this.Controls.Add(this.splitContainer1);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.panel1);
            this.Cursor = System.Windows.Forms.Cursors.Default;
            this.DoubleBuffered = true;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.KeyPreview = true;
            this.MainMenuStrip = this.SaveButton;
            this.Name = "MetaVideoEditor";
            this.Text = "MetaVideoEditor";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MetaVideoEditor_FormClosing);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.MetaVideoEditor_KeyDown);
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.ResumeLayout(false);
            this.SearchPanel.ResumeLayout(false);
            this.SearchPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.panel3.ResumeLayout(false);
            this.tabControl.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.tabPageSwitcher1.ResumeLayout(false);
            this.tabStripPage1.ResumeLayout(false);
            this.tabPanel6.ResumeLayout(false);
            this.SaveButton.ResumeLayout(false);
            this.SaveButton.PerformLayout();
            this.tabPanel7.ResumeLayout(false);
            this.tabPanel5.ResumeLayout(false);
            this.ribbonItem2.ResumeLayout(false);
            this.ribbonItem2.PerformLayout();
            this.tabStripPage3.ResumeLayout(false);
            this.tabStripPage2.ResumeLayout(false);
            this.tabPanel10.ResumeLayout(false);
            this.tabPanel8.ResumeLayout(false);
            this.tabPanel9.ResumeLayout(false);
            this.tabStripPage4.ResumeLayout(false);
            this.tabStrip1.ResumeLayout(false);
            this.tabStrip1.PerformLayout();
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private TreeView itemsView;
        private ToolTip toolTip1;
        private Panel panel1;

        private RibbonStyle.TabPageSwitcher tabPageSwitcher1;
        private RibbonStyle.TabStripPage tabStripPage4;
        private RibbonStyle.TabStripPage tabStripPage3;
        private RibbonStyle.TabStripPage tabStripPage2;
        private RibbonStyle.TabStripPage tabStripPage1;
        private RibbonStyle.TabStrip tabStrip1;
        private RibbonStyle.Tab tab1;
        private RibbonStyle.TabPanel tabPanel6;
        private RibbonStyle.TabPanel tabPanel7;
        private RibbonStyle.TabPanel tabPanel5;
        private RibbonStyle.TabPanel tabPanel4;
        private RibbonStyle.TabPanel tabPanel1;
        private RibbonStyle.TabPanel tabPanel2;
        private RibbonStyle.TabPanel tabPanel3;
        private RibbonStyle.TabPanel tabPanel11;
        private RibbonStyle.TabPanel tabPanel12;
        private RibbonStyle.TabPanel tabPanel10;
        private RibbonStyle.TabPanel tabPanel8;
        private RibbonStyle.TabPanel tabPanel9;
        private RibbonStyle.RibbonButton ribbonButton1;
        private RibbonStyle.RibbonButton ribbonButton2;
        private RibbonStyle.RibbonButton ribbonButton5;
        private RibbonStyle.RibbonButton ribbonButton6;
        private RibbonStyle.RibbonButton ribbonButton7;
        private RibbonStyle.RibbonButton ribbonButton10;
        private RibbonStyle.RibbonButton ribbonButton12;
        private RibbonStyle.RibbonButton ribbonButton13;
        private RibbonStyle.RibbonButton ribbonButton14;
        private RibbonStyle.RibbonButton ribbonButton15;
        private RibbonStyle.RibbonButton ribbonButton16;
        private RibbonStyle.RibbonButton ribbonButton19;
        private RibbonStyle.RibbonButton ribbonButton18;
        private RibbonStyle.RibbonButton ribbonButton17;
        private RibbonStyle.RibbonButton ribbonButton21;
        private RibbonStyle.RibbonButton ribbonButton20;
        private ToolStripMenuItem testToolStripMenuItem;
        private ToolStripMenuItem item1ToolStripMenuItem;
        private ToolStripMenuItem item2ToolStripMenuItem;
        private RibbonStyle.RibbonItem ribbonItem2;
        private System.Windows.Forms.ToolStripMenuItem themeToolStripItem;
        private StatusStrip statusStrip1;
        private ToolStripStatusLabel messageTextBox;
        private Label Medialabel;
        private RibbonStyle.RibbonItem SaveButton;
        private System.Windows.Forms.ToolStripMenuItem saveToolStripItem1;
        private CustomControls.ContextMenuItem ToolStripItem1;
        private CustomControls.ContextMenuItem ToolStripItem2;
        private CustomControls.ContextMenuItem ToolStripItem3;
        private CustomControls.ContextMenuItem ToolStripItem4;
        private CustomControls.ContextMenuItem ToolStripItem5;
        private CustomControls.ContextMenuItem ToolStripItem6;
        private CustomControls.ContextMenuItem ToolStripItem7;
        private CustomControls.ContextMenuItem ToolStripItem8;
        private CustomControls.ContextMenuItem ToolStripItem9;
        private CustomControls.ContextMenuItem ToolStripItem10;
        private CustomControls.ContextMenuItem ToolStripItem11;
        private CustomControls.ContextMenuItem ToolStripItem12;
        private CustomControls.ContextMenuItem SaveCurrentButton;
        private CustomControls.ContextMenuItem SaveCheckedButton;
        private CustomControls.ContextMenuItem SaveModifiedButton;
        private TextBox SearchBox;
        private Panel SearchPanel;
        private PictureBox pictureBox1;
        private Panel panel3;
        private CustomControls.DraggableTabControl tabControl;
        private TabPage tabPageOverview;
        private TabPage tabPageGeneral;
        private TabPage tabPageActors;
        private TabPage tabPageTrailers;
        private TabPage tabPagePoster;
        private TabPage tabPageBackdrop;
        private TabPage tabPageBanners;
        private TabPage tabPageGenres;
        private TabPage tabPageStudios;
        private SplitContainer splitContainer1;
        private TabPage tabPageCountries;
        private TabPage tabPageTagLines;
        private TabPage tabPageCrew;
        private CustomControls.ToolStripQueue messageDropDownButton;
        private ToolStripDropDownButton toolStripDropDownButton1;

        
        
       
    }

    

}

