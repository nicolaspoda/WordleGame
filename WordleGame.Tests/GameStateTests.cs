using NUnit.Framework;
using WordleGame.App;
using System.IO;
using System;

namespace WordleGame.Tests
{
    public class GameStateTests
    {
        private const string TempDictionaryFile = "Dictionary.txt";

        [SetUp]
        public void Setup()
        {
            File.WriteAllLines(TempDictionaryFile, new[] { "APPLE", "PLANE", "GUITAR", "PUZZLE" });
        }

        [TearDown]
        public void Teardown()
        {
            if (File.Exists(TempDictionaryFile))
                File.Delete(TempDictionaryFile);
        }

        [Test]
        public void GameState_CorrectInitialization()
        {
            var game = new GameState(5, GameMode.Normal);
            Assert.That(game.WordLength, Is.EqualTo(5));
            Assert.That(game.MaxAttempts, Is.EqualTo(6));
            Assert.That(game.IsGameOver, Is.False);
        }

        [Test]
        public void MakeGuess_CorrectFeedbackAndWin()
        {
            var game = new GameState(5, GameMode.Normal);
            game = new GameState(5, GameMode.Normal);
            var targetWord = "APPLE";
            typeof(GameState)
                .GetField("<TargetWord>k__BackingField", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)
                ?.SetValue(game, targetWord);

            string feedback = game.MakeGuess("APPLE");

            Assert.That(feedback, Is.EqualTo("🟩🟩🟩🟩🟩"));
            Assert.That(game.HasWon, Is.True);
            Assert.That(game.IsGameOver, Is.True);
        }

        [Test]
        public void IsWordInDictionary_ShouldWork()
        {
            var game = new GameState(6, GameMode.Normal);
            Assert.That(game.IsWordInDictionary("PUZZLE"), Is.True);
            Assert.That(game.IsWordInDictionary("XXXXXX"), Is.False);
        }

        [Test]
        public void MaxAttempts_IsCorrectInPracticeMode()
        {
            var game = new GameState(5, GameMode.Practice);
            Assert.That(game.MaxAttempts, Is.EqualTo(999));
        }

       
[Test]
public void Constructor_ShouldThrow_WhenNoWordsOfGivenLength()
{
    var ex = Assert.Throws<Exception>(() => new GameState(99, GameMode.Normal));
    Assert.That(ex.Message, Does.StartWith("No words found"));
}

[Test]
public void IsGameOver_ShouldReturnFalse_WhenAttemptsRemainAndNotWon()
{
    var state = new GameState(5, GameMode.Normal);
    Assert.IsFalse(state.IsGameOver);
}





    }
}
