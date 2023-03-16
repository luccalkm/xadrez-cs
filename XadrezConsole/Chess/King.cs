using ConsoleChess.BoardNS;

namespace ConsoleChess.Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override string PrintPiece()
        {
            return " R";
        }
    }
}
