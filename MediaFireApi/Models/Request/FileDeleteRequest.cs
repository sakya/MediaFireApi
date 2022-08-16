using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FileDeleteRequest : RequestModel
    {
        [JsonProperty("quick_key")]
        public string QuickKey { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }
    }
}