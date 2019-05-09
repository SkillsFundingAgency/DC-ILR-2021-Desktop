using ESFA.DC.ILR.Desktop.WPF.Service.Interface;
using Microsoft.Win32;
using FolderBrowserDialog = System.Windows.Forms.FolderBrowserDialog;
using DialogResult = System.Windows.Forms.DialogResult;

namespace ESFA.DC.ILR.Desktop.WPF.Service
{
    public class DialogInteractionService : IDialogInteractionService
    {
        private const string ChooseOutputDirectoryDescription = @"Choose Output Directory";
      
        public string GetFileNameFromOpenFileDialog()
        {
            var openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == true)
            {
                return openFileDialog.FileName;
            }

            return string.Empty;
        }
               
        public string GetFolderNameFromFolderBrowserDialog(string outputDirectoryPath, string outputDirectoryDescription)
        {
            string selectedOutputDirectory = string.Empty; 
            using (var folderBrowserDlg = new FolderBrowserDialog())
            {
                // Configure browser dialog box
                folderBrowserDlg.Description = outputDirectoryDescription;
                folderBrowserDlg.ShowNewFolderButton = true;
                folderBrowserDlg.SelectedPath = outputDirectoryPath;

                // Show the dialog
                var result = folderBrowserDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Retrieve the selected path
                    selectedOutputDirectory = folderBrowserDlg.SelectedPath;
                }
            }
            return selectedOutputDirectory;
        }
    }
}
