using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Clonked.Basecamp
{
    public interface IMimeTypeResolver
    {
        string GetMimeType(string extension);
    }
}
