using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class ApiRequest
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }

        [JsonProperty("response_format")]
        public string ResponseFormat { get; set; } = "json";
    }
}