using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class ToDoListManager : ManagerBase
    {
        public ToDoListManager(Api api) : base(api)
        {

        }

        public IEnumerable<ToDoListStub> GetActive()
        {
            var action = "/todolists.json";
            return GetToDoListsForAction(action);
        }

        public IEnumerable<ToDoListStub> GetCompleted()
        {
            var action = "/todolists/completed.json";
            return GetToDoListsForAction(action);
        }

        public ToDoListItem GetToListItemDetail(int projectId, int todoListItemId)
        {
            if (projectId == 0)
            {
                throw new ArgumentException("The project id cannot be 0", "projectId");
            }

            var action = "/projects/{0}/todos/{1}.json".FormatWith(projectId, todoListItemId);
            var request = Api.GetRequestForAction(action);
            var response = request.GetResponse<ToDoListItem>();
            return response.Content;
        }

        public ToDoListItem Create(ToDoListItemCreateRequest createRequest)
        {
            //projects/1/todolists/1/todos.json
            var action = "/projects/{0}/todolists/{1}/todos.json".FormatWith(createRequest.ProjectId, createRequest.ToDoListId);
            var content = Api.Post<ToDoListItem>(action, createRequest);

            if (content != null)
            {
                content.ProjectId = createRequest.ProjectId;
            }

            return content;
        }

        public bool Update(ToDoListItemStub listItem)
        {
            var action = "/projects/{0}/todos/{1}.json".FormatWith(listItem.ProjectId, listItem.Id);
            var content = Api.Put<ToDoListStub>(action, listItem);

            if (content != null)
            {
                content.ProjectId = listItem.ProjectId;
                ModelCopier.CopyModel(content, listItem);
                return true;
            }

            return false;
        }

        public ToDoList Create(ToDoListCreateRequest createRequest)
        {
            var action = "/projects/{0}/todolists.json".FormatWith(createRequest.ProjectId);
            var todolistlist = Api.Post<ToDoList>(action, createRequest);

            if (todolistlist != null)
            {
                todolistlist.ProjectId = createRequest.ProjectId;
            }

            return todolistlist;
        }

        public bool Update(ToDoListStub list)
        {
            var action = "/projects/{0}/todolists/{1}.json".FormatWith(list.ProjectId, list.Id);
            var content = Api.Put<ToDoList>(action, list);
            
            if (content != null)
            {
                content.ProjectId = list.ProjectId;
                ModelCopier.CopyModel(content, list);
                return true;
            }

            return false;
        }

        public bool Delete(ToDoListItemStub item)
        {
            //DELETE /projects/1/todos/1.json
            var action = "/projects/{0}/todos/{1}.json".FormatWith(item.ProjectId, item.Id);
            return Api.Delete(action);
        }

        public bool Delete(ToDoListStub list)
        {
            var action = "/projects/{0}/todolists/{1}.json".FormatWith(list.ProjectId, list.Id);
            return Api.Delete(action);
        }

        public ToDoList GetDetail(int projectId, int toDoListId)
        {
            if (projectId == 0)
            {
                throw new ArgumentException("The project id cannot be 0", "projectId");
            }

            var action = "/projects/{0}/todolists/{1}.json".FormatWith(projectId, toDoListId);
            var list = this.Api.Get<ToDoList>(action);
            list.ProjectId = projectId;
            return list;
        }

        public IEnumerable<ToDoListForPerson> GetToDosAssignedToPerson(int personId)
        {
            //people/1/assigned_todos.json
            var action = "/people/{0}/assigned_todos.json".FormatWith(personId);
            var lists = this.Api.Get<IEnumerable<ToDoListForPerson>>(action);
            return lists;
        }

        public IEnumerable<ToDoListStub> GetActiveForProject(ProjectStub project)
        {
            return GetActiveForProject(project.Id);
        }

        public IEnumerable<ToDoListStub> GetActiveForProject(int projectId)
        {
            var action = "/projects/{0}/todolists.json".FormatWith(projectId);
            return GetToDoListsForAction(projectId, action);
        }

        public IEnumerable<ToDoListStub> GetCompletedForProject(ProjectStub project)
        {
            return GetActiveForProject(project.Id);
        }

        public IEnumerable<ToDoListStub> GetCompletedForProject(int projectId)
        {
            var action = "/projects/{0}/todolists/completed.json".FormatWith(projectId);
            return GetToDoListsForAction(projectId, action);
        }

        private IEnumerable<ToDoListStub> GetToDoListsForAction(string action)
        {
            var lists = this.Api.Get<IEnumerable<ToDoListStub>>(action);
            return lists;
        }

        private IEnumerable<ToDoListStub> GetToDoListsForAction(int projectId, string action)
        {
            var lists = this.Api.Get<IEnumerable<ToDoListStub>>(action);
            if (lists != null)
            {
                foreach (var list in lists)
                {
                    list.ProjectId = projectId;
                   
                }
            }
            return lists;
        }
    }
}
