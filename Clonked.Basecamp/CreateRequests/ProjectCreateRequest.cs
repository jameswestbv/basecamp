using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    public class ProjectCreateRequest
    {
        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("archived")]
        public bool Archived { get; set; }
        [JsonProperty("starred")]
        public bool Starred { get; set; }
    }
}
