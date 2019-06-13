using System;
using System.Collections.Generic;
using System.Text;
using ESFA.DC.ILR.Desktop.Service.Interface;
using ESFA.DC.ILR.Desktop.Service.Journey;

namespace ESFA.DC.ILR.Desktop.Stubs
{
    public class CompletionContextStub : ICompletionContext
    {
        public string OutputDirectory { get; set; }

        public ProcessingCompletionStates ProcessingCompletionState { get; set; }
    }
}
