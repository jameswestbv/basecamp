using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *     {
      "id": 1028592764,
      "content": "+1",
      "created_at": "2012-03-24T09:53:34-05:00",
      "updated_at": "2012-03-24T09:53:34-05:00",
      "creator": {
        "id": 127326141,
        "name": "David Heinemeier Hansson",
        "avatar_url": "https://asset0.37img.com/global/9d2148cb8ed8e2e8ecbc625dd1cbe7691896c7d9/avatar.96.gif?r=3"
      }
    }
     * */
    public class Comment : ApiObject
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("attachments")]
        public IEnumerable<Attachment> Attachments { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
    }
}
