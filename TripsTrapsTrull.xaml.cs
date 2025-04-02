namespace MauiSandakov;

public partial class Trips : ContentPage
{
    private Button[,] gridButtons = new Button[3, 3];
    private bool isPlayerOneTurn = true;
    private string playerOneSymbol = "X";
    private string playerTwoSymbol = "O";
    private int moveCount = 0;
    private Grid gameGrid;
    private StackLayout controlButtons;
    private bool isPlayingWithBot = false;
    private bool isDarkTheme = false;

    public Trips()
    {
        CreateGameGrid();
        controlButtons = CreateControlButtons();
        UpdateTheme();

        Content = new VerticalStackLayout
        {
            Children = { gameGrid, controlButtons },
            Spacing = 20,
            Padding = new Thickness(10)
        };
    }

    private void CreateGameGrid()
    {
        gameGrid = new Grid();

        for (int i = 0; i < 3; i++)
        {
            gameGrid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            gameGrid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var button = new Button
                {
                    BackgroundColor = Colors.LightGray,
                    FontSize = 24,
                    TextColor = Colors.Black,
                    Text = " "
                };
                int r = row, c = col;
                button.Clicked += (sender, e) => OnCellClicked(r, c);

                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                gameGrid.Children.Add(button);
                gridButtons[row, col] = button;
            }
        }
    }

    private StackLayout CreateControlButtons()
    {
        var startNewGameButton = new Button { Text = "Uus mäng", BackgroundColor = Colors.Green, TextColor = Colors.White };
        startNewGameButton.Clicked += (sender, e) => ResetGame();

        var changeSymbolsButton = new Button { Text = "Muutke tähemärki", BackgroundColor = Colors.Yellow, TextColor = Colors.Black };
        changeSymbolsButton.Clicked += (sender, e) => SwapSymbols();

        var playWithBotButton = new Button { Text = "Mängi koos botiga / sõbraga", BackgroundColor = Colors.Blue, TextColor = Colors.White };
        playWithBotButton.Clicked += (sender, e) => ToggleBotMode();

        var themeButton = new Button { Text = "Muuda teemat", BackgroundColor = Colors.Purple, TextColor = Colors.White };
        themeButton.Clicked += (sender, e) => ToggleTheme();

        return new StackLayout
        {
            Children = { startNewGameButton, changeSymbolsButton, playWithBotButton, themeButton },
            Spacing = 10
        };
    }

    private void OnCellClicked(int row, int col)
    {
        if (gridButtons[row, col].Text.Trim() != "") return;

        gridButtons[row, col].Text = isPlayerOneTurn ? playerOneSymbol : playerTwoSymbol;
        isPlayerOneTurn = !isPlayerOneTurn;
        moveCount++;

        CheckGameStatus();

        if (isPlayingWithBot && !isPlayerOneTurn)
        {
            BotMove();
        }
    }

    private void BotMove()
    {
        Random rnd = new Random();
        while (true)
        {
            int row = rnd.Next(0, 3);
            int col = rnd.Next(0, 3);
            if (gridButtons[row, col].Text.Trim() == "")
            {
                gridButtons[row, col].Text = playerTwoSymbol;
                isPlayerOneTurn = true;
                moveCount++;
                CheckGameStatus();
                break;
            }
        }
    }

    private void CheckGameStatus()
    {
        for (int i = 0; i < 3; i++)
        {
            if (gridButtons[i, 0].Text.Trim() != "" &&
                gridButtons[i, 0].Text == gridButtons[i, 1].Text &&
                gridButtons[i, 1].Text == gridButtons[i, 2].Text)
            {
                ShowGameResult(gridButtons[i, 0].Text);
                return;
            }

            if (gridButtons[0, i].Text.Trim() != "" &&
                gridButtons[0, i].Text == gridButtons[1, i].Text &&
                gridButtons[1, i].Text == gridButtons[2, i].Text)
            {
                ShowGameResult(gridButtons[0, i].Text);
                return;
            }
        }

        if (gridButtons[0, 0].Text.Trim() != "" &&
            gridButtons[0, 0].Text == gridButtons[1, 1].Text &&
            gridButtons[1, 1].Text == gridButtons[2, 2].Text)
        {
            ShowGameResult(gridButtons[0, 0].Text);
            return;
        }

        if (gridButtons[0, 2].Text.Trim() != "" &&
            gridButtons[0, 2].Text == gridButtons[1, 1].Text &&
            gridButtons[1, 1].Text == gridButtons[2, 0].Text)
        {
            ShowGameResult(gridButtons[0, 2].Text);
            return;
        }

        if (moveCount == 9)
        {
            ShowGameResult("Tie");
        }
    }

    private async void ShowGameResult(string winner)
    {
        string message = winner == "Tie" ? "Tie! Kas sa tahad uuesti mängida?" : $"{winner} võitis! Kas sa tahad uuesti mängida?";
        bool playAgain = await DisplayAlert("Mäng läbi", message, "Jah", "Ei");
        if (playAgain) ResetGame();
    }

    private void ResetGame()
    {
        moveCount = 0;
        isPlayerOneTurn = true;
        foreach (var button in gridButtons) button.Text = " ";
    }

    private void SwapSymbols()
    {
        (playerOneSymbol, playerTwoSymbol) = (playerTwoSymbol, playerOneSymbol);
        ResetGame();
    }

    private void ToggleBotMode()
    {
        isPlayingWithBot = !isPlayingWithBot;
        ResetGame();
    }

    private void ToggleTheme()
    {
        isDarkTheme = !isDarkTheme;
        UpdateTheme();
    }

    private void UpdateTheme()
    {
        BackgroundColor = isDarkTheme ? Colors.Black : Colors.White;
        foreach (var button in gridButtons)
        {
            button.BackgroundColor = isDarkTheme ? Colors.DarkGray : Colors.LightGray;
            button.TextColor = isDarkTheme ? Colors.White : Colors.Black;
        }
    }
}
