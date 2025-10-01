namespace CheckerDraft;

public class Board : IBoard
{
    private readonly ISquare[,] _squares = new ISquare[8, 8];
    public IEnumerable<ISquare> Squares
    {
        get
        {
            foreach (var square in _squares)
                if (square != null)
                    yield return square;
                else
                    continue;
        }
    }

    public ISquare GetSquare(int x, int y)
    {
        return _squares[x, y];
    }

    public Board()
    {
        for (int y = 0; y < 8; y++)

        {
            for (int x = 0; x < 8; x++)
            {
                _squares[x, y] = new Square(new Position(x, y));
            }
        }

        // -----
        // ADDITIONES
        // -----  
        InitializePieces();
    }
    ISquare IBoard.GetSquare(int x, int y)
    {
        if (x < 0 || x >= 8 || y < 0 || y >= 8)
            throw new ArgumentOutOfRangeException("Coordinates out of bounds");
        return _squares[x, y];
    }

    // -----
    // ADDITIONES
    // -----  
    private void InitializePieces()
    {
        // place black (top rows)
        for (int y = 0; y < 3; y++)
            for (int x = 0; x < 8; x++)
                if ((x + y) % 2 == 1)
                    _squares[x, y].Piece = new Piece(PieceColor.Black, PieceType.Men);

        // place white (bottom rows)
        for (int y = 5; y < 8; y++)
            for (int x = 0; x < 8; x++)
                if ((x + y) % 2 == 1)
                    _squares[x, y].Piece = new Piece(PieceColor.White, PieceType.Men);
    }

    public void PrintBoard()
    {
        Console.WriteLine("    0   1   2   3   4   5   6   7");
        for (int y = 7; y >= 0; y--)
        {
            Console.Write($" {y} ");
            for (int x = 0; x < 8; x++)
            {
                var sq = _squares[x, y];
                bool dark = (x + y) % 2 == 1;

                Console.BackgroundColor = dark ? ConsoleColor.Black : ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;

                string cell = sq.ToString().PadLeft(3).PadRight(4);

                Console.Write(cell);
            }
            Console.ResetColor();
            Console.WriteLine($" {y}");
        }
        Console.WriteLine("    0   1   2   3   4   5   6   7");
        Console.ResetColor();
    }
    public void PrintBoardCoordinate()
    {
        Console.WriteLine("     0    1    2    3    4    5    6    7");
        for (int y = 7; y >= 0; y--)
        {
            Console.Write($" {y} ");
            for (int x = 0; x < 8; x++)
            {
                bool dark = (x + y) % 2 == 1;

                Console.BackgroundColor = dark ? ConsoleColor.Black : ConsoleColor.White;
                Console.ForegroundColor = ConsoleColor.White;

                // Print as (x,y)
                string coord = $"{x},{y}";

                // Pad so all cells have same width
                Console.Write(coord.PadRight(5));
            }
            Console.ResetColor();
            Console.WriteLine($" {y}");
        }
        Console.WriteLine("     0    1    2    3    4    5    6    7");
        Console.ResetColor();
    }
    public bool IsInsideBoard(Position position)
    {
        // return x >= 0 && x < 8 && y >= 0 && y < 8;
        if (position.X >= 0 && position.X < 8 && position.Y >= 0 && position.Y < 8)
            return true;
        
        return false;
    }


}
