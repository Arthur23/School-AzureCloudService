using Microsoft.WindowsAzure;
using Microsoft.WindowsAzure.Storage;
using Microsoft.WindowsAzure.Storage.Blob;
using System;
using System.Collections.Generic;
using System.IO;

namespace WCFServiceCloud
{
    public class ServiceCloud : IServiceCloud
    {
        /* ------------------ */
        // My Globals Variables
        CloudBlobContainer smallboxcontainer;
        CloudBlockBlob uploadBlockBlob;
        CloudBlobDirectory TheBlobDirectory;
        Stream streamFile_Upload;

        /*  ------------------
        *   Class Constructor
        *   Init our container
        *   Init our directories and files !
        */
        #region Constructor And Method when init the service with a client + Get container Name method to display it
        public ServiceCloud()
        {
            /****************************/
            // Set the string Connection !
            var stringConn = CloudConfigurationManager.GetSetting("Microsoft.WindowsAzure.Plugins.Diagnostics.ConnectionString");
            // 1 - Get the string Account for connection
            CloudStorageAccount Account = CloudStorageAccount.Parse(stringConn);
            // 2 - Get our test client to provide services (Accessibles methodes)
            CloudBlobClient blobClient = Account.CreateCloudBlobClient();
            // 3 - Retrieve a reference to the main container
            smallboxcontainer = blobClient.GetContainerReference("smallboxcontainer");
            // 4 - Create the container if it doesn't already exist.
            smallboxcontainer.CreateIfNotExists();

        }

        // Create the internal structure in the container
        // Call it when run the Client
        public void CreateDirectoryStruct()
        {
            List<string> directoriesToCreateAndPopulate =
                new List<string> {/* [Don't Modify = */ "Archives", "Backups", /* ]*/ "Documents", "Images", "Videos" };

            // Upload a file in each of this Directories
            // We could put a specific Readme.txt according
            // to their use

            /****************************************/
            // So retreive the Readme.txt to upload !
            //    
            //  ***Cette Partie serait changée en cas de release***
            //  Regarder à la racine du projet ou se situe la solution
            //  Le répértoire contents doit contenir un readme.txt
            //  Indications : Sur mon PC 
            //  ==> "D:\\Cours SUPINFO\\PROJETS\\4NET - SmallBoxStorage\\SmallBoxCloud\\"
            var rootProjectDirectory = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.Parent.Parent.Parent.Parent.Parent.FullName;/* = Application.StartupPath; */
            var ressourcesDirectory = Path.Combine(rootProjectDirectory, @"StartContent\Readme.txt");

            foreach (var item in directoriesToCreateAndPopulate)
            {
                //if (item == "Videos")
                //{
                //    ressourcesDirectory = Path.Combine(rootProjectDirectory, @"StartContent\Didacticiel.mp4");
                //}
                try { uploadFileInBlob(item, ressourcesDirectory); }
                catch (Exception) { throw; }
            }
        }

        // Get the container name
        public string GetContainerUri()
        {
            // Root Folder
            string containerUri = smallboxcontainer.Uri.ToString();
            // Get the last word after "/" of the uri
            return containerUri.Substring(containerUri.LastIndexOf("/"));
        }
        #endregion

        #region List Directories, folders and Files
        /* ---------------------------------
        *   List our Directories in Container
        */
        public List<Blobs> GetDirectories()
        {
            List<Blobs> Blob = new List<Blobs>();
            // For Each item in the list, create an object with stored information
            foreach (IListBlobItem item in smallboxcontainer.ListBlobs())
            {
                // Store the item type
                Type type = item.GetType();

                // If it's a directory, add to list
                if (type == typeof(CloudBlobDirectory))
                {
                    TheBlobDirectory = (CloudBlobDirectory)item;
                    // Add blob object
                    Blob.Add(new Blobs
                    {
                        Name = TheBlobDirectory.Uri.AbsolutePath,
                        Uri = TheBlobDirectory.Uri.ToString(),
                    });
                }// END IF 
            }// END ForEach
            return Blob;
        }

        public List<Blobs> ListFilesInDir(string BlobRef)
        {
            CloudBlobDirectory directory = smallboxcontainer.GetDirectoryReference(BlobRef);
            List<Blobs> Blob = new List<Blobs>();

            foreach (IListBlobItem item in directory.ListBlobs())
            {
                // Store the item type
                var type = item.GetType();
                // If it's a directory, add to list
                if (type == typeof(CloudBlobDirectory))
                {
                    TheBlobDirectory = (CloudBlobDirectory)item;
                    // Add blob object
                    Blob.Add(new Blobs
                    {
                        Name = TheBlobDirectory.Uri.AbsolutePath,
                        Uri = TheBlobDirectory.Uri.ToString(),
                    });
                }// END IF 
                else
                {
                    Blob.Add(new Blobs
                    {
                        Name = item.Uri.AbsolutePath,
                        Uri = item.Uri.ToString(),
                    });
                }
            }// END ForEach
            return Blob;
        }
        #endregion

        #region Upload a Document / Image / somethin' else from a client to the selected directory
        public void uploadFileInBlob(string sBlobRef, string sFileName)
        {
            // The reference should be container/<Directory>/<FileName>
            string fileDirRef;
            // GET THE LAST RESULT INDEX -> The splited FileName
            fileDirRef = sFileName.Substring(sFileName.LastIndexOf("\\") + 1);

            // Reference to the file destination
            TheBlobDirectory = smallboxcontainer.GetDirectoryReference(sBlobRef);
            uploadBlockBlob = TheBlobDirectory.GetBlockBlobReference(fileDirRef);

            // The stream file
            streamFile_Upload = File.Open(@sFileName, FileMode.Open, FileAccess.Read, FileShare.Read);

            // Call method with parameter made above to upload from stream
            uploadFileFromStream(streamFile_Upload);
        }

        // UPLOAD FILE WITH STREAM METHOD
        public void uploadFileFromStream(Stream stream)
        {
            using (stream)
            {
                try { uploadBlockBlob.UploadFromStream(stream); }
                catch (Exception) { throw; }
            }
        }
        #endregion


        public Stream DownloadBlobAsStream(string directoryName, string fileName)
        {
            Stream mem = new MemoryStream();
            // Must be files "block" and not a directory !
            CloudBlobDirectory blobDir = smallboxcontainer.GetDirectoryReference(directoryName);
            CloudBlockBlob blobBlock = blobDir.GetBlockBlobReference(fileName);

            if (blobBlock != null)
                blobBlock.DownloadToStream(mem);
            mem.Position = 0;
            return mem;
        }

    }
}