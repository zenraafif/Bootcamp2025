namespace CheckerDraft;

public class Move
{
    public Position From { get; set; }
    public Position To { get; set; }
    public IPiece? CapturedPiece { get; set; }
    public Position? CapturedPosition { get; set; }

    public Move(Position from, Position to)
    {
        From = from;
        To = to;
    }
}
