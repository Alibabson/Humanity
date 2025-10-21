using Humanity.Model;
using Humanity.View;
using System;
using System.Globalization;

namespace Humanity.Controller
{
    public class GameController
    {
        private readonly GameModel _model;
        private readonly ConsoleView _view;
        private bool _running = true;
        public int idx;
        public int nextRoomIdx;
        public int part;
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
               // // SCENA: awaria eksperymentu
             /*   _view.Type("> EXPERIMENT 13 – NEURAL SEPARATION: START", 50);
                _view.Type("> Subject: Adrian Holloway", 50);
                _view.Type("> Pulse: 180 bpm", 50);
                _view.Type("> Synaptic link: ACTIVE", 50);
                _view.Separator();

                _view.Ghost("It hurts… why can’t I wake up…?", 150);
                _view.Ghost("You said we’d see the light… I only see darkness…", 150);
                _view.Ghost("Daddy? Why did you put me in the chair?", 150);
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
                _view.Line(_model.Help());
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
            if(input.StartsWith("go to "))
            {
                string room = input.Substring("go to ".Length).Trim();
                nextRoomIdx =_model.NextRoomIdx(room);
                if(nextRoomIdx == -1)
                {
                    _view.Red("Error: Unknown room '"+ room + "'. Try again.\n");
                    return false;
                }
                checkPossible(nextRoomIdx);
                return true;
            }
            var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);
            var command = parts.Length > 0 ? parts[0] : "";
            var argument = parts.Length > 1 ? parts[1] : "";

            switch (command)
            {
                case "help":
                    _view.Line(_model.Help());
                    return true;


                case "look":
                    part = 1;
                    if (argument!="")
                    {
                        _view.Red("Error: LOOK command does not take any arguments. Try again without "+argument +"\n");
                        return false;
                    }
                    idx = _model.room_idx;
                    _model.pickLook(idx, part);
                    int count = 0;
                    foreach (string x in _model.look)
                    {
                        if(count %2 ==0)
                        {
                            _view.LineNoEnter(x);
                            count++;                       
                        }
                        else
                        {
                            _view.DarkCyan(x);
                            count++;
                        }
                    }
                    part = 2;

                    return true;

                case "check":
                    idx = _model.room_idx;

                    _view.LineNoEnter(_model.checkItem(idx,argument));
                    return true;


                default:            
                    return false;
            }
        }
        private void checkPossible(int nextRoomIdx)
        {
            int idx = _model.room_idx;
            switch (idx)
            {
                case 0: //LABORATORIUM   [Można tylko do F1 HALLWAY]
                    if (nextRoomIdx == 1)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 1: //F1 HALLWAY   [Można do F1 BATHROOM lub LIVING ROOM lub LAB lub KITCHEN]
                    if (nextRoomIdx == 2 || nextRoomIdx == 3 || nextRoomIdx == 4 || nextRoomIdx==0)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 2: //KITCHEN   [Można do LIVING ROOM lub F2 HALLWAY lub F1 HALLWAY]
                    if  (nextRoomIdx == 1 || nextRoomIdx == 4 || nextRoomIdx==5)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 3: //F1 BATHROOM   [Można tylko do F1 HALLWAY]
                    if (nextRoomIdx == 1)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 4: //LIVING ROOM   [Można do KITCHEN lub F1 HALLWAY]
                    if (nextRoomIdx == 2 || nextRoomIdx==1)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                 case 5: //F2 HALLWAY   [Można do F2 BATHROOM lub BEDROOM lub KITCHEN lub OFFICE] 
                    if (nextRoomIdx == 6 || nextRoomIdx == 7 || nextRoomIdx == 8 || nextRoomIdx==2)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                 case 6: //F2 BATHROOM   [Można tylko do F2 HALLWAY]
                    if (nextRoomIdx == 5)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                 case 7: //BEDROOM   [Można tylko do F2 HALLWAY]
                    if (nextRoomIdx == 5)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                 case 8: //OFFICE   [Można tylko do F2 HALLWAY]
                    if (nextRoomIdx == 5)
                    {
                        _model.GoTo_Possible(nextRoomIdx);
                        ok(nextRoomIdx);
                        _view.Loading();
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                default:
                    notOk();
                    break;
            }

        }
        public void ok(int nextRoomIdx)
        {
            _view.Line("\nYou move to the " + _model.RoomName(nextRoomIdx) + ".\n");
        }
        public void notOk()
        {
            _view.Red("Error: You can't go to that room from here. Try again.\n");
        }
    }
}
