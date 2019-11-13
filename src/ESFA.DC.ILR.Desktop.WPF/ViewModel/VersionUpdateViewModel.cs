using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class VersionUpdateViewModel : ViewModelBase
    {
        public VersionUpdateViewModel()
        {
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
        }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}