using System;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters
{
    public class ReportFiltersPropertyDefinitionViewModel
    {
        public string Name { get; set; }

        public string Type { get; set; }

        public DateTime? DateTimeValue { get; set; }

        public string StringValue { get; set; }

        public object Value { get; } // this maps from others, bit clunky, possibly will remove
    }
}
