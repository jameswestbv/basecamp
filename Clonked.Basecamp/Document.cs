using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * {
  "id": 963979453,
  "title": "Manifesto",
  "content": "Do this<br>Then that<br>Finally just so!",
  "created_at": "2012-03-27T13:19:29-05:00",
  "updated_at": "2012-03-27T13:53:24-05:00",
  "last_updater": {
    "id": 149087659,
    "name": "Jason Fried"
  },
  "comments": [
    {
      "content": "I think there should be more sass to it.",
      "created_at": "2012-03-27T13:53:24-05:00",
      "updated_at": "2012-03-27T13:53:24-05:00",
      "creator": {
        "id": 149087659,
        "name": "Jason Fried",
        "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
      }
    }
  ],
  "subscribers": [
    {
      "id": 149087659,
      "name": "Jason Fried"
    },
    {
      "id": 1071630348,
      "name": "Jeremy Kemper"
    }
  ]
}
     * */

    public class Document : ApiObject
    {
        [JsonIgnore]
        public int ProjectId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("last_updater")]
        public PersonStub LastUpdater { get; set; }
        [JsonProperty("comments")]
        public IEnumerable<Comment> Comments { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<PersonStub> Subscribers { get; set; }
    }
}
