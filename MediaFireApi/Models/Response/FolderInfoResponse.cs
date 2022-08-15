using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderInfoResponse : ApiResponse
    {
        public class FolderInfoModel
        {
            [JsonProperty("folderkey")]
            public string FolderKey { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("file_count")]
            public int FileCount { get; set; }

            [JsonProperty("folder_count")]
            public int FolderCount { get; set; }

            [JsonProperty("revision")]
            public int Revision { get; set; }

            [JsonProperty("owner_name")]
            public string OwnerName { get; set; }

            [JsonProperty("avatar")]
            public string Avatar { get; set; }

            [JsonProperty("dropbox_enabled")]
            public YesNo DropboxEnabled { get; set; }

            [JsonProperty("flag")]
            public int Flag { get; set; }
        }

        [JsonProperty("folder_info")]
        public FolderInfoModel FolderInfo { get; set; }
    }
}