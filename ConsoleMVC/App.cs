
using ConsoleMVC.CommandEngine;
using ConsoleMVC.Container;
using ConsoleMVC.Controllers;

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
            Console.Write("Enter Command: ");

            string input = String.Empty;

            input = Console.ReadLine();

            var commandRegistry = _container.Get<ICommandRegistry>();

            while (!commandRegistry.IsCommandValid(input) && input != ExitCommand)
            {
                Console.WriteLine("Please enter a valid command!");
                Console.Write("Enter Command: ");
                input = Console.ReadLine();
            }

            return input;
        }

        void Process()
        {
            string input = InitailCommand.command;

            var commandExecutor = _container.Get<ICommandExecutor>();
            var commandRegistry = _container.Get<ICommandRegistry>();

            commandRegistry.RegisterCommand(InitailCommand.command, InitailCommand.controller, InitailCommand.action);

            while (input != ExitCommand)
            {
                var view = commandExecutor.ExecuteCommand(input);
                var commands = view.Run();
                commandRegistry.RegisterCommands(commands);
                input = AskForInput();
            }
        }

        public void Run()
        {
            _configure(_container);
            Process();
        }
    }
}