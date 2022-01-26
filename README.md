# ConsoleMVC

## A .NET Console MVC Framework

## Controller Example

`App\Controllers\MedicineController.cs`

```
using ConsoleMVC.Controllers;
using ConsoleMVC.Views;

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

        public View Store(object name, object quantity)
        {
            var message = StoreMedicine(name, quantity);

            var data = new Dictionary<string, object>();
            data.Add("message", message);

            return new Views.Medicine.Create(data);
        }
    }
}
```

## View Example

`App\Views\Medicine\Create.cs`

```
using App.Controllers;
using ConsoleMVC.Views;

namespace App.Views.Medicine
{
    public class Create : View
    {
        public Create()
        {
        }

        public Create(Dictionary<string, object> data) : base(data)
        {
        }

        protected override void Print()
        {
            PrintMessage();

            Console.WriteLine("Welcome to ConsoleMVC");

            Console.WriteLine("Store: s {name} {quantity}");
            Console.WriteLine("e.g. s {Napa} {20}");
            Console.WriteLine("Back to Index: i");
            Console.WriteLine("Exit: q");
        }

        protected override void RegisterCommands()
        {
            var medicineController = new MedicineController();

            RegisterCommand("s {name} {quantity}", medicineController, "Store");
            RegisterCommand("i", medicineController, "Index");
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
