namespace HauntedTerminal.Model
{
    public class GameState
    {
        // Moduły człowieczeństwa
        public bool ReasonOnline { get; set; } = false;
        public bool EmotionOnline { get; set; } = false;
        public bool MoralityOnline { get; set; } = false;

        // Stan ogólny
        public string CurrentSceneId { get; set; } = "";
        public bool IntroPlayed { get; set; } = false;
        public int Sanity { get; set; } = 100;    // możesz użyć później

        // Prosty dziennik wspomnień (do komendy RECALL)
        public List<string> MemoryLogs { get; } = new()
        {
            "[MEMORY LOG #07]\nSubject: Anna Voss\nReaction: Panic. Neural feedback overload.\nOutcome: Terminal brain death.\nNote: Decrease voltage in next trial.",
            "[MEMORY LOG #12]\nNo volunteers left.\nBeginning self-experimentation."
        };

        public int MemoryIndex { get; set; } = 0;

        public string StatusLine =>
            $"Status — Reason: {(ReasonOnline ? "ONLINE" : "offline")} | Emotion: {(EmotionOnline ? "ONLINE" : "offline")} | Morality: {(MoralityOnline ? "ONLINE" : "offline")}";
    }
}
