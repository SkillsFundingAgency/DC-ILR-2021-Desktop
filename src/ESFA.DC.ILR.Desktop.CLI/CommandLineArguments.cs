using CommandLine;
using ESFA.DC.ILR.Desktop.CLI.Interface;

namespace ESFA.DC.ILR.Desktop.CLI
{
    public class CommandLineArguments : ICommandLineArguments
    {
        [Option('f', "filepath", Required = true, HelpText = "Provide File Path for ILR File.")]
        public string FilePath { get; set; }
        
        [Option('c', "connectionstring", Required = false, HelpText = "Connection String for ILR Database, leave blank to fall back to Config File.")]
        public string ConnectionString { get; set; }

        [Option('o', "outputdirectory", Required = false, HelpText = "Output Directory for Reports, leave blank to fall back to Config File.")]
        public string OutputDirectory { get; set; }
    }
}
