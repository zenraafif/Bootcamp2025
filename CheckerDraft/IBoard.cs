namespace CheckerDraft;

    public interface IBoard
    {
        ISquare GetSquare(int x, int y);
        IEnumerable<ISquare> Squares { get; }

        // -----
        // ADDITIONES
        // -----  
        void PrintBoard(); // PrintBoard accessible via interface IBoard
    }
    