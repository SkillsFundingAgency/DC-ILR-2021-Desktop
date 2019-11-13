using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public AboutViewModel(
            IReferenceDataVersionInformationService versionInformationService,
            IReleaseVersionInformationService releaseVersionInformationService)
        {
            AboutItems = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("Version Number:", releaseVersionInformationService.VersionNumber),
                new KeyValuePair<string, string>("Reference Data Date:", versionInformationService.Date),
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
