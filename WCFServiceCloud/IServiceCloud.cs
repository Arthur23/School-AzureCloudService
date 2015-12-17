using System.Collections.Generic;
using System.IO;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WCFServiceCloud
{
    [ServiceContract]
    public interface IServiceCloud
    {
        /// <summary>
        ///     List all methods prototypes provided by our service v 1.0
        ///     All this Methods have been tried ! And there are perfectly functional
        ///     We may evolve it in way to be consumable by any third-part client
        ///     But we also "delegate" some things to client size like zip before upload
        ///     In that way, the server stay fast to deliver service for all users
        /// </summary>
        [OperationContract]
        void CreateDirectoryStruct();

        [OperationContract]
        string GetContainerUri();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetDirectories")]
        List<Blobs> GetDirectories();

        [OperationContract]
        [WebInvoke(ResponseFormat = WebMessageFormat.Json, UriTemplate = "/GetDirectories/ListFilesInDir")]
        List<Blobs> ListFilesInDir(string BlobRef);

        [OperationContract]
        [WebInvoke(UriTemplate = "/uploadFileInBlob")]
        void uploadFileInBlob(string sBlobRef, string sFileName);

        [OperationContract]
        [WebInvoke(UriTemplate = "/uploadFileFromStream")]
        void uploadFileFromStream(Stream stream);

        [OperationContract]
        [WebInvoke(UriTemplate = "/DownloadBlobAsStream")]
        Stream DownloadBlobAsStream(string directoryName, string fileName);
    }
}
