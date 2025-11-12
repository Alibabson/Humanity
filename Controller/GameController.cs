using Humanity.Model;
using Humanity.View;

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
            //  _model.IntroPlayed = true; //to skasować albo zakomentować
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
                for (int i = 0; i <= 200; i++)
                {
                    string x = _model.Intro[rand.Next(_model.Intro.Count)];
                    _view.Type(x, 0, true);
                    Thread.Sleep(15);

                } // losowe rzeczy z konsoli żeby fajnie wyglądało


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


                foreach (var line in _model.Prologue)
                {
                    _view.TypeText(line.Text, line.DelayMs, line.Color);
                }
                _view.Spectre_Text("[olive slowblink]\nPress any button to continue...[/]");
                _view.AwaitKey();  //sprawdzmy kolory później

                ///////////////////////////////////////////////////////////////////////////// ruiny jakieś nie pamiętam co to
                //_view.TypeText(_model.prologue(), 2, "[italic teal]");
                /*  _view.Line();
                    _view.Type("Welcome, Doctor Holloway.", 24);
                    _view.Type("The machines remember you, even if you no longer remember yourself.", 24);
                    _view.Type("You dug too deep into the human mind...", 24);
                    _view.Type("...and now you’re buried inside it.", 24);
                */
                ////////////////////////////////////////////////////////////////////////////////////

                _view.TypeText(_model.Help(), 2, "[fuchsia]");  //albo kolor usunąć albo powolne pojawianie bo średnie
                _model.IntroPlayed = true;
            }
            else // jak nie gramy intro i sprawdzamy program to niech cokolwiek się pokazuje - w grze to się nigdy chyba nie pojawi
            {
                _view.Spectre_Text(_model.Help());
            }


            while (_running) // prawie nieskończona pętla tylko quitem kończymy granie a wpisywanie komend w nieskończoność. Wygranie/przegranie dodamy
            {

                var input = await _view.Narrator();  //narrator bierze od nas dane i daje ten fioletowy '>'

                var handled = HandleInput(input);  //handleinput obsługuje nam wszystkie komendy
                if (!handled)
                {
                    _view.Line(_model.Uknown());
                }
            }

            _view.Line("\nSession terminated.");
            success = !success;
        }
        public bool HandleInput(string s)       // bierze od nas komendę (LOOKI HELPY i tak dalej), a jak trzeba to drugi element jak przedmiot i pokój
        {
            var command = "";
            var argument = "";
            var input = (s ?? "").Trim().ToLowerInvariant(); //zawsze mała litera będzie uznana więc można odaplać capslock

            if (input.StartsWith("go to "))  //go to jako że jest ze spacją to inaczej się nim zajmujemy lekko
            {

                string room = input.Substring("go to ".Length).Trim();  // bierzemy wszystko po "go to"
                nextRoomIdx = _model.NextRoomIdx(room);  //sprawdzamy jaki numer ma pokój co wpisaliśmy...
                if (nextRoomIdx == -1)
                {
                    _view.Red("Error: Unknown room '" + room + "'. Try again.\n");
                    success = !success;
                    return false;
                }

                checkPossible(nextRoomIdx);  //... i czy można przejśc z obecnego
                return true;
            }
            var parts = input.Split(' ', 2, StringSplitOptions.RemoveEmptyEntries);  //rozdzielamy to co wpisaliśmy spacją i jedna część to komenda a druga to item/pokój a jak brak to null
            command = parts.Length > 0 ? parts[0] : "";
            argument = parts.Length > 1 ? parts[1] : "";

            _view.Clear();

            switch (command) //sprawdzanie co wpisaliśmy
            {
                case "use":
                    if (_model.hasDevice == false)
                    {
                        return false;
                    }
                    else
                    {
                        if (argument == "device")
                        {
                            _itemView.Destroy();
                            _view.Clear();
                            LookFunction("");
                        }
                        else return false;
                    }
                    return true;
                case "help":
                    success = true;
                    HUD();
                    _view.Spectre_Text(_model.Help());
                    return true;


                case "look":
                    _model.LookedRoom[_model.room_idx] = true;  //jak wpisujemy ręcznie look to pokój nam uznaje za LOOKED i przy kolejnym wejściu będziemy mieć opis
                    if (LookFunction(argument)) //lookFunction daje opis właśnie
                    {

                        return true;
                    }
                    else
                    {
                        return false;
                    }

                case "check":
                    idx = _model.room_idx;  //gdzie jesteśmy
                    List<string> desc = _model.checkItem(idx, argument); //opis danego przedmiotu z argumentu
                    switch (argument) //dany przedmiot z argumentu
                    {
                        case "":
                            _view.Red("Error: CHECK command requires an item name as an argument. Try again.\n");
                            success = !success;
                            return false;
                        case "monitor":
                        case "monitors":
                        case "terminal":
                            if (idx == 0)
                            {
                                _itemView.Monitor(desc);  //dajemy ItemView się tym bawić i wrzuca opis z _model CheckItem   
                                                          // _view.AwaitKey();
                                _view.Clear();
                                //HUD();
                                LookFunction("");  //wracamy do głównego ekranu pokoju po funkcji (i LOOK jak już był)

                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "whiteboard":
                            if (idx == 0)
                            {
                                _itemView.Whiteboard(desc);
                                _view.Clear();
                                //HUD();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "floor":
                            if (idx == 1)
                            {
                                _itemView.Key(desc);
                                _view.Clear();
                                //HUD();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "piano":
                            {
                                if (idx == 2)
                                {
                                    _itemView.Piano(desc);
                                    _view.AwaitKey();
                                    _view.Clear();
                                    LookFunction("");
                                }
                            }
                            return true;
                        case "clock":
                            if (idx == 2)
                            {
                                _itemView.Clock(desc);
                                _itemView.ShowClock();
                                _view.AwaitKey();
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "bookshelf":
                            if (idx == 3)
                            {
                                _itemView.Bookshelf(desc);
                                _view.Clear();
                                //HUD();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "table":     
                            if(idx==3)
                            {
                                _itemView.Table(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                                return true;
                        case "newspaper":
                            if (idx == 4)
                            {
                                _itemView.Newspaper(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "photo":
                        case "picture":
                            if (idx == 5)
                            {
                                _itemView.Photo(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;


                        case "note":
                        case "notes":
                            if (idx == 6)
                            {
                                _itemView.Note(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "cabinet":
                            if (idx == 6)
                            {
                                _itemView.Cabinet(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "music":
                        case "musicbox":
                        case "music box":
                            if (idx == 7)
                            {
                                _itemView.MusicBox(desc);
                                _view.AwaitKey();
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        case "diary":
                            if (idx == 7)
                            {
                                _itemView.Diary(desc);
                                _view.Clear();
                                LookFunction("");
                            }
                            else
                            {
                                checkError(argument);
                                return false;
                            }
                            return true;
                        /*case "safe":
                        {
                            if (idx == 8)
                            {
                               _itemView.Safe(desc);
                               _view.Clear();
                               LookFunction("");
                             }
                         }
                         return true; */
                        case "desk":
                            {
                                if (idx == 8)
                                {
                                    _itemView.Desk(desc);
                                    _view.Clear();
                                    LookFunction("");
                                }
                                else
                                {
                                    checkError(argument);
                                    return false;
                                }
                            }
                            return true;
                        default:
                            _view.Red("Error: There is no item named '" + argument + "' in this room. Try again.\n");
                            success = !success;
                            return false;
                    }
                case "exit":
                case "quit":
                    if (argument != "")
                    {
                        _view.Red("Error: EXIT/QUIT command does not take any arguments. Try again without " + argument + "\n");
                        success = !success;
                        return false;
                    }
                    _view.Type(_model.Quit(), 14, false);
                    _running = false;
                    return true;

                default:
                    return false;
            }
        }
        private void checkPossible(int nextRoomIdx)  //Czy z obecnego można iść do wpisanego
        {
            int idx = _model.room_idx;
            switch (idx)
            {
                case 0: //LABORATORIUM   [Można tylko do STAIRS]
                    if (nextRoomIdx == 1)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 1: //STAIRS   [Można do LAB lub LIVING ROOM lub HALLWAY]
                    if (nextRoomIdx == 0 || nextRoomIdx == 2 || nextRoomIdx == 5)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 2: //LIVING ROOM   [Można do KITCHEN lub HALLWAY lub STAIRS lub LIBRARY]
                    if (nextRoomIdx == 4 || nextRoomIdx == 1 || nextRoomIdx == 5 || nextRoomIdx == 3)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 3: //LIBRARY   [Można tylko do LIVING ROOM]
                    if (nextRoomIdx == 2)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 4: //KITCHEN   [Można tylko ldo LIVING ROOM]
                    if (nextRoomIdx == 2)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 5: //HALLWAY   [Można do BATHROOM lub BEDROOM lub OFFICE lub LIVING ROOM lub STAIRS] 
                    if (nextRoomIdx == 6 || nextRoomIdx == 7 || nextRoomIdx == 2 || nextRoomIdx == 1)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else if (nextRoomIdx == 8) //tutaj musimy osobnie sprawdzać bo na klucz jest OFFICE
                    {
                        if (_model.hasKey)
                        {
                            _view.Spectre_Text("[green]You used the key.[/]");
                            NextRoomProcess(nextRoomIdx);
                        }
                        else
                        {
                            locked();
                        }
                    }
                    else
                    {
                        notOk();
                    }

                    break;
                case 6: // BATHROOM   [Można do HALLWAY lub BEDROOM]
                    if (nextRoomIdx == 5 || nextRoomIdx == 7)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 7: //BEDROOM   [Można do HALLWAY lub BATHROOM]
                    if (nextRoomIdx == 5 || nextRoomIdx == 6)
                    {
                        NextRoomProcess(nextRoomIdx);
                    }
                    else
                    {
                        notOk();
                    }
                    break;
                case 8: //OFFICE   [Można tylko do HALLWAY]
                    if (nextRoomIdx == 5)
                    {
                        NextRoomProcess(nextRoomIdx);
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
        public void NextRoomProcess(int nextRoomIdx) //przejście do pokoju, losowanie ducha, wypisanie ładowania, i opis jak potrzebny
        {
            _model.GoTo_Possible(nextRoomIdx);
            ok(nextRoomIdx);
            _view.Loading();
            Thread.Sleep(10);
            RandomGhost();
            // HUD();
            LookFunction("");
        }
        public void ok(int nextRoomIdx)
        {
            _view.Line("\nYou move to the " + _model.RoomName(nextRoomIdx) + ".\n");
            success = true;

        }
        public void notOk()
        {
            _view.Red("Error: You can't go to that room from here. Try again.\n");
            success = !success;
        }
        public void locked()
        {
            _view.Red("Error: The room is locked. You probably need a key...\n");
            success = !success;
        }

        public bool success = true;  //jak się nie uda czegoś zrobić w kodzie to lepiej nie pokazywać HUDa bo 2 razy się pojawi
        public void HUD() //spisanie obecnego pokoju i licznika SANITY
        {
            if (success)
            {
                int idx = _model.room_idx;
                int sanity = _model.sanity;
                _view.Panel("[bold]CURRRENT ROOM: [/][aqua]" + _model.RoomName(idx) + "[/]\n \n", "[bold]SANITY: [/]", sanity);  //panel() robi nam miejsce na lewo/prawo i diagram z SANITY
            }
        }

        private void RandomGhost() //losowanie od 0 do 100 i 15% na wypis jednego z duchów
        {
            var rng = new Random();
            int procent = 0;
            if (_model.sanity >= 75) procent =5; // jak duzo sanity to maly procent
            else if (_model.sanity >= 50) procent = 15;
            else procent = 25;
            int chance = rng.Next(1, 101); // Losowa liczba od 1 do 100
            if (chance <= procent)
            {
                int ghostIndex = rng.Next(_model.Ghosts.Count);  //losowanie cyfry
                string ghostMessage = _model.Ghosts[ghostIndex];
                _view.TypeText(ghostMessage, 90, "[red bold rapidblink]", true);
                _model.sanity = _model.sanity - 10; //-10 sanity jak duch
                Task.Delay(2000).Wait(); //czekamy 2s po pełnym napisie
                _view.Clear();
            }
            else _model.sanity = _model.sanity - 5; //-5 sanity jak normalne przejście
        }
        private void checkError(string argument)
        {
            _view.Red("Error: There is no item named '" + argument + "' in this room. Try again.\n");  //problem z checkiem tu się wyjaśnia
            success = !success;
        }
        private bool LookFunction(string argument)
        {
            if (argument != "")
            {
                _view.Red("Error: LOOK command does not take any arguments. Try again without " + argument + "\n");  //look nie przyjmuje wartości temu wdzędzie na górze było LookFunction("")
                success = !success;
                return false;
            }
            success = true;
            HUD();  //spisuje HUD (zawsze), ale LOOK pod warunkiem
            idx = _model.room_idx;
            if (_model.LookedRoom[idx] == true) //jak było LOOK
            {
                _model.pickLook(idx, part); //wybiera opis w zależności od pokoju
                foreach (string x in _model.look)
                {
                    _view.Spectre_Text(x); //wypis
                }
            }
            return true;
        }
    }
}

