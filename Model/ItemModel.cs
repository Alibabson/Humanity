using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
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

        public List<string> GetNewspaper = new List<string>();
        public void Newspaper()
        {
            GetNewspaper.Add("[darkgoldenrod]Date: August 4, 1980 \n[/]");
            GetNewspaper.Add("\n[darkgoldenrod underline]BREAKING NEWS[/]\n");
            GetNewspaper.Add("[grey italic]Police still haven't found Amanda Claude. It's been three weeks since he's gone missing. Please report the local authorities if you have any information about a person matching these descriptions below:[/]\n");
            GetNewspaper.Add("[steelblue italic]Name: Amanda Claude\nAge: 18\nHeight: 5'7\"\nWeight: 135 lbs\nHair Color: Brown\nEye Color: Blue\nLast Seen Wearing: Black leather jacket, blue jeans, white sneakers[/]\n");
            GetNewspaper.Add("\n \n[steelblue italic]Additional info: Medicated depression, but due to lack of supplies she might seem traumatized or confused, please have patience and speak to her with kindness if you find her.[/]\n");
            GetNewspaper.Add("\n \n[grey italic]If you have any information, please contact the local police department at (555) 161-4559.[/]\n");

        }
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
                case 3:
                    logLines.Add("[orange3] \nDATE: [/][green]11/09/1980 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #010 \n[/]");
                    logLines.Add("[lime]Subject name: Robert Lavish\n[/]");
                    logLines.Add("[lime]Age: 28[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Drastic switch in moral perception - subject should be able to choose the most ethical solutions to avoid aggresion and pathological behaviour in order to come back to society after triple homicide.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Extreme confusion, paranoia, and violent outbursts. Subject was unsuccesful in choosing correct options, had problems deciding and communicating with others. He was afraid of most of his actions, yed didn't feel anything during contant with graphic or violent materials.[/]\n");
                    logLines.Add("[lime]\nAfter two weeks of confusion, patient went into a sudden coma and died shortly after. Autopsy revealed multiple brain aneurysms likely caused by internal stress.[/]\n");
                    logLines.Add("[orange3]\nNotes: \nSubject showed signs of improvement before sudden death. The erasure was too extreme to point of losing sanity.[/]");
                    return;
                case 4:
                    logLines.Add("[orange3] \nDATE: [/][green]02/14/1981 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #014 \n[/]");
                    logLines.Add("[lime]Subject name: [/][red][[REDACTED]]\n[/]");
                    logLines.Add("[lime]Age: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[lime]\nFinal Effect: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[orange3]\nNotes: \n[/][red]DELETE ALL TRACK OF THIS EXPERIMENT IMMEDIATELY!!! COMPLETE FAILURE. FORGET ABOUT IT FORGET ABOUT IT FORGEIT ABOUT IT[/]");
                    return;
                case 5:
                    logLines.Add("[orange3] \nDATE: [/][green]04/28/1982 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #022 \n[/]");
                    logLines.Add("[lime]Subject name: Doctor Adrian Holloway\n[/]");
                    logLines.Add("[lime]Age: 45[/]\n");
                    logLines.Add("[lime]Status: [/][red]? ? ?[/]\n");
                    logLines.Add("[lime]Target Effect: Proving the world that the physical pain can now be cured clinically. Everyone can now see the success of this creation. All the mental struggles or traumas can be wiped in a moment.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Unknown. Subject disconnected shortly after the procedure. Software error blocked the system from recovering patient status and left him in the middle of the process.[/]\n");
                    logLines.Add("[orange3]\nNotes: \nNONE[/]");
                    return;
                default:
                    logLines.Add("[grey] No further logs available. [/]");
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
             new Mathematics("|−94 + 5|=?", 87),
             new Mathematics("20x = 440\n\nx=?", 22),
             new Mathematics("(8² / 2)=?", 32),
             };
            public Mathematics GetRandomQuestion()
            {
                Random rand = new Random();
                int index = rand.Next(mathQuestions.Count);
                return mathQuestions[index];
             }
           }
            

        }


