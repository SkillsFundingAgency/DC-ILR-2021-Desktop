using System;
using System.Threading.Tasks;
using System.Windows.Input;
using ESFA.DC.ILR.Desktop.WPF.Command.Extensions;
using ESFA.DC.ILR.Desktop.WPF.Command.Interface;

namespace ESFA.DC.ILR.Desktop.WPF.Command
{
    public class AsyncCommand<T> : IAsyncCommand<T>
    {
        private bool _isExecuting;
        private readonly Func<T, Task> _execute;
        private readonly Func<bool> _canExecute;
        private readonly IErrorHandler _errorHandler;

        public AsyncCommand(
            Func<T, Task> execute,
            Func<bool> canExecute = null,
            IErrorHandler errorHandler = null)
        {
            _execute = execute;
            _canExecute = canExecute;
            _errorHandler = errorHandler;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute()
        {
            return !_isExecuting && (_canExecute?.Invoke() ?? true);
        }

        public async Task ExecuteAsync(object parameter)
        {
            if (CanExecute())
            {
                try
                {
                    _isExecuting = true;
                    await _execute((T)parameter);
                }
                finally
                {
                    _isExecuting = false;
                }
            }

            RaiseCanExecuteChanged();
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #region Explicit implementations
        bool ICommand.CanExecute(object parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object parameter)
        {
            ExecuteAsync(parameter).FireAndForgetSafeAsync(_errorHandler);
        }
        #endregion
    }
}
