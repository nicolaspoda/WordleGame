namespace WordleGame.App;

public static class WordValidator
{
    public static bool ValidateWord(string word)
    {
        return !string.IsNullOrEmpty(word) && word.Length == 5 && word.All(char.IsLetter);
    }
}
