using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderCopyRequest : RequestModel
    {
        [JsonProperty("folder_key_src")]
        public string FolderKeySrc { get; set; }

        [JsonProperty("folder_key_dst")]
        public string FolderKeyDst { get; set; }
    }
}