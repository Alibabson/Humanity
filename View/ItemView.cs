using System.Text;

namespace Humanity.View
{
    public class ItemView
    {
        private readonly ConsoleView _View;
        private readonly ConsoleModel _model;

        public void MonitorItem(list<string> text)
        {
            _View.Clear();
            int active = 1;
            int choice = 1;
            while (active)
            {
                _View.Green(text[0], false);
                switch (choice) {
                    case 1:
                        _View.Green(text[1], true);
                        _View.Green(text[2], false);
                        _View.Green(text[3], false);
                        break;
                    case 2:
                        _View.Green(text[1], false);
                        _View.Green(text[2], true);
                        _View.Green(text[3], false);
                    case 3:
                        _View.Green(text[1], false);
                        _View.Green(text[2], false);
                        _View.Green(text[3], true);
                        break;
                }
                Console.KeyInfo key = Console.ReadKey(true);
                if(key == ConsoleKey.UpArrow)
                {
                    choice--;
                    if (choice < 1) choice = 3;
                }
                else if (key == ConsoleKey.DownArrow)
                {
                    choice++;
                    if (choice > 3) choice = 1;
                }
                else if (key == ConsoleKey.Enter)
                {
                    active = 0;
                }
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