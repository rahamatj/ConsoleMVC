
using ConsoleMVC.Controllers;
using System.Text.RegularExpressions;

namespace ConsoleMVC.CommandEngine
{
    public class CommandRegistry : ICommandRegistry
    {
        Dictionary<string, (IController controller, string action)> _register = new Dictionary<string, (IController controller, string action)>();

        public void RegisterCommand(string command, IController controller, string action)
        {
            _register.Add(command, (controller, action));
        }

        public void RegisterCommands(Dictionary<string, (IController controller, string action)> register)
        {
            _register = register;
        }

        public (IController controller, string action) Get(string key)
        {
            if (_register != null && _register.Count > 0)
            {
                foreach (var validCommand in _register.Keys)
                {
                    if (Match(validCommand, key))
                        return _register[validCommand];
                }
            }

            throw new Exception($"Command: {key} does not exist in the command registry.");
        }

        bool Match(string command, string input)
        {
            var pattern = Regex.Replace(command, @"\s", @"\s");
            pattern = Regex.Replace(pattern, "{.*?}", "{.*?}");
            pattern = $"^{pattern}";

            var regex = new Regex(pattern);
            var match = regex.Match(input);

            return match.Success;
        }

        public bool IsCommandValid(string input)
        {
            if (_register != null && _register.Count > 0)
            {
                foreach (var validCommand in _register.Keys)
                {
                    if (Match(validCommand, input))
                        return true;
                }
            }

            return false;
        }

        public void ClearRegister()
        {
            _register.Clear();
        }
    }
}
