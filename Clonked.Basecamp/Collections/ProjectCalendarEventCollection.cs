using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp.Collections
{
    public class ProjectCalendarEventCollection : CalendarEventCollection
    {
        internal ProjectStub Project { get;set;}

        public ProjectCalendarEventCollection(): base()
        {

        }

        protected override IEnumerable<CalendarEventStub> GetUpcoming()
        {
            return Api.Calendars.GetUpcomingEventsForProject(Project.Id);
        }

        protected override IEnumerable<CalendarEventStub> GetPast()
        {
            return Api.Calendars.GetPastEventsForProject(Project.Id);
        }
    }
}
