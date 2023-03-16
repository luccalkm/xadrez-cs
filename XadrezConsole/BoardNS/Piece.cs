namespace XadrezConsole.BoardNS;
class Piece
{
    public Position Position { get; set; }
    public Color Color { get; protected set; }
    public int MoveAmount { get; protected set; }
    public Board Board { get; protected set; }

    public Piece(Color color, Board board)
    {
        Position = null;
        Color = color;
        Board = board;
        MoveAmount = 0;
    }

    public virtual char PrintPiece()
    {
        return '.';
    }
}
