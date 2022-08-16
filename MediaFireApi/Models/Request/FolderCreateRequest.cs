using Newtonsoft.Json;

namespace MediaFireApi.Models.Request
{
    public class FolderCreateRequest : ApiRequest
    {
        [JsonProperty("foldername")]
        public string FolderName { get; set; }

        [JsonProperty("parent_key")]
        public string ParentKey { get; set; }

        [JsonProperty("parent_path")]
        public string ParentPath { get; set; }

        [JsonProperty("action_on_duplicate")]
        public ActionOnDuplicate? ActionOnDuplicate { get; set; }
    }
}