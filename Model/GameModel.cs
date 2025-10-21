namespace Humanity.Model
{
    public class GameModel
    {
        public bool Reason { get; set; } = false;
        public bool Emotion { get; set; } = false;
        public bool Morality { get; set; } = false;

        // Stan ogólny
        public bool IntroPlayed { get; set; } = false;

        public int room_idx { get; set;} = 0;
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
                   "- EXAMINE [ITEM]: Describe item or room. \n" +
                   "- GO TO [ROOM]: move to the next room. \n" +
                   "- QUIT/EXIT: Terminate the session and give up on your H U M A N I T Y. \n";
        }
        public void pickLook(int idx)
        {
            look.Clear();
            switch (idx)
            {
                case 0:
                   look.Add("widzisz ");
                   look.Add("gowno.");
                    return;
                default:
                    return ;
            }
        }
    }
}
