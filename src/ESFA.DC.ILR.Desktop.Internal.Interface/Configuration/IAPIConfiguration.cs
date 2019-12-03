namespace ESFA.DC.ILR.Desktop.Internal.Interface.Configuration
{
    public interface IAPIConfiguration
    {
        string APIBaseUrl { get; }

        string ApplicationVersionPath { get; }

        string APIVersionHeaderKey { get; }

        string APIVersionNumber { get; }

        IAPIConfiguration Configuration { get; }
    }
}