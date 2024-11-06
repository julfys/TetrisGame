public class Tetromino
{
    public int[,] Shape { get; private set; }
    public int X { get; set; }
    public int Y { get; set; }

    public Tetromino(int[,] shape)
    {
        Shape = shape;
        X = 0;
        Y = 0;
    }

    public static Tetromino CreateLShape() => new Tetromino(new int[,] { { 0, 1, 0 }, { 0, 1, 0 }, { 0, 1, 1 } });
    public static Tetromino CreateJShape() => new Tetromino(new int[,] { { 0, 0, 1 }, { 0, 0, 1 }, { 0, 1, 1 } });
    public static Tetromino CreateOShape() => new Tetromino(new int[,] { { 1, 1 }, { 1, 1 } });
    public static Tetromino CreateIShape() => new Tetromino(new int[,] { { 1 }, { 1 }, { 1 }, { 1 } });
    public static Tetromino CreateTShape() => new Tetromino(new int[,] { { 0, 1, 0 }, { 1, 1, 1 } });
    public static Tetromino CreateZShape() => new Tetromino(new int[,] { { 1, 1, 0 }, { 0, 1, 1 } });
    public static Tetromino CreateSShape() => new Tetromino(new int[,] { { 0, 1, 1 }, { 1, 1, 0 } });

    public void Rotate()
    {
        int rows = Shape.GetLength(0);
        int cols = Shape.GetLength(1);
        int[,] rotated = new int[cols, rows];

        for (int y = 0; y < rows; y++)
            for (int x = 0; x < cols; x++)
                rotated[x, rows - 1 - y] = Shape[y, x];

        Shape = rotated;
    }
}
