using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FileCopyResponse : ApiResponse
    {
        [JsonProperty("new_quickkey")]
        public string NewQuickKey { get; set; }
    }
}