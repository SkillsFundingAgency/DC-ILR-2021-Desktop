using System;
using ESFA.DC.ILR.Desktop.Interface;
using ESFA.DC.ILR.Desktop.Service.Interface;

namespace ESFA.DC.ILR.Desktop.Service.Mutator
{
    public class SchemaErrorContextMutator : IContextMutator
    {
        public IDesktopContext Mutate(IDesktopContext desktopContext)
        {
            return desktopContext;
        }
    }
}
