using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class CalendarManager : ManagerBase
    {
        public CalendarManager(Api api) : base(api)
        {
        }

        public IEnumerable<CalendarStub> Get()
        {
            var calendars = Api.Get<IEnumerable<CalendarStub>>("/calendars.json");
            return calendars;
        }

        public Calendar Get(CalendarStub stub)
        {
            return Get(stub.Id);
        }

        public Calendar Get(int id)
        {
            var action = "/calendars/{0}.json".FormatWith(id);
            var calendar = Api.Get<Calendar>(action);
            return calendar;
        }

        public Calendar Create(string name)
        {
            var action = "/calendars.json";
            var request = new CalendarCreateRequest() { Name = name };
            var calander = Api.Post<Calendar>(action, request);
            return calander;
        }

        public bool Update(CalendarStub calendar)
        {
            var updatedCalendar = Api.Put<Calendar>("/calendars/{0}.json".FormatWith(calendar.Id), calendar);

            if (updatedCalendar != null)
            {
                ModelCopier.CopyModel(updatedCalendar, calendar);
                return true;
            }

            return false;
        }

        public bool Delete(CalendarStub calendar)
        {
            return Delete(calendar.Id);
        }

        public bool Delete(int calendarId)
        {
            return Api.Delete("/calendars/{0}.json".FormatWith(calendarId));
        }

        public IEnumerable<CalendarEventStub> GetUpcomingEventsForProject(int projectId)
        {
            return GetEventsForAction("/projects/{0}/calendar_events.json".FormatWith(projectId));
        }

        public IEnumerable<CalendarEventStub> GetPastEventsForProject(int projectId)
        {
            return GetEventsForAction("/projects/{0}/calendar_events/past.json".FormatWith(projectId));
        }

        public IEnumerable<CalendarEventStub> GetUpcomingEventsForCalendar(int calendarId)
        {
            return GetEventsForAction("/calendars/{0}/calendar_events.json".FormatWith(calendarId));
        }

        public IEnumerable<CalendarEventStub> GetPastEventsForCalendar(int calendarId)
        {
            return GetEventsForAction("/calendars/{0}/calendar_events/past.json".FormatWith(calendarId));
        }

        public CalendarEventStub GetEvent(ProjectStub project)
        {
            return GetEventForAction("/projects/{0}/calendar_events/1.json".FormatWith(project.Id));
        }

        public CalendarEventStub GetEvent(CalendarStub calendar)
        {
            return GetEventForAction("projects/{0}/calendar_events/1.json".FormatWith(calendar.Id));
        }

        public CalendarEventStub GetEventForProject(int projectId)
        {
            return GetEventForAction("/projects/{0}/calendar_events/1.json".FormatWith(projectId));
        }

        public CalendarEventStub GetEventForCalendar(int calendar)
        {
            return GetEventForAction("projects/{0}/calendar_events/1.json".FormatWith(calendar));
        }

        private CalendarEventStub GetEventForAction(string action)
        {
            var calendarEvent = Api.Get<CalendarEventStub>(action);
            return calendarEvent;
        }

        private IEnumerable<CalendarEventStub> GetEventsForAction(string action)
        {
            var events = Api.Get<IEnumerable<CalendarEventStub>>(action);
            return events;
        }

        public CalendarEventStub CreateEventForProject(int projectId, CalendarEventCreateRequest request)
        {
            var action = "/projects/{0}/calendar_events.json".FormatWith(projectId);
            var calendarEvent = Api.Post<CalendarEventStub>(action, request);
            return calendarEvent;
        }

        public CalendarEventStub CreateEventForCalendar(int calendarId, CalendarEventCreateRequest request)
        {
            var action = "/calendars/{0}/calendar_events.json".FormatWith(calendarId);
            var calendarEvent = Api.Post<CalendarEventStub>(action, request);
            return calendarEvent;
        }

        public void UpdateEventForProject(int projectId, CalendarEventStub calendarEvent)
        {
            var action = "/projects/{0}/calendar_events/{1}.json".FormatWith(projectId, calendarEvent.Id);
            var request = new CalendarEventCreateRequest();
            ModelCopier.CopyModel(calendarEvent, request);
            var updatedCalendarEvent = Api.Put<CalendarEventStub>(action, request);
            ModelCopier.CopyModel(updatedCalendarEvent, calendarEvent);
        }

        public void UpdateEventForCalendar(int calendarId, CalendarEventStub calendarEvent)
        {
            var action = "/calendars/{0}/calendar_events/{1}.json".FormatWith(calendarId, calendarEvent.Id);
            var request = new CalendarEventCreateRequest();
            ModelCopier.CopyModel(calendarEvent, request);
            var updatedCalendarEvent = Api.Put<CalendarEventStub>(action, request);
            ModelCopier.CopyModel(updatedCalendarEvent, calendarEvent);
        }

        private class CalendarCreateRequest
        {
            [JsonProperty("name")]
            public string Name { get; set; }
        }
    }
}
