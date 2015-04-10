using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * {
  "id": 936075699,
  "subject": "Welcome!",
  "created_at": "2012-03-22T16:56:51-05:00",
  "updated_at": "2012-03-22T16:56:51-05:00",
  "content": "This is a new message",
  "creator": {
    "id": 149087659,
    "name": "Jason Fried",
    "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
  },
  "comments": [
    {
      "id": 1028592764,
      "content": "Yeah, really, welcome!",
      "created_at": "2012-03-22T16:56:48-05:00",
      "updated_at": "2012-03-22T16:56:48-05:00"
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
     */
    public class Message
    {
        [JsonIgnore]
        public int ProjectId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("subject")]
        public string Subject { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
        [JsonProperty("comments")]
        public IEnumerable<Comment> Comments { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<PersonStub> Subscribers { get; set; }
    }
}
