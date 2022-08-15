using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FileInfoResponse : ApiResponse
    {
        public class FileInfoModel
        {

        }

        [JsonProperty("file_info")]
        public FileInfoModel FileInfo { get; set; }

        [JsonProperty("file_infos")]
        public List<FileInfoModel> FileInfos { get; set; }
    }
}