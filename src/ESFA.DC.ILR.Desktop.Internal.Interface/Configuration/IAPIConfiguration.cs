namespace ESFA.DC.ILR.Desktop.Internal.Interface.Configuration
{
    public interface IAPIConfiguration
    {
        string APIBaseUrl { get; }

        string ApplicationVersionPath { get; }

        string ReferenceDataVersionPath { get; }

        string APIVersionHeaderKey { get; }

        string APIVersionNumber { get; }

        string AcademicYear { get; }

        IAPIConfiguration Configuration { get; }
    }
}