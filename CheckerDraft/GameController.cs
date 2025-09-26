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

        public bool ValidateMove()
        {
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
