using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * {
    "id": 336154974,
    "name": "Board Meetings",
    "updated_at": "2012-03-27T13:19:29-05:00",
    "url": "https://basecamp.com/999999999/api/v1/calendars/336154974-board-meetings.json"
  },
     * */
    public class CalendarStub : ApiStubObject<Calendar>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public override Calendar GetDetail()
        {
            return Api.GetResponseFromUrl<Calendar>(Url);
        }
    }
}
