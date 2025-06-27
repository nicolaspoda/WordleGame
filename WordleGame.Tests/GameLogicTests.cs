using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class GameLogicTests
    {
        [Test]
        public void ExactMatch_ShouldReturnAllGreen()
        {
            string result = GameLogic.CheckGuess("APPLE", "APPLE");
            Assert.That(result, Is.EqualTo("🟩🟩🟩🟩🟩"));
        }

        [Test]
        public void PartialMatch_ShouldReturnCorrectFeedback()
        {
            string result = GameLogic.CheckGuess("PLANE", "APPLE");
            Assert.That(result, Is.EqualTo("🟨🟨🟨⬜🟩"));
        }

        [Test]
        public void NoMatch_ShouldReturnAllGray()
        {
            string result = GameLogic.CheckGuess("CRANE", "BOOST");
            Assert.That(result, Is.EqualTo("⬜⬜⬜⬜⬜"));
        }

        [Test]
        public void MixedMatch_ShouldHandleMultipleLettersCorrectly()
        {
            string result = GameLogic.CheckGuess("LEVEL", "HELLO");
            Assert.That(result, Is.EqualTo("🟨🟩⬜⬜🟨"));
        }
    }
}
