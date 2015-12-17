namespace SmallBox_Client
{
    partial class Form1
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

        #region Code généré par le Concepteur Windows Form

        /// <summary>
        /// Méthode requise pour la prise en charge du concepteur - ne modifiez pas
        /// le contenu de cette méthode avec l'éditeur de code.
        /// </summary>
        private void InitializeComponent()
        {
            this.uploadFile = new System.Windows.Forms.GroupBox();
            this.ZipAndUpload_progressBar = new System.Windows.Forms.ProgressBar();
            this.CompressAndUpload_btn = new System.Windows.Forms.Button();
            this.folderPath_txtbx = new System.Windows.Forms.TextBox();
            this.selectDirectory_btn = new System.Windows.Forms.Button();
            this.upload_progressBar = new System.Windows.Forms.ProgressBar();
            this.upload_btn = new System.Windows.Forms.Button();
            this.fileToUpload_txtbx = new System.Windows.Forms.TextBox();
            this.selectFile = new System.Windows.Forms.Button();
            this.uploadThisFile = new System.Windows.Forms.OpenFileDialog();
            this.listGroup = new System.Windows.Forms.GroupBox();
            this.listBlobs = new System.Windows.Forms.ListBox();
            this.getList_btn = new System.Windows.Forms.Button();
            this.containerDir = new System.Windows.Forms.TextBox();
            this.containerDirectory = new System.Windows.Forms.Label();
            this.Download_GroupBox = new System.Windows.Forms.GroupBox();
            this.flowLayoutFilesItems = new System.Windows.Forms.FlowLayoutPanel();
            this.download_progressBar = new System.Windows.Forms.ProgressBar();
            this.selectFolderDialog = new System.Windows.Forms.FolderBrowserDialog();
            this.uploadFile.SuspendLayout();
            this.listGroup.SuspendLayout();
            this.Download_GroupBox.SuspendLayout();
            this.SuspendLayout();
            // 
            // uploadFile
            // 
            this.uploadFile.Controls.Add(this.ZipAndUpload_progressBar);
            this.uploadFile.Controls.Add(this.CompressAndUpload_btn);
            this.uploadFile.Controls.Add(this.folderPath_txtbx);
            this.uploadFile.Controls.Add(this.selectDirectory_btn);
            this.uploadFile.Controls.Add(this.upload_progressBar);
            this.uploadFile.Controls.Add(this.upload_btn);
            this.uploadFile.Controls.Add(this.fileToUpload_txtbx);
            this.uploadFile.Controls.Add(this.selectFile);
            this.uploadFile.Location = new System.Drawing.Point(532, 12);
            this.uploadFile.Name = "uploadFile";
            this.uploadFile.Size = new System.Drawing.Size(573, 265);
            this.uploadFile.TabIndex = 0;
            this.uploadFile.TabStop = false;
            this.uploadFile.Text = "Uploader un fichier";
            // 
            // ZipAndUpload_progressBar
            // 
            this.ZipAndUpload_progressBar.Location = new System.Drawing.Point(7, 224);
            this.ZipAndUpload_progressBar.Name = "ZipAndUpload_progressBar";
            this.ZipAndUpload_progressBar.Size = new System.Drawing.Size(560, 30);
            this.ZipAndUpload_progressBar.TabIndex = 10;
            // 
            // CompressAndUpload_btn
            // 
            this.CompressAndUpload_btn.Enabled = false;
            this.CompressAndUpload_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CompressAndUpload_btn.Location = new System.Drawing.Point(148, 174);
            this.CompressAndUpload_btn.Name = "CompressAndUpload_btn";
            this.CompressAndUpload_btn.Size = new System.Drawing.Size(419, 44);
            this.CompressAndUpload_btn.TabIndex = 9;
            this.CompressAndUpload_btn.Text = "Compress and Upload in archives !";
            this.CompressAndUpload_btn.UseVisualStyleBackColor = true;
            this.CompressAndUpload_btn.Click += new System.EventHandler(this.CompressAndUpload_btn_Click);
            // 
            // folderPath_txtbx
            // 
            this.folderPath_txtbx.Enabled = false;
            this.folderPath_txtbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.folderPath_txtbx.Location = new System.Drawing.Point(148, 145);
            this.folderPath_txtbx.Name = "folderPath_txtbx";
            this.folderPath_txtbx.ReadOnly = true;
            this.folderPath_txtbx.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.folderPath_txtbx.Size = new System.Drawing.Size(419, 23);
            this.folderPath_txtbx.TabIndex = 8;
            this.folderPath_txtbx.Visible = false;
            // 
            // selectDirectory_btn
            // 
            this.selectDirectory_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectDirectory_btn.Location = new System.Drawing.Point(7, 143);
            this.selectDirectory_btn.Name = "selectDirectory_btn";
            this.selectDirectory_btn.Size = new System.Drawing.Size(136, 75);
            this.selectDirectory_btn.TabIndex = 7;
            this.selectDirectory_btn.Text = "Select a folder";
            this.selectDirectory_btn.UseVisualStyleBackColor = true;
            this.selectDirectory_btn.Click += new System.EventHandler(this.selectDirectory_btn_Click);
            // 
            // upload_progressBar
            // 
            this.upload_progressBar.Location = new System.Drawing.Point(7, 109);
            this.upload_progressBar.Name = "upload_progressBar";
            this.upload_progressBar.Size = new System.Drawing.Size(560, 30);
            this.upload_progressBar.TabIndex = 6;
            // 
            // upload_btn
            // 
            this.upload_btn.Enabled = false;
            this.upload_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.upload_btn.Location = new System.Drawing.Point(148, 59);
            this.upload_btn.Name = "upload_btn";
            this.upload_btn.Size = new System.Drawing.Size(419, 44);
            this.upload_btn.TabIndex = 5;
            this.upload_btn.Text = "Upload !";
            this.upload_btn.UseVisualStyleBackColor = true;
            this.upload_btn.Click += new System.EventHandler(this.upload_btn_Click);
            // 
            // fileToUpload_txtbx
            // 
            this.fileToUpload_txtbx.Enabled = false;
            this.fileToUpload_txtbx.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.fileToUpload_txtbx.Location = new System.Drawing.Point(148, 30);
            this.fileToUpload_txtbx.Name = "fileToUpload_txtbx";
            this.fileToUpload_txtbx.ReadOnly = true;
            this.fileToUpload_txtbx.ScrollBars = System.Windows.Forms.ScrollBars.Horizontal;
            this.fileToUpload_txtbx.Size = new System.Drawing.Size(419, 23);
            this.fileToUpload_txtbx.TabIndex = 4;
            this.fileToUpload_txtbx.Visible = false;
            // 
            // selectFile
            // 
            this.selectFile.Font = new System.Drawing.Font("Microsoft Sans Serif", 10.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.selectFile.Location = new System.Drawing.Point(6, 27);
            this.selectFile.Name = "selectFile";
            this.selectFile.Size = new System.Drawing.Size(136, 76);
            this.selectFile.TabIndex = 3;
            this.selectFile.Text = "Select a File";
            this.selectFile.UseVisualStyleBackColor = true;
            this.selectFile.Click += new System.EventHandler(this.selectFile_Click);
            // 
            // listGroup
            // 
            this.listGroup.Controls.Add(this.listBlobs);
            this.listGroup.Controls.Add(this.getList_btn);
            this.listGroup.Controls.Add(this.containerDir);
            this.listGroup.Controls.Add(this.containerDirectory);
            this.listGroup.Location = new System.Drawing.Point(12, 12);
            this.listGroup.Name = "listGroup";
            this.listGroup.Size = new System.Drawing.Size(514, 265);
            this.listGroup.TabIndex = 1;
            this.listGroup.TabStop = false;
            this.listGroup.Text = "Lister les objets du container";
            // 
            // listBlobs
            // 
            this.listBlobs.Font = new System.Drawing.Font("Palatino Linotype", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.listBlobs.FormattingEnabled = true;
            this.listBlobs.ItemHeight = 22;
            this.listBlobs.Location = new System.Drawing.Point(9, 143);
            this.listBlobs.Name = "listBlobs";
            this.listBlobs.Size = new System.Drawing.Size(487, 92);
            this.listBlobs.TabIndex = 5;
            this.listBlobs.SelectedIndexChanged += new System.EventHandler(this.ListFilesInDir_selectIndexChanged);
            // 
            // getList_btn
            // 
            this.getList_btn.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.getList_btn.Location = new System.Drawing.Point(9, 74);
            this.getList_btn.Name = "getList_btn";
            this.getList_btn.Size = new System.Drawing.Size(487, 47);
            this.getList_btn.TabIndex = 4;
            this.getList_btn.Text = "Tap to Get the root list !";
            this.getList_btn.UseVisualStyleBackColor = true;
            this.getList_btn.Click += new System.EventHandler(this.getList_btn_Click);
            // 
            // containerDir
            // 
            this.containerDir.Enabled = false;
            this.containerDir.Font = new System.Drawing.Font("Microsoft Sans Serif", 12F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerDir.Location = new System.Drawing.Point(107, 33);
            this.containerDir.Name = "containerDir";
            this.containerDir.Size = new System.Drawing.Size(389, 26);
            this.containerDir.TabIndex = 3;
            // 
            // containerDirectory
            // 
            this.containerDirectory.AutoSize = true;
            this.containerDirectory.Font = new System.Drawing.Font("Microsoft Sans Serif", 14.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.containerDirectory.Location = new System.Drawing.Point(6, 33);
            this.containerDirectory.Name = "containerDirectory";
            this.containerDirectory.Size = new System.Drawing.Size(101, 24);
            this.containerDirectory.TabIndex = 0;
            this.containerDirectory.Text = "Container :";
            // 
            // Download_GroupBox
            // 
            this.Download_GroupBox.Controls.Add(this.flowLayoutFilesItems);
            this.Download_GroupBox.Controls.Add(this.download_progressBar);
            this.Download_GroupBox.Location = new System.Drawing.Point(12, 283);
            this.Download_GroupBox.Name = "Download_GroupBox";
            this.Download_GroupBox.Size = new System.Drawing.Size(1093, 264);
            this.Download_GroupBox.TabIndex = 2;
            this.Download_GroupBox.TabStop = false;
            this.Download_GroupBox.Text = "Downloader un fichier";
            // 
            // flowLayoutFilesItems
            // 
            this.flowLayoutFilesItems.AutoScroll = true;
            this.flowLayoutFilesItems.BackColor = System.Drawing.SystemColors.InactiveCaption;
            this.flowLayoutFilesItems.Location = new System.Drawing.Point(7, 26);
            this.flowLayoutFilesItems.Name = "flowLayoutFilesItems";
            this.flowLayoutFilesItems.Size = new System.Drawing.Size(1081, 188);
            this.flowLayoutFilesItems.TabIndex = 7;
            // 
            // download_progressBar
            // 
            this.download_progressBar.Location = new System.Drawing.Point(9, 228);
            this.download_progressBar.Name = "download_progressBar";
            this.download_progressBar.Size = new System.Drawing.Size(1078, 30);
            this.download_progressBar.TabIndex = 6;
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1117, 559);
            this.Controls.Add(this.Download_GroupBox);
            this.Controls.Add(this.listGroup);
            this.Controls.Add(this.uploadFile);
            this.MinimumSize = new System.Drawing.Size(1080, 320);
            this.Name = "Form1";
            this.Text = "SmallBox Storage v1.0";
            this.Load += new System.EventHandler(this.form1_OnLoad);
            this.Shown += new System.EventHandler(this.form1_LoadCompleted);
            this.uploadFile.ResumeLayout(false);
            this.uploadFile.PerformLayout();
            this.listGroup.ResumeLayout(false);
            this.listGroup.PerformLayout();
            this.Download_GroupBox.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.GroupBox uploadFile;
        private System.Windows.Forms.OpenFileDialog uploadThisFile;
        private System.Windows.Forms.GroupBox listGroup;
        private System.Windows.Forms.TextBox containerDir;
        private System.Windows.Forms.Label containerDirectory;
        private System.Windows.Forms.ListBox listBlobs;
        private System.Windows.Forms.Button getList_btn;
        private System.Windows.Forms.Button selectFile;
        private System.Windows.Forms.TextBox fileToUpload_txtbx;
        private System.Windows.Forms.Button upload_btn;
        private System.Windows.Forms.GroupBox Download_GroupBox;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutFilesItems;
        private System.Windows.Forms.ProgressBar download_progressBar;
        private System.Windows.Forms.ProgressBar upload_progressBar;
        private System.Windows.Forms.TextBox folderPath_txtbx;
        private System.Windows.Forms.Button selectDirectory_btn;
        private System.Windows.Forms.FolderBrowserDialog selectFolderDialog;
        private System.Windows.Forms.ProgressBar ZipAndUpload_progressBar;
        private System.Windows.Forms.Button CompressAndUpload_btn;
    }
}

