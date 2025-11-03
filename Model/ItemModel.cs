using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Humanity.Model;
using Humanity.View;


namespace Humanity.Model
{
    public class ItemModel
    {
        public string[] passwordFragments = new string[3];
        public string password = null;
        public void JoinPassword()
        {
            password = string.Join("", passwordFragments);
        }
        public List<string> logLines = new List<string>();
        public void Logs(int page)
        {
            logLines.Clear();
            logLines.Add("[olive underline]use LEFT and RIGHT arrows to navigate & ESC to go back \n[/]");
            logLines.Add($"[lime]--- PAGE {page} / 5 ---[/]");
            switch (page)
            {
                case 1:
                    logLines.Add("[orange3] \nDATE: [/][green]05/19/1980 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #001 \n[/]");
                    logLines.Add("[lime]Subject name: William Gurner\n[/]");
                    logLines.Add("[lime]Age: 21[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Complete erasure of past memories. Subject needs to be able to function like a normal, human adult, but his recolections from his life would be non-existent.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Severe brain damage, confusion and stress. Patient died after strong physical and mental pressure.[/]\n");
                    logLines.Add("[orange3]\nNotes:\nSubject exhibited extreme aggression towards staff members prior to termination. Further analysis required to determine cause of behavior.[/]");
                    return;
                case 2:
                    logLines.Add("[orange3] \nDATE: [/][green]07/30/1980 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #005 \n[/]");
                    logLines.Add("[lime]Subject name: Amanda Claude\n[/]");
                    logLines.Add("[lime]Age: 18[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Exchange of emotions - We're expecting an erasure of past trauma and change in everyday attitude[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Depression, apathy, and emotional instability. Patient had no issues with the healing and operaiton process, but mental stability caused a [red][[REDACTED]][/] in the recreational area.[/]\n");
                    logLines.Add("[orange3]\nNotes: \nKeep an eye on cameras, weird behaviour and potentially dangerious objects within institution.[/]");
                    return;

            }
        }
        public class Mathematics
        {
            public string question { get; set; }
            public int answer { get; set; }

            public Mathematics(string q, int a)
            {
                question = q;
                answer = a;
            }
        }
            public List<Mathematics> mathQuestions = new List<Mathematics>()
          {
             new Mathematics("3x + 5 = 77 \n\nx=?", 24),
             new Mathematics("(4² - 2²)=?", 12),
             new Mathematics("2x - 7 = 173\n\nx=?", 90),
             new Mathematics("√81 + 4=?", 13),
             new Mathematics("(3 + 5) * 2=?", 16),
             new Mathematics("5x = 225\n\nx=?", 25),
             new Mathematics("(2³ + 3²)=?", 17),
             new Mathematics("(x/2) + 4 = 112\n\nx=?", 54),
             new Mathematics("(6 * 3) + (4 * 2)=?", 26),
             new Mathematics("9x - 603 = 0\n\nx=?", 67),
             new Mathematics("(5² - 3²)=?", 16),
             new Mathematics("√324=?", 18),
             new Mathematics("|−94 + 5|", 87),
             new Mathematics("20x = 440\n\nx=?", 22),
             new Mathematics("(8² / 2)", 32),
             };
            public Mathematics GetRandomQuestion()
            {
                Random rand = new Random();
                int index = rand.Next(mathQuestions.Count);
                return mathQuestions[index];
        }
            }
        }


