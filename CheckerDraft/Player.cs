namespace CheckerDraft;

    public class Player : IPlayer
    {
        public string Name { get; private set; }
        public PieceColor Color { get; private set; }
        // public IEnumerable<IPiece> Pieces { get; private set; }
        public Player(string name, PieceColor color)
        {
            Name = name;
            Color = color;
            // Pieces = new List<IPiece>();
        }
    }
