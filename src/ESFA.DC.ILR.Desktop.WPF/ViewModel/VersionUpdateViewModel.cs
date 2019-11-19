using System.Collections.Generic;
using System.Collections.ObjectModel;
using ESFA.DC.ILR.Desktop.Models;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class VersionUpdateViewModel : ViewModelBase
    {
        private readonly IWindowsProcessService _windowsProcessService;

        public VersionUpdateViewModel(
            IMessengerService messengerService,
            IWindowsProcessService windowsProcessService)
        {
            _windowsProcessService = windowsProcessService;

            VersionNavigationCommand = new RelayCommand(NavigateToVersionsUrl);
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);

            messengerService.Register<VersionMessage>(this, Initialize);
        }

        public bool ShowProgress { get; set; }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        public RelayCommand VersionNavigationCommand { get; set; }

        public ApplicationVersionResult ApplicationVersionResult { get; set; }

        public ObservableCollection<KeyValuePair<string, string>> VersionItems { get; set; }

        protected void Initialize(VersionMessage message)
        {
            ApplicationVersionResult = message.ApplicationVersion;

            VersionItems = new ObservableCollection<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("New version number: ", ApplicationVersionResult.ApplicationVersion),
                new KeyValuePair<string, string>("Version release date: ", ApplicationVersionResult.ReleaseDateTime.ToString())
            };

            ShowProgress = false;
        }

        private void NavigateToVersionsUrl()
        {
            _windowsProcessService.ProcessStart(ApplicationVersionResult.Url);
        }

        private static void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}