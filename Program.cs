using Humanity.Controller;
using Humanity.Model;
using Humanity.View;
using System.Security.Cryptography.X509Certificates;
using System.Threading.Tasks;

namespace Humanity
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //znaki specjalne
  


            var model = new GameModel();
            var view = new ConsoleView();
            var ctrl = new GameController(model, view);
            await ctrl.GameStart();
            ctrl.FakeLoad();
            await ctrl.Run();
        }

    }
}
