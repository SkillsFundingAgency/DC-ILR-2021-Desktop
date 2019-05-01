using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class DesktopContextStub : IDesktopContext
    {
        public DateTime DateTimeUtc { get; set; }

        public IDictionary<string, object> KeyValuePairs { get; set; }
    }
}
