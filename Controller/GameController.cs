using Humanity.Model;
using Humanity.View;
using System;
using System.Globalization;
using System.Reflection.PortableExecutable;

namespace Humanity.Controller
{
    public class GameController
    {
        private readonly GameModel _model;
        private readonly ConsoleView _view;
        private readonly ItemView _itemView;
        private bool _running = true;
        public int idx;
        public int nextRoomIdx;
        public int part;
        public GameController(GameModel model, ConsoleView view, ItemModel itemModel)
        {
            _model = model;
            _view = view;
            _itemView = new ItemView(view, model, itemModel);
        }
        
        public void FakeLoad()
        {
            _view.Clear();
           _view.FakeLoad();
        }
        public async Task Run()
        {
          //  _model.IntroPlayed = true; //to skasować
            _view.Clear();
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
               // _view.PlaySound("comp.wav");   //trwa 23 sekundy

               
                var rand = new Random();
                for (int i = 0; i <= 200; i ++)
                {
                    string x = _model.Intro[rand.Next(_model.Intro.Count)];
                    _view.Type(x, 0, true);
                    Thread.Sleep(15);

                }
                Thread.Sleep(10);
                _view.Red("WARNING: Consciousness integrity compromised.");
                _view.Type("> Consciousness fragmentation detected.", 10, true);
                _view.Type("> REASON... lost.", 14, true);
                _view.Type("> EMOTION... lost.", 14, true);
                _view.Type("> MORALITY... lost.", 14, true);

                Thread.Sleep(500);

                Thread.Sleep(500);
                _view.Type("> Awaiting system reboot...", 14, true);
                _view.Pulse("----- SYSTEM REBOOT -----");
                _view.Type("\r----- SYSTEM REBOOT ----- PARTIAL", 14, true);
                Thread.Sleep(2000);
                _view.Clear();
                _view.Line();

                /*  _view.Line();
                    _view.Type("Welcome, Doctor Holloway.", 24);
                    _view.Type("The machines remember you, even if you no longer remember yourself.", 24);
                    _view.Type("You dug too deep into the human mind...", 24);
                    _view.Type("...and now you’re buried inside it.", 24);
                */
                _view.TypeText(_model.Help(), 2, "[fuchsia]"); 
                _model.IntroPlayed = true;
            }
            else
            {
                _view.Line(_model.Help());
            }
        
            //////////////////////////
            while (_running)
            {
                var input = await _view.Narrator();
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

            _view.Clear();
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
                    foreach (string x in _model.look)
                    {
                        _view.Spectre_Text(x);
                    }
                    return true;

                case "check":
                    idx = _model.room_idx;
                    List<string> desc = _model.checkItem(idx, argument);
                    switch (argument)
                    {
                        case "":
                            _view.Red("Error: CHECK command requires an item name as an argument. Try again.\n");
                            return false;
                        case "monitor":
                            _itemView.Monitor(desc);
                            return true;
                        case "whiteboard":
                            _itemView.Whiteboard(desc);
                            return true;

                        default:
                            _view.Red("Error: There is no item named '" + argument + "' in this room. Try again.\n");
                            return false;
                    }
                case "exit":
                case "quit":
                    if (argument != "")
                    {
                        _view.Red("Error: EXIT/QUIT command does not take any arguments. Try again without " + argument + "\n");
                        return false;
                    }
                    _view.Type(_model.Quit(), 14, false);
                    _running = false;
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
