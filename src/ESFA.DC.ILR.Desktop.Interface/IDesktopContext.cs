using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IDesktopContext
    {
        DateTime DateTimeUtc { get; }

        IDictionary<string, object> KeyValuePairs { get; }
    }
}
