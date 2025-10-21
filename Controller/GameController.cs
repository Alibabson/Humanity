using Humanity.Model;
using Humanity.View;


namespace Humanity.Controller
{
    public class GameController
    {
        private readonly GameModel _model;
        private readonly ConsoleView _view;
        private bool _running = true;

        public GameController(GameModel model, ConsoleView view)
        {
            _model = model;
            _view = view;
        }

        public async Task Run()
        {
            /////////////////////////
            ///_view.Clear();

            if (!_model.IntroPlayed)
            {
                // SCENA: awaria eksperymentu
             /*   _view.Type("> EXPERIMENT 13 – NEURAL SEPARATION: START", 50);
                _view.Type("> Subject: Adrian Holloway", 50);
                _view.Type("> Pulse: 180 bpm", 50);
                _view.Type("> Synaptic link: ACTIVE", 50);
                _view.Separator();

                _view.DarkCyan("It hurts… why can’t I wake up…?", 150);
                _view.DarkCyan("You said we’d see the light… I only see darkness…", 150);
                _view.DarkCyan("Daddy? Why did you put me in the chair?", 150);
             */
                _view.Separator();
                _view.Red("WARNING: Consciousness integrity compromised.");
                _view.Type("> Consciousness fragmentation detected.", 10);
                _view.Type("> REASON... lost.", 14);
                _view.Type("> EMOTION... lost.", 14);
                _view.Type("> MORALITY... lost.", 14);

                Thread.Sleep(500);
                _view.Pulse("...bzzzzz... SYSTEM REBOOT... partial ...");

                _view.Line();
                _view.Type("You wake up on a cold metal floor. The air smells like burnt iron.", 18);
                _view.Type("A monitor flickers nearby.", 18);

                _view.Line();
                _view.Type("Welcome back, Doctor Holloway.", 24);
                _view.Type("You’ve been offline for... 12 years.", 24);

                _view.Separator();
                _view.Line("Commands available: LOOK, LISTEN, RECALL, MOVE <target>, HELP, QUIT");
                _model.IntroPlayed = true;
            }
            else
            {
                // Re-entry po powrocie ze scen
                _view.Line("[LAB CORE]");
                _view.Line("Broken glass, humming consoles. A door labeled [REASON CHAMBER] flickers.");
            }
        
            //////////////////////////
            while (_running)
            {
                var input = await _view.Narrator();
                if (input.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    _running = false;
                    continue;
                }

                var handled = HandleInput(input);
                if (!handled)
                {
                    _view.Line(_model.Uknown());
                }
            }

            _view.Line("\nSession terminated.");
        }
        public bool HandleInput(string s)
        {
            var input = (s ?? "").Trim().ToLowerInvariant();

            switch (input)
            {
                case "help":
                    _view.Line(_model.Help());
                    return true;


                case "look":
                    int idx = _model.room_idx;
                    _model.pickLook(idx);
                    int count = 0;
                    foreach (string x in _model.look)
                    {
                        if(count %2 ==0)
                        {
                            _view.Type(x, 5);
                            count++;
                        }
                        else
                        {
                            _view.DarkCyan(x, 10);
                            count++;
                        }
                    }
                    return true;

                case "listen":
                    _view.DarkCyan("We trusted you...", 150);
                    _view.DarkCyan("You said we'd wake up...", 150);
                    _view.DarkCyan("Why did you leave us here?", 150);
                    _view.Red("[WARNING] Emotional interference detected.");
                    //wyjebac
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
