using Newtonsoft.Json;

namespace MediaFireApi.Models
{
    public class FolderItem : Item
    {
        [JsonProperty("folderkey")]
        public string FolderKey { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("tags")]
        public string Tags { get; set; }

        [JsonProperty("privacy")]
        public Privacy? Privacy { get; set; }

        [JsonProperty("file_count")]
        public int FileCount { get; set; }

        [JsonProperty("folder_count")]
        public int FolderCount { get; set; }

        [JsonProperty("dropbox_enabled")]
        public YesNo DropboxEnabled { get; set; }

        [JsonProperty("total_folders")]
        public int TotalFolders { get; set; }

        [JsonProperty("total_files")]
        public int TotalFiles { get; set; }

        [JsonProperty("total_size")]
        public long TotalSize { get; set; }
    }
}