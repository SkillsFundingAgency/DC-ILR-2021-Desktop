using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ESFA.DC.IO.FileSystem.Config.Interfaces;

namespace ESFA.DC.ILR.Desktop.ExportDatabase.Console
{
    public class FileSystemKeyValuePersistenceServiceConfig : IFileSystemKeyValuePersistenceServiceConfig
    {
        public string Directory { get; set; }
    }
}
