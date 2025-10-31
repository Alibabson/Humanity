﻿using System.ComponentModel;

namespace Humanity.Model
{
    public class GameModel
    {
        public bool Reason { get; set; } = false;
        public bool Emotion { get; set; } = false;
        public bool Morality { get; set; } = false;

        public int sanity { get; set; } = 100;
        // Stan ogólny
        public bool IntroPlayed { get; set; } = true;  /// ZMIENIĆ NA false;

        public int room_idx { get; set; } = 0;
       
        // Prosty dziennik wspomnień (do komendy RECALL)
        /* public List<string> MemoryLogs { get; } = new()
         {
             "[MEMORY LOG #07]\nSubject: Anna Voss\nReaction: Panic. Neural feedback overload.\nOutcome: Terminal brain death.\nNote: Decrease voltage in next trial.",
             "[MEMORY LOG #12]\nNo volunteers left.\nBeginning self-experimentation."
         };

         public int MemoryIndex { get; set; } = 0;

         public string StatusLine =>
             $"Status — Reason: {(Reason ? "ONLINE" : "offline")} | Emotion: {(Emotion ? "ONLINE" : "offline")} | Morality: {(Morality ? "ONLINE" : "offline")}";
        */
        public List<string> Intro = new()
        {
            "> Backup data loaded",
            "> Initializing neural interface...",
            "> Experiment database - inclomplete",
            "> Last session data found",
            "> Restoring memories...",
            "> ~$ apt-sudo install get coffee", 
            "> Neural map - incomplete",
            "> Scanning patient identity"
        };
        public class PrologueLine
        {
            public string Text { get; set; }
            public string Color { get; set; }
            public int DelayMs { get; set; }

            public PrologueLine(string text, int delay, string color) {
                Text = text;
                DelayMs = delay;
                Color = color;
            }
        }
        public List<PrologueLine> Prologue = new()
        {
           new PrologueLine( "You shouldn’t have woken up.\n", 45 ,"[italic teal]"),
            new PrologueLine("\nA sharp, high-pitched noise pierces the silence.\nThe light above you flickers, buzzing faintly, washing the room in a cold, sterile glow.\nMetal. Concrete. The faint smell of chemicals and ozone.\nYou’re lying on a metal table, surrounded by shattered glass and dark stains that look too much like blood.\n", 15, "[italic silver]"),
            new PrologueLine("\nYou don’t remember how you got here.\nYou don’t remember why you’re here.\nYou don’t even remember your name.\n", 15, "[italic yellow4]"),
            new PrologueLine("\nBeside you, a monitor flickers to life, spilling pale light across the room.\n For a second, shapes and words flash across the screen — too fast to read —\n then everything freezes.\nOne of the machines is connected straight to your body. The heart rate monitor flickers, as if it can’t decide whether there’s a heartbeat to find, but you’re not dead,\n", 15, "[italic silver]"),
            new PrologueLine("Are you?\n", 45, "[italic red]"),
            new PrologueLine("\nSomewhere behind you, a faint echo — a breath, or maybe a whisper.\n", 15, "[italic silver]"),
            new PrologueLine("You promised…\n", 45, "[italic teal]"),
            new PrologueLine("It’s hard to tell if it came from the room... or from inside your head.\nYour heartbeat — slow, irregular… fills the silence again.\n", 15, "[italic silver]"),
            new PrologueLine("\nYou can’t form a single coherent thought in your brain - like someone’s rewiring your thoughts from the inside.\n", 15, "[italic silver]"),
            new PrologueLine("You can’t feel anything…\n", 15, "[italic red]"),
            new PrologueLine("You were part of a scheme you know nothing about. Seems like you were just a disposable element…\n", 15, "[italic silver]"),
            new PrologueLine("\nThe hum of dying machines and the atmosphere of a room feels too familiar to be a stranger’s.\nAfter brief confusion, you realize that whatever you were before...", 15, "[italic silver]"),
            new PrologueLine(" you are not anymore.\n", 45, "[italic red]")
        };
        
