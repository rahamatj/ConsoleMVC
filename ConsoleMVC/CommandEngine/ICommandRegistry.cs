
using ConsoleMVC.Container;
using ConsoleMVC.Controllers;

namespace ConsoleMVC.CommandEngine
{
    public interface ICommandRegistry : IContainable
    {
        bool IsCommandValid(string input);

        void RegisterCommand(string command, IController controller, string action);

        void RegisterCommands(Dictionary<string, (IController controller, string action)> register);

        (IController controller, string action) Get(string key);

        void ClearRegister();
    }
}
