namespace ESFA.DC.ILR.Desktop.WPF.Service.Interface
{
    public interface IDialogInteractionService
    {
        string GetFileNameFromOpenFileDialog();

        string GetFolderNameFromFolderBrowserDialog(string outputDirectoryPath, string outputDirectoryDescription);
    }
}
