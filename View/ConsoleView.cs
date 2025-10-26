﻿using System.Text;
using System.Threading.Tasks;
using System.Media;
using Spectre.Console;


namespace Humanity.View
{
    public class ConsoleView
    {
        private static readonly object _consoleLock = new();

        public void Clear() => Console.Clear();
        public void Line(string text = "") => Console.WriteLine(text);
        public void LineNoEnter(string text = "") => Console.Write(text);
        public void Type(string text, int delayMs, bool isIntro = false)
        {
            if (isIntro)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            foreach (char c in text)
            {
                lock (_consoleLock) Console.Write(c);
                Console.Beep(400, 3);
                Thread.Sleep(delayMs);
            }
            lock (_consoleLock) Console.WriteLine();
            Console.ResetColor();
        }

        public Task Pulse(string text, int pulses = 3, int on = 220, int off = 120)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            lock (_consoleLock)
            {
                for (int i = 0; i < pulses; i++)
                {
                    Console.Write("\r" + text);
                    Thread.Sleep(on);
                    try { Console.Beep(250, on); }
                    catch
                    { }
                    Console.Write("\r" + new string(' ', text.Length));
                    Thread.Sleep(off);
                }
                Console.Write("\r" + text);
            }
            Console.ResetColor();

            return Task.Run(async () =>
            {
                for (int i = 0; i < pulses; i++)
                {

                    Thread.Sleep(on);
                    try { Console.Beep(250, on); } catch { /* Console.Beep może nie wszędzie działac, zwlaszcza na Mac'ach */ }
                    await Task.Delay(off);
                    Thread.Sleep(off);
                }
            });
        }

        public async Task<string> Narrator(string prefix = "\n> ")
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(prefix);
            Console.ResetColor();
            return (Console.ReadLine() ?? "").Trim();
        }

        public void DarkCyan(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            LineNoEnter(text);
            Console.ForegroundColor = prev;
        }
        public void Ghost(string line, int delay)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.DarkCyan;
            Type($"\"{line}\"", delay);
            Console.ForegroundColor = prev;
        }
        public void AwaitKey()  //wcisnij dowolny klawisz
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
        }
        public ConsoleKey CheckKey()  //wcisnij konkretny klawisz
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
            return key.Key;
        }
        public void Yellow(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Yellow;
            Line(text);
            Console.ForegroundColor = prev;
        }
        public void Loading()
        {
            Line("Loading");
            Thread.Sleep(500);
            Line("\rLoading.");
            Thread.Sleep(500);
            Line("\rLoading..");
            Thread.Sleep(500);
            Line("\rLoading...");
            Clear();
        }
        public void Red(string text)
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Line(text);
            Console.ForegroundColor = prev;
        }
        public void Green(string text, bool good)
        {
            var prev = Console.ForegroundColor;
            if (good) {
                Console.ForegroundColor = ConsoleColor.Black;
                Console.BackgroundColor = ConsoleColor.Green;
            }
            else {
                Console.ForegroundColor = ConsoleColor.Green;
                Console.BackgroundColor = ConsoleColor.Black;
            }
            Line(text);
            Console.ForegroundColor = prev;
        }

        public void Separator() => Line(new string('─', 64));



        public void PlaySound(string fileName)
        {
            try
            {
                string path = Path.Combine(AppContext.BaseDirectory, "Sounds", fileName);

                if (!File.Exists(path))
                {
                    Console.WriteLine($"[Sound not found: {fileName}]");
                    return;
                }
                System.Diagnostics.Process.Start(new System.Diagnostics.ProcessStartInfo
                {
                    FileName = "powershell",
                    Arguments = $"-c (New-Object Media.SoundPlayer '{path}').PlaySync()",
                    CreateNoWindow = true,
                    UseShellExecute = false
                });

            }
            catch (Exception ex)
            {
                Console.WriteLine($"[Sound error: {ex.Message}]");
            }
        }

        public void Spectre_Text(string text)
        {
            AnsiConsole.Markup(text);
        }

        public void SpectreFiglet(string text)
        {
            var figlet = new FigletText(text)
            .Centered()
            .Color(Color.Red);


            AnsiConsole.Write(figlet);
        }

        public void FakeLoad()
        {
            AnsiConsole.MarkupLine("[bold green]Loading...[/]");

            AnsiConsole.Status()
                .Start("Initializing...", ctx =>
                {
                    Thread.Sleep(1000);
                    ctx.Status("Loading assets...");
                    Thread.Sleep(1000);
                    ctx.Status("Connecting...");
                    Thread.Sleep(1000);
                    ctx.Status("Almost done...");
                    Thread.Sleep(1000);
                });

            AnsiConsole.MarkupLine("[bold yellow]Ready![/]");
        }

        public void Image(string url)
        {
            var image = new CanvasImage(url);
            image.MaxWidth(90);
            AnsiConsole.Write(image);
        }
    }
}