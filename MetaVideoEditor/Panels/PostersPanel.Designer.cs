namespace MetaVideoEditor
{
    partial class PostersPanel
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
            this.panel1 = new CustomControls.DoubleBufferPanel();
            this.imageBox1 = new CustomControls.ImageBox();
            this.picsPanel = new CustomControls.DoubleBufferPanel();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.imageBox1);
            this.panel1.Controls.Add(this.picsPanel);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(987, 805);
            this.panel1.TabIndex = 0;
            // 
            // imageBox1
            // 
            this.imageBox1.AllowDrop = true;
            this.imageBox1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)));
            this.imageBox1.BackColor = System.Drawing.Color.Transparent;
            this.imageBox1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.imageBox1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.imageBox1.ImgPoster = null;
            this.imageBox1.IsChecked = false;
            this.imageBox1.IsSelected = false;
            this.imageBox1.Location = new System.Drawing.Point(306, 15);
            this.imageBox1.Name = "imageBox1";
            this.imageBox1.Size = new System.Drawing.Size(383, 525);
            this.imageBox1.TabIndex = 1;
            this.imageBox1.OnPictureChanged += new System.EventHandler(this.imageBox1_OnPictureChanged);
            this.imageBox1.OnPictureDeleted += new System.EventHandler(this.imageBox1_OnPictureDeleted);
            this.imageBox1.OnCheckChanged += new System.EventHandler(this.OnCheckChanged);
            // 
            // picsPanel
            // 
            this.picsPanel.AutoScroll = true;
            this.picsPanel.BackColor = System.Drawing.Color.Transparent;
            this.picsPanel.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.picsPanel.Dock = System.Windows.Forms.DockStyle.Bottom;
            this.picsPanel.Location = new System.Drawing.Point(0, 559);
            this.picsPanel.Name = "picsPanel";
            this.picsPanel.Size = new System.Drawing.Size(987, 246);
            this.picsPanel.TabIndex = 2;
            // 
            // PostersPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "PostersPanel";
            this.Size = new System.Drawing.Size(987, 805);
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.DoubleBufferPanel panel1;
        private CustomControls.ImageBox imageBox1;
        private CustomControls.DoubleBufferPanel picsPanel;

    }
}
