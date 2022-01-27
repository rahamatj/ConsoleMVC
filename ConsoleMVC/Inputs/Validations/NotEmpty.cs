
namespace ConsoleMVC.Inputs.Validations
{
    public class NotEmpty : IValidation
    {
        public string ErrorMessage { get; set; } = "Field can not be empty.";

        public bool Check(string input)
        {
            return input != String.Empty;
        }
    }
}
