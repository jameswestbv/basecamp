using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace Clonked.Basecamp.Managers
{
    public class AccessManager : ManagerBase
    {

        private const int MAX_PAGES = 50;

        public AccessManager(Api api) : base(api)
        {

        }

        public IEnumerable<PersonStub> GetPeopleWithAccessToProject(ProjectStub project)
        {
            return GetPeopleWithAccessToProject(project.Id);
        }

        //projects/1/accesses.json
        public IEnumerable<PersonStub> GetPeopleWithAccessToProject(int projectId)
        {
           
            int page = 1;

            List<PersonStub> allPeople = new List<PersonStub>();

            IEnumerable<PersonStub> currentRequest = Enumerable.Empty<PersonStub>();
                     
            do
            {
                currentRequest = Api.Get<IEnumerable<PersonStub>>(string.Format("/projects/{0}/accesses.json?page={1}", projectId, page));
                allPeople.AddRange(currentRequest);
                page += 1;

            } while (currentRequest.Count() > 0 & page < MAX_PAGES);

            return allPeople;
                       
        }

        public IEnumerable<PersonStub> GetPeopleWithAccessToCalendar(CalendarStub calendar)
        {
            return GetPeopleWithAccessToProject(calendar.Id);
        }

        //calendars/1/accesses.json
        public IEnumerable<PersonStub> GetPeopleWithAccessToCalendar(int calendarId)
        {
            var action = "/calendars/{0}/accesses.json".FormatWith(calendarId);
            var people = Api.Get<IEnumerable<PersonStub>>(action);
            return people;
        }

        
        public bool RemoveAccessToProject(ProjectStub project, PersonStub person)
        {
            return RemoveAccessToProject(project.Id, person.Id);
        }

        public bool RemoveAccessToProject(int projectId, int personId)
        {
            //DELETE /projects/1/accesses/1.json
            var action = "/projects/{0}/accesses/{1}.json".FormatWith(projectId, personId);
            return Api.Delete(action);
        }

        public bool RemoveAccessToCalendar(CalendarStub calendar, PersonStub person)
        {
            return RemoveAccessToProject(calendar.Id, person.Id);
        }

        public bool RemoveAccessToCalendar(int calendarId, int personId)
        {
            //DELETE /calendars/1/accesses/1.json
            var action = "/calendars/{0}/accesses/{1}.json".FormatWith(calendarId, personId);
            return Api.Delete(action);
        }

        public bool GrantAccessToProject(ProjectStub project, IEnumerable<PersonStub> people)
        {
            return GrantAccessToProject(project.Id, people.Select(p => p.Id));
        }

        public bool GrantAccessToProject(int projectId, IEnumerable<int> peopleIds)
        {
            var createRequest = new GrantAccessToAccountRequest()
            {
                Ids = peopleIds
            };
            var action = "/projects/{0}/accesses.json".FormatWith(projectId);
            return GrantAccessInner(action, createRequest);
        }

        public bool GrantAccessToProject(ProjectStub project, IEnumerable<string> emailAddresses)
        {
            return GrantAccessToProject(project.Id, emailAddresses);
        }

        public bool GrantAccessToProject(int projectId, IEnumerable<string> emailAddresses)
        {
            var createRequest = new GrantAccessToAccountRequest()
            {
                EmailAddress = emailAddresses
            };
            var action = "/projects/{0}/accesses.json".FormatWith(projectId);
            return GrantAccessInner(action, createRequest);
        }


        public bool GrantAccessToCalendar(CalendarStub calendar, IEnumerable<PersonStub> people)
        {
            return GrantAccessToProject(calendar.Id, people.Select(p => p.Id));
        }

        public bool GrantAccessToCalendar(int calendarId, IEnumerable<int> peopleIds)
        {
            var createRequest = new GrantAccessToAccountRequest()
            {
                Ids = peopleIds
            };
            var action = "/calendars/{0}/accesses.json".FormatWith(calendarId);
            return GrantAccessInner(action, createRequest);
        }

        public bool GrantAccessToCalendar(CalendarStub calendar, IEnumerable<string> emailAddresses)
        {
            return GrantAccessToProject(calendar.Id, emailAddresses);
        }

        public bool GrantAccessToCalendar(int calendarId, IEnumerable<string> emailAddresses)
        {
            var createRequest = new GrantAccessToAccountRequest()
            {
                EmailAddress = emailAddresses
            };
            var action = "/calendars/{0}/accesses.json".FormatWith(calendarId);

            return GrantAccessInner(action, createRequest);
        }


        private bool GrantAccessInner(string action, GrantAccessToAccountRequest createRequest)
        {
            //POST /projects/1/accesses.json

            
            var request = Api.GetRequestForAction(action);
            request.RequestBody = JsonConvert.SerializeObject(createRequest, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var response = request.GetResponse(HttpRequestMethod.Post);
            return response.StatusCode == System.Net.HttpStatusCode.NoContent;
        }


        /*
         * {
              "ids": [ 5, 6, 10 ],
              "email_addresses": [ "someone@example.com", "someoneelse@example.com" ]
            }
         * */
        private class GrantAccessToAccountRequest
        {
            [JsonProperty("ids")]
            public IEnumerable<int> Ids { get; set; }
            [JsonProperty("email_addresses")]
            public IEnumerable<string> EmailAddress { get; set; }
        }
            
    }
}
