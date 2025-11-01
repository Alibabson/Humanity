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
        }
       /* public Mathematics(string q , int a)
        {
            question = q;
            answer = a;
        }
        public List<Math> mathQuestions = new List<Math>()
        {
            new Math("What is 5 + 3?", 8),
            new Math("What is 12 - 4?", 8),
            new Math("What is 2 * 6?", 12),
            new Math("What is 16 / 2?", 8),
            new Math("What is 9 + 10?", 19)
        };
        public void WhiteBoard_Minigame()
        {
            //to be implemented
        } */
    }
}
