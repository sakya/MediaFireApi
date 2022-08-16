using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class DownloadDirectLinkRequest : ApiRequest
    {
        [JsonProperty("quick_key")]
        public string QuickKey { get; set; }

        [JsonProperty("link_type")]
        public string LinkType { get; set; } = "direct_download";
    }
}