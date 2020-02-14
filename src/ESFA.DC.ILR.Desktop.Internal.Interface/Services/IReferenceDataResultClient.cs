using System.IO;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Internal.Interface.Services
{
    public interface IReferenceDataResultClient
    {
        Task<Stream> GetAsync(string fileName, Stream stream);
    }
}