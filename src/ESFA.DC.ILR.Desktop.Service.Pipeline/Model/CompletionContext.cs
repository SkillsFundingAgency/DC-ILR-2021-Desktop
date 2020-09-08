using ESFA.DC.ILR.Desktop.Service.Pipeline.Interface;
using ESFA.DC.ILR.Desktop.Service.Pipeline.Journey;

namespace ESFA.DC.ILR.Desktop.Service.Pipeline.Model
{
    public class CompletionContext : ICompletionContext
    {
        public string OutputDirectory { get; set; }

        public ProcessingCompletionStates? ProcessingCompletionState { get; set; }
    }
}
