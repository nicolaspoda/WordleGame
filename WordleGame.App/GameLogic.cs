namespace WordleGame.App;

public class GameLogic
{
    public static string CheckGuess(string guess, string target)
    {
        string[] feedback = new string[5];
        var targetChars = target.ToCharArray();
        var guessChars = guess.ToCharArray();

        bool[] matched = new bool[5];

        for (int i = 0; i < 5; i++)
        {
            if (guessChars[i] == targetChars[i])
            {
                feedback[i] = "🟩";
                matched[i] = true;
                targetChars[i] = '-';
            }
        }

        for (int i = 0; i < 5; i++)
        {
            if (feedback[i] == "🟩") continue;

            int idx = Array.FindIndex(targetChars, c => c == guessChars[i]);
            if (idx != -1)
            {
                feedback[i] = "🟨";
                targetChars[idx] = '-';
            }
            else
            {
                feedback[i] = "⬜";
            }
        }

        return string.Join("", feedback);
    }
}
