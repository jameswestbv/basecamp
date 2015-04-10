using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clonked.Basecamp.Collections;
using Newtonsoft.Json;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp
{
    public class ToDoList : ToDoListStub
    {
        
        private ToDoListItemCollection _todos;
        [JsonProperty("todos")]
        public ToDoListItemCollection ToDos
        {
            get
            {
                if (_todos == null)
                {
                    _todos = new ToDoListItemCollection();
                    _todos.Api = Api;
                    _todos.List = this;
                }
                return _todos;
            }
            internal set
            {
                _todos = value;
                if (_todos != null)
                {
                    _todos.Api = Api;
                    _todos.List = this;
                }

            }
        }

    }
}
