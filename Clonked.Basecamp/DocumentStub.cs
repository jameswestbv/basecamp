using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *  {
    "id": 963979453,
    "title": "Manifesto",
    "updated_at": "2012-03-27T13:39:33-05:00",
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/documents/963979453-manifesto.json"
  }
     **/
    public class DocumentStub : ApiStubObject<Document>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("title")]
        public int Title { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public override Document GetDetail()
        {
            return Api.GetResponseFromUrl<Document>(Url);
        }
    }
}
