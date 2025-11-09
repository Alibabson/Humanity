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
        public void JoinPassword() //łączyło hasła z odpowiedzi ze starego whiteboard
        {
            password = string.Join("", passwordFragments);
        }
        public List<string> logLines = new List<string>();

        public List<string> GetKey = new List<string>();
        
        public void Logs(int page) //zależnie od strony w terminalu pokazuje odpowiedni tekst - czyści tablicę i wstawia na nowo
        {
            logLines.Clear();
            logLines.Add("[olive underline]use LEFT and RIGHT arrows to navigate & ENTER to go back \n[/]");
            logLines.Add($"[lime]--- PAGE {page} / 5 ---[/]");
            switch (page)
            {
                case 1:
                    logLines.Add("[orange3] \nDATE: [/][green]11/19/1982 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #001 \n[/]");
                    logLines.Add("[lime]Subject name: William Gurner\n[/]");
                    logLines.Add("[lime]Age: 29[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Complete erasure of past memories. Subject needs to be able to function like a normal, human adult, but his recolections from his life would be non-existent.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Severe brain damage, confusion and stress. Patient died after strong physical and mental pressure.[/]\n");
                    logLines.Add("[orange3]\nNotes:\nSubject exhibited extreme aggression towards staff members prior to termination. Further analysis required to determine cause of behavior.[/]");
                    return;
                case 2:
                    logLines.Add("[orange3] \nDATE: [/][green]12/16/1982 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #004 \n[/]");
                    logLines.Add("[lime]Subject name: Robert Lavish\n[/]");
                    logLines.Add("[lime]Age: 35[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Drastic switch in moral perception - subject should be able to choose the most ethical solutions to avoid aggresion and pathological behaviour in order to come back to society after triple homicide.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Extreme confusion, paranoia, and violent outbursts. Subject was unsuccesful in choosing correct options, had problems deciding and communicating with others. He was afraid of most of his actions, yet didn't feel anything during contant with graphic or violent materials.[/]\n");
                    logLines.Add("[lime] After two weeks of confusion, patient went into a sudden coma and died shortly after. Autopsy revealed multiple brain aneurysms likely caused by internal stress.[/]\n");
                    logLines.Add("[orange3]\nNotes: \nSubject showed signs of improvement before sudden death. The erasure was too extreme to point of losing sanity.[/]");
                    return;
                case 3:
                    logLines.Add("[orange3] \nDATE: [/][green]02/14/1983 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #010 \n[/]");
                    logLines.Add("[lime]Subject name: [/][red][[REDACTED]]\n[/]");
                    logLines.Add("[lime]Age: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[lime]\nFinal Effect: [/][red][[REDACTED]][/]\n");
                    logLines.Add("[orange3]\nNotes: \n[/][red]DELETE ALL TRACK OF THIS EXPERIMENT IMMEDIATELY!!! COMPLETE FAILURE. FORGET ABOUT IT FORGET ABOUT IT FORGET ABOUT IT[/]");
                    return;
                case 4:
                    logLines.Add("[orange3] \nDATE: [/][green]04/03/1983 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #018 \n[/]");
                    logLines.Add("[lime]Subject name: Amanda Claude\n[/]");
                    logLines.Add("[lime]Age: 27[/]\n");
                    logLines.Add("[lime]Status: [/][red]DECEASED[/]\n");
                    logLines.Add("[lime]Target Effect: Exchange of emotions - We're expecting an erasure of past trauma and change in everyday attitude.[/]\n");
                    logLines.Add("[lime]\nFinal Effect: Depression, apathy, and emotional instability. Patient had no issues with the healing and operaiton process, but mental stability caused a [red][[REDACTED]][/] in the recreational area.[/]\n");
                    logLines.Add("[orange3]\nNotes: \nKeep an eye on cameras, weird behaviour and potentially dangerious objects within institution.[/]");
                    return;
                case 5:
                    logLines.Add("[orange3] \nDATE: [/][green]04/20/1983 \n[/]");
                    logLines.Add("[orange3] \nEXPERIMENT #020 \n[/]");
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
        public List<string> whiteboardList = new List<string>() //
        {
            "REASON is its share of what they are —\nits very value equals its own part divided by the sum of all three.\n \nEMOTION is the same — its worth \nis measured by how it stands within the whole.\n \nMORALITY, too, holds the same truth — \nit exists in proportion to the total they form together.",
            "R = R/(R+E+M)\n \n \nE = E/(R+E+M)\n \n \nM = M/(R+E+M)"
        };

        public List<string> GetNewspaper = new List<string>();
        public void Newspaper() //wypełnia listę co napisać w gazecie
        {
            GetNewspaper.Add("[darkgoldenrod]Date: April 19, 1983 \n[/]");
            GetNewspaper.Add("\n[darkgoldenrod underline]BREAKING NEWS[/]\n");
            GetNewspaper.Add("[grey italic]Police still haven't found Amanda Claude. It's been three weeks since she's gone missing. Please report the local authorities if you have any information about a person matching these descriptions below:[/]\n");
            GetNewspaper.Add("[steelblue italic]Name: Amanda Claude\nAge: 18\nHeight: 5'7\"\nWeight: 135 lbs\nHair Color: Brown\nEye Color: Blue\nLast Seen Wearing: Black leather jacket, blue jeans, white sneakers[/]\n");
            GetNewspaper.Add("\n \n[steelblue italic]Additional info: Medicated depression, but due to lack of supplies she might seem traumatized or confused, please have patience and speak to her with kindness if you find her.[/]\n");
            GetNewspaper.Add("\n \n[grey italic]If you have any information, please contact the local police department at (555) 161-4559.[/]\n");

        }

        public List<string> DestroyList = new List<string>()
        {
            "[lightslateblue]Do you want to activate the device? Are you aware of the consequences?[/]\n",
            "YES",
            "NO",

            "[yellow1]it requires a password to proceed. On the back of the device it's written [/][red]THE BEST YEAR[/][yellow1].[/]", //[3]
            "\n[green]Please insert 4-digit pin code: [/]\n",

            "\n[green]PASSWORD CORRECT.\n>Erasing Terminal data...[/]", //[5]
            "\r                            ",
            "\r[green]DONE.[/]", 

            "\n[red]INCORRECT PASSWORD. TRY AGAIN.[/]\n", //[7]

        };
    }
}


