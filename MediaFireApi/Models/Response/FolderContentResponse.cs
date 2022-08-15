using System;
using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderContentResponse : ApiResponse
    {
        public abstract class FolderItem
        {
            [JsonProperty("description")]
            public string Description { get; set; }

            [JsonProperty("created")]
            public DateTime Created { get; set; }

            [JsonProperty("created_utc")]
            public DateTime CreatedUtc { get; set; }

            [JsonProperty("revision")]
            public int Revision { get; set; }

            [JsonProperty("flag")]
            public int Flag { get; set; }
        }

        public class File : FolderItem
        {
            public class Link
            {
                [JsonProperty("normal_download")]
                public string NormalDownload { get; set; }
            }

            [JsonProperty("quickkey")]
            public string QuickKey { get; set; }

            [JsonProperty("hash")]
            public string Hash { get; set; }

            [JsonProperty("filename")]
            public string Filename { get; set; }

            [JsonProperty("size")]
            public long Size { get; set; }

            [JsonProperty("privacy")]
            public Privacy Privacy { get; set; }

            [JsonProperty("password_protected")]
            public string PasswordProtected { get; set; }

            [JsonProperty("mimetype")]
            public string Mimetype { get; set; }

            [JsonProperty("filetype")]
            public string Filetype { get; set; }

            [JsonProperty("view")]
            public int View { get; set; }

            [JsonProperty("edit")]
            public int Edit { get; set; }

            [JsonProperty("downloads")]
            public int Downloads { get; set; }

            [JsonProperty("views")]
            public int Views { get; set; }

            [JsonProperty("links")]
            public Link Links { get; set; }
        }

        public class Folder : FolderItem
        {
            [JsonProperty("folderkey")]
            public string FolderKey { get; set; }

            [JsonProperty("name")]
            public string Name { get; set; }

            [JsonProperty("tags")]
            public string Tags { get; set; }

            [JsonProperty("privacy")]
            public Privacy Privacy { get; set; }

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

        public class FolderContentModel
        {
            [JsonProperty("folder_key")] public string FolderKey { get; set; }

            [JsonProperty("content_type")] public string ContentType { get; set; }

            [JsonProperty("chunk")] public int Chunk { get; set; }

            [JsonProperty("chunk_size")] public int ChunkSize { get; set; }

            [JsonProperty("files")] public List<File> Files { get; set; }

            [JsonProperty("folders")] public List<Folder> Folders { get; set; }
        }

        [JsonProperty("folder_content")]
        public FolderContentModel FolderContent { get; set; }
    }
}