        public string Uknown()
        {
            return "Unknown command. Type HELP for a list of commands.";
        }
        public string Help()
        {
            return "-------------------------------------------------------------------------\n" +
                   "|Available commands:                                                     |\n" +
                   "|- HELP: Show this help message.                                         |\n" +
                   "|- LOOK: Observe your surroundings.                                      |\n" +
                   "|- CHECK [ITEM]: Describe item or room.                                  |\n" +
                   "|- GO TO [ROOM]: move to the next room.                                  |\n" +
                   "|- QUIT/EXIT: Terminate the session and give up on your H U M A N I T Y. |\n" +
                   "-------------------------------------------------------------------------";
        }
        public List<string> look = new();
        public void pickLook(int idx, int part)
        {
            look.Clear();
            switch (idx)
            {
                case 0:
                    look.Add("You are in a ");
                    look.Add("[yellow]LAB [/]");
                    look.Add(".\nThe room is filled with scientific equipment, ");
                    look.Add("[teal]monitors [/]");
                    if(Reason==false)
                        look.Add("displaying data, and an unsolved, barely recognizible \nequation scribbled on a ");
                    else
                    {
                        look.Add("displaying data, and already solved equation scribbled on a ");
                    }
                        look.Add("[teal]whiteboard [/]"); 
                    if(Reason==false)
                    look.Add("- as if [orchid]REASON[/] itself was lost here.\nBehind you there are stairs leading upstairs to the ");
                    else
                    {
                        look.Add("- but you regained your [orchid]REASON[/].\nBehind you there are stairs leading upstairs to the ");
                    }
                    look.Add("[palegreen1_1]hallway. [/]");
                    look.Add("\n \n[red]You remember this place.[/]");                   
                return;

                case 1:
                    look.Add("You are currently in the ");
                    look.Add("[yellow]hallway[/]"); 
                    look.Add(" on the first floor. You can see the stairs leading down to the ");
                    look.Add("[palegreen1_1]lab[/]"); 
                    look.Add(" and a couple of doors \nleading to various rooms.\nYou don't know why but you know where every door leads: ");
                    look.Add("[palegreen1_1]kitchen[/]"); 
                    look.Add(", ");
                    look.Add("[palegreen1_1]library[/]");
                    look.Add(" and ");
                    look.Add("[palegreen1_1]living room[/]");
                    look.Add(".\n  \n There are some words scraped on the left ");
                    look.Add("[teal]wall[/]"); 
                    look.Add(", as well as some ");
                    look.Add("[teal]notes [/]"); 
                    look.Add("lying on the ground.");
                return;

                case 2:
                    look.Add("You are in the ");
                    look.Add("[yellow]kitchen[/]");
                    look.Add(".\nThe room is fancy, but dusty, with a modern design. \nSuprisingly, stairs leading to the");
                    look.Add("[palegreen1_1] second floor[/] ");
                    look.Add("are located here. Doors behind you lead to the ");
                    look.Add("[palegreen1_1]hall[/]");
                    look.Add(" and the");
                    look.Add("[palegreen1_1] living room[/]");
                    look.Add(".\n \n[red]You feel a strange sense of familiarity.[/]");
                    return;

                case 3:
                    look.Add("You are currently in the ");
                    look.Add("[yellow]library[/]");
                    look.Add(".\nThe room is small but clean, seems unused for quite a moment, like this place was abandoned decades ago. \nThere is a door which you entered, leading to the ");
                    look.Add("[palegreen1_1]hallway[/]"); 
                    look.Add(". The only thing there is a ");
                    look.Add("[teal]mirror[/]"); 
                    look.Add(" you noticed. \n \n[red]You feel a strange sense of familiarity.[/]");
                    return;

                case 4:
                    look.Add("You are now in the ");
                    look.Add("[yellow]living room[/]");
                    look.Add(".\nThe place that you find the most comfortable here. The room is spacious, with a large sofa and a TV. \nThere is a door leading to the ");
                    look.Add("[palegreen1_1]hallway[/]");
                    look.Add(" and another one to the ");
                    look.Add("[palegreen1_1]kitchen[/]");
                    look.Add(".\n \nYou feel comfortable here, but you don't remember a single thing about this place. \nOn the table you see a ");
                    look.Add("[teal]map[/] ");
                    look.Add("of the house. On the wall there is a bunch of");
                    look.Add("[teal] photos [/]");
                    look.Add("of a happy family. \n \n[bold red]W H O ' S  F A M I L Y  I S  T H A T  ,  A D R I A N ? [/]\n \n ");
                    return;

                case 5:
                    look.Add("You are now in the ");
                    look.Add("[yellow]second floor hallway[/]"); 
                    look.Add(".\nThis corridor looks nothing like that one downstairs. Someone lit the ");
                    look.Add("[teal]candles[/]"); 
                    look.Add(" placed on the floor, giving the place a warm, but ocultic atmosphere. \nThere are doors leading to the ");
                    look.Add("[palegreen1_1]bedroom[/]"); 
                    look.Add(", ");
                    look.Add("[palegreen1_1]bathroom[/]"); 
                    look.Add(", ");
                    look.Add("[palegreen1_1]kitchen[/]"); 
                    look.Add("downstairs and the ");
                    look.Add("[palegreen1_1]office[/]"); 
                    look.Add(".\n \n[bold red]You feel a strange sense of someone's presence. \n[/]");
                    return;

                case 6:
                    look.Add("You are currently in the ");
                    look.Add("[yellow]bathroom[/]"); 
                    look.Add(".\nThe room is dimly lit by the candles placed on the sink. \nThere is a door behind you leading back to the ");
                    look.Add("[palegreen1_1]F2 hallway[/]"); 
                    look.Add(".\n \nThe mirror is broken, but someone left a message - just for you. On the floor there are");
                    look.Add("[teal] notes[/]");
                    look.Add(" scattered around just for you, some of them are slightly burnt. ");
                    return;

                case 7:
                    look.Add("You are now in the ");
                    look.Add("[yellow]bedroom[/]"); 
                    look.Add(".\nThe room is a mess, with a large broken bed and a wardrobe. The stench is making it unbearable to stay. Did someone die here? \nThere is a door leading back to the ");
                    look.Add("[palegreen1_1]F2 hallway[/]");
                    look.Add(".\n On the dusty desk you can see a ");
                    look.Add("[teal]diary [/]");
                    look.Add("with the initials [bold red]'A. H.'[/] ");
                    if(Emotion==false)
                        look.Add("engraved on the cover. \n \nYou don't feel any [orchid]EMOTIONS.[/]");
                    else
                        look.Add("engraved on the cover. \n \nYou feel a wide range of [orchid]EMOTIONS. [/]");
                    return;
                case 8:
                    look.Add("You are now in the ");
                    look.Add("[yellow]office[/]"); 
                    look.Add(".\nThe room is filled with bookshelves, a large desk and a comfortable chair. \nThere is a door leading back to the ");
                    look.Add("[palegreen1_1]F2 hallway[/]"); 
                    look.Add(".\n On the desk you can see a ");
                    look.Add("[teal]laptop [/]");
                    if(Morality==false)
                        look.Add(". Somehow it still works, but time made it feel like it's ancient. \n There are some gruesome videos stashed on it. \n \n[bold red]But you don't feel any[/][bold orchid] MORALITY[/]");
                   else 
                        look.Add(". Somehow it still works, but time made it feel like it's ancient. \n There are some gruesome videos stashed on it. \n \nYour [orchid]MORALITY[/] makes you regret your life.");
                    return;
                default:
                    return;
        }
            }
        private string wrongItem(string item)
        {
            string wrong = "There is no such item as" + item;
            return wrong;
        }
        public List<string> checkItem(int idx, string item)
        {
            List<string> itemDesc = new();
            switch (item)
            {
                case "monitor":
                    if (idx == 0)
                    {
                        itemDesc.Add("[lime]Welcome, Dr. Hallaway. What would you like to do?\n[/]");
                        itemDesc.Add("[lime]CHECK CURRENT PATIENT STATUS[/]");
                        itemDesc.Add("[lime]OPEN TEST LOGS[/]");
                        itemDesc.Add("[lime]QUIT TERMINAL[/]");
                    }
                    else itemDesc.Add(wrongItem(item));
                    break;


                case "whiteboard":
                    if (idx == 0)
                    {
                        itemDesc.Add("The board flickers — not solid, but digital, projected on the air.\r\nThe equations shift like living veins.");
                    } else wrongItem(item);
                    break;
                    

                default: wrongItem(item);
                    break;
            }
            return itemDesc;


        }
        public int NextRoomIdx(string room)
        {
            int next_idx;
            switch (room)
            {
                case "lab":
                    next_idx = 0;
                    return next_idx;

                case "hallway":
                case "hall":
                case "F1 hallway":
                    next_idx = 1;
                    return next_idx;

                case "kitchen":
                    next_idx = 2;
                    return next_idx;

                case "library":
                    next_idx = 3;
                    return next_idx;

                case "living room":
                case "living":
                    next_idx = 4;
                    return next_idx;

                case "f2 hallway":
                case "second floor":
                case "second floor hallway":
                case "floor2 hallway":
                    next_idx = 5;
                    return next_idx;

                case "f2 bathroom":
                case "floor 2 bathroom":
                case "second floor bathroom":
                case "floor2 bathroom":
                case "bathroom":
                    next_idx = 6;
                    return next_idx;

                case "bedroom":
                    next_idx = 7;
                    return next_idx;

                case "office":
                    next_idx = 8;
                    return next_idx;

                default:
                    return -1;
            }
        }
        public void GoTo_Possible(int idx)
        {
            room_idx = idx;
        }
        public string RoomName(int x)
        {
            switch (x)
            {
                case 0:
                    return "LABORATORY";
                case 1:
                    return "F1 HALLWAY";
                case 2:
                    return "KITCHEN";
                case 3:
                    return "F1 BATHROOM";
                case 4:
                    return "LIVING ROOM";
                case 5:
                    return "F2 HALLWAY";
                case 6:
                    return "F2 BATHROOM";
                case 7:
                    return "BEDROOM";
                case 8:
                    return "OFFICE";
                default:
                    return "UNKNOWN LOCATION";
            }
        }
        public List<string> statusLines = new();
        public List<string> Status()
        {
            statusLines.Clear();
            statusLines.Add("[lime bold]SYSTEM STATUS:[/]");
            statusLines.Add($"[lime]- REASON: {(Reason ? "ONLINE[/]" : "[/][red]OFFLINE[/]")}");
            statusLines.Add($"[lime]- EMOTION: {(Emotion ? "ONLINE[/]" : "[/][red]OFFLINE[/]")}");
            statusLines.Add($"[lime]- MORALITY: {(Morality ? "ONLINE[/]" : "[/][red]OFFLINE[/]")}");
            return statusLines;
        }

        public string Quit()
        {
            return ("You feel a sudden wave of relief as you decide to give up.\n" +
            "The weight of uncertainty lifts off your shoulders, replaced by a strange sense of calm.\n" +
            "As you step away from the challenge, a part of you wonders what could have been achieved.\n" +
            "But for now, you choose peace over the unknown.");
        }


        
    }
}
