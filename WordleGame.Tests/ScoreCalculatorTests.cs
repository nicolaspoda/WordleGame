using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class ScoreCalculatorTests
    {
        [Test]
        public void CalculateScore_ShouldReturn100_WhenOneAttemptNormalMode()
        {
            int score = ScoreCalculator.CalculateScore(1, GameMode.Normal, 5, true);
Assert.That(score, Is.GreaterThanOrEqualTo(100));
        }

        [Test]
        public void CalculateScore_ShouldReturnLower_WhenManyAttemptsNormalMode()
        {
            int score = ScoreCalculator.CalculateScore(4, GameMode.Normal, 5, true);
            Assert.That(score, Is.LessThan(100));
        }

        [Test]
        public void CalculateScore_ShouldReturnHigh_WhenWonInTimedMode()
        {
            int score = ScoreCalculator.CalculateScore(2, GameMode.Timed, 5, true);
            Assert.That(score, Is.GreaterThan(0));
        }

        [Test]
        public void CalculateScore_ShouldReturnLow_WhenLostInTimedMode()
        {
            int score = ScoreCalculator.CalculateScore(6, GameMode.Timed, 5, false);
            Assert.That(score, Is.LessThan(100));
        }
[Test]
public void CalculateScore_ShouldPenalize_WhenNotWon()
{
    int score = ScoreCalculator.CalculateScore(3, GameMode.Normal, 0, false);
    Assert.That(score, Is.LessThan(100)); 
}

[Test]
public void CalculateScore_ShouldScoreNormally_WhenTimedModeAndQuick()
{
    int score = ScoreCalculator.CalculateScore(2, GameMode.Timed, 30, true);
    Assert.That(score, Is.GreaterThan(0));
}

    }
}
