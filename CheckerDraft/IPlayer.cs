namespace CheckerDraft;

    public interface IPlayer
    {
        string Name { get; }
        PieceColor Color { get; }
        // IEnumerable<IPiece> Pieces { get; }
    }

