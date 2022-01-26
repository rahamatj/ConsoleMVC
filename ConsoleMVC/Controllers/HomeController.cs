
using ConsoleMVC.Views;

namespace ConsoleMVC.Controllers
{
    public class HomeController : IController
    {
        public View Index()
        {
            return new Views.Home.Index();
        }
    }
}
