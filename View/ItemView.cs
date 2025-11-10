using Humanity.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Reflection.Metadata.Ecma335;
using System.Runtime.CompilerServices;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
namespace Humanity.View
{
    public class ItemView
    {
        private readonly ConsoleView _View;
        private readonly GameModel _Model;
        private readonly ItemModel _itemModel;

        
        public ItemView(ConsoleView view, GameModel g_model, ItemModel itemModel)
        {
            _View = view;
            _Model = g_model;
            _itemModel = itemModel;
        }

        ////TERMINAL (LAB - 0)
        public void Monitor(List<string> text)
        {
           var command = AnsiConsole.Prompt(
            new SelectionPrompt<string>()
                .Title(text[0])
                .HighlightStyle(new Style(foreground: Color.Black, background:Color.Green))
                .PageSize(4)
                .AddChoices(text.GetRange(1, text.Count - 1))
            );
            
            if(command == text[1])   //sprawdz status
            {
                _Model.Status();
                foreach(var line in _Model.statusLines)
                {
                    _View.Spectre_Text(line + "\n");
                }
                BackPrompt(text);
            }
            else if(command == text[2]) //sprawdz logi
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
                    _View.Spectre_Text("[red bold slowblink]DATA CORRUPTED.[/]");
                    _View.AwaitKey();
                }
            }
            else if(command == text[3])
            {
                _View.Spectre_Text("[grey underline]You left the console. \n Press any button to continue[/]");
                return;
            }
            return;
        }
        private void BackPrompt(List<string> text)
        {
            _View.Spectre_Text("\n[italic slowblink lime]\n PRESS ANY BUTTON TO GO BACK [/]  \n \n");
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
            if(k == ConsoleKey.RightArrow)
            {
                page++;
                if(page > 5) page = 5;
                _View.Clear();
                LOG(page, text);
            }
            else if(k == ConsoleKey.LeftArrow)
            {
                page--;
                if(page < 1) page = 1;
                _View.Clear();
                LOG(page, text);
            }
            else if(k == ConsoleKey.Enter)
            {
                _View.Clear();
                Monitor(text);
            }
        }

        /////WHITEBOARD (LAB - 0)
        public bool passed= false;
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
                        if (hint==true)
                        {
                            AnsiConsole.Write(new Columns(
                                    new Spectre.Console.Text(_itemModel.whiteboardList[0], new Style(Color.Orange3)),
                                    new Spectre.Console.Text(_itemModel.whiteboardList[1], new Style(Color.Pink3))

                                ));
                        }
                        else
                        {
                            AnsiConsole.Write(new Columns(
                                     new Spectre.Console.Text(_itemModel.whiteboardList[0], new Style(Color.Orange3))
                                 //new Spectre.Console.Text(_itemModel.whiteboardList[1], new Style(Color.Pink3))

                                 ));
                        }

                        _View.Spectre_Text("\n[red bold]HUMANITY = R + E + M = ? [/]\n");
                        if (guesses >= 3) _View.Spectre_Text("\n[grey slowblink]Type 'hint' for additional information...[/]\n");
                        _View.Spectre_Text("[green italic]HUMANITY EQUALS: [/]");

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
                    _View.Spectre_Text("[grey underline]You left the whiteboard. \n Press any button to continue[/]");
                    return false;
                }
                return false;
            }
            else
            {
                _View.Spectre_Text("[grey underline]You have already passed this whiteboard. \n[/][blue italic]HUMANITY NEEDS[/] [green italic]REASON[/][blue italic],[/] [green italic]EMOTION [/][blue italic]and [/][green italic]MORALITY[/]");
                _View.AwaitKey();
                return true;
            }
            //return true;
        }
        public void Whiteboard_passed()
        {
            passed = true;
            //_itemModel.JoinPassword();
            _View.Spectre_Text("[grey underline]You answered correctly. Press any key to exit[/]");
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



        //// NEWSPAPER (KITCHEN - 2)
        public void Newspaper(List<string> text)
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
                _View.Spectre_Text("[grey underline]You left the newspaper. \n Press any button to continue[/]");
                return;
            }
            if (command == text[1])
            {
                _itemModel.Newspaper();
                var lines = _itemModel.GetNewspaper;
                foreach (var x in lines)
                {
                    _View.Spectre_Text(x);
                }
            }
        }



        /////BOOKSHELF (LIBRARY - 3)
        public void Bookshelf(List<string> text)
        {
            List<string> tmp = text;
            bool quitted = false;
            _View.Clear();

            foreach (var line in text)
            {
                _View.Spectre_Text(line + "\n");
            }
            _View.Spectre_Text("\n[grey]Type 'back' to exit.\n \n[/]");
            while (!quitted)
            {
                var input = _View.Narrator2("-> ");
                if (string.IsNullOrWhiteSpace(input))
                {
                    break;
                }
                if (input == "back") { quitted = !quitted; break; }
                input = input.Trim().ToLowerInvariant();
                var parts = input.Split(' ', 2);
                if (parts.Length < 2)
                {
                    _View.Spectre_Text("\n[grey]Please type coordinates in [/][lime]<row> <column>[/][grey] format.[/]\n");
                    continue;
                }
                int.TryParse(parts[0], out int row);
                if (parts[0]== "") { quitted = !quitted; break; }
                int.TryParse(parts[1], out int col);
                if (parts[1] == "") { quitted = !quitted; break; }
                if (row <= 0 || col <= 0 ||row > 12 || col >60)
                {
                    _View.Spectre_Text("[grey]This bookshelf does not have that many books. Try again.[/]");
                }
                if (row == 9 && col == 5)
                {
                    _View.Clear();
                    _View.Spectre_Text("\n[green]Dobra ksi¹zka potem zmieniê[/]\n");
                    _View.AwaitKey();
                    Bookshelf(tmp);
                }
                else if (row == 6 && col == 7)
                {
                    _View.Clear();
                    _View.Spectre_Text("\n[black on white]" + ShowNotes() + "[/]\n");
                    _View.AwaitKey();
                    Bookshelf(tmp);
                }
                else
                {
                    _View.Clear();
                    _View.Spectre_Text("\n[grey]Nothing interesting here.[/]\n");
                    _View.AwaitKey();
                    Bookshelf(tmp);
                }
            }
        }

        public string ShowNotes()
        {
            string asciiNotes = @"
        _                                    
       ( )                                   
    ___|/___________________________________ 
   |__/|/_)_|___________|\____________|\____|
   |_(_|_/__|___________|______|\_____|_____|
   |___|____|____|____(_)______|____(_)_____|
   |________|____|___________(_)____________|
               (_)                           
                                              
        ";
            return asciiNotes;
        }

        /////CLOCK (LIVING ROOM - 4)
        public void ShowClock()
        {
            var asciiClock = @"
        _____
     _.'_____`._
   .'.-'  12 `-.`.
  /,' 11      1 `.\
 // 10      /   2 \\
;;         /       ::
|| 9  ----O      3 ||
::                 ;;
 \\ 8           4 //
  \`. 7       5 ,'/
   '.`-.__6__.-'.'
    ((-._____.-))
    _))       ((_
   '--'       '--'

";
            _View.Spectre_Text("[silver]The clock seems broken, clock hands are frozen in place.[/]\n \n");
            _View.Line(asciiClock);
        }

        public void Clock(List<string> text)
        {
            _View.Spectre_Text(text[0] + "\n");
            _View.Spectre_Text("\n\n[grey underline]Press any button to continue[/]");
            _View.AwaitKey();
        }

        ///// PIANO (LIVING ROOM - 4)
        public void Piano(List<string> text)
        {
            int order = 0;
            if (!_Model.hasDiaryKey)
            {
                _View.Spectre_Text(text[0]);
                _View.Spectre_Text(text[1]);
                while(order<3)
                {
                    var k = _View.CheckKey();
                    if(k == ConsoleKey.Enter)
                    {
                        return;
                    }
                    if (k == ConsoleKey.C)
                    {
                        if (order == 0) order = 1;
                        else order = 0;
                            _View.PianoBeep(262);
                    }
                    if (k==ConsoleKey.D)
                    {
                        if (order == 1) order = 2;
                        else order = 0;
                            _View.PianoBeep(294);
                    }
                    if(k ==ConsoleKey.E)
                    {
                        if (order == 2) order = 3;
                        else order = 0;
                            _View.PianoBeep(330);
                    }
                    if(k ==ConsoleKey.F)
                    {
                        order = 0;
                        _View.PianoBeep(349);
                    }
                    if (k == ConsoleKey.G)
                    {
                        order = 0;
                        _View.PianoBeep(392);
                    }
                    if (k == ConsoleKey.A)
                    {
                        order = 0;
                        _View.PianoBeep(440);
                    }
                    if (k == ConsoleKey.H)
                    {
                        order = 0;
                        _View.PianoBeep(494);
                    }
                }
                if(order==3)
                {
                    _View.Spectre_Text(text[3]);
                    _Model.hasDiaryKey = true;
                    _View.AwaitKey();
                    return;
                }
            }
            else
            {
                _View.Spectre_Text(text[2]);
                return;
            }
        }
        
        ///// NOTE (HALLWAY - 5)
        public void Note(List<string> text)
        {
            var panel = new Panel(text[0])
            {
                Border = BoxBorder.Square,
                Padding = new Padding(left:3, top:3, right:3, bottom:3),
            };
            AnsiConsole.Write(new Align(
                panel,
                HorizontalAlignment.Center,
                VerticalAlignment.Middle
                ));
            _View.AwaitKey();
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
        public void Diary(List<string> text)
        {
            foreach (var x in text)
            {
                _View.Spectre_Text(x);
            }
            _View.AwaitKey();
        }



        ////SAFE (OFFICE - 8)
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
                        _View.AwaitKey();
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
                        if (input == "1974")
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
                            _View.Spectre_Text(text[7]);
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
                _View.Spectre_Text("[grey]You already used it.[/]");
                _View.AwaitKey();
                return;
            }
        }
    }
}