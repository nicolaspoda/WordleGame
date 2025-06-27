using Avalonia.Controls;
using Avalonia.Interactivity;
using WordleGame.App;

namespace WordleGame.UI.Views;

public partial class MainWindow : Window
{
    private GameState game;
    private GameStats stats = new();

    public MainWindow()
    {
        InitializeComponent();
        game = new GameState();
        UpdateAttempts();
    }

    private void OnCheckClick(object? sender, RoutedEventArgs e)
    {
        var guess = GuessInput.Text?.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(guess) || !WordValidator.ValidateWord(guess))
        {
            FeedbackText.Text = "Invalid word (5 letters).";
            return;
        }

        if (!game.IsWordInDictionary(guess))
        {
            FeedbackText.Text = "Word not in dictionary.";
            return;
        }

        var feedback = game.MakeGuess(guess);
        FeedbackText.Text = feedback;
        UpdateAttempts();

        if (game.HasWon)
        {
            FeedbackText.Text = "Congratulations! You found the word!";
            stats.RegisterWin(game.CurrentAttempt + 1);
            ShowMessage($"Congratulations! You found the word {game.TargetWord}!");
            ResetGame();
        }
        else if (game.IsGameOver)
        {
            FeedbackText.Text = $"Game Over. The word was {game.TargetWord}.";
            stats.RegisterLoss();
            ShowMessage($"Game Over. The word was {game.TargetWord}.");
            ResetGame();
        }


        GuessInput.Text = string.Empty;
    }

    private void UpdateAttempts()
    {
        AttemptsText.Text = $"Attempt {game.CurrentAttempt + 1}/6 | Wins: {stats.Wins} | Losses: {stats.Losses} | Streak: {stats.CurrentStreak} | Avg: {stats.AverageAttempts():0.0}";
    }


    private async void ShowMessage(string message)
    {
        await MessageBox.Show(this, message, "Wordle");
    }

    private void ResetGame()
    {
        game = new GameState();
        UpdateAttempts();
        FeedbackText.Text = string.Empty;
    }

    private void OnRestartStatsClick(object? sender, RoutedEventArgs e)
    {
        stats.Reset();
        ResetGame();
    }

}
