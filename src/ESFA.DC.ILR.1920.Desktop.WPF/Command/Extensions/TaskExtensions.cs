using System;
using System.Threading.Tasks;
using ESFA.DC.ILR.Desktop.WPF.Command.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Command.Extensions
{
    public static class TaskExtensions
    {
        public static async void FireAndForgetSafeAsync(this Task task, IErrorHandler handler = null)
        {
            try
            {
                await task;
            }
            catch (Exception ex)
            {
                handler?.HandleError(ex);
            }
        }
    }
}
