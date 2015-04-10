using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;

namespace Clonked.Basecamp
{
    public class HttpContextResponseCache : IResponseCache
    {
        public void Insert(ICachePackage package)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("HttpContext.Current is null");
            }

            HttpContext.Current.Cache.Insert(package.Key, package);
        }

        public ICachePackage Get(string key)
        {
            if (HttpContext.Current == null)
            {
                throw new Exception("HttpContext.Current is null");
            }

            var package = HttpContext.Current.Cache.Get(key) as ICachePackage;
            return package;
        }
    }
}
