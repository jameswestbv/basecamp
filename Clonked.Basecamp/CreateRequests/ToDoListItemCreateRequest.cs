using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp.CreateRequests
{
    public class ToDoListItemCreateRequest
    {
        [JsonIgnore]
        public int ProjectId { get; set; }
        [JsonIgnore]
        public int ToDoListId { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("due_at")]
        public DateTime? DueAt { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("assignee")]
        public PersonStub Assignee { get; set; }
    }
}
