namespace MetaVideoEditor
{
    partial class OverviewPanel
    {
        /// <summary> 
        /// Variable nécessaire au concepteur.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary> 
        /// Nettoyage des ressources utilisées.
        /// </summary>
        /// <param name="disposing">true si les ressources managées doivent être supprimées ; sinon, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Code généré par le Concepteur de composants

        /// <summary> 
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas 
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            this.DetailsPanel = new CustomControls.DoubleBufferPanel();
            this.bannerBox = new CustomControls.ImageBox();
            this.bd2Box = new CustomControls.ImageBox();
            this.posterBox = new CustomControls.ImageBox();
            this.bd1Box = new CustomControls.ImageBox();
            this.InfosPanel = new System.Windows.Forms.Panel();
            this.generalGroup = new System.Windows.Forms.GroupBox();
            this.studiosBox = new CustomControls.TextBoxControl();
            this.genresBox = new CustomControls.TextBoxControl();
            this.runtimeBox = new CustomControls.NumericBoxControl();
            this.RatingPanel = new System.Windows.Forms.Panel();
            this.ratingBox = new CustomControls.NumericBoxControl();
            this.Star5Picture = new System.Windows.Forms.PictureBox();
            this.Star4Picture = new System.Windows.Forms.PictureBox();
            this.Star3Picture = new System.Windows.Forms.PictureBox();
            this.Star2Picture = new System.Windows.Forms.PictureBox();
            this.Star1Picture = new System.Windows.Forms.PictureBox();
            this.peopleGroup = new System.Windows.Forms.GroupBox();
            this.actorsBox = new CustomControls.TextBoxControl();
            this.crewBox = new CustomControls.TextBoxControl();
            this.overviewGroup = new System.Windows.Forms.GroupBox();
            this.overviewBox = new CustomControls.TextBoxControl();
            this.titleBox = new CustomControls.TextBoxControl();
            this.DetailsPanel.SuspendLayout();
            this.InfosPanel.SuspendLayout();
            this.generalGroup.SuspendLayout();
            this.RatingPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.Star5Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star4Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star3Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star2Picture)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star1Picture)).BeginInit();
            this.peopleGroup.SuspendLayout();
            this.overviewGroup.SuspendLayout();
            this.SuspendLayout();
            // 
            // DetailsPanel
            // 
            this.DetailsPanel.AutoScroll = true;
            this.DetailsPanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.DetailsPanel.Controls.Add(this.bannerBox);
            this.DetailsPanel.Controls.Add(this.bd2Box);
            this.DetailsPanel.Controls.Add(this.posterBox);
            this.DetailsPanel.Controls.Add(this.bd1Box);
            this.DetailsPanel.Controls.Add(this.InfosPanel);
            this.DetailsPanel.Controls.Add(this.titleBox);
            this.DetailsPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.DetailsPanel.Location = new System.Drawing.Point(0, 0);
            this.DetailsPanel.Name = "DetailsPanel";
            this.DetailsPanel.Size = new System.Drawing.Size(987, 805);
            this.DetailsPanel.TabIndex = 12;
            this.DetailsPanel.Click += new System.EventHandler(this.DetailsPanel_Click);
            // 
            // bannerBox
            // 
            this.bannerBox.AllowDrop = true;
            this.bannerBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.bannerBox.BackColor = System.Drawing.Color.Transparent;
            this.bannerBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bannerBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bannerBox.ImgPoster = null;
            this.bannerBox.IsChecked = false;
            this.bannerBox.IsSelected = false;
            this.bannerBox.Location = new System.Drawing.Point(23, 702);
            this.bannerBox.Name = "bannerBox";
            this.bannerBox.Size = new System.Drawing.Size(556, 100);
            this.bannerBox.TabIndex = 26;
            this.bannerBox.OnPictureChanged += new System.EventHandler(this.bannerBox_OnPictureChanged);
            this.bannerBox.OnPictureDeleted += new System.EventHandler(this.bannerBox_OnPictureDeleted);
            this.bannerBox.OnCheckChanged += new System.EventHandler(this.posterBox_OnCheckChanged);
            // 
            // bd2Box
            // 
            this.bd2Box.AllowDrop = true;
            this.bd2Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bd2Box.BackColor = System.Drawing.Color.Transparent;
            this.bd2Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bd2Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bd2Box.ImgPoster = null;
            this.bd2Box.IsChecked = false;
            this.bd2Box.IsSelected = false;
            this.bd2Box.Location = new System.Drawing.Point(639, 615);
            this.bd2Box.Name = "bd2Box";
            this.bd2Box.Size = new System.Drawing.Size(310, 187);
            this.bd2Box.TabIndex = 25;
            this.bd2Box.OnPictureChanged += new System.EventHandler(this.bd1Box_OnPictureChanged);
            this.bd2Box.OnPictureDeleted += new System.EventHandler(this.bd1Box_OnPictureDeleted);
            this.bd2Box.OnCheckChanged += new System.EventHandler(this.posterBox_OnCheckChanged);
            // 
            // posterBox
            // 
            this.posterBox.AllowDrop = true;
            this.posterBox.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.posterBox.BackColor = System.Drawing.Color.Transparent;
            this.posterBox.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.posterBox.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.posterBox.ImgPoster = null;
            this.posterBox.IsChecked = false;
            this.posterBox.IsSelected = false;
            this.posterBox.Location = new System.Drawing.Point(720, 53);
            this.posterBox.Name = "posterBox";
            this.posterBox.Size = new System.Drawing.Size(229, 333);
            this.posterBox.TabIndex = 0;
            this.posterBox.OnPictureChanged += new System.EventHandler(this.posterBox_OnPictureChanged);
            this.posterBox.OnPictureDeleted += new System.EventHandler(this.posterBox_OnPictureDeleted);
            this.posterBox.OnCheckChanged += new System.EventHandler(this.posterBox_OnCheckChanged);
            // 
            // bd1Box
            // 
            this.bd1Box.AllowDrop = true;
            this.bd1Box.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.bd1Box.BackColor = System.Drawing.Color.Transparent;
            this.bd1Box.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.bd1Box.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.bd1Box.ImgPoster = null;
            this.bd1Box.IsChecked = false;
            this.bd1Box.IsSelected = false;
            this.bd1Box.Location = new System.Drawing.Point(639, 405);
            this.bd1Box.Name = "bd1Box";
            this.bd1Box.Size = new System.Drawing.Size(310, 187);
            this.bd1Box.TabIndex = 2;
            this.bd1Box.OnPictureChanged += new System.EventHandler(this.bd1Box_OnPictureChanged);
            this.bd1Box.OnPictureDeleted += new System.EventHandler(this.bd1Box_OnPictureDeleted);
            this.bd1Box.OnCheckChanged += new System.EventHandler(this.posterBox_OnCheckChanged);
            // 
            // InfosPanel
            // 
            this.InfosPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.InfosPanel.AutoScroll = true;
            this.InfosPanel.BackColor = System.Drawing.Color.Transparent;
            this.InfosPanel.Controls.Add(this.generalGroup);
            this.InfosPanel.Controls.Add(this.peopleGroup);
            this.InfosPanel.Controls.Add(this.overviewGroup);
            this.InfosPanel.Location = new System.Drawing.Point(16, 113);
            this.InfosPanel.Name = "InfosPanel";
            this.InfosPanel.Size = new System.Drawing.Size(573, 538);
            this.InfosPanel.TabIndex = 20;
            this.InfosPanel.Click += new System.EventHandler(this.DetailsPanel_Click);
            // 
            // generalGroup
            // 
            this.generalGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.generalGroup.BackColor = System.Drawing.Color.Transparent;
            this.generalGroup.Controls.Add(this.studiosBox);
            this.generalGroup.Controls.Add(this.genresBox);
            this.generalGroup.Controls.Add(this.runtimeBox);
            this.generalGroup.Controls.Add(this.RatingPanel);
            this.generalGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.generalGroup.Location = new System.Drawing.Point(7, 3);
            this.generalGroup.Name = "generalGroup";
            this.generalGroup.Size = new System.Drawing.Size(549, 159);
            this.generalGroup.TabIndex = 28;
            this.generalGroup.TabStop = false;
            // 
            // studiosBox
            // 
            this.studiosBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.studiosBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.studiosBox.Location = new System.Drawing.Point(3, 120);
            this.studiosBox.multiLine = false;
            this.studiosBox.Name = "studiosBox";
            this.studiosBox.Size = new System.Drawing.Size(540, 23);
            this.studiosBox.TabIndex = 28;
            this.studiosBox.text = null;
            this.studiosBox.OnTextChanged += new System.EventHandler(this.studiosBox_OnTextChanged);
            // 
            // genresBox
            // 
            this.genresBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.genresBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.genresBox.Location = new System.Drawing.Point(3, 88);
            this.genresBox.multiLine = false;
            this.genresBox.Name = "genresBox";
            this.genresBox.Size = new System.Drawing.Size(540, 23);
            this.genresBox.TabIndex = 27;
            this.genresBox.text = null;
            this.genresBox.OnTextChanged += new System.EventHandler(this.genresBox_OnTextChanged);
            // 
            // runtimeBox
            // 
            this.runtimeBox.decimals = 0;
            this.runtimeBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.runtimeBox.increment = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.runtimeBox.Location = new System.Drawing.Point(3, 57);
            this.runtimeBox.Name = "runtimeBox";
            this.runtimeBox.Size = new System.Drawing.Size(200, 23);
            this.runtimeBox.TabIndex = 26;
            this.runtimeBox.value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.runtimeBox.OnTextChanged += new System.EventHandler(this.runtimeBox_OnTextChanged);
            // 
            // RatingPanel
            // 
            this.RatingPanel.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.RatingPanel.BackColor = System.Drawing.Color.Transparent;
            this.RatingPanel.Controls.Add(this.ratingBox);
            this.RatingPanel.Controls.Add(this.Star5Picture);
            this.RatingPanel.Controls.Add(this.Star4Picture);
            this.RatingPanel.Controls.Add(this.Star3Picture);
            this.RatingPanel.Controls.Add(this.Star2Picture);
            this.RatingPanel.Controls.Add(this.Star1Picture);
            this.RatingPanel.Location = new System.Drawing.Point(3, 22);
            this.RatingPanel.Name = "RatingPanel";
            this.RatingPanel.Size = new System.Drawing.Size(303, 31);
            this.RatingPanel.TabIndex = 0;
            // 
            // ratingBox
            // 
            this.ratingBox.decimals = 1;
            this.ratingBox.increment = new decimal(new int[] {
            1,
            0,
            0,
            65536});
            this.ratingBox.Location = new System.Drawing.Point(211, 2);
            this.ratingBox.Name = "ratingBox";
            this.ratingBox.Size = new System.Drawing.Size(89, 27);
            this.ratingBox.TabIndex = 6;
            this.ratingBox.value = new decimal(new int[] {
            0,
            0,
            0,
            0});
            this.ratingBox.OnTextChanged += new System.EventHandler(this.ratingBox_OnTextChanged);
            // 
            // Star5Picture
            // 
            this.Star5Picture.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.Star5Picture.Location = new System.Drawing.Point(160, 0);
            this.Star5Picture.Name = "Star5Picture";
            this.Star5Picture.Size = new System.Drawing.Size(28, 29);
            this.Star5Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Star5Picture.TabIndex = 4;
            this.Star5Picture.TabStop = false;
            // 
            // Star4Picture
            // 
            this.Star4Picture.Location = new System.Drawing.Point(120, 0);
            this.Star4Picture.Name = "Star4Picture";
            this.Star4Picture.Size = new System.Drawing.Size(34, 29);
            this.Star4Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Star4Picture.TabIndex = 3;
            this.Star4Picture.TabStop = false;
            // 
            // Star3Picture
            // 
            this.Star3Picture.Location = new System.Drawing.Point(80, 0);
            this.Star3Picture.Name = "Star3Picture";
            this.Star3Picture.Size = new System.Drawing.Size(34, 29);
            this.Star3Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Star3Picture.TabIndex = 2;
            this.Star3Picture.TabStop = false;
            // 
            // Star2Picture
            // 
            this.Star2Picture.Location = new System.Drawing.Point(40, 0);
            this.Star2Picture.Name = "Star2Picture";
            this.Star2Picture.Size = new System.Drawing.Size(34, 29);
            this.Star2Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Star2Picture.TabIndex = 1;
            this.Star2Picture.TabStop = false;
            // 
            // Star1Picture
            // 
            this.Star1Picture.Location = new System.Drawing.Point(0, 0);
            this.Star1Picture.Name = "Star1Picture";
            this.Star1Picture.Size = new System.Drawing.Size(34, 29);
            this.Star1Picture.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.Star1Picture.TabIndex = 0;
            this.Star1Picture.TabStop = false;
            // 
            // peopleGroup
            // 
            this.peopleGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.peopleGroup.BackColor = System.Drawing.Color.Transparent;
            this.peopleGroup.Controls.Add(this.actorsBox);
            this.peopleGroup.Controls.Add(this.crewBox);
            this.peopleGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.peopleGroup.Location = new System.Drawing.Point(7, 191);
            this.peopleGroup.Name = "peopleGroup";
            this.peopleGroup.Size = new System.Drawing.Size(549, 156);
            this.peopleGroup.TabIndex = 28;
            this.peopleGroup.TabStop = false;
            // 
            // actorsBox
            // 
            this.actorsBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.actorsBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.actorsBox.Location = new System.Drawing.Point(3, 88);
            this.actorsBox.multiLine = true;
            this.actorsBox.Name = "actorsBox";
            this.actorsBox.Size = new System.Drawing.Size(540, 60);
            this.actorsBox.TabIndex = 1;
            this.actorsBox.text = null;
            this.actorsBox.OnTextChanged += new System.EventHandler(this.actorsBox_OnTextChanged);
            // 
            // crewBox
            // 
            this.crewBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.crewBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.crewBox.Location = new System.Drawing.Point(3, 22);
            this.crewBox.multiLine = true;
            this.crewBox.Name = "crewBox";
            this.crewBox.Size = new System.Drawing.Size(540, 60);
            this.crewBox.TabIndex = 0;
            this.crewBox.text = null;
            this.crewBox.OnTextChanged += new System.EventHandler(this.crewBox_OnTextChanged);
            // 
            // overviewGroup
            // 
            this.overviewGroup.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Left | System.Windows.Forms.AnchorStyles.Right)));
            this.overviewGroup.BackColor = System.Drawing.Color.Transparent;
            this.overviewGroup.Controls.Add(this.overviewBox);
            this.overviewGroup.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overviewGroup.Location = new System.Drawing.Point(7, 379);
            this.overviewGroup.Name = "overviewGroup";
            this.overviewGroup.Size = new System.Drawing.Size(549, 136);
            this.overviewGroup.TabIndex = 27;
            this.overviewGroup.TabStop = false;
            // 
            // overviewBox
            // 
            this.overviewBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.overviewBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.overviewBox.Location = new System.Drawing.Point(3, 22);
            this.overviewBox.multiLine = true;
            this.overviewBox.Name = "overviewBox";
            this.overviewBox.Size = new System.Drawing.Size(543, 111);
            this.overviewBox.TabIndex = 0;
            this.overviewBox.text = null;
            this.overviewBox.OnTextChanged += new System.EventHandler(this.overviewBox_OnTextChanged);
            // 
            // titleBox
            // 
            this.titleBox.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.titleBox.BackColor = System.Drawing.Color.Transparent;
            this.titleBox.Font = new System.Drawing.Font("Tahoma", 27.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.titleBox.Location = new System.Drawing.Point(15, 25);
            this.titleBox.multiLine = false;
            this.titleBox.Name = "titleBox";
            this.titleBox.Size = new System.Drawing.Size(591, 49);
            this.titleBox.TabIndex = 1;
            this.titleBox.text = null;
            this.titleBox.OnTextChanged += new System.EventHandler(this.titleBox_OnTextChanged);
            // 
            // OverviewPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.DetailsPanel);
            this.Name = "OverviewPanel";
            this.Size = new System.Drawing.Size(987, 805);
            this.DetailsPanel.ResumeLayout(false);
            this.InfosPanel.ResumeLayout(false);
            this.generalGroup.ResumeLayout(false);
            this.RatingPanel.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.Star5Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star4Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star3Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star2Picture)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Star1Picture)).EndInit();
            this.peopleGroup.ResumeLayout(false);
            this.overviewGroup.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel InfosPanel;
        private System.Windows.Forms.Panel RatingPanel;
        private System.Windows.Forms.PictureBox Star5Picture;
        private System.Windows.Forms.PictureBox Star4Picture;
        private System.Windows.Forms.PictureBox Star3Picture;
        private System.Windows.Forms.PictureBox Star2Picture;
        private System.Windows.Forms.PictureBox Star1Picture;
        private CustomControls.TextBoxControl titleBox;
        private CustomControls.DoubleBufferPanel DetailsPanel;
        private CustomControls.ImageBox posterBox;
        private CustomControls.ImageBox bd1Box;
        private CustomControls.ImageBox bd2Box;
        private CustomControls.ImageBox bannerBox;
        private System.Windows.Forms.GroupBox overviewGroup;
        private CustomControls.TextBoxControl overviewBox;
        private System.Windows.Forms.GroupBox generalGroup;
        private System.Windows.Forms.GroupBox peopleGroup;
        private CustomControls.NumericBoxControl runtimeBox;
        private CustomControls.NumericBoxControl ratingBox;
        private CustomControls.TextBoxControl genresBox;
        private CustomControls.TextBoxControl studiosBox;
        private CustomControls.TextBoxControl actorsBox;
        private CustomControls.TextBoxControl crewBox;


    }
}
