using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public class CalendarEvent : CalendarEventStub
    {
        [JsonProperty("comments")]
        public IEnumerable<Comment> Comments { get; private set; }

        [JsonProperty("subscribers")]
        public IEnumerable<PersonStub> Subscribers { get; private set; }
    }
}
