namespace ESFA.DC.ILR.Desktop.CLI.Interface
{
    public interface ICommandLineArguments
    {
        string FilePath { get; }

        string ConnectionString { get; }
        
        string OutputDirectory { get; }

        // To be followed up under new story.
        //string CheckAndUpdateReferenceData { get; }
    }
}
