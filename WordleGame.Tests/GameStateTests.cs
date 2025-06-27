using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class GameStateTests
    {
        private GameState game;

        [SetUp]
        public void Setup()
        {
            game = new GameState(5);
        }

        [Test]
        public void MakeGuess_CorrectWord_WinsGame()
        {
            var feedback = game.MakeGuess("APPLE");
            Assert.That(game.HasWon, Is.True);
            Assert.That(feedback, Is.EqualTo("🟩🟩🟩🟩🟩"));
        }

        [Test]
        public void MakeGuess_IncorrectWord_AttemptsIncrease()
        {
            var attemptsBefore = game.CurrentAttempt;
            game.MakeGuess("BRAVE");
            Assert.That(game.CurrentAttempt, Is.EqualTo(attemptsBefore + 1));
        }

        [Test]
        public void IsGameOver_WhenMaxAttemptsReached()
        {
            for (int i = 0; i < 6; i++)
            {
                game.MakeGuess("BRAVE");
            }
            Assert.That(game.IsGameOver, Is.True);
        }
    }
}
