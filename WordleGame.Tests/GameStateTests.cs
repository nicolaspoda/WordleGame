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
            // Créer un dictionnaire temporaire pour les tests
            File.WriteAllLines(TempDictionaryFile, new[] { "APPLE", "PLANE", "GUITAR", "PUZZLE" });
        }

        [TearDown]
        public void Teardown()
        {
            // Nettoyer le fichier temporaire
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
            // Forcer le mot cible pour test
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
        public void TimedMode_ShouldTimeoutAfter60Seconds()
        {
            var game = new GameState(5, GameMode.Timed);
            typeof(GameState).GetProperty("StartTime")?.SetValue(game, DateTime.Now.AddMinutes(-2));
            Assert.That(game.IsTimedOut(), Is.True);
        }
    }
}
