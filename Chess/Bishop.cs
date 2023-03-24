using ConsoleChess.BoardNS;
using System.Runtime.InteropServices;

namespace ConsoleChess.Chess
{
    class Bishop : Piece
    {
        public Bishop(Board board, Color color) : base(board, color)
        {

        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            int[,] directions = new int[,] {
                 { -1, -1 }      // UP-LEFT
                ,{ -1, 1 }       // UP-RIGHT
                ,{ 1, -1 }       // DOWN-LEFT
                ,{ 1, 1 }      // DOWN-RIGHT
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dirX = directions[i, 0];
                int dirY = directions[i, 1];

                pos.DefineValuesToPosition(Position.Line + dirX, Position.Column + dirY);
                while (Board.ValidateChessBounds(pos))
                {
                    if (ValidatePosition(pos))
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    if (Board.AcessPieceAt(pos) != null)
                    {
                        break;
                    }
                    pos.Line += dirX;
                    pos.Column += dirY;
                }
            }
            return mat;
        }

        public override string GetPieceName()
        {
            return " B";
        }
    }
}
