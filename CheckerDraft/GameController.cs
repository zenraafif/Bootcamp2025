namespace CheckerDraft;

public class GameController
{
    public Action<ISquare, ISquare>? OnMoveApplied; // action
    private IBoard _board;
    private List<IPlayer> _players;
    private int _currentTurn;
    private bool GameIsOver;
    private bool GameIsDraw;
    // private Stack<(ISquare From, ISquare To, IPiece? Captured)> _moveHistory = new();
    // private readonly Stack<Move> _moveHistory = new Stack<Move>();
    private readonly Stack<(ISquare From, ISquare To, IPiece? Captured, Position? CapturedPosition)> _moveHistory
    = new Stack<(ISquare, ISquare, IPiece?, Position?)>();
    public int SumOfMove => _moveHistory?.Count ?? 0;


    public IPlayer CurrentPlayer
    {
        get
        {
            int index = _currentTurn % _players.Count;
            return _players[index];
        }
    }
    // public IPlayer CurrentPlayer => _players[_currentTurn % _players.Count];

    // +Action<.Player>? OnTurnChanged

    // +Action<ISquare, ISquare>? OnMoveApplied
    public GameController(IBoard board, List<IPlayer> players)
    {
        _board = board;
        _players = players;
        _currentTurn = 0;
    }
    public void StartGame()
    {
        Console.WriteLine("Game started");
        _board.PrintBoard();
    }
    public void SwitchTurn()
    {
        if (_currentTurn == 0)
        {
            _currentTurn = 1;
        }
        else
        {
            _currentTurn = 0;
        }
    }
    // public GetAllPossibleMoves(){}
    public bool ShouldPromote(ISquare square)
    {
        int pieceX = square.Position.X;
        int pieceY = square.Position.Y;

        int[,] TopSquareBlack = { { 0, 7 }, { 2, 7 }, { 4, 7 }, { 6, 7 } };
        int[,] TopSquareWhite = { { 1, 0 }, { 3, 0 }, { 5, 0 }, { 7, 0 } };

        //if  piece.Color == PieceColor.White && Square 
        if (square.Piece.Color == PieceColor.Black)
        {
            // Console.WriteLine("Black");
            // Console.WriteLine(TopSquareBlack.GetLength(0));
            for (int i = 0; i < TopSquareBlack.GetLength(0); i++)
            {
                int x = TopSquareBlack[i, 0];
                int y = TopSquareBlack[i, 1];
                // Console.WriteLine($"x= {x}, y= {y}");

                if (pieceX == x && pieceY == y)
                {
                    // square.Piece.Type = PieceType.King; 
                    (square.Piece as Piece)?.PromoteToKing();
                    return true;
                }
            }
        }
        else if (square.Piece.Color == PieceColor.White)
        {
            // Console.WriteLine("White");
            for (int i = 0; i < TopSquareWhite.GetLength(0); i++)
            {
                int x = TopSquareWhite[i, 0];
                int y = TopSquareWhite[i, 1];

                if (pieceX == x && pieceY == y)
                {   
                    (square.Piece as Piece)?.PromoteToKing();
                    return true;
                }
            }
        }

        return false;
    }
    // public void GetAllPossibleMoves(PieceColor currentColor)
    // {
    //     // Loop through all squares on the board.
    //     foreach (var sq in _board.Squares)
    //     {
    //         // Check if the square has a piece of the given player’s color.
    //         if (!sq.IsEmpty && sq.Piece!.Color == currentColor)//CurrentPlayer.Color
    //         {
    //             // For each piece, generate its legal moves:
    //             if (CanCaptureFrom(sq.Position))
    //             {
    //                 // Normal moves (diagonal step forward).
    //                 int dx = move.To.X - move.From.X;
    //                 int dy = move.To.Y - move.From.Y;
    //                 int absDx = Math.Abs(dx);
    //                 int absDy = Math.Abs(dy);

    //                 bool mustCapture = IsCurrentPlayerHasCapture();


    //                 // // Normal move
    //                 if (absDx == 1 && absDy == 1)
    //                 {

    //                 // Capture moves (jump over opponent).

    //                 }
    //             // If any capture is possible, then only captures are legal (forced capture rule).

    //         }
    //     }
    // }



    private List<(int x, int y)> GetValidMovesForPiece(ISquare square)
    {
        var moves = new List<(int x, int y)>();
        int x = square.Position.X;
        int y = square.Position.Y;
        var piece = square.Piece;

        // Movement direction (Black down, White up)
        int direction = (piece.Color == PieceColor.Black) ? 1 : -1;

        // Check simple diagonal moves
        int[,] directions = { { -1, direction }, { 1, direction } };

        for (int i = 0; i < directions.GetLength(0); i++)
        {
            int newX = x + directions[i, 0];
            int newY = y + directions[i, 1];

            if (_board.IsInside(newX, newY))
            {
                var target = _board.GetSquare(newX, newY);

                if (target.IsEmpty)
                {
                    moves.Add((newX, newY));
                }
                else if (target.Piece.Color != piece.Color) // enemy piece
                {
                    int jumpX = newX + directions[i, 0];
                    int jumpY = newY + directions[i, 1];

                    if (_board.IsInside(jumpX, jumpY) && _board.GetSquare(jumpX, jumpY).IsEmpty)
                    {
                        moves.Add((jumpX, jumpY)); // capture
                    }
                }
            }
        }

        return moves;
    }

