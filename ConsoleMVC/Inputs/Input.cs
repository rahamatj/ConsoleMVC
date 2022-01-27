
using ConsoleMVC.Inputs.Validations;
using ConsoleMVC.Messages;

namespace ConsoleMVC.Inputs
{
    public static class Input
    {
        public static bool Validate(string input, IValidation[] validations)
        {
            if (validations != null && validations.Length > 0)
            {
                foreach (var validation in validations)
                {
                    if (!validation.Check(input))
                    {
                        var alertMessage = new AlertMessage(validation.ErrorMessage);
                        alertMessage.Print();

                        return false;
                    }
                }
            }

            return true;
        }

        public static string Prompt(string prompt, IValidation[] validations = null)
        {

            string input = String.Empty;

            Console.WriteLine();
            Console.Write(prompt);
            input = Console.ReadLine();

            while (!Validate(input, validations))
            {
                Console.WriteLine();
                Console.Write(prompt);
                input = Console.ReadLine();
            }

            return input;
        }
    }
}
