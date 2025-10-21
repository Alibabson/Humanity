/*using Humanity.Model;
using Humanity.View;

namespace Humanity.Controller
{

    public class GameController2
    {
        private readonly Dictionary<string, IGameScene> _scenes = new();
        private readonly GameState _state;
        private readonly ConsoleView _view;
        private bool _running = true;

        public GameController2(GameState state, ConsoleView view)
        {
            _state = state;
            _view = view;
        }

        public void Register(IGameScene scene) => _scenes[scene.Id] = scene;

        public void SwitchScene(string id)
        {
            if (!_scenes.ContainsKey(id))
            {
                _view.Red($"[ENGINE] Scene '{id}' not found.");
                return;
            }
            _state.CurrentSceneId = id;
            _scenes[id].Enter();
        }

        public void Run()
        {
            while (_running)
            {
                var scene = _scenes[_state.CurrentSceneId];
                var input = _view.Narrator();
                if (input.Equals("quit", StringComparison.OrdinalIgnoreCase) ||
                    input.Equals("exit", StringComparison.OrdinalIgnoreCase))
                {
                    _running = false;
                    continue;
                }

                var handled = scene.HandleInput(input);
                if (!handled)
                {
                    _view.Line("Unknown command. Try: LOOK, LISTEN, RECALL, HELP, MOVE ...");
                }
            }

            _view.Line("\nSession terminated.");
        }
    }
}
*/
