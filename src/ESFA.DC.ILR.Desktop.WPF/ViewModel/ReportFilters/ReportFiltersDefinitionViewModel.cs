using System.Collections.ObjectModel;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters
{
    public class ReportFiltersDefinitionViewModel
    {
        public string ReportName { get; set; }

        public ObservableCollection<ReportFiltersPropertyDefinitionViewModel> Properties { get; set; }
    }
}
