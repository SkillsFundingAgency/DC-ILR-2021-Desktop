using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IReferenceDataResultClient
    {
        Task<byte[]> GetAsync(string fileName);
    }
}