using Humanity.Controller;
using Humanity.Model;
using Humanity.View;
using System.Threading.Tasks;

namespace Humanity
{
    internal static class Program
    {
        private static async Task Main()
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8; //znaki specjalne
            // BootUp(); -- animacja tytułu wyłączona dla wygody testów


            var model = new GameModel();
            var view = new ConsoleView();
            var ctrl = new GameController(model, view);   //pobieranie stanu widoku i kontrolera

           await ctrl.Run();
        }
        private static void BootUp()
        {

            try
            {
                Console.WindowHeight = 1;
                Console.WindowWidth = 1;
            }
            catch { }
            Console.Title = string.Empty;
            const string word = "HUMANITY";
            var title = new System.Text.StringBuilder();

            for (int i = 0; i < word.Length; i++)
            {
                if (i > 0) title.Append(' ');
                title.Append(word[i]);
                Console.Title = title.ToString();
                Thread.Sleep(750);
            }
            return;
        }
    }
}
