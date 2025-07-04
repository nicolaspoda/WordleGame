using System;
using System.Diagnostics.CodeAnalysis;
namespace WordleGame.App;
[ExcludeFromCodeCoverage]
class Program
{
    static void Main()
    {
        var game = new GameState();
        Console.WriteLine("Welcome to Wordle!");

        while (!game.IsGameOver)
        {
            Console.Write($"Attempt {game.CurrentAttempt + 1}/6: ");
            var input = Console.ReadLine();
            if (string.IsNullOrWhiteSpace(input))
            {
                Console.WriteLine("Invalid input.");
                continue;
            }
            input = input.ToUpper();

            if (!WordValidator.ValidateWord(input, 5))
            {
                Console.WriteLine("Invalid word. It must be 5 letters.");
                continue;
            }

            if (!game.IsWordInDictionary(input))
            {
                Console.WriteLine("Word not in dictionary.");
                continue;
            }

            var feedback = game.MakeGuess(input);
            Console.WriteLine(feedback);

            if (game.HasWon)
            {
                Console.WriteLine("Congratulations! You found the word!");
                break;
            }
        }

        if (!game.HasWon)
        {
            Console.WriteLine($"Game over. The word was: {game.TargetWord}");
        }
    }
}
