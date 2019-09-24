using System;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters
{
    public class ReportFiltersPropertyDefinitionViewModel
    {
        private readonly string _dateTimeFullyQualified = typeof(DateTime?).FullName;
        private readonly string _stringFullyQualified = typeof(string).FullName;

        public string Name { get; set; }

        public string Type { get; set; }

        public bool IsDateTime => Type == _dateTimeFullyQualified;

        public bool IsString => Type == _stringFullyQualified;

        public DateTime? DateTimeValue { get; set; }

        public string StringValue { get; set; }

        public object Value
        {
            get
            {
                if (IsDateTime)
                {
                    return DateTimeValue;
                }

                if (IsString)
                {
                    return StringValue;
                }

                return null;
            }
        }
    }
}
