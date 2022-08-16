using System;
using Newtonsoft.Json;

namespace MediaFireApi.Models.Response
{
    public class UserInfoResponse : ApiResponse
    {
        public class UserInfoModel {
            [JsonProperty("ekey")]
            public string EKey { get; set; }

            [JsonProperty("email")]
            public string Email { get; set; }

            [JsonProperty("first_name")]
            public string FirstName { get; set; }

            [JsonProperty("last_name")]
            public string LastName { get; set; }

            [JsonProperty("display_name")]
            public string DisplayName { get; set; }

            [JsonProperty("gender")]
            public string Gender { get; set; }

            [JsonProperty("birth_date")]
            public DateTime? BirthDate { get; set; }

            [JsonProperty("location")]
            public string Location { get; set; }

            [JsonProperty("website")]
            public string Website { get; set; }

            [JsonProperty("premium")]
            public YesNo? Premium { get; set; }

            [JsonProperty("bandwidth")]
            public int Bandwidth { get; set; }

            [JsonProperty("created")]
            public DateTime? Created { get; set; }

            [JsonProperty("validated")]
            public YesNo? Validated { get; set; }

            [JsonProperty("tos_accepted")]
            public string TosAccepted { get; set; }

            [JsonProperty("used_storage_size")]
            public int UsedStorageSize { get; set; }

            [JsonProperty("base_storage")]
            public int BaseStorage { get; set; }

            [JsonProperty("bonus_storage")]
            public int BonusStorage { get; set; }

            [JsonProperty("storage_limit")]
            public int StorageLimit { get; set; }

            [JsonProperty("storage_limit_exceeded")]
            public YesNo? StorageLimitExceeded { get; set; }

            [JsonProperty("options")]
            public int Options { get; set; }
        }

        [JsonProperty("user_info")]
        public UserInfoModel UserInfo { get; set; }
    }
}