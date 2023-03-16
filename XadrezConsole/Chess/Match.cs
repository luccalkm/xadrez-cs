using ConsoleChess.BoardNS;
namespace ConsoleChess.Chess
{
    class Match
    {
        public Board Board { get; set; }
        private int _round;
        private Color _player;

        public Match()
        {
            Board = new Board(8, 8);
            _round = 1;
            _player = Color.White;
            PlacePiecesOnBoard();
        }

        public void ExecuteMove(Position origin, Position target)
        {
            Piece piece = Board.RemovePieceFromBoard(origin);
            piece.IncrementMoveAmount();
            // Piece capturedPiece = Board.RemovePieceFromBoard(target);
            Board.MovePieceOnBoard(piece, target);
        }

        private void PlacePiecesOnBoard()
        {
            Board.MovePieceOnBoard(new Tower(Board, Color.Black), new ChessPosition('c', 1).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new King(Board, Color.Black), new ChessPosition('f', 7).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new Tower(Board, Color.Black), new ChessPosition('b', 4).ConvertToMatrixPosition());
        }
    }
}
