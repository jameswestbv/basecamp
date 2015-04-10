using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.Collections
{
    public class CalendarEventCollection : ApiObject, IEnumerable<CalendarEventStub>
    {
        internal CalendarStub Calendar { get; set; }

        [JsonConstructor()]
        internal CalendarEventCollection()
        {
            _combinedItems = new CalendarEventStub[0];
        }

        private IEnumerable<CalendarEventStub> _upcomingEvents;

        public IEnumerable<CalendarEventStub> Upcoming 
        { 
            get
            {
                if (Api != null)
                {
                    _upcomingEvents = GetUpcoming();
                }
                return _upcomingEvents;
            }
            internal set
            {
                _upcomingEvents = AssociateValues(value);
            }
        }

        protected virtual IEnumerable<CalendarEventStub> GetUpcoming()
        {
            return Api.Calendars.GetUpcomingEventsForCalendar(Calendar.Id);
        }

        private IEnumerable<CalendarEventStub> _past;

        public IEnumerable<CalendarEventStub> Past 
        {
            get
            {
                if (Api != null)
                {
                    _past = GetPast();
                }

                return _past;
            }
            internal set
            {
                _past = AssociateValues(value);
            }
        }

        protected virtual IEnumerable<CalendarEventStub> GetPast()
        {
            return Api.Calendars.GetPastEventsForCalendar(Calendar.Id);
        }

        private IEnumerable<CalendarEventStub> AssociateValues(IEnumerable<CalendarEventStub> items)
        {
            if (items == null)
            {
                return null;
            }

            return items;
        }

        private void CombineLists()
        {
            var items = new List<CalendarEventStub>();
            if (Past != null)
            {
                items.AddRange(Past);
            }
            if (Upcoming != null)
            {
                items.AddRange(Upcoming);
            }
            _combinedItems = items;
        }

        private IEnumerable<CalendarEventStub> _combinedItems;

        public IEnumerator<CalendarEventStub> GetEnumerator()
        {
            CombineLists();

            return _combinedItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _combinedItems.GetEnumerator();
        }
    }
}
