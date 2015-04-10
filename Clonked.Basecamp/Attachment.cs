using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * {
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
    },
    "attachable": {
      "id": 70219655,
      "type": "Upload",
      "url": "https://basecamp.com/1111/api/v1/projects/2222/uploads/70219655.json"
    }
  }
     * */

    public class Attachment
    {
        [JsonProperty("key")]
        public string Key { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("byte_size")]
        public int ByteSize { get; set; }
        [JsonProperty("content_type")]
        public string ContentType { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
        [JsonProperty("attachable")]
        public TypeMeta Attachable { get; set; }
    }
}
