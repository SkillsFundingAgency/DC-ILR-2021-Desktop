namespace ESFA.DC.ILR.Desktop.Service.Tasks.Interface
{
    public interface IPreProcessingDesktopTaskContext
    {
        string FileName { get; set; }

        string OriginalFileName { get; set; }

        string Container { get; set; }

        int Ukprn { set; }

        long FileSizeInBytes { set; }

        string ReferenceDataFileName { get; set; }
    }
}
