using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;
using ESFA.DC.ILR._1920.Desktop.WPF.Command;
using ESFA.DC.ILR.Desktop.Service.Interface;
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

        private readonly IIlrDesktopService _ilrDesktopService;

        public MainViewModel(IIlrDesktopService ilrDesktopService)
        {
            if (IsInDesignMode)
            {
                FileName = "C:/Users/TestFiles/ILRFile.xml";
            }
            else
            {
                FileName = "No file chosen";
            }

            ChooseFileCommand = new RelayCommand(ShowChooseFileDialog, () => !Processing);
            ProcessFileCommand = new AsyncCommand(ProcessFile, () => !Processing);

            _ilrDesktopService = ilrDesktopService;
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

        public bool Processing
        {
            get => _processing;
            set
            {
                _processing = value;

                ChooseFileCommand.RaiseCanExecuteChanged();
                ProcessFileCommand.RaiseCanExecuteChanged();
            }
        }

        public RelayCommand ChooseFileCommand { get; set; }

        public AsyncCommand ProcessFileCommand { get; set; }

        private void ShowChooseFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                FileName = openFileDialog.FileName;
            }
        }

        private async Task ProcessFile()
        {
            Processing = true;

            await _ilrDesktopService.ProcessAsync(FileName, CancellationToken.None);

            Processing = false;
        }
    }
}