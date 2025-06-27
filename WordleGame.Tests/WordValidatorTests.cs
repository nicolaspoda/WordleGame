using NUnit.Framework;
using WordleGame.App;

namespace WordleGame.Tests
{
    public class WordValidatorTests
    {
        [Test]
        public void ValidWord_ShouldReturnTrue()
        {
            Assert.That(WordValidator.ValidateWord("APPLE"), Is.True);
        }

        [Test]
        public void WordTooShort_ShouldReturnFalse()
        {
            Assert.That(WordValidator.ValidateWord("APP"), Is.False);
        }

        [Test]
        public void WordWithNonLetters_ShouldReturnFalse()
        {
            Assert.That(WordValidator.ValidateWord("APPL3"), Is.False);
        }

        [Test]
        public void EmptyWord_ShouldReturnFalse()
        {
            Assert.That(WordValidator.ValidateWord(""), Is.False);
        }
    }
}
