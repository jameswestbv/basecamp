using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    public class CommentCreateRequest
    {
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<int> SubscriberIds { get; set; }
        [JsonProperty("attachments")]
        public IEnumerable<AttachmentToken> Attachments { get; set; }
    }
}
