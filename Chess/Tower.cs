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

            int[,] directions = new int[,] { 
                 { -1, 0 }      // UP
                ,{ 1, 0 }       // DOWN
                ,{ 0, 1 }       // RIGHT
                ,{ 0, -1 }      // LEFT
            };
            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dirX = directions[i,0];
                int dirY = directions[i,1];

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
            return " T";
        }
    }
}
