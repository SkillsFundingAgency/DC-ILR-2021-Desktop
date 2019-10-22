namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IServiceConfiguration
    {
        string ReleaseDate { get; }

        string ReferenceDataDate { get; }

        IServiceConfiguration Configuration { get; }
    }
}
