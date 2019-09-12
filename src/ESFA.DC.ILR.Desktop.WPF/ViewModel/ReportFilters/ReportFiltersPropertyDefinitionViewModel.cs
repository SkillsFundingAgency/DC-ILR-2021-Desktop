using System;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters
{
    public class ReportFiltersPropertyDefinitionViewModel
    {
        private readonly string DateTimeFullyQualified = typeof(DateTime?).FullName;
        private readonly string StringFullyQualified = typeof(string).FullName;

        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsDateTime => Type == DateTimeFullyQualified;

        public bool IsString => Type == StringFullyQualified;

        public DateTime? DateTimeValue { get; set; }

        public string StringValue { get; set; }

        public object Value { get; } // this maps from others, bit clunky, possibly will remove
    }
}
