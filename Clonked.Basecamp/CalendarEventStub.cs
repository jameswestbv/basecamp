using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *   {
    "id": 883432030,
    "summary": "something coming up",
    "description": "",
    "created_at": "2012-03-28T11:50:00-05:00",
    "updated_at": "2012-03-28T12:24:59-05:00",
    "all_day": false,
    "starts_at": "2012-03-28T07:00:00-05:00",
    "ends_at": "2012-03-28T07:00:00-05:00",
    "creator": {
      "id": 149087659,
      "name": "Jason Fried",
      "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
    },
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/calendar_events/883432030-something-coming-up.json"
  },
     * */
    public class CalendarEventStub : ApiStubObject<CalendarEvent>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("summary")]
        public string Summary { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("all_day")]
        public bool AllDay { get; set; }
        [JsonProperty("starts_at")]
        public DateTime StartsAt { get; set; }
        [JsonProperty("ends_at")]
        public DateTime? EndsAt { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
        [JsonProperty("Url")]
        public string Url { get; set; }

        public override CalendarEvent GetDetail()
        {
            var calendarEvent = Api.GetResponseFromUrl<CalendarEvent>(Url);
            calendarEvent.Api = Api;
            return calendarEvent;
        }
    }
}
