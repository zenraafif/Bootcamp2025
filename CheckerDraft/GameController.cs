namespace CheckerDraft;


    public class GameController
    {
        private IBoard _board;
        private List<IPlayer> _players;
        private int _currentTurn;
        private Stack<(ISquare From, ISquare To, IPiece? Captured)> _moveHistory = new();
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

        // 1. Starting square must contain a piece
        if (fromSquare.IsEmpty)
            throw new InvalidOperationException("No piece at starting position!");

        var piece = fromSquare.Piece!;

        // 2. Must belong to current player
        if (piece.Color != CurrentPlayer.Color)
            throw new InvalidOperationException("You can only move your own pieces!");

        // 3. Destination must be empty
        if (!toSquare.IsEmpty)
            throw new InvalidOperationException("Target square is not empty!");

        // 4. Movement direction (for Men)
        int dx = move.To.X - move.From.X;
        int dy = move.To.Y - move.From.Y;
        int absDx = Math.Abs(dx);
        int absDy = Math.Abs(dy);

        bool mustCapture = CurrentPlayerHasCapture();

        if (piece.Type == PieceType.Men)
        {
            // Normal move (1 diagonal forward)
            if (absDx == 1 && absDy == 1)
            {
                if (mustCapture)
                    throw new InvalidOperationException("You must capture if possible!");
                return true;
            }

            throw new InvalidOperationException("Invalid move for Men!");

            // --- Capture move ---
            if (absDx == 2 && absDy == 2)
            {
                int midX = (move.From.X + move.To.X) / 2;
                int midY = (move.From.Y + move.To.Y) / 2;
                var midSquare = _board.GetSquare(midX, midY);

                if (!midSquare.IsEmpty && midSquare.Piece!.Color != piece.Color)
                    return true;

                throw new InvalidOperationException("Invalid capture: no opponent piece to jump over!");
            }
        }

        return true;
    }



        public void ApplyMove(Move move)
        {
            //Heres
            // var fromSquare = _board.GetSquare(move.From.X, move.To.Y);
            // var toSquare = _board.GetSquare(move.From.X, move.To.Y);
            var fromSquare = _board.GetSquare(move.From.X, move.From.Y);
            var toSquare   = _board.GetSquare(move.To.X, move.To.Y);


        if (fromSquare.IsEmpty)
        {
            throw new InvalidOperationException("There is no piece at starting position!");
        }

            toSquare.Piece = fromSquare.Piece;
            fromSquare.Piece = null;

            int horizontalChange = move.To.X - move.From.X;
            int verticalChange = move.To.Y - move.From.Y;

            int absHorizontalChange = Math.Abs(horizontalChange);
            int absVerticalChange = Math.Abs(verticalChange);

            // check if a jump is capture (simplified version)
            if (absHorizontalChange == 2 && absVerticalChange == 2)
            {
                //find captured piece position
                // int capturedPieceX = (move.From.X - move.To.X); //Taking the average (from + to) / 2 = gives that midpoint
                // int capturedPieceY = (move.From.Y - move.To.Y);
                int capturedPieceX = (move.From.X + move.To.X) / 2;
                int capturedPieceY = (move.From.Y + move.To.Y) / 2;


                var captured = _board.GetSquare(capturedPieceX, capturedPieceY);
                //captured is the square that should contain the opponent’s piece.

                if (!captured.IsEmpty)
                {
                    move.CapturedPiece = captured.Piece;
                    move.CapturedPosition = captured.Position;
                    captured.Piece = null;
                }
            }



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






// public bool ValidateMove(Move move)
// {
//     var fromSquare = _board.GetSquare(move.From.X, move.From.Y);
//     var toSquare   = _board.GetSquare(move.To.X, move.To.Y);

//     if (fromSquare.IsEmpty)
//         throw new InvalidOperationException("No piece at starting position!");

//     var piece = fromSquare.Piece!;
//     if (piece.Color != CurrentPlayer.Color)
//         throw new InvalidOperationException("You can only move your own pieces!");
//     if (!toSquare.IsEmpty)
//         throw new InvalidOperationException("Target square is not empty!");

//     int dx = move.To.X - move.From.X;
//     int dy = move.To.Y - move.From.Y;
//     int absDx = Math.Abs(dx);
//     int absDy = Math.Abs(dy);

//     bool mustCapture = CurrentPlayerHasCapture();

    // --- Normal move ---
    // if (absDx == 1 && absDy == 1)
    // {
    //     if (mustCapture)
    //         throw new InvalidOperationException("You must capture if possible!");
    //     return true;
    // }

    // // --- Capture move ---
    // if (absDx == 2 && absDy == 2)
    // {
    //     int midX = (move.From.X + move.To.X) / 2;
    //     int midY = (move.From.Y + move.To.Y) / 2;
    //     var midSquare = _board.GetSquare(midX, midY);

    //     if (!midSquare.IsEmpty && midSquare.Piece!.Color != piece.Color)
    //         return true;

    //     throw new InvalidOperationException("Invalid capture: no opponent piece to jump over!");
    // }

//     throw new InvalidOperationException("Invalid move!");
// }