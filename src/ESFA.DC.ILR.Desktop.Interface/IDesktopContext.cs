using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IDesktopContext
    {
        DateTime DateTimeUtc { get; }

        string OutputDirectory { get; }

        IDictionary<string, object> KeyValuePairs { get; }
    }
}
