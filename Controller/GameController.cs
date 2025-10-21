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

        public void Run()
        {
            while (_running)
            {
                var input = _view.Narrator();
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
