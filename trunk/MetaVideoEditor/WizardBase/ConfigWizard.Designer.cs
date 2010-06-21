namespace MetaVideoEditor
{
    partial class ConfigWizard
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ConfigWizard));
            this.wizardControl1 = new WizardBase.WizardControl();
            this.startStep1 = new WizardBase.StartStep();
            this.intermediateStep1 = new WizardBase.IntermediateStep();
            this.useXBMCbox = new System.Windows.Forms.CheckBox();
            this.useDVDIDbox = new System.Windows.Forms.CheckBox();
            this.UseMBbox = new System.Windows.Forms.CheckBox();
            this.label1 = new System.Windows.Forms.Label();
            this.intermediateStep2 = new WizardBase.IntermediateStep();
            this.ImportButton = new System.Windows.Forms.Button();
            this.DeleteButton = new System.Windows.Forms.Button();
            this.AddButton = new System.Windows.Forms.Button();
            this.FolderList = new System.Windows.Forms.ListView();
            this.label2 = new System.Windows.Forms.Label();
            this.intermediateStep3 = new WizardBase.IntermediateStep();
            this.checkedListBox1 = new System.Windows.Forms.CheckedListBox();
            this.label3 = new System.Windows.Forms.Label();
            this.intermediateStep1.SuspendLayout();
            this.intermediateStep2.SuspendLayout();
            this.intermediateStep3.SuspendLayout();
            this.SuspendLayout();
            // 
            // wizardControl1
            // 
            this.wizardControl1.BackButtonEnabled = true;
            this.wizardControl1.BackButtonVisible = true;
            this.wizardControl1.CancelButtonEnabled = true;
            this.wizardControl1.CancelButtonVisible = true;
            this.wizardControl1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.wizardControl1.EulaButtonEnabled = true;
            this.wizardControl1.EulaButtonText = "eula";
            this.wizardControl1.EulaButtonVisible = false;
            this.wizardControl1.HelpButtonEnabled = true;
            this.wizardControl1.HelpButtonText = "";
            this.wizardControl1.HelpButtonVisible = false;
            this.wizardControl1.Location = new System.Drawing.Point(0, 0);
            this.wizardControl1.Name = "wizardControl1";
            this.wizardControl1.NextButtonEnabled = true;
            this.wizardControl1.NextButtonVisible = true;
            this.wizardControl1.Size = new System.Drawing.Size(573, 421);
            this.wizardControl1.WizardSteps.AddRange(new WizardBase.WizardStep[] {
            this.startStep1,
            this.intermediateStep1,
            this.intermediateStep2,
            this.intermediateStep3});
            this.wizardControl1.FinishButtonClick += new System.EventHandler(this.wizardControl1_FinishButtonClick);
            this.wizardControl1.CancelButtonClick += new System.EventHandler(this.wizardControl1_CancelButtonClick);
            // 
            // startStep1
            // 
            this.startStep1.BindingImage = null;
            this.startStep1.Icon = null;
            this.startStep1.Name = "startStep1";
            this.startStep1.Subtitle = "This wizard will help you to configure MetaVideoEditor for the first time";
            this.startStep1.Title = "Welcome to MetaVideoEditor";
            // 
            // intermediateStep1
            // 
            this.intermediateStep1.BindingImage = null;
            this.intermediateStep1.Controls.Add(this.useXBMCbox);
            this.intermediateStep1.Controls.Add(this.useDVDIDbox);
            this.intermediateStep1.Controls.Add(this.UseMBbox);
            this.intermediateStep1.Controls.Add(this.label1);
            this.intermediateStep1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep1.Name = "intermediateStep1";
            this.intermediateStep1.Subtitle = "";
            this.intermediateStep1.Title = "Media Center software";
            // 
            // useXBMCbox
            // 
            this.useXBMCbox.AutoSize = true;
            this.useXBMCbox.Location = new System.Drawing.Point(46, 170);
            this.useXBMCbox.Name = "useXBMCbox";
            this.useXBMCbox.Size = new System.Drawing.Size(56, 17);
            this.useXBMCbox.TabIndex = 3;
            this.useXBMCbox.Text = "XBMC";
            this.useXBMCbox.UseVisualStyleBackColor = true;
            // 
            // useDVDIDbox
            // 
            this.useDVDIDbox.AutoSize = true;
            this.useDVDIDbox.Location = new System.Drawing.Point(46, 147);
            this.useDVDIDbox.Name = "useDVDIDbox";
            this.useDVDIDbox.Size = new System.Drawing.Size(130, 17);
            this.useDVDIDbox.TabIndex = 2;
            this.useDVDIDbox.Text = "Windows DVD Library";
            this.useDVDIDbox.UseVisualStyleBackColor = true;
            // 
            // UseMBbox
            // 
            this.UseMBbox.AutoSize = true;
            this.UseMBbox.Location = new System.Drawing.Point(46, 124);
            this.UseMBbox.Name = "UseMBbox";
            this.UseMBbox.Size = new System.Drawing.Size(152, 17);
            this.UseMBbox.TabIndex = 1;
            this.UseMBbox.Text = "MediaBrowser / MyMovies";
            this.UseMBbox.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(26, 85);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(201, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "What media center software do you use?";
            // 
            // intermediateStep2
            // 
            this.intermediateStep2.BindingImage = null;
            this.intermediateStep2.Controls.Add(this.ImportButton);
            this.intermediateStep2.Controls.Add(this.DeleteButton);
            this.intermediateStep2.Controls.Add(this.AddButton);
            this.intermediateStep2.Controls.Add(this.FolderList);
            this.intermediateStep2.Controls.Add(this.label2);
            this.intermediateStep2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep2.Name = "intermediateStep2";
            this.intermediateStep2.Subtitle = "";
            this.intermediateStep2.Title = "Folders configuration";
            // 
            // ImportButton
            // 
            this.ImportButton.Location = new System.Drawing.Point(394, 283);
            this.ImportButton.Name = "ImportButton";
            this.ImportButton.Size = new System.Drawing.Size(155, 30);
            this.ImportButton.TabIndex = 4;
            this.ImportButton.Text = "MediaBrowser folders";
            this.ImportButton.UseVisualStyleBackColor = true;
            this.ImportButton.Click += new System.EventHandler(this.ImportButton_Click);
            // 
            // DeleteButton
            // 
            this.DeleteButton.Enabled = false;
            this.DeleteButton.Location = new System.Drawing.Point(211, 283);
            this.DeleteButton.Name = "DeleteButton";
            this.DeleteButton.Size = new System.Drawing.Size(155, 30);
            this.DeleteButton.TabIndex = 3;
            this.DeleteButton.Text = "Remove";
            this.DeleteButton.UseVisualStyleBackColor = true;
            this.DeleteButton.Click += new System.EventHandler(this.DeleteButton_Click);
            // 
            // AddButton
            // 
            this.AddButton.Location = new System.Drawing.Point(29, 283);
            this.AddButton.Name = "AddButton";
            this.AddButton.Size = new System.Drawing.Size(155, 30);
            this.AddButton.TabIndex = 2;
            this.AddButton.Text = "Add";
            this.AddButton.UseVisualStyleBackColor = true;
            this.AddButton.Click += new System.EventHandler(this.AddButton_Click);
            // 
            // FolderList
            // 
            this.FolderList.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.FolderList.Location = new System.Drawing.Point(29, 146);
            this.FolderList.Name = "FolderList";
            this.FolderList.Size = new System.Drawing.Size(520, 111);
            this.FolderList.TabIndex = 1;
            this.FolderList.UseCompatibleStateImageBehavior = false;
            this.FolderList.View = System.Windows.Forms.View.List;
            this.FolderList.SelectedIndexChanged += new System.EventHandler(this.FolderList_SelectedIndexChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(26, 85);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(196, 13);
            this.label2.TabIndex = 0;
            this.label2.Text = "Select folders containing your video files";
            // 
            // intermediateStep3
            // 
            this.intermediateStep3.BindingImage = null;
            this.intermediateStep3.Controls.Add(this.checkedListBox1);
            this.intermediateStep3.Controls.Add(this.label3);
            this.intermediateStep3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.intermediateStep3.Name = "intermediateStep3";
            this.intermediateStep3.Subtitle = "";
            this.intermediateStep3.Title = "Providers";
            // 
            // checkedListBox1
            // 
            this.checkedListBox1.BackColor = System.Drawing.SystemColors.Control;
            this.checkedListBox1.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.checkedListBox1.CheckOnClick = true;
            this.checkedListBox1.FormattingEnabled = true;
            this.checkedListBox1.Location = new System.Drawing.Point(46, 124);
            this.checkedListBox1.Name = "checkedListBox1";
            this.checkedListBox1.Size = new System.Drawing.Size(469, 225);
            this.checkedListBox1.TabIndex = 1;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(26, 85);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(179, 13);
            this.label3.TabIndex = 0;
            this.label3.Text = "Select the providers you want to use";
            // 
            // ConfigWizard
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(573, 421);
            this.Controls.Add(this.wizardControl1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ConfigWizard";
            this.intermediateStep1.ResumeLayout(false);
            this.intermediateStep1.PerformLayout();
            this.intermediateStep2.ResumeLayout(false);
            this.intermediateStep2.PerformLayout();
            this.intermediateStep3.ResumeLayout(false);
            this.intermediateStep3.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private global::WizardBase.WizardControl wizardControl1;
        private global::WizardBase.StartStep startStep1;
        private WizardBase.IntermediateStep intermediateStep1;
        private WizardBase.IntermediateStep intermediateStep2;
        private WizardBase.IntermediateStep intermediateStep3;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox useXBMCbox;
        private System.Windows.Forms.CheckBox useDVDIDbox;
        private System.Windows.Forms.CheckBox UseMBbox;
        private System.Windows.Forms.Button ImportButton;
        private System.Windows.Forms.Button DeleteButton;
        private System.Windows.Forms.Button AddButton;
        private System.Windows.Forms.ListView FolderList;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.CheckedListBox checkedListBox1;
        private System.Windows.Forms.Label label3;
    }
}