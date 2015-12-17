using System;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Windows.Forms;

namespace SmallBox_Client
{
    public partial class Form1 : Form
    {
        ServiceCloudReference.ServiceCloudClient client;
        private string folderName;

        public Form1()
        {
            InitializeComponent();
        }

        private void form1_OnLoad(object sender, EventArgs e)
        {
            // Init the service reference from client on client load !
            client = new ServiceCloudReference.ServiceCloudClient();
        }
        private void form1_LoadCompleted(object sender, EventArgs e)
        {
            // Avoid letting users upload in root dir (container) !
            selectFile.Enabled = false;
            upload_btn.Enabled = false;
        }

        // Get the root list directories
        private void getList_btn_Click(object sender, EventArgs e)
        {
            upload_progressBar.Value = 0;
            ZipAndUpload_progressBar.Value = 0;
            download_progressBar.Value = 0;
            // GET the container name
            string dirName = client.GetContainerUri();
            containerDir.Text = dirName;

            // Reset items in listbox
            listBlobs.Items.Clear();
            listBlobs.Enabled = true;

            // Call the service method to display directories
            var listBlobsInContainer = client.GetDirectories();

            foreach (var blob in listBlobsInContainer)
            {
                // List AbsolutePath of blobs
                listBlobs.Items.Add(blob.Name);
            }

            // Avoid letting users upload here !
            selectFile.Enabled = false;
            upload_btn.Enabled = false;
            flowLayoutFilesItems.Controls.Clear();
        }

        // What Happen when you click on a directory in the listbox !
        private void ListFilesInDir_selectIndexChanged(object sender, EventArgs e)
        {
            // Set blobRef on item selected from listblox
            string blobRef = listBlobs.SelectedItem.ToString();

            // String separator to split the string and retrieve result we wanted
            string[] stringSeparators = new string[] { "/" };
            string[] result;
            // Split a string delimited by another string and return all elements.
            result = blobRef.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
            // Get the string index 2 -> Blob Directory under smallboxcontainer
            blobRef = result[2];
            if (blobRef != "Archives" && blobRef != "Backups")
            {
                // Let users upload here
                selectFile.Enabled = true;
            }

            // Add the directory name to the container text box
            containerDir.Text += "/" + blobRef;

            // Call the service method to display directories
            var listBlobsInDirectory = client.ListFilesInDir(blobRef);

            // Reset items in listbox
            int i = 0;
            listBlobs.Items.Clear();
            foreach (var blob in listBlobsInDirectory)
            {
                // List AbsolutePath of blobs
                listBlobs.Items.Add(blob.Name);

                ///<summary>
                ///     For Each items
                ///     Add a button in FlowLayout
                ///     To download the target file
                /// </summary> 
                Button button = new Button();
                button.Tag = i;
                button.Text = blob.Uri;
                button.Height = 90;
                button.Width = 145;
                button.Click += DownLoad_This_File_Click;
                button.Font = new Font("Arial", 9.5f);
                flowLayoutFilesItems.Controls.Add(button);
                // Increment i to increase tag for each file
                i++;
            }
            // Empeche la selection 
            // listBlobs.SelectionMode = SelectionMode.None; // Génère une exception si on clique dans la listbox après...
            listBlobs.Enabled = false;
        }

        // What happen when you click "Select a file" button
        private void selectFile_Click(object sender, EventArgs e)
        {
            upload_progressBar.Value = 0;

            // Show the dialog to select a file and put the name in textbox
            using (var selectFileDialog = new OpenFileDialog())
            {
                // If Dialog still open
                if (selectFileDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the filename selected
                    uploadThisFile.FileName = selectFileDialog.FileName;

                    //Make textbox visible with filename
                    fileToUpload_txtbx.Visible = true;
                    fileToUpload_txtbx.Text = selectFileDialog.FileName;

                    upload_btn.Enabled = true;
                }
            }
        }