    public Dictionary<ISquare, List<(int x, int y)>> GetMovablePieces(PieceColor color)
    {
        var result = new Dictionary<ISquare, List<(int x, int y)>>();

        foreach (var square in _board.Squares)
        {
            if (square.IsEmpty || square.Piece.Color != color)
                continue;

            var moves = GetValidMovesForPiece(square);
            if (moves.Count > 0)
                result[square] = moves;
        }

        return result;
    }




    private bool CanCaptureFrom(Position pos)
    {
        var square = _board.GetSquare(pos.X, pos.Y);
        if (square.IsEmpty) return false;

        var piece = square.Piece!;
        int dir = piece.Color == PieceColor.White ? -1 : 1;

        // directions for diagonal moves
        int[,] directions = { { 1, dir }, { -1, dir }, { 1, -dir }, { -1, -dir } };

        foreach (var d in Enumerable.Range(0, 4))
        {
            int dx = directions[d, 0];
            int dy = directions[d, 1];

            int midX = pos.X + dx;
            int midY = pos.Y + dy;
            int toX = pos.X + 2 * dx;
            int toY = pos.Y + 2 * dy;

            if (toX < 0 || toX >= 8 || toY < 0 || toY >= 8) continue;

            var midSquare = _board.GetSquare(midX, midY);
            var toSquare = _board.GetSquare(toX, toY);

            if (!midSquare.IsEmpty && midSquare.Piece!.Color != piece.Color && toSquare.IsEmpty)
                return true; // capture available
        }

        return false;
    }

    private bool IsCurrentPlayerHasCapture()
    {
        foreach (var sq in _board.Squares)
        {
            if (!sq.IsEmpty && sq.Piece!.Color == CurrentPlayer.Color)
            {
                if (CanCaptureFrom(sq.Position))
                    return true;
            }
        }
        return false;
    }


    public bool ValidateMove(Move move)
    {
        var fromSquare = _board.GetSquare(move.From.X, move.From.Y);
        var toSquare = _board.GetSquare(move.To.X, move.To.Y);

        if (fromSquare.IsEmpty)
            throw new InvalidOperationException("No piece at starting position!");

        var piece = fromSquare.Piece!;
        if (piece.Color != CurrentPlayer.Color)
            throw new InvalidOperationException("You can only move your own pieces!");
        if (!toSquare.IsEmpty)
            throw new InvalidOperationException("Target square is not empty!");

                                                // 4 5 3 4      1 2 2 3     2 3 4 5
        // int dx = move.To.X - move.From.X; ===> 3 - 4 = -1    2 - 1 = 1   2 - 4 = 2
        // int dy = move.To.Y - move.From.Y; ===> 4 - 5 = -1    3 - 2 = 1   5 - 3 = 2
        int dx = move.To.X - move.From.X;
        int dy = move.To.Y - move.From.Y;
        int absDx = Math.Abs(dx);
        int absDy = Math.Abs(dy);

        bool mustCapture = IsCurrentPlayerHasCapture();

        // // Normal move
        if (absDx == 1 && absDy == 1)
        {
            if (mustCapture)
                throw new InvalidOperationException("You must capture if possible!");

            // Forward direction for Men
            if (piece.Type != PieceType.King)
            {
                // White color move forward with (-1)
                if (piece.Color == PieceColor.White && dy != -1)
                {
                    throw new InvalidOperationException("Men only move forward - White");
                }
                // Black color mover forward with (1)
                if (piece.Color == PieceColor.Black && dy != 1)
                {
                    throw new InvalidOperationException("Men only move forward - Black");
                }
            }
            return true;
        }

        // Capture move
        if (absDx == 2 && absDy == 2)
        {
            int midX = (move.From.X + move.To.X) / 2;
            int midY = (move.From.Y + move.To.Y) / 2;
            var midSquare = _board.GetSquare(midX, midY);

            if (!midSquare.IsEmpty && midSquare.Piece!.Color != piece.Color)
            {
                // Check forward capture for man (not king)
                if (piece.Type != PieceType.King)
                {
                    if (piece.Color == PieceColor.White && dy != -2)
                        throw new InvalidOperationException("White men can only capture forward!");
                    if (piece.Color == PieceColor.Black && dy != 2)
                        throw new InvalidOperationException("Black men can only capture forward!");
                }

                return true;
            }

            throw new InvalidOperationException("Invalid capture: no opponent piece to jump over!");
        }
        //--------------------------

        throw new InvalidOperationException("Invalid move!");
    }

