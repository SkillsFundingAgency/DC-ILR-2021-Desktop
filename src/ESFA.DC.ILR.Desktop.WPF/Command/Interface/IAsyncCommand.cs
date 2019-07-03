using System.Threading.Tasks;
using System.Windows.Input;

namespace ESFA.DC.ILR.Desktop.WPF.Command.Interface
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }

    public interface IAsyncCommand<T> : ICommand
    {
        Task ExecuteAsync(object parameter);

        bool CanExecute();
    }
}
