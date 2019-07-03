using System;

namespace ESFA.DC.ILR.Desktop.WPF.Command.Interface
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
