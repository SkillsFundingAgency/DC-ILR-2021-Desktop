namespace ESFA.DC.ILR.Desktop.Internal.Interface.Configuration
{
    public interface IFeatureSwitchConfiguration
    {
        bool VersionUpdate { get; }

        bool ReportFilters { get; }

        IFeatureSwitchConfiguration Configuration { get; }
    }
}