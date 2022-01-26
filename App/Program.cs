
using ConsoleMVC;
using ConsoleMVC.CommandEngine;
using ConsoleMVC.Container;

var container = new AppContainer();

container.Register<ICommandRegistry>(new CommandRegistry());
container.Register<ICommandExecutor>(new CommandExecutor(
        container.Get<ICommandRegistry>()
    ));

var app = new App(container);

app.Run();
