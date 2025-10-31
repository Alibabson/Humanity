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

        public void Whiteboard(List<string> text)
        {

        }
    }
}