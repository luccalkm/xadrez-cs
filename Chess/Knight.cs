using ConsoleChess.BoardNS;

namespace ConsoleChess.Chess
{
    class Knight : Piece
    {
        public Knight(Board board, Color color) : base(board, color)
        {

        }
        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            int[,] directions = new int[,] {
                 { -2, -1 }       // 
                ,{ -2, 1 }        // 
                ,{ 1, -2 }        // 
                ,{ -1, -2 }       // 
                ,{ 2, -1 }        // 
                ,{ 2, 1 }         // 
                ,{ -1, 2 }        // 
                ,{ 1, 2 }         // 
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
                    break;
                }
            }
            return mat;
        }

        public override string GetPieceName()
        {
            return " H";
        }
    }
}
