using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using Clonked.Basecamp.Managers;

namespace Clonked.Basecamp.Collections
{
    [JsonObjectAttribute()]
    public class ToDoListItemCollection : ApiObject, IEnumerable<ToDoListItemStub> 
    {
        internal ToDoListStub List { get; set; }

        [JsonConstructor()]
        internal ToDoListItemCollection()
        {

        }

        private IEnumerable<ToDoListItemStub> _activeToDos;
        [JsonProperty("remaining")]
        public IEnumerable<ToDoListItemStub> ActiveToDos 
        { 
            get
            {
                return _activeToDos;
            }
            internal set
            {
                _activeToDos = AssociateValues(value);
            }
        }

        private IEnumerable<ToDoListItemStub> _completedToDos;
        [JsonProperty("completed")]
        public IEnumerable<ToDoListItemStub> CompletedToDos 
        {
            get
            {
                return _completedToDos;
            }
            internal set
            {
                _completedToDos = AssociateValues(value, isCompleted: true);
            }
        }

        private IEnumerable<ToDoListItemStub> AssociateValues(IEnumerable<ToDoListItemStub> items, bool isCompleted = false)
        {
            if (items == null)
            {
                return null;
            }

            var todoitems = items.ToList();
            todoitems.ForEach(l =>
            {
                l.ProjectId = List.ProjectId;
                l.ToDoListId = List.Id;
                l.Completed = isCompleted;
            });
            return todoitems;
        }

        private void CombineLists()
        {
            var items = new List<ToDoListItemStub>();
            if (CompletedToDos != null)
            {
                items.AddRange(CompletedToDos);
            }
            if (ActiveToDos != null)
            {
                items.AddRange(ActiveToDos);
            }
            _combinedItems = items;
        }

        private IEnumerable<ToDoListItemStub> _combinedItems;

        public IEnumerator<ToDoListItemStub> GetEnumerator()
        {
            CombineLists();

            return _combinedItems.GetEnumerator();
        }

        System.Collections.IEnumerator System.Collections.IEnumerable.GetEnumerator()
        {
            return _combinedItems.GetEnumerator();
        }

    }
}
