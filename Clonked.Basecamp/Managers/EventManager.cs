using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp.Managers
{
    public class EventManager : ManagerBase
    {
        public EventManager(Api api) : base(api)
        {

        }

        public IEnumerable<Event> GetForPerson(PersonStub person, DateTime since)
        {
            return GetForProject(person.Id, since);
        }

        public IEnumerable<Event> GetForPerson(int personId, DateTime since)
        {
            ///people/1/events.json?since=2012-03-24T11:00:00-06:00
            var action = "/people/{0}/events.json?since={1}".FormatWith(personId, since.ToString("s") + since.ToString("zzz"));
            return GetInner(action);
        }

        public IEnumerable<Event> GetForProject(ProjectStub project, DateTime since)
        {
            return GetForProject(project.Id, since);
        }

        public IEnumerable<Event> GetForProject(int projectId, DateTime since)
        {
            //projects/1/events.json?since=2012-03-24T11:00:00-06:00
            var action = "/projects/{0}/events.json?since={1}".FormatWith(projectId, since.ToString("s") + since.ToString("zzz"));
            return GetInner(action);
        }

        public IEnumerable<Event> Get(DateTime since)
        {
            var action = "/events.json?since={0}".FormatWith(since.ToString("s") + since.ToString("zzz"));
            return GetInner(action);
        }

        private IEnumerable<Event> GetInner(string action)
        {
            var allEvents = new List<Event>();

            var events = Api.Get<IEnumerable<Event>>(action);
            allEvents.AddRange(events);
            int page = 2;
            while (events.Count() == 50)
            {
                var nextPageAction = action + "&page=" + page++;
                events = Api.Get<IEnumerable<Event>>(nextPageAction);
                allEvents.AddRange(events);
            }

            return allEvents;
        }
    }
}
