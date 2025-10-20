using Humanity.Controller;
using Humanity.Model;
using Humanity.View;

namespace Humanity
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Title = "H U M A N I T Y";
            Console.OutputEncoding = System.Text.Encoding.UTF8;   // Tytuł i znaki specjalne

            var state = new GameState();
            var view = new ConsoleView();
            var ctrl = new GameController(state, view);   //pobieranie stanu widoku i kontrolera

            // Rejestracja scen
            ctrl.Register(new IntroScene(ctrl, state, view));         //cokolwiek to jest
          //  ctrl.Register(new ReasonChamberScene(ctrl, state, view));

            // Start
            ctrl.SwitchScene(IntroScene.IdStatic);
            ctrl.Run();
        }
    }
}
