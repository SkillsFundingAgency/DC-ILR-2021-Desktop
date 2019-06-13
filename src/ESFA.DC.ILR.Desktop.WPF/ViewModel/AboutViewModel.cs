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
        public AboutViewModel()
        {
            CloseWindowCommand = new RelayCommand<ICloseable>(CloseWindow);
        }

        public RelayCommand<ICloseable> CloseWindowCommand { get; }

        public ObservableCollection<KeyValuePair<string, string>> AboutItems { get; } = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Version Number:", "123.123.123"),
            new KeyValuePair<string, string>("Reference Data Date:", new DateTime(2019, 12, 25, 1, 2, 3).ToString()),
        };

        private void CloseWindow(ICloseable window)
        {
            window?.Close();
        }
    }
}
