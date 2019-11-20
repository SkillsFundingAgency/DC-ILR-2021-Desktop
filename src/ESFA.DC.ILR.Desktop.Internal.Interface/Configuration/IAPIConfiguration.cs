namespace ESFA.DC.ILR.Desktop.Internal.Interface.Configuration
{
    public interface IAPIConfiguration
    {
        string APIBaseUrl { get; set; }

        string ApplicationVersionPath { get; set; }

        string APIVersionHeaderKey { get; set; }

        string APIVersionNumber { get; set; }
    }
}