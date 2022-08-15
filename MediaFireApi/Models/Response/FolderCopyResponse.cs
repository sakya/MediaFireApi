using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderCopyResponse : ApiResponse
    {
        [JsonProperty("new_folderkeys")]
        public string NewFolderKey { get; set; }
    }
}