using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;

namespace ESFA.DC.ILR.Desktop.Service.Model
{
    public class CompletionContext : ICompletionContext
    {
        public string OutputDirectory { get; set; }

        public ProcessingCompletionStates? ProcessingCompletionState { get; set; }
    }
}
