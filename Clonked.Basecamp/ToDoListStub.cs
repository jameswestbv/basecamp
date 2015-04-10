using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;
using Clonked.Basecamp.Collections;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp
{
    /*
     * {
    "id": 968316918,
    "name": "Launch list",
    "description": "What we need for launch",
    "updated_at": "2012-03-22T16:56:52-05:00",
    "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/todolists/968316918-launch-list.json",
    "completed": false,
    "position": 1,
    "completed_count": 3,
    "remaining_count": 5,
    "creator": {
      "id": 127326141,
      "name": "David Heinemeier Hansson",
      "avatar_url": "https://asset0.37img.com/global/9d2148cb8ed8e2e8ecbc625dd1cbe7691896c7d9/avatar.96.gif?r=3"
    },
  },
     * */

    public class ToDoListStub : ApiStubObject<ToDoList>
    {
        [JsonProperty("id")]
        public int Id { get; set; }

        [JsonIgnore]
        public int ProjectId
        {
            get;
            set;
        }

        [JsonProperty("name")]
        public string Name { get; set; }
        [JsonProperty("description")]
        public string Description { get; set; }
        [JsonProperty("updated_at")]
        public DateTime UpdatedAt { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("completed_count")]
        public int NumberCompleted { get; set; }
        [JsonProperty("remaining_count")]
        public int NumberRemaining { get; set; }
        [JsonProperty("creator")]
        public PersonStub Creator { get; set; }

        [JsonProperty("completed")]
        public bool Completed { get; set; }

        public override ToDoList GetDetail()
        {
            if (Api != null)
            {
                if (!string.IsNullOrEmpty(Url))
                {
                    return Api.GetResponseFromUrl<ToDoList>(Url);
                }
                return Api.ToDoLists.GetDetail(ProjectId, Id);
            }

            return null;
        }

        public ToDoListItem CreateToDo(string content, DateTime? dueAt = null, PersonStub assignee = null)
        {
            var createRequest = new ToDoListItemCreateRequest()
            {
                ProjectId = this.ProjectId,
                ToDoListId = this.Id,
                Content = content,
                DueAt = dueAt,
                Assignee = assignee
            };

            var item = Api.ToDoLists.Create(createRequest);
            return item;
        }

        public bool UpdateToDo(ToDoListItemStub item)
        {
            return Api.ToDoLists.Update(item);
        }


    }
}
