using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderInfoRequest : RequestModel
    {
        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }
    }
}