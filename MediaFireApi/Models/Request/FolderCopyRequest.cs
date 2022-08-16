using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderCopyRequest : ApiRequest
    {
        [JsonProperty("folder_key_src")]
        public string FolderKeySrc { get; set; }

        [JsonProperty("folder_path_src")]
        public string FolderPathSrc { get; set; }

        [JsonProperty("folder_key_dst")]
        public string FolderKeyDst { get; set; }

        [JsonProperty("folder_path_dst")]
        public string FolderPathDst { get; set; }

    }
}