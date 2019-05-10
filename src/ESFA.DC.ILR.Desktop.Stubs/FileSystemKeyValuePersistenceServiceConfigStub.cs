using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.IO.FileSystem.Config.Interfaces;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class FileSystemKeyValuePersistenceServiceConfigStub : IFileSystemKeyValuePersistenceServiceConfig
    {
        public string Directory { get; set; }
    }
}
