using ESFA.DC.ILR.Desktop.Service.Pipeline.Attributes;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline
{
    public enum IlrDesktopTaskKeys
    {
        [DisplayText(TaskNameDisplayTextConstants.PreExecution)]
        PreExecution,

        [DisplayText(TaskNameDisplayTextConstants.BuildDatabase)]
        DatabaseCreate,

        [DisplayText(TaskNameDisplayTextConstants.MdbCreate)]
        MdbCreate,

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

        [DisplayText(TaskNameDisplayTextConstants.MdbExport)]
        MdbExport,

        [DisplayText(TaskNameDisplayTextConstants.ReportGeneration)]
        ReportService,

        [DisplayText(TaskNameDisplayTextConstants.StoreDesktopContext)]
        StoreDesktopContext,

        [DisplayText(TaskNameDisplayTextConstants.ExportMdbProcess)]
        ExportMdbProcess,

        [DisplayText(TaskNameDisplayTextConstants.MdbPublish)]
        MdbPublish,

        [DisplayText(TaskNameDisplayTextConstants.PostExecution)]
        PostExecution,
    }
}
