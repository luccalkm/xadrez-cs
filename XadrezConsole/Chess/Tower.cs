using ConsoleChess.BoardNS;

namespace ConsoleChess.Chess
{
    class Tower : Piece
    {
        public Tower(Board board, Color color) : base(board, color)
        {

        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0,0);

            // Check upwards
            pos.DefineValuesToPosition(Position.Line - 1, Position.Column);
            while (Board.ValidatePosition(pos))
            {
                if (CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                if (Board.AccessPiece(pos) != null)
                {
                    break;
                }
                pos.Line--;
            }

            // Check downwards
            pos.DefineValuesToPosition(Position.Line + 1, Position.Column);
            while (Board.ValidatePosition(pos))
            {
                if (CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                if (Board.AccessPiece(pos) != null)
                {
                    break;
                }
                pos.Line++;
            }

            // Check to the left
            pos.DefineValuesToPosition(Position.Line, Position.Column - 1);
            while (Board.ValidatePosition(pos))
            {
                if (CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                if (Board.AccessPiece(pos) != null)
                {
                    break;
                }
                pos.Column--;
            }

            // Check to the right
            pos.DefineValuesToPosition(Position.Line, Position.Column + 1);
            while (Board.ValidatePosition(pos))
            {
                if (CanMove(pos))
                {
                    mat[pos.Line, pos.Column] = true;
                }
                if (Board.AccessPiece(pos) != null)
                {
                    break;
                }
                pos.Column++;
            }


            return mat;
        }

        public override string PrintPiece()
        {
            return " T";
        }
    }
}
