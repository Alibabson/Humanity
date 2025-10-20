using HauntedTerminal.Controller;
using HauntedTerminal.Model;
using HauntedTerminal.View;
using HauntedTerminal.Scenes;

namespace HauntedTerminal
{
    internal static class Program
    {
        private static void Main()
        {
            Console.Title = "The Holloway Experiment – Fragments of Humanity";
            Console.OutputEncoding = System.Text.Encoding.UTF8;

            var state = new GameState();
            var view = new ConsoleView();
            var ctrl = new GameController(state, view);

            // Rejestracja scen
            ctrl.Register(new IntroScene(ctrl, state, view));
          //  ctrl.Register(new ReasonChamberScene(ctrl, state, view));

            // Start
            ctrl.SwitchScene(IntroScene.IdStatic);
            ctrl.Run();
        }
    }
}
