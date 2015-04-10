using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *    "count": 3,
    "updated_at": "2012-03-22T17:35:50-05:00",
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/calendar_events.json"
     */

    public class ItemMeta
    {
        ItemMeta()
        {

        }

        [JsonProperty("count")]
        public int Count { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
