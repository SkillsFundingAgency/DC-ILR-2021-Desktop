using System.Collections.ObjectModel;
using System.Linq;
using ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters;
using GalaSoft.MvvmLight;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class ReportFiltersViewModel : ViewModelBase
    {
        private ReportFiltersDefinitionViewModel _selectedReport;

        public ReportFiltersViewModel()
        {
            Reports = new ObservableCollection<ReportFiltersDefinitionViewModel>()
            {
                new ReportFiltersDefinitionViewModel()
                {
                    ReportName = "Report One",
                    Properties = new ObservableCollection<ReportFiltersPropertyDefinitionViewModel>()
                    {
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property One",
                            Type = "System.DateTime",
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Two",
                            Type = "System.String",
                        },
                    },
                },
                new ReportFiltersDefinitionViewModel()
                {
                    ReportName = "Report Two",
                    Properties = new ObservableCollection<ReportFiltersPropertyDefinitionViewModel>()
                    {
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property One",
                            Type = "System.DateTime",
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Two",
                            Type = "System.String",
                        },
                    },
                },
            };

            SelectedReport = Reports.FirstOrDefault();
        }

        public ObservableCollection<ReportFiltersDefinitionViewModel> Reports { get; set; }

        public ReportFiltersDefinitionViewModel SelectedReport
        {
            get => _selectedReport;
            set
            {
                Set(ref _selectedReport, value);
            }
        }
    }
}
