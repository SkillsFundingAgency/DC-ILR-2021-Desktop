using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Interface.Services;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class VersionUpdateViewModel : ViewModelBase
    {
        private readonly IWindowsProcessService _windowsProcessService;
        private readonly IReleaseVersionInformationService _versionInformationService;
        private readonly IVersionService _versionService;
        private readonly IVersionFactory _versionFactory;

        public VersionUpdateViewModel(
            IWindowsProcessService windowsProcessService,
            IReleaseVersionInformationService versionInformationService,
            IVersionService versionService,
            IVersionFactory versionFactory)
        {
            _windowsProcessService = windowsProcessService;
            _versionInformationService = versionInformationService;
            _versionService = versionService;
            _versionFactory = versionFactory;

            VersionNavigationCommand = new RelayCommand(NavigateToVersions);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);

            Task.Factory.StartNew(Initialize);
        }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        public RelayCommand VersionNavigationCommand { get; set; }

        public ApplicationVersionResult ApplicationVersionResult { get; set; }

        public ObservableCollection<KeyValuePair<string, string>> VersionItems { get; set; }

        private async Task Initialize()
        {
            ApplicationVersionResult = await _versionService.GetLatestApplicationVersion(GetCurrentApplicationVersion());

            VersionItems = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("New version number: ", ApplicationVersionResult.ApplicationVersion),
                new KeyValuePair<string, string>("Version release date: ", ApplicationVersionResult.ApplicationVersion)
            };
        }

        private Version GetCurrentApplicationVersion()
        {
            var version = _versionInformationService.VersionNumber;
            return _versionFactory.GetVersion(version);
        }

        private void NavigateToVersions()
        {
            _windowsProcessService.ProcessStart(ApplicationVersionResult.Url);
        }

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}