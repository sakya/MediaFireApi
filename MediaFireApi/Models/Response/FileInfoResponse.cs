using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FileInfoResponse : ApiResponse
    {
        [JsonProperty("file_info")]
        public FileItem FileItemInfo { get; set; }

        [JsonProperty("file_infos")]
        public List<FileItem> FileInfos { get; set; }
    }
}