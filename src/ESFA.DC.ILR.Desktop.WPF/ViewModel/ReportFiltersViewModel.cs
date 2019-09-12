using System;
using System.Collections.ObjectModel;
using System.Linq;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

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
                            Type = typeof(string).FullName,
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Two",
                            Type = typeof(DateTime?).FullName,
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
                            Type = typeof(DateTime?).FullName,
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Two",
                            Type = typeof(string).FullName,
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Three",
                            Type = typeof(string).FullName,
                        },
                        new ReportFiltersPropertyDefinitionViewModel()
                        {
                            Name = "Property Four",
                            Type = typeof(DateTime?).FullName,
                        },
                    },
                },
            };

            SelectedReport = Reports.FirstOrDefault();

            SaveCommand = new RelayCommand<ICloseable>(SaveFilters);
            CancelCommand = new RelayCommand<ICloseable>(CancelFilters);
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

        public RelayCommand<ICloseable> SaveCommand { get; set; }

        public RelayCommand<ICloseable> CancelCommand { get; set; }

        private void SaveFilters(ICloseable window)
        {
            window?.Close();
        }

        private void CancelFilters(ICloseable window)
        {
            window?.Close();
        }
    }
}
