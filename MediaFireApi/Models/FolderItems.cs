using System;
using Newtonsoft.Json;

namespace MediaFireApi.Models
{
    public abstract class Item
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("created_utc")]
        public DateTime? CreatedUtc { get; set; }

        [JsonProperty("delete_date")]
        public DateTime? DeleteDate { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("flag")]
        public int Flag { get; set; }
    }

    public class FileItem : Item
    {
        public class Link
        {
            [JsonProperty("direct_download")]
            public string DirectDownload { get; set; }

            [JsonProperty("normal_download")]
            public string NormalDownload { get; set; }

            [JsonProperty("view")]
            public string View { get; set; }

            [JsonProperty("read")]
            public string Read { get; set; }
        }

        [JsonProperty("quickkey")]
        public string QuickKey { get; set; }

        [JsonProperty("parent_folderkey")]
        public string ParentFolderKey { get; set; }

        [JsonProperty("hash")]
        public string Hash { get; set; }

        [JsonProperty("filename")]
        public string Filename { get; set; }

        [JsonProperty("size")]
        public long Size { get; set; }

        [JsonProperty("privacy")]
        public Privacy Privacy { get; set; }

        [JsonProperty("mimetype")]
        public string Mimetype { get; set; }

        [JsonProperty("filetype")]
        public FileType? Filetype { get; set; }

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

        [JsonProperty("shared_by_user")]
        public int SharedByUser { get; set; }
    }

    public class FolderItem : Item
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
}