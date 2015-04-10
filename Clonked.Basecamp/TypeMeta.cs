using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    /*
     * "attachable": {
      "id": 70219655,
      "type": "Upload",
      "url": "https://basecamp.com/1111/api/v1/projects/2222/uploads/70219655.json"
    }
     * */
    public class TypeMeta
    {
        TypeMeta()
        {

        }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("type")]
        public string Type { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
    }
}
