namespace ESFA.DC.ILR.Desktop.Interface.Configuration
{
    public interface IAPIConfiguration
    {
        string APIBaseUrl { get; set; }

        string ApplicationVersionPath { get; set; }

        string APIVersionHeaderKey { get; set; }

        string APIVersionNumber { get; set; }
    }
}