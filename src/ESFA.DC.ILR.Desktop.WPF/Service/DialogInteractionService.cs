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
               
        public string GetFolderBrowserDialog()
        {
            string outputDirectory = string.Empty; 

            var folderBrowserDlg = new FolderBrowserDialog();
            using (folderBrowserDlg)
            {
                // Configure browser dialog box
                folderBrowserDlg.Description = ChooseOutputDirectoryDescription;
                folderBrowserDlg.ShowNewFolderButton = true;
                folderBrowserDlg.SelectedPath = outputDirectory;

                // Show the dialog.
                var result = folderBrowserDlg.ShowDialog();
                if (result == DialogResult.OK)
                {
                    // Retrieve the selected path
                    outputDirectory = folderBrowserDlg.SelectedPath;
                }
            }
            return outputDirectory;
        }
    }
}
