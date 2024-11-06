using System;

public class GameField
{
    public int[,] Field { get; private set; }
    public int Width { get; private set; }
    public int Height { get; private set; }
    public int Score { get; private set; } = 0; // Счет игрока

    public GameField(int width, int height)
    {
        Width = width;
        Height = height;
        Field = new int[height, width];
    }

    public bool CheckCollision(Tetromino tetromino, int newX, int newY)
    {
        for (int y = 0; y < tetromino.Shape.GetLength(0); y++)
        {
            for (int x = 0; x < tetromino.Shape.GetLength(1); x++)
            {
                if (tetromino.Shape[y, x] == 1)
                {
                    int checkX = newX + x;
                    int checkY = newY + y;

                    if (checkX < 0 || checkX >= Width || checkY >= Height)
                        return true;

                    if (checkY >= 0 && Field[checkY, checkX] == 1)
                        return true;
                }
            }
        }
        return false;
    }

    public void PlaceTetromino(Tetromino tetromino)
    {
        for (int y = 0; y < tetromino.Shape.GetLength(0); y++)
            for (int x = 0; x < tetromino.Shape.GetLength(1); x++)
                if (tetromino.Shape[y, x] == 1 && tetromino.Y + y >= 0)
                    Field[tetromino.Y + y, tetromino.X + x] = 1;
    }

    public void ClearLines()
    {
        int linesCleared = 0;
        for (int y = Height - 1; y >= 0; y--)
        {
            bool isFullLine = true;
            for (int x = 0; x < Width; x++)
            {
                if (Field[y, x] == 0)
                {
                    isFullLine = false;
                    break;
                }
            }

            if (isFullLine)
            {
                linesCleared++;
                for (int row = y; row > 0; row--)
                    for (int x = 0; x < Width; x++)
                        Field[row, x] = Field[row - 1, x];
                for (int x = 0; x < Width; x++)
                    Field[0, x] = 0;
                y++;
            }
        }
        Score += linesCleared * 100; // Добавляем очки за каждую очищенную линию
    }

    public void Render(Tetromino tetromino)
    {
        Console.Clear();
        Console.WriteLine($"Score: {Score}"); // Печать счета

        // Верхняя граница
        Console.WriteLine(new string('═', Width + 2));

        // Игровое поле и границы
        for (int y = 0; y < Height; y++)
        {
            Console.Write("║");
            for (int x = 0; x < Width; x++)
            {
                if (IsTetrominoBlock(tetromino, x, y))
                    Console.Write("█");
                else
                    Console.Write(Field[y, x] == 1 ? "█" : " ");
            }
            Console.WriteLine("║");
        }

        // Нижняя граница
        Console.WriteLine(new string('═', Width + 2));
    }

    private bool IsTetrominoBlock(Tetromino tetromino, int x, int y)
    {
        int relativeX = x - tetromino.X;
        int relativeY = y - tetromino.Y;
        return relativeX >= 0 && relativeX < tetromino.Shape.GetLength(1) &&
               relativeY >= 0 && relativeY < tetromino.Shape.GetLength(0) &&
               tetromino.Shape[relativeY, relativeX] == 1;
    }
}
