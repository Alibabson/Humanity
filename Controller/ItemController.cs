using Humanity.Model;
using Humanity.View;
using Spectre.Console;
using Spectre.Console.Cli;
namespace Humanity.Controller
{
    public class ItemController
    {
        private readonly ConsoleView _View;
        private readonly GameModel _Model;
        private readonly ItemModel _itemModel;


        public ItemController(ConsoleView view, GameModel g_model, ItemModel itemModel)
        {
            _View = view;
            _Model = g_model;
            _itemModel = itemModel;
        }

        ////TERMINAL (LAB - 0)
        public void Monitor(List<string> text)
        {
            var command = "";
            if (_Model.Reason && _Model.Emotion && _Model.Morality)
            {
                 command = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                     .Title(text[0])
                     .HighlightStyle(new Style(foreground: Color.Black, background: Color.Green))
                     .PageSize(4)
                     .AddChoices(text.GetRange(1, text.Count - 1))
                 );
            }
            else
            {
                command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(text[0])
                    .HighlightStyle(new Style(foreground: Color.Black, background: Color.Green))
                    .PageSize(4)   
                    .AddChoices(text[1], text[2], text[4])
                );
            }
            if (command == text[1])   //sprawdz status
            {
                _Model.Status();
                foreach (var line in _Model.statusLines)
                {
                    _View.Spectre_Text(line + "\n");
                }
                BackPrompt(text);
            }
            else if (command == text[2]) //sprawdz logi
            {
                if (!_Model.DEVICE)
                {
                    _View.Clear();
                    int page = 1;
                    LOG(page, text);
                }
                else
                {
                    _View.Clear();
                    _View.Spectre_Text(_itemModel.terminalAddons[0]);
                    _View.AwaitKey();
                }
            }
            else if (command == text[3])    //// KONIEC GRY
            {
                _View.Clear();
                command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(_itemModel.Epilog[0])
                    .HighlightStyle(new Style(foreground: Color.Black, background: Color.Green))
                    .PageSize(4)
                    .AddChoices(_itemModel.Epilog[1], _itemModel.Epilog[2])
                );
                if (command == _itemModel.Epilog[1])
                {
                    foreach (var line in _Model.EpilogueGood)
                    {
                        _View.TypeText(line.Text, line.DelayMs, line.Color);
                    }
                }
                if(command == _itemModel.Epilog[2])
                {
                    foreach (var line in _Model.EpilogueBad)
                    {
                        _View.TypeText(line.Text, line.DelayMs, line.Color);
                    }
                }
                _View.Spectre_Text(_Model.pressAny[0]);
                _View.AwaitKey();
                _View.Clear();
                _View.Spectre_Text(_Model.goodbye);
                Task.Delay(5000).Wait();
                //return;
                Environment.Exit(0);

            }
            else if (command == text[4])
            {
                _View.Spectre_Text(_itemModel.terminalAddons[1]);
                return;
            }
                return;
        }
        private void BackPrompt(List<string> text)
        {
            _View.Spectre_Text(_itemModel.terminalAddons[2]);
            _View.AwaitKey();
            _View.Clear();
            Monitor(text);
        }
        private void LOG(int page, List<string> text)
        {
            _itemModel.Logs(page);
            foreach (var line in _itemModel.logLines)
            {
                _View.Spectre_Text(line + "\n");
            }
            var k = _View.CheckKey();
            if (k == ConsoleKey.RightArrow)
            {
                page++;
                if (page > 5) page = 5;
                _View.Clear();
                LOG(page, text);
            }
            else if (k == ConsoleKey.LeftArrow)
            {
                page--;
                if (page < 1) page = 1;
                _View.Clear();
                LOG(page, text);
            }
            else if (k == ConsoleKey.Enter)
            {
                _View.Clear();
                Monitor(text);
            }
        }

        /////WHITEBOARD (LAB - 0)
        public bool passed = false;
        private int guesses = 0;
        private bool hint = false;
        public bool Whiteboard(List<string> text)
        {
            var txt = text;
            if (!passed)
            {

                _View.Spectre_Text(text[0] + "\n");
                _View.Spectre_Text(text[1]);
                var command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                    .PageSize(4)
                    .AddChoices(text[2], text[3])
                );

                if (command == text[2])
                {
                    _View.Clear();
                    while (!passed)
                    {
                        if (hint == true)
                        {
                            AnsiConsole.Write(new Columns(
                                    new Text(_itemModel.whiteboardList[0], new Style(Color.DodgerBlue3)),
                                    new Text(_itemModel.whiteboardList[1], new Style(Color.Purple3))

                                ));
                        }
                        else
                        {
                            AnsiConsole.Write(new Columns(
                                     new Text(_itemModel.whiteboardList[0], new Style(Color.DodgerBlue3))
                                 //new Spectre.Console.Text(_itemModel.whiteboardList[1], new Style(Color.Pink3))

                                 ));
                        }

                        _View.Spectre_Text(_itemModel.whiteboardList[2]);
                        if (guesses >= 3) _View.Spectre_Text(_itemModel.whiteboardList[3]);
                        _View.Spectre_Text(_itemModel.whiteboardList[4]);

                        string ans = _View.ReadLine();
                        if (guesses >= 3)
                        {
                            if (ans.ToLower() == "hint")
                            {
                                hint = true;
                                _View.Clear();
                                continue;
                            }
                        }
                        if (ans == "1")
                        {
                            //_View.Clear();
                            Whiteboard_passed();
                            return true;
                        }
                        else
                        {
                            guesses++;
                            _View.IncorrectBeep();
                            _View.Clear();
                            //Whiteboard(txt);
                        }
                    }
                    //return false;
                }
                else if (command == text[3])
                {
                    _View.Spectre_Text(_itemModel.whiteboardList[5]);
                    return false;
                }
                return false;
            }
            else
            {
                _View.Spectre_Text(_itemModel.whiteboardList[6]);
                _View.AwaitKey();
                return true;
            }
            //return true;
        }
        public void Whiteboard_passed()
        {
            passed = true;
            //_itemModel.JoinPassword();
            _View.Spectre_Text(_itemModel.whiteboardList[7]);
            _View.Spectre_Text(_Model.continuee);
            _View.AwaitKey();
        }



        //////FLOOR (STAIRS - 1)
        public void Key(List<string> text)
        {
            foreach (var line in text)
            {
                _View.Spectre_Text(line + "\n");
            }
            _View.AwaitKey();
        }



        //// NEWSPAPER (KITCHEN - 4)
        public void Newspaper(List<string> text)
        {
            _View.Spectre_Text(text[0] + "\n");
            /*var command = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title("")
                .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                .PageSize(4)
                .AddChoices(text[1], text[2]) 
            );
            if (command == text[2])
            {
                return;
            }
            if (command == text[1])
            { */
            _itemModel.Newspaper();
            var lines = _itemModel.GetNewspaper;
            foreach (var x in lines)
            {
                _View.Spectre_Text(x);
            }
            _View.Spectre_Text(_Model.continuee);
            _View.AwaitKey();
            //}
        }



        /////BOOKSHELF (LIBRARY - 3)
        public void Bookshelf(List<string> text)
        {
            List<string> tmp = text;
            bool quitted = false;
            _View.Clear();

            _View.Spectre_Text(_itemModel.bookshelf[0]);
            while (!quitted)
            {
                var input = _View.Narrator2("-> ");
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                if (input == "back") { quitted = !quitted; return; }
                input = input.Trim().ToLowerInvariant();
                var parts = input.Split(' ', 2);
                if (parts.Length < 2)
                {
                    _View.Spectre_Text(_itemModel.bookshelf[1]);
                    continue;
                }
                int.TryParse(parts[0], out int row);
                if (parts[0] == "") { quitted = !quitted; break; }
                int.TryParse(parts[1], out int col);
                if (parts[1] == "") { quitted = !quitted; break; }
                if (row <= 0 || col <= 0 || row > 12 || col > 60)
                {
                    _View.Spectre_Text(_itemModel.bookshelf[2]);
                }
                if (row == 9 && col == 5)
                {
                    _View.Clear();
                    _View.Spectre_Text(text[0]);
                    var command = AnsiConsole.Prompt(
                    new SelectionPrompt<string>()
                      .Title("")
                      .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                      .PageSize(4)
                      .AddChoices(text[1], text[2])
                     );
                    if (command == text[1])
                    {
                        Poem(tmp);
                    }
                    else
                    {
                        //Bookshelf(tmp);
                    }
                }
                else if (row == 4 && col == 20)
                {
                    _View.Clear();
                    _View.Spectre_Text("\n[black on white]" + _itemModel.ShowNotes() + "[/]\n");
                    _View.AwaitKey();
                    //Bookshelf(tmp);
                }
                else
                {
                    _View.Clear();
                    _View.Spectre_Text(_itemModel.bookshelf[3]);
                    _View.AwaitKey();
                    //Bookshelf(tmp);
                }
            }
        }

        
        private bool PoemOpened = false;
        public void Poem(List<string> text)
        {
            _View.Clear();
            if (!PoemOpened)
            {
                foreach (string s in _itemModel.Poem)
                {
                    _View.Spectre_Text(s);
                }
                _View.CheckKey();
                if (KeyFragment("REASON"))
                {
                    PoemOpened = true;
                }
                else
                {
                    _View.Spectre_Text(_Model.wrong);
                    _View.AwaitKey();
                    Bookshelf(text);
                    return;
                }

            }
            else
            {
                _View.Spectre_Text(_itemModel.List_replay[0]);
            }
            //_View.AwaitKey();
            Bookshelf(text);
        }

        //////TABLE (LIBRARY - 3)
        public void Table(List<string> text)
        {
            _View.Clear();
            foreach (string s in text)
            {
                _View.Spectre_Text(s);
            }
            _View.AwaitKey();
        }




        /////CLOCK (LIVING ROOM - 2)
       

        public void Clock(List<string> text)
        {
            _View.Spectre_Text(text[0] + "\n");
            _View.Spectre_Text(_Model.continuee);
        }

        ///// PIANO (LIVING ROOM - 4)
        public void Piano(List<string> text)
        {
            int order = 0;
            if (!_Model.hasDiaryKey)
            {
                _View.Spectre_Text(text[0]);
                _View.Spectre_Text(text[1]);
                _View.Spectre_Text("\n[silver]" + _itemModel.ShowPiano() + "[/]\n");
            }
            else
            {
                order = 0;
                _View.Spectre_Text(text[2]);
                _View.Spectre_Text("\n[silver]" + _itemModel.ShowPiano() + "[/]\n");

            }
            while (order < 4)
            {
                var k = _View.CheckKey();
                if (k == ConsoleKey.Enter)
                {
                    return;
                }
                if (k == ConsoleKey.C)
                {
                    order = 0;
                    _View.PianoBeep(262);
                }
                if (k == ConsoleKey.D)
                {
                    if (order == 0 && !_Model.hasDiaryKey) order = 1;
                    else order = 0;
                    _View.PianoBeep(294);
                }
                if (k == ConsoleKey.E)
                {
                    order = 0;
                    _View.PianoBeep(330);
                }
                if (k == ConsoleKey.F)
                {
                    if (order == 2 && !_Model.hasDiaryKey) order = 3;
                    else order = 0;
                    _View.PianoBeep(349);
                }
                if (k == ConsoleKey.G)
                {
                    order = 0;
                    _View.PianoBeep(392);
                }
                if (k == ConsoleKey.A)
                {
                    if (order == 1 && !_Model.hasDiaryKey) order = 2;
                    else if (order == 3 && !_Model.hasDiaryKey) order = 4;
                    _View.PianoBeep(440);
                }
                if (k == ConsoleKey.H)
                {
                    order = 0;
                    _View.PianoBeep(494);
                }
            }
            if (order == 4 && !_Model.hasDiaryKey)
            {
                _View.Spectre_Text(text[3]);
                //_View.AwaitKey();
                _Model.hasDiaryKey = true;
                return;
            }
        }






        ///// NOTE (HALLWAY - 5)
        public void Note(string text)
        {
            _View.box(text);
        }
        public void Mirror(List<string> text)
        {
            _View.Spectre_Text(text[0]);
            _View.Spectre_Text(text[1]);
            var command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title(text[2])
                    .HighlightStyle(new Style(foreground: Color.Black, background: Color.Green))
                    .PageSize(4)
                    .AddChoices(text[3], text[4])
                );
            if (command == text[3])
            {
                Note(text[5]);
                _View.AwaitKey();
            }
            if (command == text[4])
            {
                return;
            }
        }

        ////PHOTO (HALLWAY - 5)
        public void Photo(List<string> text)
        {
            _itemModel.Photo();
            var lines = _itemModel.GetPhoto;
            for (int i = 0; i < 5; i++) 
            { 
                _View.Spectre_Text(lines[i]); 
            }
            _View.Spectre_Text(_itemModel.ShowPhoto() + "\n");

            if (_Model.KnowsSafeLocation && !_Model.SafeOpened)
            {
                _View.Spectre_Text(text[0] + "\n");
                var command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
                    .Title("")
                    .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                    .PageSize(4)
                    .AddChoices(text[1], text[2])
                );
                if (command == text[2])
                {
                    return;
                }
                if (command == text[1])
                {
                    _View.Spectre_Text(lines[5]);
                    //_View.AwaitKey();
                    List<string> sejf = _Model.checkItem(5, "safe");
                    /*foreach (var x in sejf)
                    {
                        _View.Spectre_Text(x);
                    } */
                    Safe(sejf);
                }
            }
            _View.AwaitKey();
        }
       

        ////SAFE
        public void Safe(List<string> text)
        {
            _View.Spectre_Text(text[0]);
            var command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
               .Title("")
               .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
               .PageSize(4)
               .AddChoices(text[1], text[2])
               );
            if (command == text[1])
            {
                while (!_Model.SafeOpened)
                {
                    var ans = _View.Narrator2("$ ");
                    if (ans == _Model.SafePassword)
                    {
                        _View.Spectre_Text(text[3]);
                        _Model.hasDevice = true;
                        _Model.SafeOpened = true;
                        //_View.AwaitKey();
                    }
                    else
                    {
                        _View.Clear();
                        _View.Spectre_Text(text[4]);
                    }
                }

            }
            if (command == text[2])
            {
                return;
            }
        }



        //// CABINET (BATHROOM - 6)
        public void Cabinet(List<string> text)
        {
            _View.Spectre_Text(text[0]);
            var command = AnsiConsole.Prompt(
             new SelectionPrompt<string>()
            .Title("")
            .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
            .PageSize(4)
            .AddChoices(text[1], text[2])
            );
            if (command == text[1])
            {
                if (!_Model.hasMusicBoxKey)
                {
                    _View.Spectre_Text(text[3]);
                    _Model.hasMusicBoxKey = true;
                    _View.AwaitKey();
                }
                else
                {
                    _View.Clear();
                    _View.Spectre_Text(text[4]);
                    _View.AwaitKey();
                }
            }
            if (command == text[2])
            {
                return;
            }
        }



        /////MUSIC BOX (BEDROOM - 7)
        public void MusicBox(List<string> text)
        {
            if (!_Model.hasMusicBoxKey)
            {
                foreach (var l in text)
                {
                    _View.Spectre_Text(l);
                }
            }
            if (_Model.hasMusicBoxKey)
            {
                _View.Spectre_Text(text[0]);
                var command = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                .Title("")
                .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                .PageSize(4)
                .AddChoices(text[1], text[2])
                );
                if (command == text[1])
                {
                    _View.Spectre_Text(text[3]);
                    _Model.hasRing = true;
                    _View.AwaitKey();
                }
                if (command == text[2])
                {
                    return;
                }
            }
        }

        /////DIARY  (BEDROOM - 7)
        private bool openedDiary = false;
        public void Diary(List<string> text)
        {
            foreach (var l in text)
            {
                _View.Spectre_Text(l);
            }
            DiaryPage(1, text);
            return;
            //_View.AwaitKey()
        }
        private void DiaryPage(int page, List<string> text)
        {
            //int page = 1;
            _itemModel.DiaryPages(page);
            
            if (openedDiary)
            {
                _View.Spectre_Text(_itemModel.DiaryAddons[0]);
                _View.AwaitKey();
                return;
            }
            if(!_Model.hasDiaryKey)
            {
                _View.Spectre_Text(_itemModel.DiaryAddons[1]);
                _View.AwaitKey();
                    return;
            }
            foreach (var l in _itemModel.DiaryLines)
            {
                _View.Spectre_Text(l);
            }
            while (!openedDiary && _Model.hasDiaryKey)
            {
                var k = _View.CheckKey();
                if (k == ConsoleKey.RightArrow)
                {
                    page++;
                    if (page > 5) page = 5;
                    _View.Clear();
                    DiaryPage(page, text);
                }
                else if (k == ConsoleKey.LeftArrow)
                {
                    page--;
                    if (page < 1) page = 1;
                    _View.Clear();
                    DiaryPage(page, text);
                }
                else if (k == ConsoleKey.Enter)
                {
                    _View.Clear();
                    //_View.AwaitKey();
                    if (KeyFragment("EMOTION") && !openedDiary)
                    {
                        openedDiary = true;
                    }
                    else
                    {
                        _View.Spectre_Text(_Model.wrong);
                        Diary(text);
                    }
                }
            }
        }




        /////DESK (OFFICE - 8)
        public void Desk(List<string> text)
        {
            _View.Spectre_Text(text[0]);
            var command = AnsiConsole.Prompt(
                new SelectionPrompt<string>()
               .Title("")
               .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
               .PageSize(4)
               .AddChoices(text[1], text[2], text[3])
               );
            if (command == text[1])
            {
                _View.Clear();
                // _View.Spectre_Text();
                //_View.AwaitKey();

                var panel = new Panel(_itemModel.Cipher[0])
                {
                    Border = BoxBorder.Square,
                    Padding = new Padding(left: 2, top: 2, right: 2, bottom: 2),
                };
                AnsiConsole.Write(new Align(
                    panel,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle
                    ));
                _View.AwaitKey();
            }
            else if (command == text[2])
            {
                _View.Clear();
                _View.Spectre_Text("\n\n\n");
                var panel = new Panel(_itemModel.Cipher[1])
                {
                    Border = BoxBorder.Square,
                    Padding = new Padding(left: 2, top: 2, right: 2, bottom: 2),
                };
                AnsiConsole.Write(new Align(
                    panel,
                    HorizontalAlignment.Center,
                    VerticalAlignment.Middle
                    ));
                _Model.KnowsSafeLocation = true;
                _View.AwaitKey();
            }
            else
            {
                return;
            }
        }

        /////DEVICE  (OFFICE - 8)
        public void Destroy()
        {
            if (!_Model.DEVICE)
            {
                List<string> text = _itemModel.DestroyList;
                _View.Spectre_Text(text[0]);
                var command = AnsiConsole.Prompt(
                 new SelectionPrompt<string>()
                .Title("")
                .HighlightStyle(new Style(foreground: Color.White, background: Color.Grey))
                .PageSize(4)
                .AddChoices(text[1], text[2])
                );
                if (command == text[1])
                {
                    bool corr = false;
                    while (!corr)
                    {
                        _View.Spectre_Text(text[3]);
                        _View.Spectre_Text(text[4]);
                        var input = _View.Narrator2("#");
                        if (input == "1967")
                        {
                            if (KeyFragment("MORALITY"))
                            {
                                corr = true;
                                _View.Spectre_Text(text[5]);
                                Thread.Sleep(1000);
                                _View.Spectre_Text(text[6]);
                                _View.Spectre_Text(text[7]);
                                _Model.DEVICE = true;
                            }
                            else
                            {
                                _View.Spectre_Text(_Model.wrong);
                                _View.AwaitKey();
                                return;
                            }
                        }
                        else
                        {
                            _View.Spectre_Text(text[8]);
                        }
                    }
                    _View.AwaitKey();
                    return;
                }
                else
                {
                    return;
                }
            }
            else
            {
                _View.Spectre_Text(_itemModel.DeviceAddons);
                _View.AwaitKey();
                return;
            }
        }

        /////Fragmenty Cz³owieczeñstwa
        public bool KeyFragment(string which)
        {
            _View.Clear();
            
            switch (which)
            {
                case "REASON":
                    {
                        _View.Spectre_Text(_itemModel.Fragment[0]+"\n");
                        var input = _View.Narrator2(">>>", true).Trim().ToLower();
                        if (input == "reason")
                        {
                            ShowStatus("r");                           
                            ShowStatus("r");
                            //_View.AwaitKey();
                            _View.Clear();
                            return true;
                        }
                        else return false;

                    }
                case "EMOTION":
                    {
                        _View.Spectre_Text(_itemModel.Fragment[1]);
                        var input = _View.Narrator2("", true).Trim().ToLower();
                        if (input == "emotion")
                        {
                            ShowStatus("e");
                            ShowStatus("e");
                           // _View.AwaitKey();
                            _View.Clear();
                            return true;
                        }
                        else return false;
                    }
                case "MORALITY":
                    {
                        _View.Spectre_Text(_itemModel.Fragment[2]);
                        var input = _View.Narrator2("", true).Trim().ToLower();
                        if (input == "morality")
                        {
                            ShowStatus("m");
                            ShowStatus("m");
                            //_View.AwaitKey();
                            _View.Clear();
                            return true;
                        }
                        else return false;

                    }
                default: return false;
            }
            //return false;
        }
        public void ShowStatus(string rem)
        {
            _View.Clear();
            List<string> list = new List<string>();
            _Model.Status();
            list = _Model.statusLines;
            foreach (string line in list)
            {   
                _View.Spectre_Text(line+"\n");
            }
            //Task.Delay(10000);
            if (rem == "r") _Model.Reason = true;
            else if (rem == "e") _Model.Emotion = true;
            else if (rem == "m") _Model.Morality = true;

            if(_Model.Reason && _Model.Emotion && _Model.Morality) { _View.Spectre_Text(_itemModel.terminalAddons[3]); }
            _View.Spectre_Text(_Model.continuee);
            _View.AwaitKey();
            return;
        }

    }
}