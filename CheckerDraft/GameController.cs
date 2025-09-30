namespace CheckerDraft;

public class GameController
{
    private IBoard _board;
    private List<IPlayer> _players;
    private int _currentTurn;
    // private Stack<(ISquare From, ISquare To, IPiece? Captured)> _moveHistory = new();
    // private readonly Stack<Move> _moveHistory = new Stack<Move>();
    private readonly Stack<(ISquare From, ISquare To, IPiece? Captured, Position? CapturedPosition)> _moveHistory 
    = new Stack<(ISquare, ISquare, IPiece?, Position?)>();


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
        //topSquareBlack = {0,7}, {2,7}, {4,7}, {6,7}
        //topSquareWhite = {1,0}, {3,0}, {5,0}, {7,0}

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
                    // square.Piece.Type = PieceType.King; 
                    (square.Piece as Piece)?.PromoteToKing();
                    return true;
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
                    (square.Piece as Piece)?.PromoteToKing();
                    return true;
            }
        }

        return false;
    }
    public bool ShouldPromotes(ISquare square)
{
    if (square.IsEmpty)
        return false;

    var piece = square.Piece;

    if (piece.Color == PieceColor.Black && square.Position.Y == 7)
        return true;

    if (piece.Color == PieceColor.White && square.Position.Y == 0)
        return true;

    return false;
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

    private bool CurrentPlayerHasCapture()
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

        int dx = move.To.X - move.From.X;
        int dy = move.To.Y - move.From.Y;
        int absDx = Math.Abs(dx);
        int absDy = Math.Abs(dy);

        bool mustCapture = CurrentPlayerHasCapture();

        // Normal move
        if (absDx == 1 && absDy == 1)
        {
            if (mustCapture)
                throw new InvalidOperationException("You must capture if possible!");
            return true;
        }

        // Capture move
        if (absDx == 2 && absDy == 2)
        {
            int midX = (move.From.X + move.To.X) / 2;
            int midY = (move.From.Y + move.To.Y) / 2;
            var midSquare = _board.GetSquare(midX, midY);

            if (!midSquare.IsEmpty && midSquare.Piece!.Color != piece.Color)
                return true;

            throw new InvalidOperationException("Invalid capture: no opponent piece to jump over!");
        }

        throw new InvalidOperationException("Invalid move!");
    }

    public void ApplyMove(Move move)
    {
        var fromSquare = _board.GetSquare(move.From.X, move.From.Y);
        var toSquare = _board.GetSquare(move.To.X, move.To.Y);


        if (fromSquare.IsEmpty)
        {
            throw new InvalidOperationException("There is no piece at starting position!");
        }

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
        if (absHorizontalChange == 2 && absVerticalChange == 2)
        {

            var captured = _board.GetSquare(capturedPieceX, capturedPieceY);
            //captured is the square that should contain the opponent’s piece.

            if (!captured.IsEmpty)
            {
                move.CapturedPiece = captured.Piece;
                move.CapturedPosition = captured.Position;
                captured.Piece = null;
            }
        }

        _moveHistory.Push(
            (fromSquare,
            toSquare,
            move.CapturedPiece,
            capturedSquare.Position)
        );

        // _moveHistory.Push((fromSquare, toSquare, move.CapturedPiece, capturedSquare.Position));

        // Debugging
        Console.WriteLine("Move History");
        foreach (var moved in _moveHistory)
        {
            Console.WriteLine(
                $"From ({moved.From.Position.X},{moved.From.Position.Y}) -> " +
                $"To ({moved.To.Position.X},{moved.To.Position.Y}) " +
                $"{(moved.Captured != null ? $"Captured : {moved.Captured.Color} {moved.Captured.Type} at {move.CapturedPosition}" : "")}"
            );
        }
        int SumOfMove = _moveHistory.Count;
        Console.WriteLine($"Move applied count: {SumOfMove}");

        
        // foreach (var moved in _moveHistory)
        // {
        //     Console.WriteLine(
        //         $"From {moved.From} -> To {moved.To} " +
        //         $"{(moved.CapturedPiece != null && moved.CapturedPosition != null
        //             ? $"Captured {moved.CapturedPiece.Color} {moved.CapturedPiece.Type} at {moved.CapturedPosition}"
        //             : "No capture")}"
        //     );
        // }
        /*

        */
    }

    public bool IsDraw()
    {
        return false;
    }
    public bool IsGameOver()
    {
        return false;
    }
    public bool HasAdditionalMove()
    {
        return false;
    }



}
