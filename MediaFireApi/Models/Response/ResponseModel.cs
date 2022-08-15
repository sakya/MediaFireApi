using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class ResponseModel<T>
    {
        [JsonProperty("response")]
        public T Response { get; set; }
    }
}