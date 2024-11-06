using System;
using System.Threading;

public class Game
{
    private GameField gameField;
    private Tetromino currentTetromino;
    private bool gameOver;

    public Game(int width, int height)
    {
        gameField = new GameField(width, height);
        gameOver = false;
    }

    private Tetromino GetRandomTetromino()
    {
        Random rand = new Random();
        return rand.Next(7) switch
        {
            0 => Tetromino.CreateLShape(),
            1 => Tetromino.CreateJShape(),
            2 => Tetromino.CreateOShape(),
            3 => Tetromino.CreateIShape(),
            4 => Tetromino.CreateTShape(),
            5 => Tetromino.CreateZShape(),
            _ => Tetromino.CreateSShape()
        };
    }

    public void Start()
    {
        currentTetromino = GetRandomTetromino();
        currentTetromino.X = gameField.Width / 2;
        currentTetromino.Y = -1;

        while (!gameOver)
        {
            Thread.Sleep(500);
            Update();
            gameField.Render(currentTetromino);
            HandleInput();
        }

        Console.WriteLine("Game Over! Press 'R' to restart or any other key to exit.");
        if (Console.ReadKey(true).Key == ConsoleKey.R)
        {
            gameField = new GameField(gameField.Width, gameField.Height);
            gameOver = false;
            Start();
        }
    }

    private void Update()
    {
        if (!gameField.CheckCollision(currentTetromino, currentTetromino.X, currentTetromino.Y + 1))
        {
            currentTetromino.Y++;
        }
        else
        {
            gameField.PlaceTetromino(currentTetromino);
            gameField.ClearLines();
            currentTetromino = GetRandomTetromino();
            currentTetromino.X = gameField.Width / 2;
            currentTetromino.Y = -1;

            if (gameField.CheckCollision(currentTetromino, currentTetromino.X, currentTetromino.Y))
                gameOver = true;
        }
    }

    private void HandleInput()
    {
        if (Console.KeyAvailable)
        {
            var key = Console.ReadKey(true).Key;

            switch (key)
            {
                case ConsoleKey.LeftArrow:
                    if (!gameField.CheckCollision(currentTetromino, currentTetromino.X - 1, currentTetromino.Y))
                        currentTetromino.X--;
                    break;
                case ConsoleKey.RightArrow:
                    if (!gameField.CheckCollision(currentTetromino, currentTetromino.X + 1, currentTetromino.Y))
                        currentTetromino.X++;
                    break;
                case ConsoleKey.UpArrow:
                    currentTetromino.Rotate();
                    if (gameField.CheckCollision(currentTetromino, currentTetromino.X, currentTetromino.Y))
                        currentTetromino.Rotate(); // Undo rotation if it collides
                    break;
                case ConsoleKey.DownArrow:
                    if (!gameField.CheckCollision(currentTetromino, currentTetromino.X, currentTetromino.Y + 1))
                        currentTetromino.Y++;
                    break;
            }
        }
    }
}
