using System.Collections.Generic;
using System.Linq;

namespace WordleGame.App
{
    public class GameStats
    {
        public int Wins { get; private set; }
        public int Losses { get; private set; }
        public int CurrentStreak { get; private set; }
        public List<int> AttemptsList { get; } = new();
        public int TotalScore { get; private set; }

        public void RegisterWin(int attempts, GameMode mode)
        {
            Wins++;
            CurrentStreak++;
            AttemptsList.Add(attempts);

            int score = ScoreCalculator.CalculateScore(attempts, mode, CurrentStreak, true);
            TotalScore += score;
        }

        public void RegisterLoss(GameMode mode)
        {
            Losses++;
            CurrentStreak = 0;

            int score = ScoreCalculator.CalculateScore(6, mode, CurrentStreak, false);
            TotalScore += score;
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
            TotalScore = 0;
            AttemptsList.Clear();
        }
    }
}
