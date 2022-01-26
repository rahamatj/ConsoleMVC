
using ConsoleMVC.Controllers;
using ConsoleMVC.Views.Messages;
using System.Collections;

namespace ConsoleMVC.Views
{
    public abstract class View
    {
        Dictionary<string, (IController controller, string action)> _commands = new Dictionary<string, (IController controller, string action)>();

        protected Dictionary<string, object> _data;

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

        public Dictionary<string, (IController controller, string action)> Run()
        {
            RegisterCommands();
            Print();

            return _commands;
        }
    }
}
