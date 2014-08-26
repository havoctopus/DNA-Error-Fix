namespace DNA_Error_Fix_UI
{
    partial class DNAErrorFixFrm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DNAErrorFixFrm));
            this.openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
            this.bwMasterLoad = new System.ComponentModel.BackgroundWorker();
            this.bwFix = new System.ComponentModel.BackgroundWorker();
            this.statusLbl = new System.Windows.Forms.ToolStripStatusLabel();
            this.statusStrip1 = new System.Windows.Forms.StatusStrip();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.selectAutosomalFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.clearSelectionToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.fixErrorsAndNoCallsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveConsolidatedFileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveReplacedReportToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveFixedFilesToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.quickTutorialToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.websiteToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.licenseToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.aboutToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.output = new System.Windows.Forms.TextBox();
            this.folderBrowserDialog1 = new System.Windows.Forms.FolderBrowserDialog();
            this.saveFileDialog1 = new System.Windows.Forms.SaveFileDialog();
            this.bwSaveConsolidated = new System.ComponentModel.BackgroundWorker();
            this.genderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.maleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.femaleToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.statusStrip1.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // openFileDialog1
            // 
            this.openFileDialog1.Multiselect = true;
            this.openFileDialog1.ShowReadOnly = true;
            this.openFileDialog1.SupportMultiDottedExtensions = true;
            // 
            // bwMasterLoad
            // 
            this.bwMasterLoad.WorkerReportsProgress = true;
            this.bwMasterLoad.WorkerSupportsCancellation = true;
            this.bwMasterLoad.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwMasterLoad_DoWork);
            this.bwMasterLoad.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwMasterLoad_ProgressChanged);
            this.bwMasterLoad.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwMasterLoad_RunWorkerCompleted);
            // 
            // bwFix
            // 
            this.bwFix.WorkerReportsProgress = true;
            this.bwFix.WorkerSupportsCancellation = true;
            this.bwFix.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwFix_DoWork);
            this.bwFix.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.bwFix_ProgressChanged);
            this.bwFix.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwFix_RunWorkerCompleted);
            // 
            // statusLbl
            // 
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(38, 17);
            this.statusLbl.Text = "Done.";
            // 
            // statusStrip1
            // 
            this.statusStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.statusLbl});
            this.statusStrip1.Location = new System.Drawing.Point(0, 340);
            this.statusStrip1.Name = "statusStrip1";
            this.statusStrip1.Size = new System.Drawing.Size(584, 22);
            this.statusStrip1.TabIndex = 1;
            this.statusStrip1.Text = "statusStrip1";
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fileToolStripMenuItem,
            this.genderToolStripMenuItem,
            this.toolsToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Padding = new System.Windows.Forms.Padding(4, 2, 0, 2);
            this.menuStrip1.Size = new System.Drawing.Size(584, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.selectAutosomalFilesToolStripMenuItem,
            this.clearSelectionToolStripMenuItem,
            this.toolStripSeparator1,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(37, 20);
            this.fileToolStripMenuItem.Text = "&File";
            // 
            // selectAutosomalFilesToolStripMenuItem
            // 
            this.selectAutosomalFilesToolStripMenuItem.Name = "selectAutosomalFilesToolStripMenuItem";
            this.selectAutosomalFilesToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.selectAutosomalFilesToolStripMenuItem.Text = "Select &Autosomal  Files ...";
            this.selectAutosomalFilesToolStripMenuItem.Click += new System.EventHandler(this.selectAutosomalFilesToolStripMenuItem_Click);
            // 
            // clearSelectionToolStripMenuItem
            // 
            this.clearSelectionToolStripMenuItem.Enabled = false;
            this.clearSelectionToolStripMenuItem.Name = "clearSelectionToolStripMenuItem";
            this.clearSelectionToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.clearSelectionToolStripMenuItem.Text = "&Clear Selection / Reset";
            this.clearSelectionToolStripMenuItem.Click += new System.EventHandler(this.clearSelectionToolStripMenuItem_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(204, 6);
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(207, 22);
            this.exitToolStripMenuItem.Text = "E&xit";
            this.exitToolStripMenuItem.Click += new System.EventHandler(this.exitToolStripMenuItem_Click);
            // 
            // toolsToolStripMenuItem
            // 
            this.toolsToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.fixErrorsAndNoCallsToolStripMenuItem,
            this.saveConsolidatedFileToolStripMenuItem,
            this.saveReplacedReportToolStripMenuItem,
            this.saveFixedFilesToolStripMenuItem});
            this.toolsToolStripMenuItem.Name = "toolsToolStripMenuItem";
            this.toolsToolStripMenuItem.Size = new System.Drawing.Size(48, 20);
            this.toolsToolStripMenuItem.Text = "&Tools";
            // 
            // fixErrorsAndNoCallsToolStripMenuItem
            // 
            this.fixErrorsAndNoCallsToolStripMenuItem.Enabled = false;
            this.fixErrorsAndNoCallsToolStripMenuItem.Name = "fixErrorsAndNoCallsToolStripMenuItem";
            this.fixErrorsAndNoCallsToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.fixErrorsAndNoCallsToolStripMenuItem.Text = "&Fix Errors and No-Calls";
            this.fixErrorsAndNoCallsToolStripMenuItem.Click += new System.EventHandler(this.fixErrorsAndNoCallsToolStripMenuItem_Click);
            // 
            // saveConsolidatedFileToolStripMenuItem
            // 
            this.saveConsolidatedFileToolStripMenuItem.Enabled = false;
            this.saveConsolidatedFileToolStripMenuItem.Name = "saveConsolidatedFileToolStripMenuItem";
            this.saveConsolidatedFileToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveConsolidatedFileToolStripMenuItem.Text = "&Save Consolidated File";
            this.saveConsolidatedFileToolStripMenuItem.Click += new System.EventHandler(this.saveConsolidatedFileToolStripMenuItem_Click);
            // 
            // saveReplacedReportToolStripMenuItem
            // 
            this.saveReplacedReportToolStripMenuItem.Enabled = false;
            this.saveReplacedReportToolStripMenuItem.Name = "saveReplacedReportToolStripMenuItem";
            this.saveReplacedReportToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveReplacedReportToolStripMenuItem.Text = "Save &Replaced Report";
            this.saveReplacedReportToolStripMenuItem.Click += new System.EventHandler(this.saveReplacedReportToolStripMenuItem_Click);
            // 
            // saveFixedFilesToolStripMenuItem
            // 
            this.saveFixedFilesToolStripMenuItem.Enabled = false;
            this.saveFixedFilesToolStripMenuItem.Name = "saveFixedFilesToolStripMenuItem";
            this.saveFixedFilesToolStripMenuItem.Size = new System.Drawing.Size(193, 22);
            this.saveFixedFilesToolStripMenuItem.Text = "Save &Fixed Files";
            this.saveFixedFilesToolStripMenuItem.Click += new System.EventHandler(this.saveFixedFilesToolStripMenuItem_Click);
            // 
            // helpToolStripMenuItem
            // 
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.quickTutorialToolStripMenuItem,
            this.websiteToolStripMenuItem,
            this.licenseToolStripMenuItem,
            this.aboutToolStripMenuItem});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            this.helpToolStripMenuItem.Size = new System.Drawing.Size(44, 20);
            this.helpToolStripMenuItem.Text = "&Help";
            // 
            // quickTutorialToolStripMenuItem
            // 
            this.quickTutorialToolStripMenuItem.Name = "quickTutorialToolStripMenuItem";
            this.quickTutorialToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.quickTutorialToolStripMenuItem.Text = "&Quick Tutorial";
            this.quickTutorialToolStripMenuItem.Click += new System.EventHandler(this.quickTutorialToolStripMenuItem_Click);
            // 
            // websiteToolStripMenuItem
            // 
            this.websiteToolStripMenuItem.Name = "websiteToolStripMenuItem";
            this.websiteToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.websiteToolStripMenuItem.Text = "&Website";
            this.websiteToolStripMenuItem.Click += new System.EventHandler(this.websiteToolStripMenuItem_Click);
            // 
            // licenseToolStripMenuItem
            // 
            this.licenseToolStripMenuItem.Name = "licenseToolStripMenuItem";
            this.licenseToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.licenseToolStripMenuItem.Text = "&License";
            this.licenseToolStripMenuItem.Click += new System.EventHandler(this.licenseToolStripMenuItem_Click);
            // 
            // aboutToolStripMenuItem
            // 
            this.aboutToolStripMenuItem.Name = "aboutToolStripMenuItem";
            this.aboutToolStripMenuItem.Size = new System.Drawing.Size(149, 22);
            this.aboutToolStripMenuItem.Text = "&About";
            this.aboutToolStripMenuItem.Click += new System.EventHandler(this.aboutToolStripMenuItem_Click);
            // 
            // output
            // 
            this.output.AllowDrop = true;
            this.output.Dock = System.Windows.Forms.DockStyle.Fill;
            this.output.Font = new System.Drawing.Font("Courier New", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.output.Location = new System.Drawing.Point(0, 24);
            this.output.Multiline = true;
            this.output.Name = "output";
            this.output.ReadOnly = true;
            this.output.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.output.Size = new System.Drawing.Size(584, 316);
            this.output.TabIndex = 3;
            this.output.DragDrop += new System.Windows.Forms.DragEventHandler(this.output_DragDrop);
            this.output.DragEnter += new System.Windows.Forms.DragEventHandler(this.output_DragEnter);
            // 
            // bwSaveConsolidated
            // 
            this.bwSaveConsolidated.WorkerSupportsCancellation = true;
            this.bwSaveConsolidated.DoWork += new System.ComponentModel.DoWorkEventHandler(this.bwSaveConsolidated_DoWork);
            this.bwSaveConsolidated.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.bwSaveConsolidated_RunWorkerCompleted);
            // 
            // genderToolStripMenuItem
            // 
            this.genderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.maleToolStripMenuItem,
            this.femaleToolStripMenuItem});
            this.genderToolStripMenuItem.Name = "genderToolStripMenuItem";
            this.genderToolStripMenuItem.Size = new System.Drawing.Size(57, 20);
            this.genderToolStripMenuItem.Text = "&Gender";
            // 
            // maleToolStripMenuItem
            // 
            this.maleToolStripMenuItem.CheckOnClick = true;
            this.maleToolStripMenuItem.Name = "maleToolStripMenuItem";
            this.maleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.maleToolStripMenuItem.Text = "&Male";
            this.maleToolStripMenuItem.Click += new System.EventHandler(this.maleToolStripMenuItem_Click);
            // 
            // femaleToolStripMenuItem
            // 
            this.femaleToolStripMenuItem.Checked = true;
            this.femaleToolStripMenuItem.CheckOnClick = true;
            this.femaleToolStripMenuItem.CheckState = System.Windows.Forms.CheckState.Checked;
            this.femaleToolStripMenuItem.Name = "femaleToolStripMenuItem";
            this.femaleToolStripMenuItem.Size = new System.Drawing.Size(152, 22);
            this.femaleToolStripMenuItem.Text = "&Female";
            this.femaleToolStripMenuItem.Click += new System.EventHandler(this.femaleToolStripMenuItem_Click);
            // 
            // DNAErrorFixFrm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(584, 362);
            this.Controls.Add(this.output);
            this.Controls.Add(this.statusStrip1);
            this.Controls.Add(this.menuStrip1);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "DNAErrorFixFrm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "DNA Error Fix";
            this.Load += new System.EventHandler(this.DNAErrorFixFrm_Load);
            this.statusStrip1.ResumeLayout(false);
            this.statusStrip1.PerformLayout();
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.OpenFileDialog openFileDialog1;
        private System.ComponentModel.BackgroundWorker bwMasterLoad;
        private System.ComponentModel.BackgroundWorker bwFix;
        private System.Windows.Forms.ToolStripStatusLabel statusLbl;
        private System.Windows.Forms.StatusStrip statusStrip1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem fileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem selectAutosomalFilesToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem clearSelectionToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem toolsToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem aboutToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem websiteToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem fixErrorsAndNoCallsToolStripMenuItem;
        private System.Windows.Forms.TextBox output;
        private System.Windows.Forms.ToolStripMenuItem licenseToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem quickTutorialToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveConsolidatedFileToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveReplacedReportToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem saveFixedFilesToolStripMenuItem;
        private System.Windows.Forms.FolderBrowserDialog folderBrowserDialog1;
        private System.Windows.Forms.SaveFileDialog saveFileDialog1;
        private System.ComponentModel.BackgroundWorker bwSaveConsolidated;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem exitToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem genderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem maleToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem femaleToolStripMenuItem;
    }
}

