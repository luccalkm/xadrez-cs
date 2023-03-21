﻿using ConsoleChess.BoardNS;
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

            Round++;
            SwitchPlayerTurn();
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
            if (color == Color.White)
            {
                return Color.Black;
            }
            return Color.White;
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
            Piece king = GetKing(color);
            if(king == null)
            {
                throw new BoardException($"No {color} king on the board!");
            }

            foreach(Piece piece in GetAlivePieces(OppositeColor(color)))
            {
                bool[,] mat = piece.PossibleMovements();
                if (mat[king.Position.Line, king.Position.Column])
                {
                    return true;
                }
            }
            return false;
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
            if (!Board.AcessPieceAt(origin).CanMoveTo(destination))
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
            CreateNewPiece('a', 1, new King(Board, Color.Black));
            CreateNewPiece('a', 2, new Tower(Board, Color.Black));
            CreateNewPiece('b', 1, new Tower(Board, Color.Black));
            CreateNewPiece('b', 2, new Tower(Board, Color.Black));
            CreateNewPiece('h', 1, new Tower(Board, Color.White));
            CreateNewPiece('h', 3, new King(Board, Color.White));
        }
        #endregion
    }
}
