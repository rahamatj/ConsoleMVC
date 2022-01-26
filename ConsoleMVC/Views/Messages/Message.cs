
namespace ConsoleMVC.Views.Messages
{
    public abstract class Message
    {
        protected string _message;
        protected ConsoleColor ForegroundColor { get; set; } = ConsoleColor.Gray;

        public Message(string message)
        {
            _message = message;
        }

        public virtual void Print()
        {
            Console.ForegroundColor = ForegroundColor;
            Console.WriteLine(_message);
            Console.ResetColor();
        }
    }
}
