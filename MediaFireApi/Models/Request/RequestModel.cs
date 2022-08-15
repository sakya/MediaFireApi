using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class RequestModel
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }

        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; } = "json";
    }
}