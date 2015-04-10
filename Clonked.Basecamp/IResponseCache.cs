using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public interface IResponseCache
    {
        void Insert(ICachePackage package);
        ICachePackage Get(string key);
    }
}
