using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderCreateResponse : ApiResponse
    {
        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }
    }
}