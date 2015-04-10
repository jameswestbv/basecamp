using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Clonked.Basecamp.Managers
{
    public class AttachmentManager : ManagerBase
    {
        public AttachmentManager(Api api) : base(api)
        {
        }

        public IEnumerable<Attachment> Get()
        {
            var action = "/attachments.json";
            return GetInner(action);
        }

        public AttachmentToken CreateAttachmentToken(Stream stream, string filename)
        {
            var bytes = new byte[0];

            var length = (int)stream.Length;
            bytes = new byte[(int)stream.Length];
            stream.Read(bytes, 0, length);

            var extension = System.IO.Path.GetExtension(filename);

            var request = Api.GetRequestForAction("/attachments.json");
            var httpRequest = request.CreateWebReqeuest();
            httpRequest.ContentType = Api.MimeTypeResolver.GetMimeType(extension);
            httpRequest.ContentLength = bytes.Length;
            httpRequest.Method = HttpRequestMethod.Post;

            using (var writer = new BinaryWriter(httpRequest.GetRequestStream()))
            {
                writer.Write(bytes);
            }

            var response = request.GetResponse<AttachmentToken>(HttpRequestMethod.Post);
            var token = response.Content;
            token.Name = System.IO.Path.GetFileName(filename);
            return token;
        }

        public AttachmentToken CreateAttachmentToken(string filePath)
        {
            using (var stream = System.IO.File.OpenRead(filePath))
            {
                return CreateAttachmentToken(stream, filePath);
            }
        }

        public IEnumerable<Attachment> GetForProject(ProjectStub project)
        {
            return GetForProject(project.Id);
        }

        public IEnumerable<Attachment> GetForProject(int projectId)
        {
            var action = "projects/{0}/attachments.json".FormatWith(projectId);
            return GetInner(action);
        }

        private IEnumerable<Attachment> GetInner(string action)
        {
            var allAttachments = new List<Attachment>();

            var attachments = Api.Get<IEnumerable<Attachment>>(action);
            allAttachments.AddRange(attachments);
            int page = 2;
            while (attachments.Count() == 50)
            {
                var nextPageAction = action + "&page=" + page++;
                attachments = Api.Get<IEnumerable<Attachment>>(nextPageAction);
                allAttachments.AddRange(attachments);
            }

            return allAttachments;
        }
    }
}
