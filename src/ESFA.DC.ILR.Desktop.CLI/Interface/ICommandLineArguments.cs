namespace ESFA.DC.ILR.Desktop.CLI.Interface
{
    public interface ICommandLineArguments
    {
        string FilePath { get; }

        string ConnectionString { get; }
        
        string OutputDirectory { get; }

        string CheckAndUpdateReferenceData { get; }
    }
}