        // Delete temporary Directory recursivly after unzip localy
        public static void DeleteDirectory(string target_dir)
        {
            string[] files = Directory.GetFiles(target_dir);
            string[] dirs = Directory.GetDirectories(target_dir);
            // Delete all files
            foreach (string file in files)
            {
                File.SetAttributes(file, FileAttributes.Normal);
                File.Delete(file);
            }
            // Delete all sub directories
            foreach (string dir in dirs)
            {
                DeleteDirectory(dir);
            }
            Directory.Delete(target_dir, false);
        }

        private void selectDirectory_btn_Click(object sender, EventArgs e)
        {
            ZipAndUpload_progressBar.Value = 0;

            // Show the dialog to select a folder and put the name in textbox
            using (var selectFolderDialog = new FolderBrowserDialog())
            {
                // If Dialog still open
                if (selectFolderDialog.ShowDialog() == DialogResult.OK)
                {
                    // Get the folder name selected
                    folderName = selectFolderDialog.SelectedPath;

                    //Path the folderPath in the textbox
                    folderPath_txtbx.Visible = true;
                    folderPath_txtbx.Text = folderName;
                    // Enable compress & upload button
                    CompressAndUpload_btn.Enabled = true;
                }
            }
        }


        // What Happen when you click on upload btn !
        // If we attempt to upload a Zip, we will decompress it before uploading
        private void upload_btn_Click(object sender, EventArgs e)
        {
            // Set progressBar Value to 0
            upload_progressBar.Value = 0;

            // File Name
            string fileName = fileToUpload_txtbx.Text;

            // Store reference to get the directory only
            string blobDirRef = listBlobs.Items[0].ToString();

            // Split blobReference after each character defined
            string[] stringSeparatorsDir = new string[] { "/" };

            // Store resulte in array to get what we want with index
            string[] resultDir;

            // Split a string delimited by char and return NON EMPTY results
            resultDir = blobDirRef.Split(stringSeparatorsDir, StringSplitOptions.RemoveEmptyEntries);

            // Get the string index 2 -> The directory name
            blobDirRef = resultDir[2];

            ///
            /// UNZIP A FILE THEN UPLOAD ALL OF THEM IN CLOUD SERVICE
            ///
            #region UnZip file localy if necessary on the computer, Then call the service Upload Method
            ///<summary>
            /// 1 - Is it a Zip File ?
            ///     - If yes : Unzip in a folder to the desktop user, Then Upload, Finally Delete local temp folder
            ///     - If no  : Call our traditional method to upload from stream
            ///</summary>
            if (fileName.EndsWith(".zip"))
            {
                //Folder with archive name without extension
                string newFolder = Path.GetFileNameWithoutExtension(fileName);

                // Get Temp/Desktop directory + archive name
                // string tempExtractPath = Path.GetTempPath() + newFolder;
                string tempExtractPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), newFolder);

                // Determine whether the directory exists.
                if (!Directory.Exists(@tempExtractPath))
                {
                    // Try to create the directory by archive name
                    Directory.CreateDirectory(@tempExtractPath);
                    upload_progressBar.Value = 20;
                }

