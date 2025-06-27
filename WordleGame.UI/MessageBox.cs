using Avalonia;
using Avalonia.Controls;
using Avalonia.Layout;
using Avalonia.Media;
using Avalonia.Interactivity;
using System.Threading.Tasks;

namespace WordleGame.UI
{
    public static class MessageBox
    {
        public static async Task Show(Window parent, string text, string title)
        {
            var dialog = new Window
            {
                Title = title,
                Width = 300,
                Height = 150,
                WindowStartupLocation = WindowStartupLocation.CenterOwner,
                Content = new StackPanel
                {
                    Margin = new Thickness(10),
                    Spacing = 10,
                    VerticalAlignment = VerticalAlignment.Center,
                    Children =
                    {
                        new TextBlock
                        {
                            Text = text,
                            TextWrapping = TextWrapping.Wrap,
                            HorizontalAlignment = HorizontalAlignment.Center
                        },
                        new Button
                        {
                            Content = "OK",
                            Width = 80,
                            HorizontalAlignment = HorizontalAlignment.Center
                        }
                    }
                }
            };

            var button = ((dialog.Content as StackPanel)?.Children[1]) as Button;
            button!.Click += (_, _) => dialog.Close();

            await dialog.ShowDialog(parent);
        }
    }
}
