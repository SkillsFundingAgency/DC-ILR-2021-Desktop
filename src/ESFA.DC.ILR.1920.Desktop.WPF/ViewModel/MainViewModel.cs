using System;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.CommandWpf;
using Microsoft.Win32;

namespace ESFA.DC.ILR._1920.Desktop.WPF.ViewModel
{
    /// <summary>
    /// This class contains properties that the main View can data bind to.
    /// <para>
    /// Use the <strong>mvvminpc</strong> snippet to add bindable properties to this ViewModel.
    /// </para>
    /// <para>
    /// You can also use Blend to data bind with the tool's support.
    /// </para>
    /// <para>
    /// See http://www.galasoft.ch/mvvm
    /// </para>
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private bool _processing = false;
        private string _fileName;

        public MainViewModel()
        {
            if (IsInDesignMode)
            {
                FileName = "C:/Users/TestFiles/ILRFile.xml";
            }
            else
            {
                FileName = "No file chosen";
            }

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog, () => !_processing);
            ProcessFileCommand = new RelayCommand(ProcessFile, () => !_processing);
        }

        public string FileName
        {
            get => _fileName;
            set
            {
                _fileName = value;
                RaisePropertyChanged();
            }
        }

        public ICommand ChooseFileCommand { get; set; }

        public ICommand ProcessFileCommand { get; set; }

        private void ShowChooseFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }

        private void ProcessFile()
        {
        }
    }
}