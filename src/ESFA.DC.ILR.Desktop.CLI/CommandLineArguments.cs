using CommandLine;
using ESFA.DC.ILR.Desktop.CLI.Interface;

namespace ESFA.DC.ILR.Desktop.CLI
{
    public class CommandLineArguments : ICommandLineArguments
    {
        [Option('f', "filepath", Required = true, HelpText = "Provide File Path for ILR File.")]
        public string FilePath { get; set; }
        
        [Option('c', "connectionstring", Required = false, HelpText = "Connection String for ILR Database. Leave blank to fall back to Config File. If added, connection string value should be surrounded by double quotes and the output will automatically Export to SQL")]
        public string ConnectionString { get; set; }

        [Option('o', "outputdirectory", Required = false, HelpText = "Output Directory for Reports, leave blank to fall back to Config File.")]
        public string OutputDirectory { get; set; }

        [Option('u', "updatereferencedata", Required = false, HelpText = "'Y' (On) or 'N' (off) to download the latest reference data, if available. Leave blank to default to 'N'.")]
        public string CheckAndUpdateReferenceData { get; set; }
    }
}
