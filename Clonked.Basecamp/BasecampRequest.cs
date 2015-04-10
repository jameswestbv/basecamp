using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;
using Newtonsoft.Json;
using System.Collections;
using System.Security.Cryptography;
using System.Net.Cache;

namespace Clonked.Basecamp
{
    public class BasecampRequest
    {
        public Api Api { get; set; }
        private string Action { get; set; }
        public string Url { get; private set; }
        public string RequestBody { get; set; }
        public string ContentType { get; set; }
        public long ContentLength { get; set; }
        internal ICachePackage CachePackage { get; set; }

        public BasecampRequest(Api api)
        {
            Api = api;
            ContentType = "application/json; charset=utf-8";
        }

        public BasecampRequest(Api api, Uri absoluteUrl)
            : this(api)
        {
            Url = absoluteUrl.ToString();
        }

        public BasecampRequest(Api api, string action)
            : this(api)
        {
            Action = action.TrimStart('/');
            Url = GetRequestUrl();
        }

        private HttpWebRequest _request;

        internal HttpWebRequest PrepareBaseRequest()
        {
            var request = (HttpWebRequest)HttpWebRequest.Create(Url);
            SetAuthorization(request, Api.Authentication.GetCredentials());
            request.UserAgent = "Clonked.Basecamp C# Api Implementation";
            request.CachePolicy = new RequestCachePolicy(RequestCacheLevel.CacheIfAvailable);
            return request;
        }

        // learned from http://stackoverflow.com/questions/4328439/httpwebrequest-with-caching-enabled-throws-exceptions
        // Manual construction of HTTP basic auth so we don't get an unnecessary server
        // roundtrip telling us to auth, which is what we get if we simply use
        // HttpWebRequest.Credentials.
        private void SetAuthorization(HttpWebRequest request, NetworkCredential credentials)
        {
            string userAndPass = string.Format("{0}:{1}", credentials.UserName, credentials.Password);
            byte[] authBytes = Encoding.UTF8.GetBytes(userAndPass.ToCharArray());
            request.Headers["Authorization"] = "Basic " + Convert.ToBase64String(authBytes);
        }

        internal HttpWebRequest CreateWebReqeuest()
        {
            if (_request == null)
            {
                _request = PrepareBaseRequest();
            }

            return _request;
        }

        private string _cacheKey;
        internal string GetCacheKey()
        {
            if (!string.IsNullOrEmpty(_cacheKey))
            {
                return _cacheKey;
            }

            var key = Api.Authentication.GetCredentials().UserName + Url;
            using (MD5 md5Hash = MD5.Create())
            {
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(key));
                var sb = new StringBuilder();

                // Loop through each byte of the hashed data and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                {
                    sb.Append(data[i].ToString("x2"));
                }
                _cacheKey = sb.ToString();
            }

            return _cacheKey;
        }

        internal HttpWebRequest GetPreparedWebRequest(string method)
        {
            if (_request == null)
            {
                _request = PrepareBaseRequest();
                _request.Method = method;

                if (_request.Method == "GET")
                {
                    CachePackage = Api.ResponseCache.Get(GetCacheKey());
                    if (CachePackage != null)
                    {
                        if (CachePackage.LastModified != DateTime.MinValue)
                        {
                            _request.IfModifiedSince = CachePackage.LastModified;
                        }
                        if (!string.IsNullOrWhiteSpace(CachePackage.ETag))
                        {
                            _request.Headers.Add(HttpRequestHeader.IfNoneMatch, CachePackage.ETag);
                        }
                    }
                }

                if (!string.IsNullOrWhiteSpace(RequestBody))
                {
                    using (var writer = new StreamWriter(_request.GetRequestStream()))
                    {
                        writer.Write(RequestBody);
                    }
                    _request.ContentType = ContentType;
                }

                if (ContentLength > 0)
                {
                    _request.ContentLength = ContentLength;
                }
            }

            return _request;
        }

        public BasecampResponse GetResponse(string method = "GET")
        {
            var request = GetPreparedWebRequest(method);

            var responseBody = string.Empty;
            var statusCode = HttpStatusCode.InternalServerError;

            try
            {
                var httpResponse = (HttpWebResponse)request.GetResponse();
                statusCode = httpResponse.StatusCode;
                if (statusCode == HttpStatusCode.NotModified)
                {
                    responseBody = CachePackage.ResponseBody;
                }
                else
                {
                    using (var reader = new StreamReader(httpResponse.GetResponseStream()))
                    {
                        responseBody = reader.ReadToEnd();
                    }

                    if (method == "GET")
                    {
                        var package = new CachePackage()
                        {
                            Key = GetCacheKey(),
                            LastModified = httpResponse.LastModified,
                            ETag = httpResponse.Headers["ETag"],
                            ResponseBody = responseBody,
                            Url = Url
                        };

                        Api.ResponseCache.Insert(package);
                    }
                }

            }
            catch (WebException ex)
            {
                if (ex.Status == WebExceptionStatus.ProtocolError)
                {
                    statusCode = ((HttpWebResponse)ex.Response).StatusCode;
                    if (statusCode == HttpStatusCode.NotModified)
                    {
                        responseBody = CachePackage.ResponseBody;
                    }
                }
                else
                {
                    throw ex;
                }
            }

            return new BasecampResponse(Api)
            {
                StatusCode = statusCode,
                ResponseBody = responseBody
            };
        }

        public BasecampResponse<T> GetResponse<T>(string method = "GET")
        {
            var baseCampResponse = GetResponse(method);
            var response = new BasecampResponse<T>(Api, baseCampResponse);
            var content = response.Content;
            return response;
        }

        private string GetRequestUrl()
        {
            return Api.Authentication.GetEndPointUrlBase() + Action;
        }
    }
}
