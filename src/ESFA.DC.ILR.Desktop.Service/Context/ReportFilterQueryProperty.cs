using ESFA.DC.ILR.Desktop.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Context
{
    public class ReportFilterQueryProperty : IReportFilterQueryProperty
    {
        public string Name { get; set; }

        public object Value { get; set; }
    }
}
