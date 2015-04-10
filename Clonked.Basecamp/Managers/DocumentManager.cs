using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class DocumentManager : ManagerBase
    {
        public DocumentManager(Api api) : base(api)
        {
        }

        public IEnumerable<DocumentStub> Get()
        {
            var action = "documents.json";
            return Api.Get<IEnumerable<DocumentStub>>(action);
        }

        public IEnumerable<DocumentStub> GetForProject(int projectId)
        {
            var action = "projects/{0}/documents.json".FormatWith(projectId);
            return Api.Get<IEnumerable<DocumentStub>>(action);
        }

        public Document GetForProject(int projectId, int documentId)
        {
            var action = "projects/{0}/documents/{1}.json".FormatWith(projectId, documentId);
            return Api.Get<Document>(action);
        }

        public Document Create(DocumentCreateRequest request)
        {
            var action = "projects/{0}/documents.json".FormatWith(request.ProjectId);
            var document = Api.Post<Document>(action, request);
            document.ProjectId = request.ProjectId;
            return document;
        }

        public bool Update(Document document)
        {
            var action = "projects/{0}/documents/{1}.json".FormatWith(document.ProjectId, document.Id);
            var updateProperties = new DocumentCreateRequest();
            ModelCopier.CopyModel(document, updateProperties);
            updateProperties.SubscriberIds = document.Subscribers.Select(s => s.Id);
            var updatedDocument = Api.Put<Document>(action, updateProperties);
            if (updatedDocument != null)
            {
                updatedDocument.ProjectId = document.ProjectId;
                ModelCopier.CopyModel(updatedDocument, document);
                return true;
            }

            return false;
        }

        public bool Delete(Document document)
        {
            return Delete(document.ProjectId, document.Id);
        }

        public bool Delete(int projectId, int documentId)
        {
            var action = "projects/{0}/documents/{1}.json".FormatWith(projectId, documentId);
            return Api.Delete(action);
        }
    }
}
