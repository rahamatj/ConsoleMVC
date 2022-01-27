
namespace ConsoleMVC.Inputs.Validations
{
    public class YesNoEmpty : IValidation
    {
        public string ErrorMessage { get; set; } = "Input can only be 'y' or 'n' or leave empty to confirm.";

        public bool Check(string input)
        {
            if (input == String.Empty || input == "y" || input == "n")
                return true;

            return false;
        }
    }
}
