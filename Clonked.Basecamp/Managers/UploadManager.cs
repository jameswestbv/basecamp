using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Clonked.Basecamp.CreateRequests;

namespace Clonked.Basecamp.Managers
{
    public class UploadManager : ManagerBase
    {
        public UploadManager(Api api) : base(api)
        {

        }
        public Upload CreateForProject(int projectId, string content, string fileName, Stream stream, IEnumerable<int> peopleIds = null)
        {
            var token = Api.Attachments.CreateAttachmentToken(stream, fileName);
            return CreateForProject(projectId, content, token, peopleIds);
        }

        public Upload CreateForProject(int projectId, string content, string filePath, IEnumerable<int> peopleIds = null)
        {
            var token = Api.Attachments.CreateAttachmentToken(filePath);
            return CreateForProject(projectId, content, token, peopleIds);
        }

        public Upload CreateForProject(int projectId, string content, AttachmentToken token, IEnumerable<int> peopleIds = null)
        {
            var request = new UploadCreateRequest()
            {
                Content = content,
                ProjectId = projectId,
                Subscribers = peopleIds
            };
            request.SetAttachmentToken(token);

            return Create(request);
        }

        public Upload Create(UploadCreateRequest request)
        {
            var action = "projects/{0}/uploads.json".FormatWith(request.ProjectId);
            var response = Api.Post<Upload>(action, request);
            return response;
        }

        public Upload GetForProject(int projectId, int uploadId)
        {
            ///projects/1/uploads/2.json
            var action = "projects/1/uploads/{1}.json".FormatWith(projectId, uploadId);
            var response = Api.Get<Upload>(action);
            return response;
        }
    }
}
