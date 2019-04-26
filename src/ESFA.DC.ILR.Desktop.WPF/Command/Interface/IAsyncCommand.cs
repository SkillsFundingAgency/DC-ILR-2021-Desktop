using System.Threading.Tasks;
using System.Windows.Input;

namespace ESFA.DC.ILR.Desktop.WPF.Command.Interface
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
