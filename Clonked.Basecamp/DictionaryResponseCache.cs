using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public class DictionaryResponseCache : IResponseCache
    {
        public IDictionary<string, ICachePackage> Cache { get; set; }

        public DictionaryResponseCache()
        {
            Cache = new Dictionary<string, ICachePackage>();
        }

        public DictionaryResponseCache(IDictionary<string, ICachePackage> dictionary)
        {
            Cache = dictionary;
        }

        public void Insert(ICachePackage package)
        {
            if (Cache.Keys.Any(p => p == package.Key))
            {
                Cache[package.Key] = package;
            }
            else
            {
                Cache.Add(package.Key, package);
            }
            
        }

        public ICachePackage Get(string key)
        {
            if (Cache.Keys.Any(p => p == key))
            {
                return Cache[key];
            }

            return null;
        }
    }
}
