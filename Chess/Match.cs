using ConsoleChess.BoardNS;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Data.Common;
using System.Security.Cryptography.X509Certificates;

namespace ConsoleChess.Chess
{
    class Match
    {
        public Board Board { get; private set; }
        public bool Check { get; private set; }
        public int Round { get; private set; }
        public Color CurrentPlayerColor { get; private set; }
        public bool isMatchOver;
        private HashSet<Piece> _pieces;
        private HashSet<Piece> _capturedPieces;

        public Match()
        {
            isMatchOver = false;
            Board = new Board(8, 8);
            Round = 1;
            CurrentPlayerColor = Color.White;
            _pieces = new HashSet<Piece>();
            _capturedPieces = new HashSet<Piece>();
            PlacePiecesOnBoard();
        }

        public Piece SwitchPiecePosition(Position origin, Position target)
        {
            Piece piece = Board.RemovePieceFromBoard(origin);
            piece.IncrementMoveAmount();
            Piece capturedPiece = Board.RemovePieceFromBoard(target);
            Board.MovePieceTo(piece, target);

            if (capturedPiece != null)
            {
                _capturedPieces.Add(capturedPiece);
            }
            return capturedPiece;
        }

        public void UndoMove(Position origin, Position destination, Piece capturedPiece)
        {
            Piece piece = Board.RemovePieceFromBoard(destination);
            piece.DescreaseMoveAmount();
            if(capturedPiece != null)
            {
                Board.MovePieceTo(capturedPiece, destination);
                _capturedPieces.Remove(capturedPiece);
            }
            Board.MovePieceTo(piece, origin);
        }

        public void ExecuteMove(Position origin, Position target)
        {
            Piece capturedPiece = SwitchPiecePosition(origin, target);

            if (isKingChecked(CurrentPlayerColor))
            {
                UndoMove(origin, target, capturedPiece);
                throw new BoardException("You cant move your king to a check position!");
            }

            if (isKingChecked(OppositeColor(CurrentPlayerColor)))
            {
                Check = true;
            }
            else
            {
                Check = false;
            }

            if (Checkmate(OppositeColor(CurrentPlayerColor)))
            {
                isMatchOver = true;
            }
            else
            {
                Round++;
                SwitchPlayerTurn();
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

        public HashSet<Piece> GetCapturedPieces(Color color)
        {
            HashSet<Piece> newCapturedPieces = new();
            foreach ( Piece p in _capturedPieces )
            {
                if (p.Color == color)
                {
                    newCapturedPieces.Add(p);
                }
            }
            return newCapturedPieces;
        }
        public HashSet<Piece> GetAlivePieces(Color color)
        {
            HashSet<Piece> newPieces = new();
            foreach (Piece p in _pieces)
            {
                if (p.Color == color)
                {
                    newPieces.Add(p);
                }
            }

            newPieces.ExceptWith(GetCapturedPieces(color));

            return newPieces;
        }

        private Color OppositeColor(Color color)
        {
            return (color == Color.White) ? Color.Black : Color.White;
        }

        private Piece GetKing(Color color)
        {
            foreach (Piece piece in GetAlivePieces(color))
            {
                if (piece is King)
                {
                    return piece;
                }
            }
            return null;
        }

        public bool isKingChecked(Color color)
        {
            Piece king = GetKing(color) ?? throw new BoardException($"No {color} king on the board!");

            foreach (Piece piece in GetAlivePieces(OppositeColor(color)))
            {
                bool[,] mat = piece.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
        }

        public bool Checkmate(Color color)
        {
            if (!isKingChecked(color))
            {
                return false;
            }

            foreach (Piece p in GetAlivePieces(color))
            {
                bool[,] mat = p.PossibleMovements();
                for (int i = 0; i < Board.Lines; i++)
                {
                    for (int j = 0; j < Board.Columns; j++)
                    {
                        if (mat[i, j])
                        {
                            Position origin = p.Position;
                            Position destination = new Position(i, j);
                            Piece capturedPiece = SwitchPiecePosition(origin, destination);
                            bool canMove = isKingChecked(color);
                            UndoMove(origin, destination, capturedPiece);
                            if (!canMove)
                            {
                                return false;
                            }
                        }
                    }
                }
            }
            return true;
        }

        #region Validations
        public void ValidateOriginPosition(Position origin)
        {
            if (Board.AcessPieceAt(origin) == null)
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
            if (!Board.AcessPieceAt(origin).IsMovingPossible(destination))
            {
                throw new BoardException("Destination position is invalid.");
            }
        }
        #endregion

        #region Creating pieces
        public void CreateNewPiece(char column, int line, Piece piece)
        {
            Board.MovePieceTo(piece, new ChessPosition(column, line).ConvertToMatrixPosition());
            _pieces.Add(piece);
        }

        private void PlacePiecesOnBoard()
        {
            String letras = "abcedfgh";

            CreateNewPiece('a', 1, new Tower(Board, Color.White));
            CreateNewPiece('b', 1, new Knight(Board, Color.White));
            CreateNewPiece('c', 1, new Bishop(Board, Color.White));
            CreateNewPiece('d', 1, new Queen(Board, Color.White));
            CreateNewPiece('e', 1, new King(Board, Color.White));
            CreateNewPiece('f', 1, new Bishop(Board, Color.White));
            CreateNewPiece('g', 1, new Knight(Board, Color.White));
            CreateNewPiece('h', 1, new Tower(Board, Color.White));
            for(int i = 0; i < letras.Length; i++)
            {
                CreateNewPiece(letras[i], 2, new Pawn(Board, Color.White));
            }

            CreateNewPiece('a', 8, new Tower(Board, Color.Black));
            CreateNewPiece('b', 8, new Knight(Board, Color.Black));
            CreateNewPiece('c', 8, new Bishop(Board, Color.Black));
            CreateNewPiece('d', 8, new Queen(Board, Color.Black));
            CreateNewPiece('e', 8, new King(Board, Color.Black));
            CreateNewPiece('f', 8, new Bishop(Board, Color.Black));
            CreateNewPiece('g', 8, new Knight(Board, Color.Black));
            CreateNewPiece('h', 8, new Tower(Board, Color.Black));
            for (int i = 0; i < letras.Length; i++)
            {
                CreateNewPiece(letras[i], 7, new Pawn(Board, Color.Black));
            }
        }
        #endregion
    }
}
