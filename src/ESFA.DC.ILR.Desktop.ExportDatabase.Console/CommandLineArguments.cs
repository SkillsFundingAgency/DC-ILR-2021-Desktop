using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CommandLine;
using ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console
{
    public class CommandLineArguments : ICommandLineArguments
    {
        [Option('f', "filename", Required = true, HelpText = "Provide File name for Desktop context.")]
        public string FilePath { get; set; }

        [Option('c', "container", Required = false, HelpText = "container path for desktop context")]
        public string Container { get; set; }
    }
}
