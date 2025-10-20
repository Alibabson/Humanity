namespace Humanity.Model
{
    public class GameState
    {
        public bool Reason { get; set; } = false;
        public bool Emotion { get; set; } = false;
        public bool Morality { get; set; } = false;

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
            $"Status — Reason: {(Reason ? "ONLINE" : "offline")} | Emotion: {(Emotion ? "ONLINE" : "offline")} | Morality: {(Morality ? "ONLINE" : "offline")}";
    }
}
