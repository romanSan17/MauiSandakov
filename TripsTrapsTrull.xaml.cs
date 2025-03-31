namespace MauiSandakov;

public partial class Trips : ContentPage
{
    private Button[,] gridButtons = new Button[3, 3]; // Игровое поле 3x3
    private bool isPlayerOneTurn = true; // Кто ходит первым
    private string playerOneSymbol = "X"; // Символ игрока 1
    private string playerTwoSymbol = "O"; // Символ игрока 2
    private int moveCount = 0; // Счётчик ходов

    public Trips()
    {
        
        CreateGameGrid();
        
    }

    // Создание игрового поля
    private void CreateGameGrid()
    {
        var grid = new Grid();

        // Создаем 3 строки и 3 столбца
        for (int i = 0; i < 3; i++)
        {
            grid.RowDefinitions.Add(new RowDefinition { Height = new GridLength(1, GridUnitType.Star) });
            grid.ColumnDefinitions.Add(new ColumnDefinition { Width = new GridLength(1, GridUnitType.Star) });
        }

        // Добавление кнопок в сетку
        for (int row = 0; row < 3; row++)
        {
            for (int col = 0; col < 3; col++)
            {
                var button = new Button
                {
                    BackgroundColor = Colors.LightGray,
                    FontSize = 24,
                    TextColor = Colors.Black
                };
                button.Clicked += (sender, e) => OnCellClicked(row, col);

                // Указываем строки и столбцы для каждой кнопки
                Grid.SetRow(button, row);
                Grid.SetColumn(button, col);

                grid.Children.Add(button);
                gridButtons[row, col] = button;
            }
        }

        // Устанавливаем сетку в качестве контента
        Content = grid;
    }

    // Создание кнопок управления игрой
    private StackLayout CreateControlButtons()
    {
        var startNewGameButton = new Button
        {
            Text = "Новая игра",
            BackgroundColor = Colors.Green,
            TextColor = Colors.White
        };
        startNewGameButton.Clicked += (sender, e) => ResetGame();

        var changeSymbolsButton = new Button
        {
            Text = "Сменить символы",
            BackgroundColor = Colors.Yellow,
            TextColor = Colors.Black
        };
        changeSymbolsButton.Clicked += (sender, e) => SwapSymbols();

        return new StackLayout
        {
            Children = { startNewGameButton, changeSymbolsButton },
            Padding = new Thickness(10),
            Spacing = 10
        };
    }

    // Обработчик клика по ячейке
    private void OnCellClicked(int row, int col)
    {
        if (gridButtons[row, col].Text != "") return; // Если ячейка уже занята

        // Отображаем символ игрока в ячейке
        gridButtons[row, col].Text = isPlayerOneTurn ? playerOneSymbol : playerTwoSymbol;

        // Переходим к следующему ходу
        isPlayerOneTurn = !isPlayerOneTurn;
        moveCount++;

        CheckGameStatus();
    }

    // Проверка состояния игры
    private void CheckGameStatus()
    {
        // Проверка на победу по строкам, столбцам и диагоналям
        for (int i = 0; i < 3; i++)
        {
            if (gridButtons[i, 0].Text != "" &&
                gridButtons[i, 0].Text == gridButtons[i, 1].Text &&
                gridButtons[i, 1].Text == gridButtons[i, 2].Text)
            {
                ShowGameResult(gridButtons[i, 0].Text);
                return;
            }

            if (gridButtons[0, i].Text != "" &&
                gridButtons[0, i].Text == gridButtons[1, i].Text &&
                gridButtons[1, i].Text == gridButtons[2, i].Text)
            {
                ShowGameResult(gridButtons[0, i].Text);
                return;
            }
        }

        // Проверка на победу по диагоналям
        if (gridButtons[0, 0].Text != "" &&
            gridButtons[0, 0].Text == gridButtons[1, 1].Text &&
            gridButtons[1, 1].Text == gridButtons[2, 2].Text)
        {
            ShowGameResult(gridButtons[0, 0].Text);
            return;
        }

        if (gridButtons[0, 2].Text != "" &&
            gridButtons[0, 2].Text == gridButtons[1, 1].Text &&
            gridButtons[1, 1].Text == gridButtons[2, 0].Text)
        {
            ShowGameResult(gridButtons[0, 2].Text);
            return;
        }

        // Если все клетки заняты и нет победителя — ничья
        if (moveCount == 9)
        {
            ShowGameResult("Ничья");
        }
    }

    // Показать результат игры
    private async void ShowGameResult(string winner)
    {
        string message = winner == "Ничья" ? "Ничья! Хотите сыграть снова?" : $"{winner} победил! Хотите сыграть снова?";
        bool playAgain = await DisplayAlert("Игра завершена", message, "Да", "Нет");

        if (playAgain)
        {
            ResetGame();
        }
        else
        {
            // Используем PopAsync() для закрытия текущей страницы и возврата на предыдущую
            await Navigation.PopAsync();
        }
    }

    // Сбросить игру
    private void ResetGame()
    {
        moveCount = 0;
        isPlayerOneTurn = true;

        foreach (var button in gridButtons)
        {
            button.Text = "";
        }
    }

    // Сменить символы игроков
    private void SwapSymbols()
    {
        var temp = playerOneSymbol;
        playerOneSymbol = playerTwoSymbol;
        playerTwoSymbol = temp;

        ResetGame();
    }
}