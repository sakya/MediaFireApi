using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderCopyResponse : ApiResponse
    {
        [JsonProperty("new_folderkeys")]
        public List<string> NewFolderKey { get; set; }
    }
}