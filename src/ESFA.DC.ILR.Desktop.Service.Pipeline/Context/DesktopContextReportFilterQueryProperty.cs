using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Context
{
    public class DesktopContextReportFilterQueryProperty : IDesktopContextReportFilterPropertyQuery
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}
