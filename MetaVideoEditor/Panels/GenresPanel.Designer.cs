namespace MetaVideoEditor
{
    partial class GenresPanel
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
            this.panel1 = new CustomControls.DoubleBufferPanel();
            this.panel2 = new CustomControls.DoubleBufferPanel();
            this.label2 = new System.Windows.Forms.Label();
            this.doubleBufferPanel1 = new CustomControls.DoubleBufferPanel();
            this.textBox = new CustomControls.AdjHeightTextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.panel1.SuspendLayout();
            this.panel2.SuspendLayout();
            this.doubleBufferPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel1
            // 
            this.panel1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.panel1.Controls.Add(this.panel2);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.panel1.Location = new System.Drawing.Point(0, 0);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(987, 805);
            this.panel1.TabIndex = 0;
            // 
            // panel2
            // 
            this.panel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.panel2.AutoScroll = true;
            this.panel2.BackColor = System.Drawing.Color.Transparent;
            this.panel2.Controls.Add(this.label2);
            this.panel2.Controls.Add(this.doubleBufferPanel1);
            this.panel2.Location = new System.Drawing.Point(14, 15);
            this.panel2.Name = "panel2";
            this.panel2.Size = new System.Drawing.Size(956, 777);
            this.panel2.TabIndex = 0;
            this.panel2.Click += new System.EventHandler(this.panel2_Click);
            this.panel2.Resize += new System.EventHandler(this.panel2_Resize);
            // 
            // label2
            // 
            this.label2.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label2.Location = new System.Drawing.Point(0, 0);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(956, 777);
            this.label2.TabIndex = 1;
            this.label2.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // doubleBufferPanel1
            // 
            this.doubleBufferPanel1.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.doubleBufferPanel1.Controls.Add(this.textBox);
            this.doubleBufferPanel1.Controls.Add(this.label1);
            this.doubleBufferPanel1.Location = new System.Drawing.Point(20, 10);
            this.doubleBufferPanel1.Name = "doubleBufferPanel1";
            this.doubleBufferPanel1.Size = new System.Drawing.Size(916, 30);
            this.doubleBufferPanel1.TabIndex = 0;
            this.doubleBufferPanel1.DoubleClick += new System.EventHandler(this.doubleBufferPanel1_DoubleClick);
            // 
            // textBox
            // 
            this.textBox.Dock = System.Windows.Forms.DockStyle.Fill;
            this.textBox.Font = new System.Drawing.Font("Microsoft Sans Serif", 18.55393F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Pixel);
            this.textBox.Location = new System.Drawing.Point(0, 0);
            this.textBox.Name = "textBox";
            this.textBox.Size = new System.Drawing.Size(914, 28);
            this.textBox.Size_AdjustableHeight = 28;
            this.textBox.TabIndex = 1;
            this.textBox.Visible = false;
            this.textBox.Leave += new System.EventHandler(this.textBox_Leave);
            this.textBox.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.textBox_KeyPress);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 11.25F, System.Drawing.FontStyle.Italic, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.ForeColor = System.Drawing.Color.Black;
            this.label1.Location = new System.Drawing.Point(80, 5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(0, 18);
            this.label1.TabIndex = 0;
            this.label1.DoubleClick += new System.EventHandler(this.label1_DoubleClick);
            // 
            // GenresPanel
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel1);
            this.Name = "GenresPanel";
            this.Size = new System.Drawing.Size(987, 805);
            this.panel1.ResumeLayout(false);
            this.panel2.ResumeLayout(false);
            this.doubleBufferPanel1.ResumeLayout(false);
            this.doubleBufferPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private CustomControls.DoubleBufferPanel panel2;
        private CustomControls.DoubleBufferPanel doubleBufferPanel1;
        private System.Windows.Forms.Label label1;
        private CustomControls.AdjHeightTextBox textBox;
        private System.Windows.Forms.Label label2;
        private CustomControls.DoubleBufferPanel panel1;

    }
}
