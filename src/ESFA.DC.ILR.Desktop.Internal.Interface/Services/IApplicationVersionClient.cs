using ESFA.DC.ILR.Desktop.Models;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IApplicationVersionClient
    {
        Task<ApplicationVersion> GetApplicationVersionsAsync();
    }
}