using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class GameLogicTests
    {
        [Test]
        public void CheckGuess_AllCorrect()
        {
            var result = GameLogic.CheckGuess("APPLE", "APPLE");
            Assert.That(result, Is.EqualTo("🟩🟩🟩🟩🟩"));
        }

        [Test]
        public void CheckGuess_AllIncorrect()
        {
            var result = GameLogic.CheckGuess("ZZZZZ", "APPLE");
            Assert.That(result, Is.EqualTo("⬜⬜⬜⬜⬜"));
        }

        [Test]
        public void CheckGuess_SomeCorrectPositions()
        {
            var result = GameLogic.CheckGuess("APRIL", "APPLE");
            Assert.That(result, Is.EqualTo("🟩🟩⬜⬜🟨"));
        }

        [Test]
        public void CheckGuess_DuplicateLettersHandled()
        {
            var result = GameLogic.CheckGuess("ALLEY", "APPLE");
            Assert.That(result, Is.EqualTo("🟩🟨⬜🟨⬜"));
        }
    }
}
