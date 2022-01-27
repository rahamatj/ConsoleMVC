
namespace ConsoleMVC.Inputs.Validations
{
    public interface IValidation
    {
        string ErrorMessage { get; set; }
        bool Check(string input);
    }
}
