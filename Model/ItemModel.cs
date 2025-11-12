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



        /////TERMINAL
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
                    logLines.Add("[orange3] \nDATE: [/][green]03/23/1983 \n[/]");
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

        ////WHITEBOARD
        public List<string> whiteboardList = new List<string>() //
        {
            "REASON is its share of what they are —\nits very value equals its own part divided by the sum of all three.\n \nEMOTION is the same — its worth \nis measured by how it stands within the whole.\n \nMORALITY, too, holds the same truth — \nit exists in proportion to the total they form together.",
            "R = R/(R+E+M)\n \n \nE = E/(R+E+M)\n \n \nM = M/(R+E+M)"
        };


        //// NEWSPAPER
        public List<string> GetNewspaper = new List<string>();
        public void Newspaper() //wypełnia listę co napisać w gazecie
        {
            GetNewspaper.Clear();
            GetNewspaper.Add("[darkgoldenrod]Date: April 9th, 1983 \n[/]");
            GetNewspaper.Add("\n[darkgoldenrod underline]BREAKING NEWS[/]\n");
            GetNewspaper.Add("[grey italic]Police still haven't found Amanda Claude. It's been three weeks since she's gone missing. Please report the local authorities if you have any information about a person matching these descriptions below:[/]\n");
            GetNewspaper.Add("[steelblue italic]Name: Amanda Claude\nAge: 18\nHeight: 5'7\"\nWeight: 135 lbs\nHair Color: Brown\nEye Color: Blue\nLast Seen Wearing: Black leather jacket, blue jeans, white sneakers[/]\n");
            GetNewspaper.Add("\n \n[steelblue italic]Additional info: Medicated depression, but due to lack of supplies she might seem traumatized or confused, please have patience and speak to her with kindness if you find her.[/]\n");
            GetNewspaper.Add("\n \n[grey italic]If you have any information, please contact the local police department at (555) 161-4559.[/]\n");

        }

        ////BOOKSHELF
        ////przeczytane
        public List<string> List_replay = new List<string>()
        {
            "[grey]You already opened the list.[/]\n"
        };
        ////poemat Elary
        public List<string> Poem = new List<string>()
        {
            "[lightslateblue]Y[/][grey53]ou once told me that the mind could be mapped,[/]\n",
            "[lightslateblue]O[/][grey53]r that a soul was only patterns and light.[/]\n",
            "[lightslateblue]U[/][grey53]nder your hands, even love became an experiment.[/]\n",

            "\n",
            "[lightslateblue]K[/][grey53]nowing you, I should have seen this coming.[/]\n",
            "[lightslateblue]N[/][grey53]ever did I think obsession could sound so quiet.[/]\n",
            "[lightslateblue]O[/][grey53]nly the machines answer me now.[/]\n",
            "[lightslateblue]W[/][grey53]hen I whisper your name, they hum instead.[/]\n",

            "\n",
            "[lightslateblue]W[/][grey53]hat are you chasing, Adrian?[/]\n",
            "[lightslateblue]H[/][grey53]ow many pieces of yourself will you take apart[/]\n",
            "[lightslateblue]A[/][grey53]nd still pretend there's something left to save?[/]\n",
            "[lightslateblue]T[/][grey53]he truth is, I don't recognize the man in your eyes.[/]\n",

            "\n",
            "[lightslateblue]Y[/][grey53]ou said creation demands sacrifice.[/]\n",
            "[lightslateblue]O[/][grey53]n that, perhaps, you were right -[/]\n",
            "[lightslateblue]U[/][grey53]ntil I realized what the sacrifice was.[/]\n",

            "\n",
            "[lightslateblue]D[/][grey53]o you even see it now?[/]\n",
            "[lightslateblue]E[/][grey53]verything you touched began to fade.[/]\n",
            "[lightslateblue]S[/][grey53]ometimes I wonder if that's all you ever wanted.[/]\n",
            "[lightslateblue]T[/][grey53]o prove you could unmake the world,[/]\n",
            "[lightslateblue]R[/][grey53]ebuild it in your image,[/]\n",
            "[lightslateblue]O[/][grey53]r simply watch it collapse.[/]\n",
            "[lightslateblue]Y[/][grey53]ou turned wonder into ashes, Adrian.[/]\n",
            "[lightslateblue]E[/][grey53]ven love.[/]\n",
            "[lightslateblue]D[/][grey53]oomed with no chance of a return.[/]\n",

            "\n",
            "[grey53]-E.[/]\n"
        };
        //
        public List<string> GetPhoto = new List<string>();
        public void Photo() //wypełnia listę co napisać w Photo
        {
            GetPhoto.Clear();
            //GetPhoto.Add("[olive]Photo Caption:[/]\n");
            GetPhoto.Add("[grey]A photo of Doctor Adrian Holloway and a woman on their wedding day, labeled as [/][gold1]\"04/20/1967\"[/][grey]. They both look happy.[/]\n");
            GetPhoto.Add("[grey]There's a note sticked to the side of the picture, saying:[/]\n");
            GetPhoto.Add("[orange3]\nI'll never forget \nthe song that played \non our wedding![/]\n");
            GetPhoto.Add("[orange3]The best [/][red]month,[/]\n");
            GetPhoto.Add("[orange3]The best [/][red]day.[/]\n");
            GetPhoto.Add("[grey]You found a[/] [gold1]Safe[/] [grey]behind the picture![/]\n");
        }
        ////SZYFR
        public List<string> Cipher = new List<string>()
        {
            "[orange3]A → 0    K → 0   U → 0\n\nB → 1    L → 1   V → 1\n\nC → 2    M → 2   W → 2\n\nD → 3    N → 3   X → 3\n\nE → 4    O → 4   Y → 4\n\nF → 5    P → 5   Z → 5\n\nG → 6    Q → 6\n\nH → 7    R → 7\n\nI → 8    S → 8\n\nJ → 9    T → 9[/]\n",
            "[grey63]My love,\n\nI’ve decided to lock it away.\nYou begged me to use it,\nbut you never understood how much depends on this work.\nIt can change the world…\nand you tried to stop me from what you called madness.\n\nI couldn’t let that happen.\nSo I protected myself — and in doing so, I hurt you.\n\nI hid your solution,\nthe one thing you believed could save us.\nMy work is the greatest idea of my life, and you knew that…\n\nWe kept our best memories there.\nThat [/][gold1]safe is our entire marriage [/][grey63]—\nand now it holds our deepest pain as well.\n\nI never wanted things to end like this.\n\nYou were always the [/][gold1]key[/][grey63].\nEven now.\n\n— A.[/]"
        };

        ////DEVICE
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

        ////Fragmenty Człowieczeństwa
        public List<string> Fragment = new List<string>()
        {
           "[red]A mind without ______ is a room without light.[/]\n",
           "[red]A heart without _______ is a voice without sound.[/]\n",
           "[red]A choice without ________ is a road without direction.[/]\n"
        };


    }
}


