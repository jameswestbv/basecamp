using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Clonked.Basecamp.Managers;
using Clonked.Basecamp.Collections;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp
{
    /*
     * {
          "id": 567469885,
          "name": "Vacation",
          "created_at": "2012-03-28T13:14:30-05:00",
          "updated_at": "2012-03-28T13:26:07-05:00",
          "creator": {
            "id": 149087659,
            "name": "Jason Fried",
            "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3"
          },
          "accesses": {
            "count": 3,
            "updated_at": "2012-03-28T13:14:31-05:00",
            "url": "https://basecamp.com/999999999/api/v1/calendars/567469885-vacation/accesses.json"
          },
          "calendar_events": {
            "count": 1,
            "updated_at": "2012-03-28T13:26:07-05:00",
            "urls": {
              "upcoming": "https://basecamp.com/999999999/api/v1/calendars/567469885-vacation/calendar_events.json",
              "past": "https://basecamp.com/999999999/api/v1/calendars/567469885-vacation/calendar_events/past.json"
            }
          }
        }
     * */
    public class Calendar : CalendarStub
    {
        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }
        [JsonProperty("accesses")]
        internal ItemMeta AccessInfo { get; set; }
        [JsonProperty("calendar_events")]
        internal ItemMeta CalendarEventInfo { get; set; }

        public CalendarEventStub Create(CalendarEventCreateRequest request)
        {
            return Api.Calendars.CreateEventForCalendar(this.Id, request);
        }

        public CalendarEventStub Create(CalendarEventStub calendarEvent)
        {
            var createRequest = new CalendarEventCreateRequest();
            ModelCopier.CopyModel(calendarEvent, createRequest);
            return Api.Calendars.CreateEventForCalendar(this.Id, createRequest);
        }

        public void Update(CalendarEventStub calendarEvent)
        {
            Api.Calendars.UpdateEventForCalendar(this.Id, calendarEvent);
        }

        private CalendarEventCollection _calendarEvents;

        public CalendarEventCollection CalendarEvents
        {
            get
            {
                if (_calendarEvents == null)
                {
                    var collection = new CalendarEventCollection();
                    collection.Api = Api;
                    collection.Calendar = this;
                    _calendarEvents = collection;
                }
                return _calendarEvents;
            }
            internal set
            {
                _calendarEvents = value;
                if (_calendarEvents != null)
                {
                    _calendarEvents.Api = Api;
                    _calendarEvents.Calendar = this;
                }
            }
        }

        private IEnumerable<PersonStub> _people;
        public IEnumerable<PersonStub> People
        {
            get
            {
                if (_people == null && Api != null)
                {
                    _people = Api.Accesses.GetPeopleWithAccessToCalendar(this.Id);
                }
                return _people;
            }
            internal set
            {
                _people = value;
            }
        }

        public bool GrantAccess(int personId)
        {
            return Api.Accesses.GrantAccessToCalendar(this.Id, new int[] { personId });
        }

        public bool GrantAccess(IEnumerable<int> peopleIds)
        {
            return Api.Accesses.GrantAccessToCalendar(this.Id, peopleIds);
        }

        public bool GrantAccess(PersonStub person)
        {
            return Api.Accesses.GrantAccessToCalendar(this, new PersonStub[] { person });
        }

        public bool GrantAccess(IEnumerable<PersonStub> people)
        {
            return Api.Accesses.GrantAccessToCalendar(this, people);
        }

        public bool GrantAccess(string emailAddress)
        {
            return Api.Accesses.GrantAccessToCalendar(this, new string[] { emailAddress });
        }

        public bool GrantAccess(IEnumerable<string> emailAddresses)
        {
            return Api.Accesses.GrantAccessToCalendar(this, emailAddresses);
        }



    }
}
