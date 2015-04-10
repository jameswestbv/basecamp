using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class MessageManager : ManagerBase
    {
        public MessageManager(Api api) : base(api)
        {
        }

        public Message GetMessageForProject(int projectId, int messageId)
        {
            //projects/1/messages/1.json
            var action = "/projects/{0}/messages/{1}.json".FormatWith(projectId, messageId);
            var response = Api.Get<Message>(action);
            return response;
        }

        public Message Create(MessageCreateRequest createRequest)
        {
            var content = Api.Post<Message>("/projects/{0}/messages.json".FormatWith(createRequest.ProjectId), createRequest);
            content.ProjectId = createRequest.ProjectId;
            return content;
        }

        public bool Update(Message message)
        {
            var action = "/projects/{0}/messages/{1}.json".FormatWith(message.ProjectId, message.Id);
            var updated = Api.Put<Message>(action, message);

            if (updated != null)
            {
                updated.ProjectId = message.ProjectId;
                ModelCopier.CopyModel(updated, message);
                return true;
            }

            return false;
        }

        public bool Delete(Message message)
        {
            return Delete(message.ProjectId, message.Id);
        }

        public bool Delete(int projectId, int messageId)
        {
            var action = "/projects/{0}/messages/{1}.json".FormatWith(projectId, messageId);
            return Api.Delete(action);
        }
    }
}
