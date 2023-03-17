using ConsoleChess.BoardNS;
namespace ConsoleChess.Chess
{
    class Match
    {
        public Board Board { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayerColor { get; private set; }
        public bool isMatchOver;

        public Match()
        {
            isMatchOver = false;
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayerColor = Color.White;
            PlacePiecesOnBoard();
        }

        public void SwitchPiecePosition(Position origin, Position target)
        {
            Piece piece = Board.RemovePieceFromBoard(origin);
            piece.IncrementMoveAmount();
            // Piece capturedPiece = Board.RemovePieceFromBoard(target);
            Board.MovePieceOnBoard(piece, target);
        }

        public void ExecuteMove(Position origin, Position target)
        {
            SwitchPiecePosition(origin, target);
            Round++;
            SwitchPlayerTurn();
        }

        public void ValidateOriginPosition(Position origin)
        {
            if(Board.AcessPieceAt(origin) == null)
            {
                throw new BoardException("This position is empty!");
            }

            if (CurrentPlayerColor != Board.AcessPieceAt(origin).Color)
            {
                throw new BoardException($"Its not {CurrentPlayerColor}'s turn!");
            }
            
            if (!Board.AcessPieceAt(origin).CanMove())
            {
                throw new BoardException("No available moves to this piece!");
            }
        }

        public void ValidateDestinationPosition(Position origin, Position destination)
        {
            if (!Board.AcessPieceAt(origin).CanMoveTo(destination))
            {
                throw new BoardException("Destination position is invalid.");
            }
        }

        public void SwitchPlayerTurn()
        {
            if(CurrentPlayerColor == Color.White)
            {
                CurrentPlayerColor = Color.Black;
            }
            else
            {
                CurrentPlayerColor = Color.White;
            }
        }

        private void PlacePiecesOnBoard()
        {
            Board.MovePieceOnBoard(new King(Board, Color.Black), new ChessPosition('a', 1).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new Tower(Board, Color.Black), new ChessPosition('a', 2).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new Tower(Board, Color.Black), new ChessPosition('b', 1).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new Tower(Board, Color.Black), new ChessPosition('b', 2).ConvertToMatrixPosition());
            Board.MovePieceOnBoard(new Tower(Board, Color.White), new ChessPosition('b', 7).ConvertToMatrixPosition());
        }
    }
}