                // Take each item in the archive and extract them
                using (ZipArchive archive = ZipFile.OpenRead(fileName))
                {
                    ///<summary>
                    /// To act on the progressBar while uploading ! 
                    /// 1 - Count number of file
                    /// 2 - Set the total miss value for progressBar ( => 80 here )
                    /// 3 - Set the iterate Value !
                    /// </summary>
                    int nbFiles = archive.Entries.Count;
                    int restProgress = upload_progressBar.Maximum - upload_progressBar.Value;
                    int iterateValue = restProgress / nbFiles;

                    foreach (ZipArchiveEntry entry in archive.Entries)
                    {
                        // Provide support for some extension
                        if (entry.FullName.EndsWith(".txt", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".img", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".jpg", StringComparison.OrdinalIgnoreCase) || entry.FullName.EndsWith(".PNG", StringComparison.OrdinalIgnoreCase))
                        {
                            // Set the filName to the entry name
                            string sfileName = entry.FullName;
                            // Get the fullPath of the file
                            string sfileFullPath = Path.Combine(@tempExtractPath, @sfileName);

                            // Extract file to the specified temp directory (desktop by default) - True to Overwrite
                            entry.ExtractToFile(Path.Combine(@tempExtractPath, @sfileName), true);
                            upload_progressBar.Increment(iterateValue / 2);

                            //For Each Entry ==> Call the upload method to register blob reference and FilePath
                            try { client.uploadFileInBlob(blobDirRef, sfileFullPath); }
                            catch (Exception) { throw; }
                            finally { upload_progressBar.Increment(iterateValue / 2); }
                        }
                        //else
                        //{
                        //    /*
                        //    *   Potential Evolutions :  
                        //    *   Manage all extensions files to provide full Services
                        //    *   Create CloudDirectory If it's a folder
                        //    *   Create CloudBlock if it's an image a text etc...
                        //    *   Create CloudPage if it's for small files
                        //    */
                        //}   
                    }
                    // Delete our desktop temp directories & files
                    // Of course it's a temporary dir, because we delete it !
                    DeleteDirectory(@tempExtractPath);
                    upload_progressBar.Value = 100;
                    MessageBox.Show("All Files have been correctly unzipped and synchronized to " + blobDirRef + " directory");
                }
            }
            else
            {
                upload_progressBar.Value = 50;

                //Call the upload method to register blob reference and FileName
                try { client.uploadFileInBlob(blobDirRef, fileName); }
                catch (Exception) { throw; }

                upload_progressBar.Value = 100;
                MessageBox.Show("The File have been correctly added to the " + blobDirRef + " directory");
            }
            #endregion
        }

        // What Happen when we click on an object in the flyoutPanel !
        #region DownloadFile to Desktop directory !
        ///<summary>
        ///     For Each items
        ///     Add a button in FlowLayout
        ///     To download the target file
        /// </summary> 
        private void DownLoad_This_File_Click(object sender, EventArgs e)
        {
            download_progressBar.Value = 25;
            // Our sender is the btn we clicked
            // We receive all information we need
            Button btn = (Button)sender;
            string blobUri = btn.Text;

            string[] stringSeparators = new string[] { "/" };
            string[] result;
            result = blobUri.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);

            string blobRef = result[result.Length - 2];
            string fileName = blobUri.Substring(blobUri.LastIndexOf("/") + 1);

            // Call Download method !
            try
            {
                string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
                string destinationPath = Path.Combine(desktopPath, fileName);

                // Create file
                var fileStream = File.Create(destinationPath);
                // Get my stream File
                Stream myDownloadedFile = client.DownloadBlobAsStream(blobRef, fileName);
                myDownloadedFile.CopyTo(fileStream);

                // Close stream and free resources
                myDownloadedFile.Close();
                fileStream.Close();

            }
            catch (Exception) { throw; }
            finally
            {
                download_progressBar.Value = 100;
                MessageBox.Show("Successfully downloaded this file in your Desktop folder ! Great !");
            }
        }
        #endregion

        // What Happen when we click on the Compress&Upload button !
        #region Zip folder and store it in "Archives"
        private void CompressAndUpload_btn_Click(object sender, EventArgs e)
        {
            ///<summary>
            /// - We retrieve a reference to the folder Path
            ///         <para> folderName </para> 
            /// - Then we Zip the entire Folder
            /// - Then we inject this in the upload method to the archive directory destination in cloud
            /// </summary>
            // Store the folderPaTh Destination : Desktop + foldername Selected
            string folderCreated = folderName.Substring(folderName.LastIndexOf("\\") + 1) + ".zip";
            string folderZippedPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), folderCreated);

            // Determine whether the directory exists.
            if (Directory.Exists(@folderName) && !Directory.Exists(@folderZippedPath))
            {
                // Try to create the directory by archive name
                // Finally Upload the file into Archives Directory !
                try
                {
                    ZipFile.CreateFromDirectory(@folderName, @folderZippedPath);
                    // Increase the progressBar Value like 20%
                    ZipAndUpload_progressBar.Value = 20;
                }
                catch (Exception) { throw; }
                finally { client.uploadFileInBlob("Archives", folderZippedPath); File.Delete(folderZippedPath); }
                // Increase the progressBar Value to Maximum
                ZipAndUpload_progressBar.Value = 100;
                MessageBox.Show("The new zip file has been correctly created and uploaded in Cloud Archives directory! Well job");
            }
        }
        #endregion
    }
}
