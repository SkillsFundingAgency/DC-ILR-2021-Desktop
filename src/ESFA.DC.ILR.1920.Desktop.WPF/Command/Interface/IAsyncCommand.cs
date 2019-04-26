using System.Threading.Tasks;
using System.Windows.Input;

namespace ESFA.DC.ILR._1920.Desktop.WPF.Command.Interface
{
    public interface IAsyncCommand : ICommand
    {
        Task ExecuteAsync();

        bool CanExecute();
    }
}
