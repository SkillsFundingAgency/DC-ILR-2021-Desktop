using System;
using System.Collections.Generic;

namespace ESFA.DC.ILR.Desktop.Models
{
    public class ApplicationVersion
    {
        public string Url { get; set; }

        public DateTime LastUpdated { get; set; }
                
        public IReadOnlyCollection<Version> Versions { get; set; }
    }
}