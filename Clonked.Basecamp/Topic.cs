using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *   {
    "id": 1028592764,
    "title": "Prep the materials before the board meeting with Bezos",
    "excerpt": "I'll be there!",
    "created_at": "2012-03-24T09:53:35-05:00",
    "updated_at": "2012-03-24T09:53:35-05:00",
    "attachments": 0,
    "last_updater": {
      "id": 149087659,
      "name": "Jason Fried"
    },
    "topicable": {
      "id": 174886926,
      "type": "CalendarEvent",
      "url": "https://basecamp.com/999999999//api/v1/projects/605816632-bcx/calendar_events/174886926-prep-the-materials.json"
    }
  },
     */

    public class Topic
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public string Title { get; set; }
        [JsonProperty("description")]
        public string Excerpt { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("attachments")]
        public int Attachments { get; set; }
        [JsonProperty("last_updater")]
        public PersonStub LastUpdater { get; set; }
        [JsonProperty("topicable")]
        public TypeMeta TopicableItem { get; set; }
    }
}
