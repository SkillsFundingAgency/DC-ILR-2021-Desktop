using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Context;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Stub;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.ViewModel.ReportFilters;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class ReportFiltersViewModel : ViewModelBase
    {
        private readonly IReportFilterService _reportFilterService;
        private ReportFiltersDefinitionViewModel _selectedReport;

        public ReportFiltersViewModel(IReportFilterService reportFilterService)
        {
            _reportFilterService = reportFilterService;

            Reports = BuildReportFilterDefinitions(_reportFilterService.GetReportFilterDefinitions());

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

        public ObservableCollection<ReportFiltersDefinitionViewModel> BuildReportFilterDefinitions(IEnumerable<IReportFilterDefinition> reportFilterDefinitions)
        {
            var definitions = reportFilterDefinitions?.Select(d => new ReportFiltersDefinitionViewModel()
            {
                ReportName = d.ReportName,
                Properties = new ObservableCollection<ReportFiltersPropertyDefinitionViewModel>(d.Properties.Select(
                    pd => new ReportFiltersPropertyDefinitionViewModel()
                    {
                        Name = pd.Name,
                        Type = pd.Type,
                    })),
            }) ?? Enumerable.Empty<ReportFiltersDefinitionViewModel>();

            return new ObservableCollection<ReportFiltersDefinitionViewModel>(definitions);
        }

        private void SaveFilters(ICloseable window)
        {
            _reportFilterService.SaveReportFilterQueries(BuildQueries());

            window?.Close();
        }

        private void CancelFilters(ICloseable window)
        {
            window?.Close();
        }

        private IEnumerable<IReportFilterQuery> BuildQueries()
        {
            return Reports.Select(r => new ReportFilterQuery()
            {
                ReportName = r.ReportName,
                FilterProperties = r.Properties.Select(p => new ReportFilterQueryProperty()
                {
                    Name = p.Name,
                    Value = p.Value,
                }),
            });
        }
    }
}
