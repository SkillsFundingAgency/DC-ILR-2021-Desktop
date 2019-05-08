using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using Microsoft.Win32;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DialogInteractionService : IDialogInteractionService
    {
        public string GetFileNameFromOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }
    }
}
