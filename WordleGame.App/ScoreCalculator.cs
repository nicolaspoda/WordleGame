namespace WordleGame.App
{
    public static class ScoreCalculator
    {
        public static int CalculateScore(int attempts, GameMode mode, int streak, bool won)
        {
            if (!won)
                return 0;

            int baseScore = 100;
            int attemptPenalty = attempts * 10;
            int streakBonus = streak * 5;

            int modeBonus = mode switch
            {
                GameMode.Timed => 50,
                GameMode.Practice => -20,
                _ => 0
            };

            int score = baseScore - attemptPenalty + streakBonus + modeBonus;
            return score < 0 ? 0 : score;
        }
    }
}
