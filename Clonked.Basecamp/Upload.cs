using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *{
  "id": 31709,
  "created_at": "2012-03-27T22:48:49-04:00",
  "updated_at": "2012-03-28T11:36:10-04:00",
  "content": "Hi there!",
  "attachments": [
    {
      "key": "40b8a84cb1a30dbe04457dc99e094b6299deea41",
      "name": "bearwave.gif",
      "byte_size": 508254,
      "content_type": "image/gif",
      "created_at": "2012-03-27T22:48:49-04:00",
      "url": "https://basecamp.com/1111/api/v1/projects/2222/attachments/3333/40b8a84cb1a30dbe04457dc99e094b6299deea41/original/bearwave.gif",
      "creator": {
        "id": 73,
        "name": "Nick Quaranto",
        "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
      }
    }
  ],
  "comments": [
    {
      "id": 5566323,
      "content": "Testing a comment",
      "created_at": "2012-03-28T11:36:10-04:00",
      "updated_at": "2012-03-28T11:36:10-04:00",
      "attachments": [],
      "creator": {
        "id": 73,
        "name": "Nick Quaranto",
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
    public class Upload
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("attachments")]
        internal IEnumerable<Attachment> Attachments { get; set; }
        [JsonProperty("comments")]
        public IEnumerable<Comment> Comments { get; set; }
        [JsonProperty("subscribers")]
        public IEnumerable<PersonStub> Subscribers { get; set; }

        public Attachment Attachment 
        { 
            get
            {
                return Attachments.FirstOrDefault();
            }
        }
    }
}
