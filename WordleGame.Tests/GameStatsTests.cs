using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class GameStatsTests
    {
        [Test]
        public void RegisterWin_ShouldUpdateStats()
        {
            var stats = new GameStats();
            stats.RegisterWin(3, GameMode.Normal);

            Assert.That(stats.Wins, Is.EqualTo(1));
            Assert.That(stats.CurrentStreak, Is.EqualTo(1));
            Assert.That(stats.TotalScore, Is.GreaterThan(0));
            Assert.That(stats.AttemptsList.Count, Is.EqualTo(1));
        }

        [Test]
        public void RegisterLoss_ShouldResetStreak()
        {
            var stats = new GameStats();
            stats.RegisterWin(3, GameMode.Normal);
            stats.RegisterLoss(GameMode.Normal);

            Assert.That(stats.Wins, Is.EqualTo(1));
            Assert.That(stats.Losses, Is.EqualTo(1));
            Assert.That(stats.CurrentStreak, Is.EqualTo(0));
        }

        [Test]
        public void AverageAttempts_ShouldBeCorrect()
        {
            var stats = new GameStats();
            stats.RegisterWin(3, GameMode.Normal);
            stats.RegisterWin(5, GameMode.Normal);

            Assert.That(stats.AverageAttempts(), Is.EqualTo(4));
        }

        [Test]
        public void Reset_ShouldClearStats()
        {
            var stats = new GameStats();
            stats.RegisterWin(3, GameMode.Normal);
            stats.RegisterLoss(GameMode.Normal);

            stats.Reset();

            Assert.That(stats.Wins, Is.EqualTo(0));
            Assert.That(stats.Losses, Is.EqualTo(0));
            Assert.That(stats.CurrentStreak, Is.EqualTo(0));
            Assert.That(stats.AttemptsList.Count, Is.EqualTo(0));
            Assert.That(stats.TotalScore, Is.EqualTo(0));
        }
    }
}
