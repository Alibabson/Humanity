using System.Text;

namespace Humanity.View
{
    public class ConsoleView
    {
        public void Clear() => Console.Clear();

        public void Line(string text = "") => Console.WriteLine(text);

        public void Type(string text, int delayMs)
        {
            foreach (char c in text)
            {
                Console.Write(c);
                Thread.Sleep(delayMs);
            }
            Console.WriteLine();
        }

        public void Pulse(string text, int pulses = 6, int on = 220, int off = 120)
        {
            for (int i = 0; i < pulses; i++)
            {
                Console.Write("\r" + text);
                Thread.Sleep(on);
                Console.Beep(250,on);
                Console.Write("\r" + new string(' ', text.Length));
                Thread.Sleep(off);
            }
            Console.Write("\r" + text + "\n");
        }

        public string Narrator(string prefix = "\n> ")
        {
            Console.ForegroundColor = ConsoleColor.Green;
            Console.Write(prefix);
            Console.ResetColor();
            return (Console.ReadLine() ?? "").Trim();
        }

        public void DarkCyan(string line, int delay)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Type($"\"{line}\"", delay);
            Console.ForegroundColor = prev;
        }

        public void Yellow(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Line(text);
            Console.ForegroundColor = prev;
        }

        public void Red(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Line(text);
            Console.ForegroundColor = prev;
        }

        public void Separator() => Line(new string('─', 64));
    }
}
