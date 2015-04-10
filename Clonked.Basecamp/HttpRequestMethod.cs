using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public static class HttpRequestMethod
    {
        public static string Post { get { return "POST"; } }
        public static string Get { get { return "GET"; } }
        public static string Put { get { return "PUT"; } }
        public static string Delete { get { return "DELETE"; } }
    }
}
