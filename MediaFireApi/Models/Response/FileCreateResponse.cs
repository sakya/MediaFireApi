using System;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FileCreateResponse : ApiResponse
    {
        [JsonProperty("fileinfo")]
        public FileItem FileInfo { get; set; }
    }
}