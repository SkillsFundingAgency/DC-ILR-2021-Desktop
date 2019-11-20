namespace ESFA.DC.ILR.Desktop.Internal.Interface.Configuration
{
    public interface IServiceConfiguration
    {
        string ReleaseDate { get; }

        string ReferenceDataDate { get; }

        IServiceConfiguration Configuration { get; }
    }
}
