using CommandLine;
using ESFA.DC.ILR.Desktop.CLI.Interface;

namespace ESFA.DC.ILR.Desktop.CLI
{
    public class CommandLineArguments : ICommandLineArguments
    {
        [Option('f', "filepath", Required = true, HelpText = "Provide File Path for ILR File.")]
        public string FilePath { get; set; }
    }
}
