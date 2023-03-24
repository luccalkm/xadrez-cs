using ConsoleChess.BoardNS;

namespace ConsoleChess.Chess
{
    class Pawn : Piece
    {
        public Pawn(Board board, Color color) : base(board, color) { }

        private bool CheckPawnColor()
        {
            return Color == Color.White;
        }

        private bool IsEnemy(Position pos)
        {
            Piece p = Board.AcessPieceAt(pos);
            return p != null && p.Color != Color;
        }

        private bool IsFree(Position pos)
        {
            return Board.AcessPieceAt(pos) == null;
        }

        public override bool[,] PossibleMovements()
        {
            bool[,] mat = new bool[Board.Lines, Board.Columns];
            Position pos = new Position(0, 0);

            int[,] directions = new int[,] {
                 { -1, 0 }      // UP
                ,{ -2, 0 }      // UP FIRST MOVE
                ,{ -1, -1 }     // UP LEFT
                ,{ -1, 1 }      // UP RIGHT
            };

            for (int i = 0; i < directions.GetLength(0); i++)
            {
                int dirX = directions[i, 0];
                int dirY = directions[i, 1];

                if(MoveAmount != 0 && dirX == -2)
                {
                    continue;
                }

                if (CheckPawnColor())
                {
                    pos.DefineValuesToPosition(Position.Line + dirX, Position.Column + dirY);
                    if (IsFree(pos) && dirY == 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    if(IsEnemy(pos) && dirY != 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                }
                else
                {
                    pos.DefineValuesToPosition(Position.Line + Math.Abs(dirX), Position.Column + Math.Abs(dirY));
                    if (IsFree(pos) && dirY == 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                    if (IsEnemy(pos) && dirY != 0)
                    {
                        mat[pos.Line, pos.Column] = true;
                    }
                }
            }
            return mat;
        }

        public override string GetPieceName()
        {
            return " P";
        }
    }
}
