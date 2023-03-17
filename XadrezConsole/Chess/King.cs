using ConsoleChess.BoardNS;
using System.Runtime.InteropServices;

namespace ConsoleChess.Chess
{
    class King : Piece
    {
        public King(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            for (int i = -1; i <= 1; i++)
            {
                for (int j = -1; j <= 1; j++)
                {
                    pos.DefineValuesToPosition(Position.Line + i, Position.Column + j);
                    if (Board.ValidateChessBounds(pos) && ValidatePosition(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                }
            }
            return mat;
        }

        public override string PrintPiece()
        {
            return " R";
        }
    }
}
