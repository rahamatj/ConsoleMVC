
namespace ConsoleMVC.Views.Messages
{
    public class AlertMessage : Message
    {
        public AlertMessage(string message) : base(message)
        {
            ForegroundColor = ConsoleColor.Red;
        }
    }
}
