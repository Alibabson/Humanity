namespace Humanity.Model
{
    public class GameModel
    {
        public bool Reason { get; set; } = false;
        public bool Emotion { get; set; } = false;
        public bool Morality { get; set; } = false;

        // Stan ogólny
        public bool IntroPlayed { get; set; } = false;

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
        public List<string> look = new();
        public string Uknown()
        {
            return "Unknown command. Type HELP for a list of commands.";
        }
        public string Help()
        {
            return "Available commands:\n" +
                   "- HELP: Show this help message.\n" +
                   "- LOOK: Observe your surroundings.\n" +
                   "- CHECK [ITEM]: Describe item or room. \n" +
                   "- GO TO [ROOM]: move to the next room. \n" +
                   "- QUIT/EXIT: Terminate the session and give up on your H U M A N I T Y. \n";
        }
        public void pickLook(int idx, int part)
        {
            look.Clear();
            switch (idx)
            {
                case 0:
                    if (part == 1)
                    {
                        look.Add("You are in a ");
                        look.Add("LAB");   //this is blue
                        look.Add(". The room is filled with scientific equipment, ");
                        look.Add("monitors displaying data"); //and this
                        look.Add(", and an unsolved equation scribbled on a ");
                        look.Add("whiteboard"); //and this
                        look.Add(".\nBehind you there are stairs leading upstairs to the ");
                        look.Add("hallway.");
                        look.Add("\n \nYou remember this place.");
                        
                    }


                    return;
                case 1:
                    return;
                case 2:
                    return;
                case 3:
                    return;
                case 4:
                    return;
                case 5:
                    return;
                case 6:
                    return;
                case 7:
                    return;
                case 8:
                    return;
                default:
                    return;
            }
        }
        public string checkItem(int idx, string item)
        {
            /*switch (idx)
            {
                case 0:
                    switch (item)
                    {
                        case "monitor":
                            return;
                        case "floor":
                            return;
                        default:
                            return;
                    }
                case 1:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 2:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 3:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 4:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 5:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 6:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 7:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                case 8:
                    switch (item)
                    {
                        case "":
                            return;
                        default:
                            return;
                    }
                default:
                    return;
            } */
            return "";
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

                    
                case "bathroom":
                case "bath":
                    next_idx = 3;
                    return next_idx;

                case "living room":
                case "living":
                    next_idx = 4;
                    return next_idx;

                case "F2 hallway":
                case "Floor 2 hallway":
                case "second floor hallway":
                case "Floor2 hallway":
                    next_idx = 5;
                    return next_idx;

                case "F2 bathroom":
                case "Floor 2 bathroom":
                case "second floor bathroom":
                case "Floor2 bathroom":
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
    }
}
