using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp.Managers
{
    public abstract class ManagerBase
    {
        internal Api Api { get; set; }

        internal ManagerBase(Api api)
        {
            Api = api;
        }
    }
}
