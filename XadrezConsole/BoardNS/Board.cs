namespace ConsoleChess.BoardNS
{
    class Board
    {
        public int Lines { get; set;}
        public int Columns{ get; set; }
        private Piece[,] _pieces;

        public Board(int lines, int columns)
        {
            Lines = lines;
            Columns = columns;
            _pieces = new Piece[lines, columns];
        }

        // Access Piece through line and col
        public Piece AcessPieceAt(int line, int column)
        {
            CheckPosition(new Position(line, column));
            return _pieces[line, column];
        }
        // Access Piece through position
        public Piece AcessPieceAt(Position pos)
        {
            CheckPosition(pos);
            return _pieces[pos.Line, pos.Column];
        }

        #region PieceValidation
        public bool ExistsPiece(Position pos)
        {
            CheckPosition(pos);
            return AcessPieceAt(pos) != null;
        }

        public bool ValidateChessBounds(Position pos)
        {
            if (pos.Line < 0 || pos.Column < 0 || pos.Line >= Lines || pos.Column >= Lines)
            {
                return false;
            }
            return true;
        }

        public void CheckPosition(Position pos)
        {
            if (!ValidateChessBounds(pos))
            {
                throw new BoardException("\nPosition is out of bounds!");
            }
        }
        #endregion

        public void MovePieceTo(Piece p, Position pos)
        {
            if (ExistsPiece(pos))
            {
                throw new BoardException("\nThere is a piece in this position\n");
            }
            _pieces[pos.Line, pos.Column] = p;
            p.Position = pos;
        }

        public Piece RemovePieceFromBoard(Position pos)
        {
            if(AcessPieceAt(pos) == null)
            {
                return null;
            }
            Piece foundPiece = AcessPieceAt(pos);
            foundPiece.Position = null;
            _pieces[pos.Line, pos.Column] = null;
            return foundPiece;
        }
    }
}