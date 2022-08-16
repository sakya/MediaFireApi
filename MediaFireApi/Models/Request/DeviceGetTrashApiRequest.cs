using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class DeviceGetTrashApiRequest : ApiRequest
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }

        [JsonProperty("data_only")]
        public YesNo? DataOnly { get; set; }

        [JsonProperty("content_type")]
        public FolderContentType ContentType { get; set; }

        [JsonProperty("chunk")]
        public int Chunk { get; set; }
    }
}