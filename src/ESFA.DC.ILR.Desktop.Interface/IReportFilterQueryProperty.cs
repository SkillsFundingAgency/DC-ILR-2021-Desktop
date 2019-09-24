using System;
using System.Collections.Generic;
using System.Text;

namespace ESFA.DC.ILR.Desktop.Interface
{
    public interface IReportFilterQueryProperty
    {
        string Name { get; }

        object Value { get; }
    }
}
