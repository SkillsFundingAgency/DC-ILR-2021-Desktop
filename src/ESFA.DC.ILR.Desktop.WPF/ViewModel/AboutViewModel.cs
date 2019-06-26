using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        private IReferenceDataVersionInformationService _versionInformationService;
        private IReleaseVersionInformationService _releaseVersionInformationService;

        public AboutViewModel(IReferenceDataVersionInformationService versionInformationService, IReleaseVersionInformationService releaseVersionInformationService)
        {
            _versionInformationService = versionInformationService;
            _releaseVersionInformationService = releaseVersionInformationService;

            AboutItems = new ObservableCollection<KeyValuePair<string, string>>()
            {
                new KeyValuePair<string, string>("Version Number:", _releaseVersionInformationService.VersionNumber),
                new KeyValuePair<string, string>("Reference Data Date:", _versionInformationService.Date),
            };
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
        }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        public ObservableCollection<KeyValuePair<string, string>> AboutItems { get; }

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}
