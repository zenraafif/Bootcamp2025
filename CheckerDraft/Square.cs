namespace CheckerDraft;

    public class Square : ISquare
    {
        public Position Position { get; private set; }
        public IPiece Piece { get; set; }

        public bool IsEmpty
        {
            get
            {
                return Piece == null;
            }
        }

        public Square(Position position, IPiece piece = null)
        {
            Position = position;
            Piece = piece;
        }
        

        public override string ToString()
        {
            if (Piece == null)
                return "   "; // empty square

            // Print Piece
            string symbol = Piece.Type == PieceType.Men ? "M" : "K";
            return Piece.Color == PieceColor.White ? $" W{symbol}" : $" B{symbol}";
        }
    }
    