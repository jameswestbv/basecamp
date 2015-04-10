using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    /*
     * {
  "content": "Here's the new logo!",
  "attachments": [
    {
      "token": "4f71ea23-134660425d1818169ecfdbaa43cfc07f4e33ef4c",
      "name": "new_logo.png"
    }
  ],
  "subscribers": [ 1, 5, 6]
}
     */
    public class UploadCreateRequest
    {
        [JsonIgnore]
        public int ProjectId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("attachments")]
        public IEnumerable<AttachmentToken> Attachments { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<int> Subscribers { get; set; }

        internal void SetAttachmentToken(AttachmentToken token)
        {
            Attachments = new AttachmentToken[1] { token };
        }
    }
}