    public void ApplyMove(Move move)
    {
        var fromSquare = _board.GetSquare(move.From.X, move.From.Y);
        var toSquare = _board.GetSquare(move.To.X, move.To.Y);
        // Debugging
        // Console.WriteLine($"fromSquare: ({move.From.X},{move.From.Y}), " +
        //                 $"Empty={fromSquare.IsEmpty}, " +
        //                 $"Piece={(fromSquare.Piece == null ? "None" : $"{fromSquare.Piece.Color} {fromSquare.Piece.Type}")}");

        // Console.WriteLine($"toSquare: ({move.To.X},{move.To.Y}), " +
        //                 $"Empty={toSquare.IsEmpty}, " +
        //                 $"Piece={(toSquare.Piece == null ? "None" : $"{toSquare.Piece.Color} {fromSquare.Piece.Type}")}");

        if (fromSquare.IsEmpty)
        {
            throw new InvalidOperationException("There is no piece at starting position!");
        }

        // Change variable value after a move
        toSquare.Piece = fromSquare.Piece;
        fromSquare.Piece = null;

        // Debugging
        // Console.WriteLine($"FromSquare: {fromSquare?.Piece?.Color}");
        // Console.WriteLine($"ToSquare: {toSquare?.Piece?.Color}");
        ShouldPromote(toSquare);

        int horizontalChange = move.To.X - move.From.X;
        int verticalChange = move.To.Y - move.From.Y;

        int absHorizontalChange = Math.Abs(horizontalChange);
        int absVerticalChange = Math.Abs(verticalChange);

        // check if a jump is capture
        //find captured piece position
        int capturedPieceX = (move.From.X + move.To.X) / 2;
        int capturedPieceY = (move.From.Y + move.To.Y) / 2;
        var capturedSquare = _board.GetSquare(capturedPieceX, capturedPieceY);

        _moveHistory.Push(
            (fromSquare,
            toSquare,
            move.CapturedPiece,
            capturedSquare.Position)
        );

        HasPieces(CurrentPlayer.Color);
        // Debugging MOVE HISTORY
        Console.WriteLine("Move History");
        foreach (var moved in _moveHistory)
        {
            Console.WriteLine(
                $"From ({moved.From.Position.X},{moved.From.Position.Y}) -> " +
                $"To ({moved.To.Position.X},{moved.To.Position.Y}) " +
                $"{(moved.Captured != null ? $"Captured : {moved.Captured.Color} {moved.Captured.Type} at {moved.CapturedPosition}" : "")}"
            );
        }

        // CAPTURE
        if (absHorizontalChange == 2 && absVerticalChange == 2)
        {
            if (!capturedSquare.IsEmpty)
            {
                move.CapturedPiece = capturedSquare.Piece;
                move.CapturedPosition = capturedSquare.Position;
                capturedSquare.Piece = null; // remove captured piece
            }

            // Double capture check
            if (move.CapturedPiece != null && CanCaptureFrom(move.To))
            {
                Console.WriteLine("Double move available! Continue with the same piece.");
                return;
            }
        }
        
        OnMoveApplied?.Invoke(fromSquare, toSquare); // Action

        if (IsGameOver(CurrentPlayer.Color))
        {
            Console.WriteLine("GAME OVER");

            if (GameIsDraw)
            {
                Console.WriteLine("Result: DRAW!");
            }
            else
            {
                Console.WriteLine($"Winner: {GetOpponent(CurrentPlayer.Color)}");
            }

            return;
        }
        SwitchTurn();
    }
    public PieceColor GetOpponent(PieceColor color)
    {
        if (color == PieceColor.White)
            return PieceColor.Black;
        else
            return PieceColor.White;
    }

    public bool HasPieces(PieceColor currentPlayerColor)
    {
        foreach (var square in _board.Squares)
        {
            if (!square.IsEmpty)// check if this square has a piece on it 
            {
                if (square.Piece.Color == currentPlayerColor)
                {
                    // Debugging
                    // Console.WriteLine(square.Piece.Color);
                    return true;
                }
            }
        }
        return false;
    }
    public bool IsDraw()
    {
        if (SumOfMove >= 40)
        {
            GameIsDraw = true;
            return true;
        }
        else
        {
            GameIsDraw = false;
            return false;
        }
    }

    public bool IsGameOver(PieceColor currentPlayerColor)
    {
        // when IsDraw true => game is over
        if (IsDraw())
        {
            Console.WriteLine("IsGameOver = Draw");
            return true;
        }
        // when player piece empty => game is over
        if (!HasPieces(currentPlayerColor))
        {   
            Console.WriteLine("IsGameOver = Has Not Pieces");
            return true;
        }

        return false;
    }
}
