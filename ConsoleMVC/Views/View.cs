
using ConsoleMVC.Controllers;
using ConsoleMVC.Messages;

namespace ConsoleMVC.Views
{
    public abstract class View
    {
        Dictionary<string, (IController controller, string action)> _commands = new Dictionary<string, (IController controller, string action)>();

        protected readonly Dictionary<string, object> _request = new Dictionary<string, object>();
        protected readonly Dictionary<string, object> _data;

        protected string NextCommand { get; set; } = null;

        public View()
        {
        }

        public View(Dictionary<string, object> data)
        {
            _data = data;
        }

        protected void RegisterCommand(string command, IController controller, string action)
        {
            _commands.Add(command, (controller, action));
        }

        protected void RegisterNextCommand(string command, IController controller, string action)
        {
            RegisterCommand(command, controller, action);
            NextCommand = command;
        }

        protected virtual void PrintMessage()
        {
            if (_data != null && _data.Count > 0)
            {
                if (_data.ContainsKey("message"))
                {
                    var message = (Message)_data["message"];
                    message.Print();
                }
            }
        }

        protected abstract void Print();
        protected virtual void RegisterCommands()
        {
        }

        public (Dictionary<string, (IController controller, string action)> commands, string nextCommand, Dictionary<string, object> request) Run()
        {
            RegisterCommands();
            Print();

            return (_commands, NextCommand, _request);
        }
    }
}
