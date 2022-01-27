
namespace ConsoleMVC.Inputs.Validations
{
    public class Integer : IValidation
    {
        public string ErrorMessage { get; set; } = "Field must be an integer.";

        public bool Check(string input)
        {
            int i;

            return int.TryParse(input, out i);
        }
    }
}
