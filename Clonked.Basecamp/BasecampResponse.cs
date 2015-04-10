using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;
using System.Net;

namespace Clonked.Basecamp
{
    public class BasecampResponse
    {
        internal BasecampResponse(Api api)
        {
            Api = api;
        }

        internal Api Api { get; set; }
        public HttpStatusCode StatusCode { get; set; }
        internal string ResponseBody { get; set; }
    }

    public class BasecampResponse<T> : BasecampResponse
    {
        public BasecampResponse(Api api, BasecampResponse response) : base(api)
        {
            StatusCode = response.StatusCode;
            try
            {
                var converter = new ApiJsonConverter() { Api = response.Api };
                if (!string.IsNullOrEmpty(response.ResponseBody))
                {
                    response.ResponseBody = response.ResponseBody.Replace("{","{\"api\":\"\",");
                }
                Content = JsonConvert.DeserializeObject<T>(response.ResponseBody, converter);
            }
            catch (JsonSerializationException)
            {
                Content = JsonConvert.DeserializeObject<T>(response.ResponseBody);
            }
        }

        public T Content { get; set; }
    }
}
