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
        public List<string> look = new();
        public void pickLook(int idx, int part)
        {
            look.Clear();
            switch (idx)
            {
                case 0:
                    look.Add("You are in a ");
                    look.Add("LAB");   //this is blue
                    look.Add(".\n The room is filled with scientific equipment, ");
                    look.Add("monitors "); //and this
                    if(Reason==false)
                        look.Add("displaying data, and an unsolved, barely recognizible equation scribbled on a ");
                    else
                    {
                        look.Add("displaying data, and already solved equation scribbled on a ");
                    }
                        look.Add("whiteboard"); //and this
                    if(Reason==false)
                    look.Add("- as if REASON itself was lost here.\nBehind you there are stairs leading upstairs to the ");
                    else
                    {
                        look.Add("- but you regained your REASON.\nBehind you there are stairs leading upstairs to the ");
                    }
                    look.Add("hallway.");
                    look.Add("\n \nYou remember this place.");                   
                return;

                case 1:
                    look.Add("You are currently in the ");
                    look.Add("hallway"); //blue
                    look.Add(" on the first floor. You can see the stairs leading down to the ");
                    look.Add("lab"); //blue
                    look.Add(" and a couple of closed doors leading to various rooms.\nYou don't know why but you know where every door leads: ");
                    look.Add("kitchen"); //blue
                    look.Add(", ");
                    look.Add("bathroom"); //blue
                    look.Add(" and ");
                    look.Add("living room"); //blue
                    look.Add(".\n  \n There are some words scraped on the left");
                    look.Add(" wall"); //blue
                    look.Add(", as well as some ");
                    look.Add("notes "); //blue
                    look.Add("lying on the ground.");
                return;

                case 2:
                    look.Add("You are in the ");
                    look.Add("kitchen"); //blue
                    look.Add(".\nThe room is fancy, but dusty, with a modern design. \nSuprisingly, stairs leading to the");
                    look.Add(" second floor"); //blue
                    look.Add("are located here. Doors behind you lead to the ");
                    look.Add("hall"); //blue
                    look.Add("and the");
                    look.Add("  living room"); //blue
                    look.Add(".\n \nYou feel a strange sense of familiarity.");
                    return;

                case 3:
                    look.Add("You are currently in the ");
                    look.Add("library"); //blue
                    look.Add(".\nThe room is small but clean, seems unused for quite a moment, like this place was abandoned decades ago. \nThere is a door which you entered, leading to the ");
                    look.Add("hallway"); //blue
                    look.Add("The only thing is a ");
                    look.Add("mirror"); //blue
                    look.Add(" you noticed. \n \nYou feel a strange sense of familiarity.");
                    return;

                case 4:
                    look.Add("You are now in the ");
                    look.Add("living room"); //blue
                    look.Add(".\nThe place that you find the most comfortable here. The room is spacious, with a large sofa and a TV. \nThere is a door leading to the ");
                    look.Add("hallway"); //blue
                    look.Add(" and another one to the ");
                    look.Add("kitchen"); //blue
                    look.Add(".\n \nYou feel successful here, but you don't remember a single thing about this place. \n On the table you see a ");

                    look.Add("map ");
                    look.Add("of the house. On the wall there is a bunch of");
                    look.Add(" photos ");
                    look.Add("of a happy family. \n \n W H O ' S  F A M I L Y  I S  T H A T  ,  A D R I A N ? \n \n ");
                    return;

                case 5:
                    look.Add("You are now in the ");
                    look.Add("second floor hallway"); //blue
                    look.Add(".\nThis corridor looks nothing like that one downstairs. Someone lit the ");
                    look.Add("candles"); //blue
                    look.Add(" placed on the floor, giving the place a warm, but ocultic atmosphere. \nThere are doors leading to the ");
                    look.Add("bedroom"); //blue
                    look.Add(", ");
                    look.Add("bathroom"); //blue
                    look.Add(", ");
                    look.Add("kitchen"); //blue
                    look.Add("downstairs and the ");
                    look.Add("office"); //blue
                    look.Add(".\n \nYou feel a strange sense of someone's presence. \n");
                    return;

                case 6:
                    look.Add("You are currently in the ");
                    look.Add("bathroom"); //blue
                    look.Add(".\nThe room is dimly lit by the candles placed on the sink. \nThere is a door behind you leading back to the ");
                    look.Add("F2 hallway"); //blue
                    look.Add(".\n \nThe mirror is broken, but someone left a message - just for you. On the floor there are");
                    look.Add(" notes");
                    look.Add(" scattered around just for you, some of them are slightly burnt. ");
                    return;

                case 7:
                    look.Add("You are now in the ");
                    look.Add("bedroom"); //blue
                    look.Add(".\nThe room is a mess, with a large broken bed and a wardrobe. The stench is making it unbearable to stay. Did someone die here? \nThere is a door leading back to the ");
                    look.Add("F2 hallway"); //blue
                    look.Add(".\n On the dusty desk you can see a ");
                    look.Add("diary ");
                    look.Add("with the initials 'A. H.' ");
                    if(Emotion==false)
                        look.Add("engraved on the cover. \n \nYou don't feel any EMOTIONS.");
                    else
                        look.Add("engraved on the cover. \n \nYou feel a wide range of EMOTIONS.");
                    return;
                case 8:
                    look.Add("You are now in the ");
                    look.Add("office"); //blue
                    look.Add(".\nThe room is filled with bookshelves, a large desk and a comfortable chair. \nThere is a door leading back to the ");
                    look.Add("F2 hallway"); //blue
                    look.Add(".\n On the desk you can see a ");
                    look.Add("laptop ");
                    if(Morality==false)
                        look.Add(". Somehow it still works, but time made it feel like it's ancient. \n There are some gruesome videos stashed on it. \n \nBut you don't feel any MORALITY");
                   else 
                        look.Add(". Somehow it still works, but time made it feel like it's ancient. \n There are some gruesome videos stashed on it. \n \nYourMORALITY makes you regret your life.");
                    return;
                default:
                    return;
        }
            }
        //  public List<string> itemDesc = new();
     
        public List<string> checkItem(int idx, string item)
        {
            List<string> itemDesc = new(); // lokalna lista zamiast pola publicznego

            switch (idx)
            {
                case 0:
                    switch (item)
                    {
                        case "monitor":
                            itemDesc.Add("Welcome, Dr. Hallaway. What would you like to do?\n");
                            itemDesc.Add("1. Option A");
                            itemDesc.Add("2. Option B");
                            itemDesc.Add("3. Option C");
                            break;
                        //   _itemView.MonitorItem(itemDesc);
                        //   return;

                        case "whiteboard":
                            itemDesc.Add("The board flickers — not solid, but digital, projected on the air.\r\nThe equations shift like living veins.");
                            break;
                        // return;
                        default:
                            itemDesc.Add("You see nothing special.");
                            break;
                            //   return;
                    }
                    break;
                /*     case 1:
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

                default:
                    itemDesc.Add("Unknown");
                    break;
                    //  return; 
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

                    
                case "bathroom":
                case "bath":
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
