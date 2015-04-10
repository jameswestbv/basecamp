using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public class Person : PersonStub
    {
        [JsonProperty("email_address")]
        public string Email { get; set; }
        [JsonProperty("admin")]
        public bool Admin { get; set; }
        [JsonProperty("type")]
        public string Type { get { return "Person"; } }
        [JsonProperty("avatar_url")]
        public string AvatarUrl { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("events")]
        public ItemMeta EventInfo { get; set; }
    }
}
