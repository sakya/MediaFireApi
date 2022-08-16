using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class DownloadDirectLinkResponse : ApiResponse
    {
        public class DirectLink
        {
            [JsonProperty("quick_key")]
            public string QuickKey { get; set; }

            [JsonProperty("direct_download")]
            public string DirectDownload { get; set; }
        }

        [JsonProperty("links")]
        public List<DirectLink> Links { get; set; }
    }
}