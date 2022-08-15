using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class RenewSessionTokenResponse : ApiResponse
    {
        [JsonProperty("session_token")]
        public string SessionToken { get; set; }
    }
}