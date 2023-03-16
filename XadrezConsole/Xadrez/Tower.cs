using XadrezConsole.BoardNS;

namespace XadrezConsole.Xadrez
{
    class Tower : Piece
    {
        public Tower(Color color, Board Board) : base(color, Board)
        {

        }

        public override char PrintPiece()
        {
            return 'T';
        }
    }
}
