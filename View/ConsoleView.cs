using System.Text;
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
        public void Type(string text, int delayMs)
        {
            foreach (char c in text)
            {
                lock (_consoleLock) Console.Write(c);
                Thread.Sleep(delayMs);
            }
            lock (_consoleLock) Console.WriteLine();
        }

        public Task Pulse(string text, int pulses = 3, int on = 220, int off = 120)
        {
            // Uruchamiamy pętlę w tle tak, by nie blokować wątku wywołującego.
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
                Console.Write("\r" + text + "\n");
            }

            return Task.Run(async () =>
            {
                for (int i = 0; i < pulses; i++)
                {
                  
                    Thread.Sleep(on);
                    try { Console.Beep(250, on); } catch { /* Console.Beep może nie działać wszędzie */ }
                    await Task.Delay(off);
                    Thread.Sleep(off);
                }
            });
        }

        public async Task<string> Narrator(string prefix = "\n> ")
        {
            Console.ForegroundColor = ConsoleColor.Green;
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
        public void AwaitKey()
        {
            ConsoleKeyInfo key = Console.ReadKey(true);
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
            if (good){
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
                // Pełna ścieżka do pliku w folderze Sounds/
                string path = Path.Combine(AppContext.BaseDirectory, "Sounds", fileName);

                if (!File.Exists(path))
                {
                    Console.WriteLine($"[Sound not found: {fileName}]");
                    return;
                }
        // Windows – używa systemowego Media Playera (wmplayer) w tle
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


    }



}