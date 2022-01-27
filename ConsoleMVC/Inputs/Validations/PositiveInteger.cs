
namespace ConsoleMVC.Inputs.Validations
{
    public class PositiveInteger : IValidation
    {
        public string ErrorMessage { get; set; } = "Field must be a positive integer.";

        public bool Check(string input)
        {
            int i;

            if (int.TryParse(input, out i))
            {
                return i >= 0;
            }

            return false;
        }
    }
}
