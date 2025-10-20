using System.Text;

namespace HauntedTerminal.View
{
    public class ConsoleView
    {
        public void Clear() => Console.Clear();

        public void Line(string text = "") => Console.WriteLine(text);

        public void Type(string text, int delayMs = 18)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        public void Pulse(string text, int pulses = 3, int on = 220, int off = 120)
        {
            for (int i = 0; i < pulses; i++)
            {
                Console.Write("\r" + text);
                Thread.Sleep(on);
                Console.Write("\r" + new string(' ', text.Length));
                Thread.Sleep(off);
            }
            Console.Write("\r" + text + "\n");
        }

        public string Prompt(string prefix = "\n> ")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prefix);
            Console.ResetColor();
            return (Console.ReadLine() ?? "").Trim();
        }

        public void Ghost(string line)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Type($"\"{line}\"", 30);
            Console.ForegroundColor = prev;
        }

        public void SystemWarn(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Line(text);
            Console.ForegroundColor = prev;
        }

        public void SystemError(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Line(text);
            Console.ForegroundColor = prev;
        }

        public void Separator() => Line(new string('─', 64));
    }
}
