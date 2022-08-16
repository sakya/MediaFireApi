using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderUpdateRequest : RequestModel
    {
        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }

        [JsonProperty("folder_path")]
        public string FolderPath { get; set; }

        [JsonProperty("foldername")]
        public string FolderName { get; set; }

        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("privacy")]
        public Privacy? Privacy { get; set; }

        [JsonProperty("privacy_recursive")]
        public YesNo? PrivacyRecursive { get; set; }
    }
}