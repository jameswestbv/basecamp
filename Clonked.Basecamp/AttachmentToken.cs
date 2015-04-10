using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public class AttachmentToken
    {
        [JsonProperty("token")]
        public string Token { get; set; }
        [JsonProperty("name")]
        public string Name { get; set; }
    }
}
