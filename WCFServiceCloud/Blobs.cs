using System;
using System.Runtime.Serialization;

namespace WCFServiceCloud
{
    [DataContract]
    public class Blobs
    {
        [DataMember]
        public string Name { get; internal set; }
        [DataMember]
        public string Uri { get; internal set; }
        /*******************
        /* Whatever we want 
        /* Had no time to use more than necessary
        */
        //[DataMember]
        //public long Size { get; internal set; }
        //[DataMember]
        //public Type TypeOfBlob { get; set; }
    }
}