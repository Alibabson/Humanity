using Humanity.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace Humanity.View
{
    public class ItemView
    {
        private readonly ConsoleView _View;
        public ItemView(ConsoleView view)
        {
            _View = view;
        }
        private void updown(int a, int b, ref int choice, ref bool active)
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
        }
        public void MonitorItem(List<string> text)
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
    }
}