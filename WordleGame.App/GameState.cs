namespace WordleGame.App;

public class GameState
{
    public string TargetWord { get; }
    public int MaxAttempts { get; } = 6;
    public int CurrentAttempt { get; private set; }
    public bool HasWon { get; private set; }
    public bool IsGameOver => HasWon || CurrentAttempt >= MaxAttempts;
    private readonly List<string> Dictionary;

    public GameState()
    {
        var path = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dictionary.txt");
        Dictionary = File.ReadAllLines(path).Select(w => w.ToUpper()).ToList();
        var rnd = new Random();
        TargetWord = Dictionary[rnd.Next(Dictionary.Count)];
    }

    public GameState(string forcedWord)
    {
        Dictionary = File.ReadAllLines(Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Dictionary.txt"))
                         .Select(w => w.ToUpper()).ToList();
        TargetWord = forcedWord.ToUpper();
    }




    public bool IsWordInDictionary(string word)
    {
        return Dictionary.Contains(word.ToUpper());
    }

    public string MakeGuess(string guess)
    {
        CurrentAttempt++;

        var feedback = GameLogic.CheckGuess(guess.ToUpper(), TargetWord);

        if (guess.ToUpper() == TargetWord)
        {
            HasWon = true;
        }

        return feedback;
    }
}
