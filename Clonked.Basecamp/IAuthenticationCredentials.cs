using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;

namespace Clonked.Basecamp
{
    public interface IAuthenticationCredentials
    {
        NetworkCredential GetCredentials();
        string GetEndPointUrlBase();
    }
}
