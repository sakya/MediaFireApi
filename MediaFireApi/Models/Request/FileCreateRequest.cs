using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FileCreateRequest : RequestModel
    {
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("parent_key")]
        public string ParentKey { get; set; }

        [JsonProperty("parent_path")]
        public string ParentPath { get; set; }
    }
}