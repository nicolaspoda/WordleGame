using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace WordleGame.App
{
    public class GameState
    {
        public string TargetWord { get; }
        public int CurrentAttempt { get; private set; }
        public bool IsGameOver => CurrentAttempt >= MaxAttempts || HasWon;
        public bool HasWon { get; private set; }
        public int WordLength { get; }
        public GameMode Mode { get; }
        public int MaxAttempts { get; }
        private readonly List<string> dictionary;
        public DateTime StartTime { get; set; }

        public GameState(int wordLength = 5, GameMode mode = GameMode.Normal)
        {
            WordLength = wordLength;
            Mode = mode;
            StartTime = DateTime.Now;
            MaxAttempts = (mode == GameMode.Practice) ? 999 : 6;

            var dictionaryPath = Path.Combine(AppContext.BaseDirectory, "Dictionary.txt");
            dictionary = File.ReadAllLines(dictionaryPath)
                .Where(w => w.Length == wordLength)
                .Select(w => w.Trim().ToUpper())
                .ToList();


            if (!dictionary.Any())
                throw new Exception($"No words found with length {wordLength}");

            var random = new Random();
            TargetWord = dictionary[random.Next(dictionary.Count)];
            CurrentAttempt = 0;
            HasWon = false;
        }

        public bool IsWordInDictionary(string word)
        {
            return dictionary.Contains(word.ToUpper());
        }

        public string MakeGuess(string guess)
        {
            guess = guess.ToUpper();
            if (guess == TargetWord)
            {
                HasWon = true;
            }

            var feedback = GameLogic.CheckGuess(guess, TargetWord);
            CurrentAttempt++;
            return feedback;
        }

        public bool IsTimedOut()
        {
            return Mode == GameMode.Timed && (DateTime.Now - StartTime).TotalSeconds >= 60;
        }
    }
}
