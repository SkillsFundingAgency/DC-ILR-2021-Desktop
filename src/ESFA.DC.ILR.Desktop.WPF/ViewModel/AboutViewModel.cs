using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using GalaSoft.MvvmLight;

namespace ESFA.DC.ILR.Desktop.WPF.ViewModel
{
    public class AboutViewModel : ViewModelBase
    {
        public ObservableCollection<KeyValuePair<string, string>> AboutItems { get; } = new ObservableCollection<KeyValuePair<string, string>>()
        {
            new KeyValuePair<string, string>("Version Number:", "123.123.123"),
            new KeyValuePair<string, string>("Reference Data Date:", new DateTime(2019, 12, 25, 1, 2, 3).ToString()),
        };
    }
}
