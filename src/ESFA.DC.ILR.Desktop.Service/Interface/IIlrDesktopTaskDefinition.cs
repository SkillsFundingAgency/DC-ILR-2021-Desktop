using System;
using System.Collections.Generic;
using ESFA.DC.ILR.Desktop.Service.Mutator;
using ESFA.DC.ILR.Desktop.Service.Tasks;

namespace ESFA.DC.ILR.Desktop.Service.Interface
{
    public interface IIlrDesktopTaskDefinition
    {
        IlrDesktopTaskKeys Key { get; }

        IlrDesktopTaskKeys? FailureKey { get; }

        ContextMutatorKeys? FailureContextMutatorKey { get; }
    }
}
