using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.Models;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IVersionMediatorService
    {
        Task<ApplicationVersionResult> GetNewVersion();
    }
}