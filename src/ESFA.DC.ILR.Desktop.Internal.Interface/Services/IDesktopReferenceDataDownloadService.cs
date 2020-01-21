using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IDesktopReferenceDataDownloadService
    {
        Task GetReferenceData(string fileName, string versionNumber);
    }
}
