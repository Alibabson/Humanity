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



        public async Task GameStart()
        {
            {
                Console.Title = string.Empty;  //tytuł konsoli jest pusty
                _ = Task.Run(async () =>
                {
                    const string word = "HUMANITY";
                    var title = new System.Text.StringBuilder();
                    for (int i = 0; i < word.Length; i++)
                    {
                        Clear();
                        if (i > 0) title.Append(' ');  //dokleja nam spację
                        title.Append(word[i]);
                        Console.Title = title.ToString();
                        SpectreFiglet(title.ToString());
                        Console.Beep(500, 20);
                        await Task.Delay(750);
                    }
                });

                Task.Delay(6000).Wait();
                Spectre_Text("\n[italic bold olive]A game by [/][magenta]OnyxMoonStar. [/]\n");
                Spectre_Text("\r[italic slowblink olive]Press any button to start... [/]  \n \n");
                AwaitKey();
                return;
            }
        }


        public void Type(string text, int delayMs, bool isIntro = false) //powolny napis
        {
            if (isIntro)
            {
                Console.ForegroundColor = ConsoleColor.Green;
            }
            foreach (char c in text)
            {
                lock (_consoleLock) Console.Write(c);
                //Console.Beep(400, 3);
                Thread.Sleep(delayMs);
            }
            lock (_consoleLock) Console.WriteLine();
            Console.ResetColor();
        }

       public void TypeText(string text, int delay, string color, bool beep=false) //powolny napis ale z kolorem (Markup)
        {
            int count = 0;
            foreach (char c in text)
            {
                // każda litera idzie przez markup i dany kolor.

                AnsiConsole.Markup($"{color}{Markup.Escape(c.ToString())}[/]");
                if (beep)
                {
                    try { 
                        if(count % 2 == 0)  Console.Beep(200, delay/2); //to dla duchów żeby pikało (co 2 bo spacje są)
                    } 
                    catch 
                    { }

                }
                count++;
                Thread.Sleep(delay);
            }
            Console.WriteLine();
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

        public void IncorrectBeep() //te 2 beep'y są jak źle dajemy odpowiedź w whiteboard
        {
            try
            {
                Console.Beep(400, 150);
                Thread.Sleep(100);
                Console.Beep(300, 150);
            }
            catch
            { }
        }
        public void PianoBeep(int hz)
        {
            try {
                Console.Beep(hz, 400);
             }
            catch { }
        }
        public async Task<string> Narrator(string prefix = "\n> ")  // daje znak > po lewej i pozwala na wpisywnaie komend. Możemy zmieniać ten znaczek
        {
            Console.ForegroundColor = ConsoleColor.Magenta;
            Console.Write(prefix);
            Console.ResetColor();
            return (Console.ReadLine() ?? "").Trim(); //trim usuwa niepotrzebne spacje które możemy dać przypadkiem
        }
        public string Narrator2(string prefix)  // daje znak > po lewej i pozwala na wpisywnaie komend. Możemy zmieniać ten znaczek
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(prefix);
            Console.ResetColor();
            return (Console.ReadLine() ?? "").Trim(); //trim usuwa niepotrzebne spacje które możemy dać przypadkiem
        }
        public string ReadLine() //z jakiegoś powodu ReadKey nie działało to powstało drugie identyczne a jak nie wiem gdzie jest jakie to zostawiłem oba
        {
            string key = Console.ReadLine();
            return key;
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
        public void Panel(string currRoom, string currSanity, int meter)  //pasek sanity i pokój
        {
            var sanityBar = new BarChart()  //barChart to pasek
            .AddItem(currSanity, meter, Color.Green) // wypełnienie zielone, wartość "meter" czyli ile sanity
             .Width(meter/2); //było za długie

            var left = new Markup(currRoom); //na lewej mamy obecny pokój
            AnsiConsole.Write(new Columns(left, sanityBar).Expand()); //.Expand() - rozciąga na całą szerokość (kolumna 1 to pokój[left], kolumna 2 to pasek sanity). COLUMN rozdziela
        }
        public void Red(string text) //błędy na czerwono był
        {
            var prev = Console.ForegroundColor;
            Console.ForegroundColor = ConsoleColor.Red;
            Line(text);
            Console.ForegroundColor = prev;
        }



        public void PlaySound(string fileName) //muza dźwięki
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

        public void Spectre_Text(string text)  //tekst z kolorem
        {
            AnsiConsole.Markup(text);
        }
        public void SpectreFiglet(string text) //figlet to duży tekst ASCII
        {
            var figlet = new FigletText(text)
            .Centered()
            .Color(Color.Red);


            AnsiConsole.Write(figlet);
        }

        public void FakeLoad() //Sztuczne ładowanie ekranu dla fajnego efektu (.Status to taki pasek ładowania)
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

        public void Image(string url)  //zdjęcia pixel-art w konsoli
        {
            var image = new CanvasImage(url);
            image.MaxWidth(90);
            AnsiConsole.Write(image);
        }
        
        }
}