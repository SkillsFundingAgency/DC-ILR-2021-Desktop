using ESFA.DC.ILR.Desktop.Service.Tasks.Attributes;

namespace ESFA.DC.ILR.Desktop.Service.Tasks
{
    public enum IlrDesktopTaskKeys
    {
        [DisplayText(TaskNameDisplayTextConstants.PreExecution)]
        PreExecution,

        [DisplayText(TaskNameDisplayTextConstants.BuildDatabase)]
        DatabaseCreate,

        [DisplayText(TaskNameDisplayTextConstants.FileValidation)]
        FileValidationService,

        [DisplayText(TaskNameDisplayTextConstants.ReferenceData)]
        ReferenceDataService,

        [DisplayText(TaskNameDisplayTextConstants.Validation)]
        ValidationService,

        [DisplayText(TaskNameDisplayTextConstants.FundingCalculation)]
        FundingService,

        [DisplayText(TaskNameDisplayTextConstants.StoreData)]
        DataStore,

        [DisplayText(TaskNameDisplayTextConstants.ReportGeneration)]
        ReportService,

        [DisplayText(TaskNameDisplayTextConstants.PostExecution)]
        PostExecution,
    }
}
