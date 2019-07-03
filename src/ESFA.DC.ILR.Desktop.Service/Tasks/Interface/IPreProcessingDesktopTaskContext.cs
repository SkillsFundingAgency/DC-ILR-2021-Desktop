namespace ESFA.DC.ILR.Desktop.Service.Tasks.Interface
{
    public interface IPreProcessingDesktopTaskContext
    {
        string FileName { get; set; }

        string OriginalFileName { get; set; }

        string Container { get; set; }

        long FileSizeInBytes { set; }
    }
}
