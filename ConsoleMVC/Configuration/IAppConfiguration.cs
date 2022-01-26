
using ConsoleMVC.Container;

namespace ConsoleMVC.Configuration
{
    public interface IAppConfiguration : IContainable
    {
        T Get<T>() where T : ISection;
    }
}
