using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public class ToDoListForPerson : ToDoListStub
    {
        [JsonProperty("assigned_todos")]
        public IEnumerable<ToDoListItemStub> AssignedToDos { get; set; }
    }
}
