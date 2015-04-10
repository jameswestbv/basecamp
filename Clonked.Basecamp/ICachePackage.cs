using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public interface ICachePackage
    {
        string Key { get; set; }
        string ETag { get; set; }
        DateTime LastModified { get; set; }
        string Url { get; set; }
        string ResponseBody { get; set; }
    }
}
