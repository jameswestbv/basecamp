using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    public class DocumentCreateRequest
    {
        [JsonIgnore]
        public int ProjectId { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<int> SubscriberIds { get; set; }
    }
}
