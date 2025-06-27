using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Avalonia.Media;
using Avalonia.Layout;
using System;
using System.Linq;
using WordleGame.App;


namespace WordleGame.UI.Views;

public partial class MainWindow : Window
{
    private GameState game = null!;
    private GameStats stats = new();

    public MainWindow()
    {
        InitializeComponent();

        WordLengthSelector.SelectionChanged += OnSettingsChanged;
        GameModeSelector.SelectionChanged += OnSettingsChanged;

        ResetGame();
        UpdateInstructionText();
    }

    private void OnSettingsChanged(object? sender, SelectionChangedEventArgs e)
    {
        ResetGame();
        UpdateInstructionText();
    }

    private void OnCheckClick(object? sender, RoutedEventArgs e)
    {
        if (game.IsTimedOut())
        {
            FeedbackText.Text = $"⏰ Time's up! The word was {game.TargetWord}.";
            stats.RegisterLoss(game.Mode);
            ShowMessage($"Time's up! The word was {game.TargetWord}.");
            ResetGame();
            return;
        }

        var guess = GuessInput.Text?.Trim().ToUpper();

        if (string.IsNullOrWhiteSpace(guess) || !WordValidator.ValidateWord(guess, game.WordLength))
        {
            FeedbackText.Text = $"❌ Invalid word ({game.WordLength} letters).";
            return;
        }

        if (!game.IsWordInDictionary(guess))
        {
            FeedbackText.Text = "❌ Word not in dictionary.";
            return;
        }

        var feedback = game.MakeGuess(guess);
        FeedbackText.Text = feedback;
        DisplayFeedback(feedback);
        UpdateAttempts();

        if (game.HasWon)
        {
            FeedbackText.Text = "🎉 Congratulations! You found the word!";
            stats.RegisterWin(game.CurrentAttempt + 1, game.Mode);
            ShowMessage($"🎉 You found the word {game.TargetWord}!");
            ResetGame();
        }
        else if (game.IsGameOver || game.IsTimedOut())
        {
            FeedbackText.Text = $"❌ Game Over. The word was {game.TargetWord}.";
            stats.RegisterLoss(game.Mode);
            ShowMessage($"Game Over. The word was {game.TargetWord}.");
            ResetGame();
        }

        GuessInput.Text = string.Empty;
    }

    private void DisplayFeedback(string feedback)
    {
        FeedbackPanel.Children.Clear();

        foreach (char c in feedback)
        {
            var border = new Border
            {
                Width = 40,
                Height = 40,
                BorderBrush = Brushes.White,
                BorderThickness = new Thickness(2),
                Margin = new Thickness(2),
                Child = new TextBlock
                {
                    Text = c.ToString(),
                    Foreground = Brushes.White,
                    FontSize = 20,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center
                }
            };

            border.Background = c.ToString() switch
            {
                "🟩" => Brushes.Green,
                "🟨" => Brushes.Gold,
                "⬜" => Brushes.Gray,
                _ => Brushes.Transparent
            };


            FeedbackPanel.Children.Add(border);
        }
    }


    private async void ShowMessage(string message)
    {
        await MessageBox.Show(this, message, "Wordle");
    }

    private void ResetGame()
    {
        try
        {
            game = new GameState(GetSelectedWordLength(), GetSelectedGameMode());
            FeedbackText.Text = string.Empty;
            GuessInput.Text = string.Empty;
            FeedbackPanel.Children.Clear();
            UpdateAttempts();
        }
        catch (Exception ex)
        {
            FeedbackText.Text = ex.Message;
        }
    }

    private void OnRestartStatsClick(object? sender, RoutedEventArgs e)
    {
        stats.Reset();
        ResetGame();
    }

    private GameMode GetSelectedGameMode()
    {
        var selected = (GameModeSelector.SelectedItem as ComboBoxItem)?.Content?.ToString();
        return selected switch
        {
            "Timed" => GameMode.Timed,
            "Practice" => GameMode.Practice,
            _ => GameMode.Normal
        };
    }

    private int GetSelectedWordLength()
    {
        var selected = (WordLengthSelector.SelectedItem as ComboBoxItem)?.Content?.ToString();
        return int.TryParse(selected, out int length) ? length : 5;
    }

    private void UpdateInstructionText()
    {
        InstructionText.Text = $"Enter a {GetSelectedWordLength()}-letter word:";
    }

    private void UpdateAttempts()
    {
        string timer = game.Mode == GameMode.Timed
            ? $" | Time Left: {Math.Max(0, 60 - (int)(DateTime.Now - game.StartTime).TotalSeconds)}s"
            : "";

        AttemptsText.Text = $"Attempt {game.CurrentAttempt + 1}/{game.MaxAttempts} | Wins: {stats.Wins} | Losses: {stats.Losses} | Streak: {stats.CurrentStreak} | Avg: {stats.AverageAttempts():0.0} | Score: {stats.TotalScore}{timer}";
    }
}
