using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Clonked.Basecamp.Managers;
using System.Net;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public class Api
    {
        private static IResponseCache _defaultResponseCache = new DictionaryResponseCache();
        public static IResponseCache DefaultResponseCache { get { return _defaultResponseCache; } set { _defaultResponseCache = value; } }

        public IMimeTypeResolver MimeTypeResolver { get; set; }
        public IAuthenticationCredentials Authentication { get; set; }
        public IResponseCache ResponseCache { get; set; }

        public Api(IAuthenticationCredentials credentials) : this()
        {
            Authentication = credentials;   
        }

        public Api(IAuthenticationCredentials credentials, IResponseCache cache) : this(credentials)
        {
            ResponseCache = cache;
        }

        public Api(int accountId, string username, string password) : this()
        {
            Authentication = new BasicAuthenticationCredentials()
            {
                UserName = username,
                Password = password,
                AccountId = accountId
            };
        }

        public Api()
        {
            Projects = new ProjectManager(this);
            ToDoLists = new ToDoListManager(this);
            People = new PeopleManager(this);
            Events = new EventManager(this);
            Accesses = new AccessManager(this);
            Calendars = new CalendarManager(this);
            Topics = new TopicManager(this);
            Messages = new MessageManager(this);
            Attachments = new AttachmentManager(this);
            Uploads = new UploadManager(this);
            Comments = new CommentManager(this);
            Documents = new DocumentManager(this);

            MimeTypeResolver = new MimeTypeResolver();
            ResponseCache = Api.DefaultResponseCache;
        }

        public ProjectManager Projects { get; private set; }
        public ToDoListManager ToDoLists { get; private set; }
        public PeopleManager People { get; private set; }
        public EventManager Events { get; private set; }
        public AccessManager Accesses { get; private set; }
        public CalendarManager Calendars { get; private set; }
        public TopicManager Topics { get; private set; }
        public MessageManager Messages { get; private set; }
        public AttachmentManager Attachments { get; private set; }
        public UploadManager Uploads { get; private set; }
        public CommentManager Comments { get; private set; }
        public DocumentManager Documents { get; private set; }

        public BasecampRequest GetRequest()
        {
            var request = new BasecampRequest(this);
            return request;
        }

        public BasecampRequest GetRequest(string absoluteUrl)
        {
            var request = new BasecampRequest(this, new Uri(absoluteUrl));
            return request;
        }

        public BasecampRequest GetRequestForAction(string action)
        {
            var request = new BasecampRequest(this, action);
            return request;
        }

        public T GetResponseFromUrl<T>(string absoluteUrl)
        {
            var request = new BasecampRequest(this, new Uri(absoluteUrl));
            var response = request.GetResponse<T>();
            return response.Content;
        }

        public T Get<T>(string action)
        {
            var request = new BasecampRequest(this, action);
            var response = request.GetResponse<T>();
            return response.Content;
        }

        public bool Delete(string action)
        {
            var request = GetRequestForAction(action);
            var response = request.GetResponse(HttpRequestMethod.Delete);
            return response.StatusCode == HttpStatusCode.NoContent;
        }

        public T Put<T>(string action, object content) where T : class
        {
            var request = GetRequestForAction(action);
            request.RequestBody = JsonConvert.SerializeObject(content, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var response = request.GetResponse<T>(HttpRequestMethod.Put);

            if (response.StatusCode == HttpStatusCode.OK)
            {
                return response.Content;
            }

            return null;
        }

        public T Post<T>(string action, object content) where T : class
        {
            var request = GetRequestForAction(action);
            request.RequestBody = JsonConvert.SerializeObject(content, new JsonSerializerSettings() { NullValueHandling = NullValueHandling.Ignore });
            var response = request.GetResponse<T>(HttpRequestMethod.Post);

            if (response.StatusCode == HttpStatusCode.Created)
            {
                return response.Content;
            }

            return null;
        }
    }
}
