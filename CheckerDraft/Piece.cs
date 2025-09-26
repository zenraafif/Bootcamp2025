namespace CheckerDraft;

    public class Piece : IPiece
    {
        public PieceColor Color { get; private set; }
        public PieceType Type { get; private set; }
        public Piece(PieceColor color, PieceType type)
        {
            Color = color;
            Type = type;
        }
    }
