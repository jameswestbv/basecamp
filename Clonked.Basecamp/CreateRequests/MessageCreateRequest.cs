using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    public class MessageCreateRequest
    {
        [JsonIgnore]
        public int ProjectId { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<int> SubscriberIds { get; set; }
        [JsonProperty("attachments")]
        public IEnumerable<AttachmentToken> Attachments { get; set; }
    }
}
