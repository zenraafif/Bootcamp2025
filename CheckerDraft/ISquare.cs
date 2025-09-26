namespace CheckerDraft;

    public interface ISquare
    {
        Position Position { get; }
        IPiece Piece { get; set; } // is occupied by a piece or null
        bool IsEmpty { get; }
    }
