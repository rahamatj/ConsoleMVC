
namespace ConsoleMVC.Messages
{
    public class SuccessMessage : Message
    {
        public SuccessMessage(string message) : base(message)
        {
            ForegroundColor = ConsoleColor.Green;
        }

    }
}
