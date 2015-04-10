using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Clonked.Basecamp.Managers
{
    public class PeopleManager : ManagerBase
    {

        private const int MAX_PAGES = 50;

        public PeopleManager(Api api) : base(api)
        {

        }

        public IEnumerable<PersonStub> Get()
        {
            int page = 1;

            List<PersonStub> allPeople = new List<PersonStub>();

            IEnumerable<PersonStub> currentRequest = Enumerable.Empty<PersonStub>();

            do {

                currentRequest = Api.Get<IEnumerable<PersonStub>>(string.Format("/people.json?page={0}", page));
                allPeople.AddRange(currentRequest);
                page += 1;

            } while (currentRequest.Count() > 0 & page < MAX_PAGES);
            
            return allPeople;
        }

        public Person Get(int personId)
        {
            var person = Api.Get<Person>("/people/{0}.json".FormatWith(personId));
            return person;
        }

        public Person GetCurrent()
        {
            var person = Api.Get<Person>("/people/me.json");
            return person;
        }

        public bool Delete(PersonStub person)
        {
            return Delete(person.Id);
        }

        public bool Delete(int personId)
        {
            return Api.Delete("/people/{0}.json".FormatWith(personId));
        }
    }
}
