namespace WordleGame.App
{
    public class GameStats
    {
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int CurrentStreak { get; private set; }
        public List<int> AttemptsList { get; } = new();

        public void RegisterWin(int attempts)
        {
            Wins++;
            CurrentStreak++;
            AttemptsList.Add(attempts);
        }

        public void RegisterLoss()
        {
            Losses++;
            CurrentStreak = 0;
        }

        public double AverageAttempts()
        {
            if (AttemptsList.Count == 0) return 0;
            return AttemptsList.Average();
        }

        public void Reset()
        {
            Wins = 0;
            Losses = 0;
            CurrentStreak = 0;
            AttemptsList.Clear();
        }
    }
}
