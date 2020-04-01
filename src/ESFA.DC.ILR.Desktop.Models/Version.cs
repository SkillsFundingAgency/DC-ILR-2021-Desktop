using System;

namespace ESFA.DC.ILR.Desktop.Models
{
    public class Version
    {
        public string VersionName { get; set; }

        public string FileName { get; set; }

        public int Major { get; set; }

        public int Minor { get; set; }

        public int Increment { get; set; }

        public DateTime ReleaseDateTime { get; set; }

        public ReferenceData ReferenceDataVersion { get; set; }
    }
}