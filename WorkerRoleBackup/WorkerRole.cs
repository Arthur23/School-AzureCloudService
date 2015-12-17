using System.Diagnostics;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.WindowsAzure.ServiceRuntime;
using WCFServiceCloud;
using System;
using System.IO;
using System.IO.Compression;

namespace WorkerRoleBackup
{
    public class WorkerRole : RoleEntryPoint
    {
        private readonly CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        private readonly ManualResetEvent runCompleteEvent = new ManualResetEvent(false);

        ServiceCloud backupWorker;
        string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        string folderToCreate, fileToCreate, folderToCreateZipped;

        public override void Run()
        {
            Trace.TraceInformation("WorkerRoleBackup is running");

            // INIT Our worker on the Cloud
            backupWorker = new ServiceCloud();

            try
            {
                RunAsync(cancellationTokenSource.Token).Wait();
            }
            finally
            {
                this.runCompleteEvent.Set();
            }
        }

        public override bool OnStart()
        {
            ServicePointManager.DefaultConnectionLimit = 12;

            bool result = base.OnStart();

            Trace.TraceInformation("WorkerRoleBackup has been started");

            return result;
        }

        public override void OnStop()
        {
            Trace.TraceInformation("WorkerRoleBackup is stopping");

            this.cancellationTokenSource.Cancel();
            this.runCompleteEvent.WaitOne();

            base.OnStop();

            Trace.TraceInformation("WorkerRoleBackup has stopped");
        }

        private async Task RunAsync(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                Trace.TraceInformation("Processing to backup");
                ///<summary>
                /// Space for Backup
                /// All files wich are in directories will be zipped
                /// And the file will move to the "Backup" directory
                /// 
                ///     Get Directories
                ///     Then Get files in directories
                ///     Download each files with the name of the directory
                ///     Zip those files
                ///     Upload in "Backups"
                /// </summary>

                // Call the service method to list directories
                var allDirectories = backupWorker.GetDirectories();
                foreach (var blob in allDirectories)
                {
                    string blobDirectory = blob.Name;

                    // String separator to split the string and retrieve result we wanted
                    string[] stringSeparators = new string[] { "/" };
                    string[] result;
                    // Split a string delimited by another string and return all elements.
                    result = blobDirectory.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                    // Get the string index 2 -> Blob Directory under smallboxcontainer
                    string blobRef = result[2];

                    // List files in the directory
                    var allFiles = backupWorker.ListFilesInDir(blobRef);

                    // We want to save all others directories to Backups
                    if (blobRef != "Backups")
                    {
                        // Download each File
                        foreach (var blobFile in allFiles)
                        {
                            string blobSubDirecotry = blobFile.Name;

                            // Split a string delimited by another string and return all elements.
                            result = blobSubDirecotry.Split(stringSeparators, StringSplitOptions.RemoveEmptyEntries);
                            // Get the string index 3 -> Blob file under directory
                            string fileRef = result[3];

                            // Call Download method !
                            try
                            {
                                folderToCreate = Path.Combine(desktopPath, blobRef);
                                folderToCreateZipped = folderToCreate + ".zip";
                                fileToCreate = Path.Combine(folderToCreate, "backup-" + fileRef);

                                // Determine whether the directory exists.
                                if (!Directory.Exists(@folderToCreate))
                                {
                                    // Try to create the directory by archive name
                                    Directory.CreateDirectory(@folderToCreate);
                                }
                                // Create file
                                var fileStream = File.Create(fileToCreate, 40000, FileOptions.Asynchronous);
                                // Get my stream File
                                Stream myDownloadedFile = backupWorker.DownloadBlobAsStream(blobRef, fileRef);
                                myDownloadedFile.CopyTo(fileStream);

                                // Free memory
                                fileStream.Close();
                                myDownloadedFile.Dispose();
                            }
                            catch (Exception) { throw; }
                        }
                    }
                    // Each file in one directory have been downloaded and stored locally
                    // Now we have to zip the entire folder retrieved above!
                    // Try to create the directory by archive name
                    // We want to save all others directories to Backups
                    if (blobRef != "Backups")
                    {
                        try { ZipFile.CreateFromDirectory(@folderToCreate, @folderToCreateZipped); }
                        catch (Exception ex)
                        {
                            Trace.Fail(ex.Message);
                            throw;
                        }
                        Trace.TraceInformation("Folder Zipped");
                        // Finally Upload the file into Backups Directory !
                        backupWorker.uploadFileInBlob("Backups", folderToCreateZipped);
                        File.Delete(folderToCreateZipped);
                        DeleteDirectory(folderToCreate);
                    }
                }

                /****************************************************
                *   You can set here the interval between two backups
                *   It would be interesting to store this value in localsettings 
                *   To let the choice to the user :)
                */
                // 1 Heure !
                int hours = 1;
                int minuts = 60;
                int seconds = 60;
                int millisec = 60;

                await Task.Delay(hours * minuts * seconds * millisec);
            }
        }
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
    }
}
