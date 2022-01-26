
namespace ConsoleMVC.Container
{
    public interface IAppContainer : IContainable
    {
        void Register<T>(IContainable containable) where T : IContainable;

        T Get<T>() where T : IContainable;
    }
}
