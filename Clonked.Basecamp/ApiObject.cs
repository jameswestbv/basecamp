using System;
using Newtonsoft.Json;

namespace Clonked.Basecamp
{
    public abstract class ApiStubObject<T> : ApiObject
    {
        public abstract T GetDetail();
    }

    public abstract class ApiObject
    {
        public Api Api { get; set; }
    }
}
