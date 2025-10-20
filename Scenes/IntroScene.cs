using HauntedTerminal.Controller;
using HauntedTerminal.Model;
using HauntedTerminal.View;

namespace HauntedTerminal.Scenes
{
    public class IntroScene : IGameScene
    {
        public const string IdStatic = "intro";
        public string Id => IdStatic;

        private readonly GameController _ctrl;
        private readonly GameState _state;
        private readonly ConsoleView _view;

        public IntroScene(GameController ctrl, GameState state, ConsoleView view)
        {
            _ctrl = ctrl; _state = state; _view = view;
        }

        public void Enter()
        {
            _view.Clear();

            if (!_state.IntroPlayed)
            {
                // SCENA: awaria eksperymentu
                _view.Type("> EXPERIMENT 13 – NEURAL SEPARATION: START", 6);
                _view.Type("> Subject: Adrian Holloway", 6);
                _view.Type("> Pulse: 180 bpm", 6);
                _view.Type("> Synaptic link: ACTIVE", 6);
                _view.Separator();

                _view.Ghost("It hurts… why can’t I wake up…?");
                _view.Ghost("He said we’d see the light… I only see red…");
                _view.Ghost("Daddy? Why did you put me in the chair?");

                _view.Separator();
                _view.SystemError("WARNING: Consciousness integrity compromised.");
                _view.Type("> Consciousness fragmentation detected.", 10);
                _view.Type("> REASON... lost.", 14);
                _view.Type("> EMOTION... lost.", 14);
                _view.Type("> MORALITY... lost.", 14);

                Thread.Sleep(500);
                _view.Pulse("...bzzzzz... SYSTEM REBOOT... partial ...", 2);

                _view.Line();
                _view.Type("You wake up on a cold metal floor. The air smells like burnt iron.", 18);
                _view.Type("A monitor flickers nearby.", 18);

                _view.Line();
                _view.Type("Welcome back, Doctor Holloway.", 24);
                _view.Type("You’ve been offline for... 12 years.", 24);

                _view.Separator();
                _view.Line("Commands available: LOOK, LISTEN, RECALL, MOVE <target>, HELP, QUIT");
                _state.IntroPlayed = true;
            }
            else
            {
                // Re-entry po powrocie ze scen
                _view.Line("[LAB CORE]");
                _view.Line("Broken glass, humming consoles. A door labeled [REASON CHAMBER] flickers.");
                _view.Line(_state.StatusLine);
            }
        }

        public bool HandleInput(string s)
        {
            var input = (s ?? "").Trim().ToLowerInvariant();

            switch (input)
            {
                case "help":
                    _view.Line("Try: LOOK, LISTEN, RECALL, MOVE REASON, STATUS, QUIT");
                    return true;

                case "status":
                    _view.Line(_state.StatusLine);
                    return true;

                case "look":
                    _view.Line("You see broken monitors, scattered instruments, and a glowing console.");
                    _view.Line("The door to the [REASON CHAMBER] flickers in and out of existence.");
                    return true;

                case "listen":
                    _view.Ghost("We trusted you...");
                    _view.Ghost("You said we'd wake up...");
                    _view.Ghost("Why did you leave us here?");
                    _view.SystemWarn("[WARNING] Emotional interference detected.");
                    return true;

                case "recall":
                    var log = _state.MemoryLogs[_state.MemoryIndex % _state.MemoryLogs.Count];
                    _state.MemoryIndex++;
                    _view.Line(log);
                    return true;

                case "move reason":
                case "move to reason":
                case "move to reason chamber":
                case "enter reason":
                    _view.Line("Access granted. Initiating cognitive reconstruction protocol...");
                    Thread.Sleep(400);
                   // _ctrl.SwitchScene(ReasonChamberScene.IdStatic);
                    return true;

                default:
                    // drobna pomoc składniowa
                    if (input.StartsWith("move "))
                    {
                        _view.Line("Unknown destination. Try: MOVE REASON");
                        return true;
                    }
                    return false;
            }
        }
    }
}
