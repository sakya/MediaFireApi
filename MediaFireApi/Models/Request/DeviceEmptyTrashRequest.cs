using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class DeviceEmptyTrashRequest : ApiRequest
    {
        [JsonProperty("device_id")]
        public string DeviceId { get; set; }
    }
}