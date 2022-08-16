using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderDeleteRequest : RequestModel
    {
        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }

        [JsonProperty("folder_path")]
        public string FolderPath { get; set; }
    }
}