
using ConsoleMVC.CommandEngine;
using ConsoleMVC.Container;
using ConsoleMVC.Controllers;
using ConsoleMVC.Messages;

namespace ConsoleMVC
{
    public class App
    {
        IAppContainer _container;
        Action<IAppContainer> _configure = (_container) => {};

        public (string command, IController controller, string action) InitailCommand { get; set; } = ("0", new HomeController(), "Index");
        public string ExitCommand { get; set; } = "q";


        public App(IAppContainer container)
        {
            _container = container;
        }

        public void Configure(Action<IAppContainer> configure)
        {
            _configure = configure;
        }

        string AskForInput()
        {
            Console.WriteLine();
            Console.Write("Enter Command: ");

            string input = String.Empty;

            input = Console.ReadLine();

            var commandRegistry = _container.Get<ICommandRegistry>();

            while (!commandRegistry.IsCommandValid(input) && input != ExitCommand)
            {
                var alertMessage = new AlertMessage("Please enter a valid command!");
                alertMessage.Print();

                Console.WriteLine();
                Console.Write("Enter Command: ");
                input = Console.ReadLine();
            }

            return input;
        }

        string NextCommand(string nextCommand)
        {
            if (nextCommand != null)   
                return nextCommand;
            
            return AskForInput();
        }

        void Process()
        {
            string input = InitailCommand.command;
            Dictionary<string, object> request = null;

            var commandExecutor = _container.Get<ICommandExecutor>();
            var commandRegistry = _container.Get<ICommandRegistry>();

            commandRegistry.RegisterCommand(InitailCommand.command, InitailCommand.controller, InitailCommand.action);

            while (input != ExitCommand)
            {
                var view = commandExecutor.ExecuteCommand(input, request);
                var (c, nc, r) = view.Run();
                request = r;
                commandRegistry.RegisterCommands(c);

                input = NextCommand(nc);
            }
        }

        public void Run()
        {
            _configure(_container);
            Process();
        }
    }
}