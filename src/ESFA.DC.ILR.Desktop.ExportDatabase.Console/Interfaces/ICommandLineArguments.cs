using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console.Interfaces
{
    public interface ICommandLineArguments
    {
        string FilePath { get; }

        string Container { get; }
    }
}
