using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FileCopyRequest : ApiRequest
    {
        [JsonProperty("quick_key")]
        public string QuickKey { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }

        [JsonProperty("folder_path")]
        public string FolderPath { get; set; }
    }
}