using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public class IlrDesktopTaskDefinition : IIlrDesktopTaskDefinition
    {
        public IlrDesktopTaskDefinition(IlrDesktopTaskKeys key, IlrDesktopTaskKeys? failureKey = null)
        {
            Key = key;
            FailureKey = failureKey;
        }

        public IlrDesktopTaskKeys Key { get; }

        public IlrDesktopTaskKeys? FailureKey { get; }
    }
}
