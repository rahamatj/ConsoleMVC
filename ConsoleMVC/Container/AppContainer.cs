using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleMVC.Container
{
    public class AppContainer : IAppContainer
    {
        Dictionary<string, IContainable> _container = new Dictionary<string, IContainable>();

        public void Register<T>(IContainable containable) where T : IContainable
        {
            _container.Add(typeof(T).Name, containable);
        }

        T GetObject<T>()
        {
            var key = typeof(T).Name;
            if (!_container.ContainsKey(key))
                throw new Exception($"{key} does not exist in the app container.");

            return (T)_container[key];
        }

        public T Get<T>() where T : IContainable
        {
            try
            {
                return GetObject<T>();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return default(T);
            }
        }
    }
}
