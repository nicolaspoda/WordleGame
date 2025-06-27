using System.Linq;

namespace WordleGame.App
{
    public static class WordValidator
    {
        public static bool ValidateWord(string word, int wordLength)
        {
            return word.Length == wordLength && word.All(char.IsLetter);
        }
    }
}
