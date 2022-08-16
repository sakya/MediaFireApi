using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class UploadCheckRequest : RequestModel
    {
        [JsonProperty("filename")]
        public string FileName { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }

        [JsonProperty("path")]
        public string Path { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("resumable")]
        public YesNo? Resumable { get; set; }

        [JsonProperty("preemptive")]
        public YesNo? Preemptive { get; set; }
    }
}