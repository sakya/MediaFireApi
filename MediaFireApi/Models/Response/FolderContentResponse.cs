using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderContentResponse : ApiResponse
    {
        public class FolderContentModel
        {
            [JsonProperty("folder_key")]
            public string FolderKey { get; set; }

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

        [JsonProperty("folder_content")]
        public FolderContentModel FolderContent { get; set; }
    }
}