using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *   {
    "id": 149087659,
    "name": "Jason Fried",
    "email_address": "jason@37signals.com",
    "avatar_url": "https://asset0.37img.com/global/4113d0a133a32931be8934e70b2ea21efeff72c1/avatar.96.gif?r=3",
    "updated_at": "2012-03-22T16:56:48-05:00",
    "url": "https://basecamp.com/999999999/api/v1/people/149087659-jason-fried.json"
  },
     * */

    public class PersonStub : ApiStubObject<Person>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }

        public override Person GetDetail()
        {
            return Api.People.Get(Id);
        }
    }
}
