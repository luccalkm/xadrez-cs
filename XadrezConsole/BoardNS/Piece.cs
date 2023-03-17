using System.Drawing;

namespace ConsoleChess.BoardNS;
abstract class Piece
{
    public Position Position { get; set; }
    public Color Color { get; protected set; }
    public int MoveAmount { get; protected set; }
    public Board Board { get; protected set; }

    public Piece(Board board, Color color)
    {
        Position = null;
        Color = color;
        Board = board;
        MoveAmount = 0;
    }

    public void IncrementMoveAmount()
    {
        MoveAmount++;
    }

    protected bool CanMove(Position desiredPosition)
    {
        Piece p = Board.AccessPiece(desiredPosition);
        return p == null || p.Color != Color;
    }

    public abstract bool[,] PossibleMovements();

    public virtual string PrintPiece()
    {
        return ". ";
    }
}
