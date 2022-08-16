using Newtonsoft.Json;

namespace MediaFireApi.Models
{
    public class FileItem : Item
    {
        public class Link
        {
            [JsonProperty("direct_download")]
            public string DirectDownload { get; set; }

            [JsonProperty("normal_download")]
            public string NormalDownload { get; set; }

            [JsonProperty("download")]
            public string Download { get; set; }

            [JsonProperty("view")]
            public string View { get; set; }

            [JsonProperty("read")]
            public string Read { get; set; }

            [JsonProperty("edit")]
            public string Edit { get; set; }

            [JsonProperty("watch")]
            public string Watch { get; set; }

            [JsonProperty("listen")]
            public string Listen { get; set; }

            [JsonProperty("streaming")]
            public string Streaming { get; set; }
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
        public Privacy? Privacy { get; set; }

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
}