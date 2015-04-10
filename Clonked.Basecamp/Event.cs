using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * {
    "id": 1054456336,
    "created_at": "2012-03-24T11:00:50-05:00",
    "updated_at": "2012-03-24T11:00:50-05:00",
    "creator": {
      "id": 149087659,
      "name": "Jason Fried",
      "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
    },
    "bucket": {
      "id": 605816632,
      "name": "BCX",
      "type": "Project",
      "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx.json"
    },
    "summary": "re-assigned a to-do to Funky ones: Design it",
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/todos/223304243-design-it.json"
  },
     * */
    public class Event
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
        [JsonProperty("bucket")]
        public EventBucket Bucket { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }

    public class EventBucket
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
