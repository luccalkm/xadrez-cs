using XadrezConsole.BoardNS;

namespace XadrezConsole.Xadrez
{
    class King : Piece
    {
        public King(Color color, Board Board) : base(color, Board)
        {

        }

        public override char PrintPiece()
        {
            return 'R';
        }
    }
}
