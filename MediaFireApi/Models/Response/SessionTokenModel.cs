using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class SessionTokenModel
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }
    }
}