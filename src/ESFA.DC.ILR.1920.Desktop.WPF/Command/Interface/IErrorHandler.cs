using System;

namespace ESFA.DC.ILR._1920.Desktop.WPF.Command.Interface
{
    public interface IErrorHandler
    {
        void HandleError(Exception ex);
    }
}
