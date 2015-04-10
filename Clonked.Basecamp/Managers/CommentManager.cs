using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class CommentManager : ManagerBase
    {
        public CommentManager(Api api) : base(api)
        {

        }

        public Comment CreateForMessage(int projectId, int messageId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/messages/{1}/comments.json".FormatWith(projectId, messageId);
            return CreateInner(action, comment);
        }

        public Comment CreateForToDoList(int projectId, int todDoListId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/todolists/{1}/comments.json".FormatWith(projectId, todDoListId);
            return CreateInner(action, comment);
        }

        public Comment CreateForToDoListItem(int projectId, int toDoListItemId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/todos/{1}/comments.json".FormatWith(projectId, toDoListItemId);
            return CreateInner(action, comment);
        }

        public Comment CreateForCalendarEvent(int projectId, int calendarEventId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/calendar_events/{1}/comments.json".FormatWith(projectId, calendarEventId);
            return CreateInner(action, comment);
        }

        public Comment CreateForUpload(int projectId, int uploadId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/uploads/{1}/comments.json".FormatWith(projectId, uploadId);
            return CreateInner(action, comment);
        }

        public Comment CreateForDocument(int projectId, int documentId, CommentCreateRequest comment)
        {
            var action = "projects/{0}/documents/{1}/comments.json".FormatWith(projectId, documentId);
            return CreateInner(action, comment);
        }

        internal Comment CreateInner(string action, CommentCreateRequest comment)
        {
            var response = Api.Post<Comment>(action, comment);
            return response;
        }

        public bool Delete(int projectId, int commentId)
        {
            var action = "projects/{0}/comments/{1}.json".FormatWith(projectId, commentId);
            return Api.Delete(action);
        }
    }
}
