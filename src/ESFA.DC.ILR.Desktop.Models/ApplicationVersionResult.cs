using System;

namespace ESFA.DC.ILR.Desktop.Models
{
    public class ApplicationVersionResult
    {
        public string Url { get; set; }

        public string ApplicationVersion { get; set; }

        public DateTime? ReleaseDateTime { get; set; } 

        public string LatestReferenceDataVersion { get; set; }

        public string LatestReferenceDataFileName { get; set; }
    }
}