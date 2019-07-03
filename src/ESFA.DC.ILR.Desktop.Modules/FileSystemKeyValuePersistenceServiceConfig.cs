using ESFA.DC.IO.FileSystem.Config.Interfaces;

namespace ESFA.DC.ILR.Desktop.WPF.Config
{
    public class FileSystemKeyValuePersistenceServiceConfig : IFileSystemKeyValuePersistenceServiceConfig
    {
        public string Directory { get; set; }
    }
}
