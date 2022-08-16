using System.Collections.Generic;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class UploadCheckResponse : ApiResponse
    {
        public class ResumableUploadModel
        {
            [JsonProperty("all_units_ready")]
            public YesNo? AllUnitsReady { get; set; }

            [JsonProperty("number_of_units")]
            public int NumberOfUnits { get; set; }

            [JsonProperty("unit_size")]
            public int UnitSize { get; set; }

            [JsonProperty("upload_key")]
            public string UploadKey { get; set; }
        }

        public class UploadUrlModel
        {
            [JsonProperty("simple")]
            public string Simple { get; set; }

            [JsonProperty("simple_fallback")]
            public string SimpleFallback { get; set; }

            [JsonProperty("resumable")]
            public string Resumable { get; set; }

            [JsonProperty("resumable_fallback")]
            public string ResumableFallback { get; set; }
        }

        [JsonProperty("hash_exists")]
        public YesNo? HashExists { get; set; }

        [JsonProperty("in_account")]
        public YesNo? InAccount { get; set; }

        [JsonProperty("in_folder")]
        public YesNo? InFolder { get; set; }

        [JsonProperty("file_exists")]
        public YesNo? FileExists { get; set; }

        [JsonProperty("different_hash")]
        public YesNo? DifferentHash { get; set; }

        [JsonProperty("duplicate_quickkey")]
        public string DuplicateQuickkey { get; set; }

        [JsonProperty("preemptive")]
        public string Preemptive { get; set; }

        [JsonProperty("resumable_upload")]
        public List<ResumableUploadModel> ResumableUpload { get; set; }

        [JsonProperty("available_space")]
        public long AvailableSpace { get; set; }

        [JsonProperty("used_storage_size")]
        public long UsedStorageSize { get; set; }

        [JsonProperty("storage_limit")]
        public long StorageLimit { get; set; }

        [JsonProperty("storage_limit_exceeded")]
        public YesNo? StorageLimitExceeded { get; set; }

        [JsonProperty("upload_url")]
        public UploadUrlModel UploadUrl { get; set; }
    }
}