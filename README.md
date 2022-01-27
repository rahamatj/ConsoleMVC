# ConsoleMVC

## A .NET Console MVC Framework

## Controller Example

`App\Controllers\MedicineController.cs`

```
using ConsoleMVC.Controllers;
using ConsoleMVC.Views;
using ConsoleMVC.Messages;

namespace App.Controllers
{
    public class MedicineController : IController
    {
        public View Index()
        {
            return new Views.Medicine.Index();
        }

        public View Create()
        {
            return new Views.Medicine.Create();
        }

        Message StoreMedicine(object name, object quantity)
        {
            try
            {
                // Store to database code here

                return new SuccessMessage("Medicine added successfully!");
            }
            catch (Exception ex)
            {
                return new AlertMessage(ex.Message);
            }
        }

        public View Store(Dictionary<string, object> request)
        {
            var message = StoreMedicine(request["name"], request["quantity"]);

            var data = new Dictionary<string, object>();
            data.Add("message", message);

            return new Views.Medicine.Index(data);
        }
    }
}
```

## View Example

`App\Views\Medicine\Index.cs`

```
using ConsoleMVC.Views;
using App.Controllers;

namespace App.Views.Medicine
{
    internal class Index : View
    {
        public Index()
        {
        }

        public Index(Dictionary<string, object> data) : base(data)
        {
        }

        protected override void Print()
        {
            PrintMessage();

            Console.WriteLine();
            Console.WriteLine("(a) Add Medicine");
            Console.WriteLine("(q) Exit");
        }

        protected override void RegisterCommands()
        {
            RegisterCommand("a", new MedicineController(), "Create");
        }
    }
}

```

`App\Views\Medicine\Create.cs`

```
using ConsoleMVC.Inputs;
using ConsoleMVC.Inputs.Validations;
using ConsoleMVC.Views;
using App.Controllers;

namespace App.Views.Medicine
{
    internal class Create : View
    {
        protected override void Print()
        {
            Console.WriteLine();
            Console.WriteLine("Add medicine:");

            _request["name"] = Input.Prompt("Enter name: ", new IValidation[] 
            {
                new NotEmpty()
            });

            _request["quantity"] = Input.Prompt("Enter quantity: ", new IValidation[] 
            {
                new NotEmpty(),
                new PositiveInteger()
            });
        }

        protected override void RegisterCommands()
        {
            RegisterNextCommand("s", new MedicineController(), "Store");
        }
    }
}
```



## Initial Command

To print the initial screen `app.InitialCommand` must be set

`App\Program.cs`

```
app.InitailCommand = ("0", new MedicineController(), "Index");
```

## Exit Command
Which command exits the app can be set at `app.ExitCommand`.
Default exit command is "q".

`App\Program.cs`

```
app.ExitCommand = "e";
```
