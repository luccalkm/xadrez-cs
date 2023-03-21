using System.Linq;

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

    public void DescreaseMoveAmount()
    {
        MoveAmount--;
    }

    protected bool ValidatePosition(Position desiredPosition)
    {
        Piece p = Board.AcessPieceAt(desiredPosition);
        return p == null || p.Color != Color;
    }

    public abstract bool[,] PossibleMovements();

    public bool CanMove()
    {
        return PossibleMovements().Cast<bool>().Any(x => x);
    }

    public bool CanMoveTo(Position destination)
    {
        Board.CheckPosition(destination);
        return PossibleMovements()[destination.Line, destination.Column];
    }

    public virtual string GetPieceName()
    {
        return ". ";
    }
}
