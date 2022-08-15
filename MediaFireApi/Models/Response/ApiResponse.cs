using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public abstract class ApiResponse
    {
        [JsonProperty("asynchronous")]
        public YesNo? Asynchronous { get; set; }

        [JsonProperty("current_api_version")]
        public string CurrentApiVersion { get; set; }

        [JsonProperty("action")]
        public string Action { get; set; }

        [JsonProperty("result")]
        public ApiResult Result { get; set; }

        [JsonProperty("error")]
        public int? Error { get; set; }

        [JsonProperty("message")]
        public string Message { get; set; }
    }
}