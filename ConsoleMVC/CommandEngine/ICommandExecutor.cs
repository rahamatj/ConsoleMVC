
using ConsoleMVC.Container;
using ConsoleMVC.Controllers;
using ConsoleMVC.Views;

namespace ConsoleMVC.CommandEngine
{
    public interface ICommandExecutor : IContainable
    {
        View ExecuteCommand(string input, Dictionary<string, object> request = null);
    }
}
