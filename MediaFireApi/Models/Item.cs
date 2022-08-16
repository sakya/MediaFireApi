using System;
using Newtonsoft.Json;

namespace MediaFireApi.Models
{
    public abstract class Item
    {
        [JsonProperty("description")]
        public string Description { get; set; }

        [JsonProperty("created")]
        public DateTime? Created { get; set; }

        [JsonProperty("created_utc")]
        public DateTime? CreatedUtc { get; set; }

        [JsonProperty("delete_date")]
        public DateTime? DeleteDate { get; set; }

        [JsonProperty("revision")]
        public int Revision { get; set; }

        [JsonProperty("flag")]
        public int Flag { get; set; }
    }
}