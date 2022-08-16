using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class FolderDepthResponse : ApiResponse
    {
        public class FolderDepthModel
        {
            public class ChainFolder
            {
                [JsonProperty("folderkey")]
                public string FolderKey { get; set; }

                [JsonProperty("name")]
                public string Name { get; set; }
            }

            [JsonProperty("depth")]
            public int Depth { get; set; }

            [JsonProperty("chain_folders")]
            public List<ChainFolder> ChaiFolders { get; set; }
        }

        [JsonProperty("folder_depth")]
        public FolderDepthModel FolderDepth { get; set; }
    }
}