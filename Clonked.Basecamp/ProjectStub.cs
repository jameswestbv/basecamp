using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     *   {
    "id": 605816632,
    "name": "BCX",
    "description": "The Next Generation",
    "updated_at": "2012-03-23T13:55:43-05:00",
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx.json",
    "archived": false
    "starred": true
    }
     * */
    public class ProjectStub : ApiStubObject<Project>
    {
        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("archived")]
        public bool Archived { get; set; }
        [JsonProperty("starred")]
        public bool Starred { get; set; }

        public override Project GetDetail()
        {
            var project = Api.GetResponseFromUrl<Project>(Url);
            project.Api = Api;
            return project;
        }
    }
}
