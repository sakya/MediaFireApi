using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FileUpdateRequest : RequestModel
    {
        [JsonProperty("quick_key")]
        public string QuickKey { get; set; }

        [JsonProperty("file_path")]
        public string FilePath { get; set; }

        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("truncate")]
        public YesNo? Truncate { get; set; }

        [JsonProperty("privacy")]
        public Privacy? Privacy { get; set; }
    }
}