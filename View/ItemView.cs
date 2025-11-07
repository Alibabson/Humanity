using Humanity.Model;
using Spectre.Console;
using System;
using System.Collections.Generic;
using System.Reflection.Metadata.Ecma335;
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

        /* private void updown(int a, int b, ref int choice, ref bool active)
         {
             ConsoleKeyInfo key = Console.ReadKey(true);
             if (key.Key == ConsoleKey.UpArrow)
             {

                 choice--;
                 if (choice < a) choice = b;
                 _View.Clear();
             }
             else if (key.Key == ConsoleKey.DownArrow)
             {

                 choice++;
                 if (choice > b) choice = a;
                 _View.Clear();
             }
             else if (key.Key == ConsoleKey.Enter)
             {
                 active = false;
             }
         } */
        /*public void MonitorItem(List<string> text)
        {
            bool active = true;
            int choice = 1;
            Console.BackgroundColor = ConsoleColor.Black;
            while (active)
            {
                _View.Green(text[0], false);
                switch (choice)
                {
                    case 1:
                        _View.Green(text[1], true);
                        _View.Green(text[2], false);
                        _View.Green(text[3], false);
                        break;
                    case 2:
                        _View.Green(text[1], false);
                        _View.Green(text[2], true);
                        _View.Green(text[3], false);
                        break;
                    case 3:
                        _View.Green(text[1], false);
                        _View.Green(text[2], false);
                        _View.Green(text[3], true);
                        break;
                }
           updown(1, 3, ref choice, ref active);
            }
            if (choice == 1)
            {
                //modul wybór 1
                _View.Red("Modu³ w budowie");
            }
            else if (choice == 2)
            {
                // modu³ wybór 2
                _View.Red("Modu³ w budowie");
            }
            else if (choice == 3)
            {
                //modu³ wybór 3
                _View.Red("Modu³ w budowie");
            }
            return;
        }
        */
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
                int page = 1;
                LOG(page,text);             
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
            if(_View.CheckKey() == ConsoleKey.RightArrow)
            {
                page++;
                if(page > 5) page = 5;
                _View.Clear();
                LOG(page, text);
            }
            else if(_View.CheckKey() == ConsoleKey.LeftArrow)
            {
                page--;
                if(page < 1) page = 1;
                _View.Clear();
                LOG(page, text);
            }
            else if(_View.CheckKey() == ConsoleKey.Escape)
            {
                _View.Clear();
                Monitor(text);
            }
        }

        // WHITEBOARD //
        public bool passed= false;
        private int guesses = 0;
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
                        if (guesses >= 2)
                        {
                            AnsiConsole.Write(new Columns(
                                    new Spectre.Console.Text(_itemModel.newspaperList[0], new Style(Color.Orange3)),
                                    new Spectre.Console.Text(_itemModel.newspaperList[1], new Style(Color.Pink3))

                                ));
                        }
                        else
                        {
                            AnsiConsole.Write(new Columns(
                                     new Spectre.Console.Text(_itemModel.newspaperList[0], new Style(Color.Orange3))
                                 //new Spectre.Console.Text(_itemModel.newspaperList[1], new Style(Color.Pink3))

                                 ));
                        }

                        _View.Spectre_Text("\n[red bold]HUMANITY = ?[/]\n");
                        _View.Spectre_Text("[green italic]HUMANITY EQUALS: [/]");

                        string ans = _View.ReadLine();
                        if (ans == "1")
                        {
                            _View.Clear();
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
                _View.Spectre_Text("[grey underline]You have already passed this whiteboard. \n[/][blue italic]HUMANITY NEEDS[/] [green italic] REASON[/][blue italic],[/] [green italic] MORALITY [/][blue italic]and [/][green italic]REASON[/]");
                _View.AwaitKey();
                return true;
            }
            return true;
        }
        public void Whiteboard_passed()
        {
            passed = true;
            //_itemModel.JoinPassword();
            _View.Spectre_Text("[grey underline]You guessed correctly. Press any key to exit[/]");
            _View.AwaitKey();
        }
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
   '--'    '--'

";
            _View.Line(asciiClock);
        }

        public void Clock(List<string> text)
        {
            _View.Spectre_Text(text[0] + "\n");
            _View.Spectre_Text("\n\n[grey underline]Press any button to continue[/]");
            _View.AwaitKey();
        }

    }
}