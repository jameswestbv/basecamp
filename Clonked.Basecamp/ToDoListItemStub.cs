using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Text.RegularExpressions;

namespace Clonked.Basecamp
{
    /*
     * {
        "id": 223304243,
        "content": "Design it",
        "due_at": "2012-03-24",
        "comments_count": 0,
        "created_at": "2012-03-24T09:53:35-05:00",
        "updated_at": "2012-03-24T09:55:52-05:00",
        "assignee": {
          "id": 149087659,
          "type": "Person",
          "name": "Jason Fried"
        },
        "position": 1,
        "url": "https://basecamp.com/999999999/api/v1/projects/605816632-bcx/todos/223304243-design-it.json"
      },
     * */

    public class ToDoListItemStub : ApiStubObject<ToDoListItem>
    {
        [JsonIgnore]
        private int _projectId;
        public int ProjectId
        {
            get
            {
                if (_projectId == 0 && !string.IsNullOrWhiteSpace(Url))
                {
                    //https://basecamp.com/999999999/api/v1/projects/605816632-bcx/todolists/968316918-launch-list.json
                    var regex = new Regex(@"projects\/(\d+)\-");
                    if (regex.IsMatch(Url))
                    {
                        var match = regex.Match(Url);
                        int.TryParse(match.Groups[1].Value, out _projectId);
                    }
                }

                return _projectId;
            }
            set
            {
                _projectId = value;
            }
        }
        [JsonProperty("todolist_id")]
        public int ToDoListId { get; set; }

        [JsonProperty("id")]
        public int Id { get; set; }
        [JsonProperty("content")]
        public string Content { get; set; }
        [JsonProperty("due_at")]
        public DateTime? DueAt { get; set; }
        [JsonProperty("comments_count")]
        public int CommentCount { get; set; }
        [JsonProperty("created_at")]
        public DateTime? CreatedAt { get; set; }
        [JsonProperty("updated_at")]
        public DateTime? UpdatedAt { get; set; }       
        [JsonProperty("assignee")]
        public PersonStub Assignee { get; set; }
        [JsonProperty("position")]
        public int Position { get; set; }
        [JsonProperty("completed")]
        public bool Completed { get; set; }
        [JsonProperty("url")]
        public string Url { get; set; }

        public override ToDoListItem GetDetail()
        {
            if (!string.IsNullOrWhiteSpace(Url))
            {
                return Api.GetResponseFromUrl<ToDoListItem>(Url);
            }

            return Api.ToDoLists.GetToListItemDetail(ProjectId, ToDoListId);
        }



    }
}
