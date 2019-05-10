using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class DesktopContext : IDesktopContext
    {
        public DateTime DateTimeUtc { get; set; }

        public IDictionary<string, object> KeyValuePairs { get; set; }
    }
}
