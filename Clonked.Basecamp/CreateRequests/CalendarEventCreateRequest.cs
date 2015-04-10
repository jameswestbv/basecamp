using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    /*
     * {
  "summary": "My all-day event spanning two days",
  "description": "Details to follow",
  "all_day": true,
  "starts_at": "2012-03-28",
  "ends_at": "2012-03-30"
}
     */ 
    public class CalendarEventCreateRequest
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("all_day")]
        public bool AllDay { get; set; }
        [JsonProperty("starts_at")]
        public DateTime StartsAt { get; set; }
        [JsonProperty("ends_at")]
        public DateTime? EndsAt { get; set; }
    }
}
