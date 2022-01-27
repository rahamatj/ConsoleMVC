
using ConsoleMVC.Controllers;
using ConsoleMVC.Views;
using System.Collections;
using System.Reflection;
using System.Text.RegularExpressions;

namespace ConsoleMVC.CommandEngine
{
    public class CommandExecutor : ICommandExecutor
    {
        ICommandRegistry _commandRegistry;

        public CommandExecutor(ICommandRegistry commandRegistry)
        {
            _commandRegistry = commandRegistry;
        }

        View InvokeAction(IController controller, string action, object[] parameters = null)
        {
            var type = controller.GetType();
            var method = type.GetMethod(action);

            if (method == null)
                throw new Exception($"{action} not found in controller: {type.Name}");

            return (View)method.Invoke(controller, parameters);
        }

        object[] ExtractParameters(string input)
        {
            var parameters = new ArrayList();
            var regex = new Regex("{.*?}");

            foreach (var match in regex.Matches(input))
            {
                parameters.Add(match.ToString().Trim(new char[] { '{', '}' }));
            }

            return parameters.ToArray();
        }

        object[] CreateParameters(Dictionary<string, object> request, string input)
        {
            object[] parameters = null;

            if (request != null && request.Count > 0)
                parameters = new object[] { request };

            if (parameters != null)
                return parameters.Concat(ExtractParameters(input)).ToArray();
            else
                return ExtractParameters(input);
        }

        public View ExecuteCommand(string input, Dictionary<string, object> request)
        {
            try
            {
                var (controller, action) = _commandRegistry.Get(input);

                return InvokeAction(controller, action, CreateParameters(request, input));
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return null;
            }
        }
    }
}
