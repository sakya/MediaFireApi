using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderContentRequest : RequestModel
    {
        [JsonProperty("folder_key")]
        public string FolderKey { get; set; }

        [JsonProperty("content_type")]
        public FolderContentType ContentType { get; set; }

        [JsonProperty("chunk")]
        public int Chunk { get; set; }

        [JsonProperty("chunk_size")]
        public int ChunkSize { get; set; }

        [JsonProperty("details")]
        public string Details { get; set; } = "yes";

        [JsonProperty("order_direction")]
        public Order OrderDirection { get; set; } = Order.Asc;

        [JsonProperty("order_by")]
        public string OrderBy { get; set; } = "name";

        [JsonProperty("filter")]
        public string Filter { get; set; }
    }
}