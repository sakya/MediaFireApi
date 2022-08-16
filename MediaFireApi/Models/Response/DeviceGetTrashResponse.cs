using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class DeviceGetTrashResponse : ApiResponse
    {
        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("folderkey")]
        public string FolderKey { get; set; }

        [JsonProperty("folder_count")]
        public int FolderCount { get; set; }

        [JsonProperty("file_count")]
        public int FileCount { get; set; }

        [JsonProperty("content_type")]
        public string ContentType { get; set; }

        [JsonProperty("chunk_number")]
        public int ChunkNumber { get; set; }

        [JsonProperty("more_chunks")]
        public YesNo MoreChunks { get; set; }

        [JsonProperty("files")]
        public List<FileItem> Files { get; set; }

        [JsonProperty("folders")]
        public List<FolderItem> Folders { get; set; }
    }
}