using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Interface.Services
{
    public interface IVersionService
    {
        Task<ApplicationVersionResult> GetLatestApplicationVersion(Version currentVersion);
    }
}