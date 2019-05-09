using System.Collections.Generic;
using System.Threading.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IConfigService
    {
        IDictionary<string, string> UserSettingsKeyValuePair { get; }

        bool SaveConfigAppSettings(string key, string value);
    }
}
