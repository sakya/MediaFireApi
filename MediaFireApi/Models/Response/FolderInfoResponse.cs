using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderInfoResponse : ApiResponse
    {
        [JsonProperty("folder_info")]
        public FolderItem FolderItemInfo { get; set; }

        [JsonProperty("folder_infos")]
        public List<FolderItem> FolderInfos { get; set; }
    }
}