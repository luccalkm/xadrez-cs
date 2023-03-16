﻿namespace XadrezConsole.BoardNS;

class Board
{
    public int Line { get; set; }
    public int Columns { get; set; }
    private Piece[,] _pieces;

    public Board()
    {
        Line = 8;
        Columns = 8;
        _pieces = new Piece[8, 8];
    }

    // Access Piece through line and col
    public Piece AccessPiece(int line, int column)
    {
        return _pieces[line, column];
    }
    // Access Piece through position
    public Piece AccessPiece(Position pos)
    {
        return _pieces[pos.Line, pos.Column];
    }

    public bool ExistsPiece(Position pos)
    {
        CheckPosition(pos);
        return AccessPiece(pos) != null;
    }
    
    public bool ValidatePosition(Position pos)
    {
        if(pos.Line < 0 || pos.Column < 0 || pos.Line >= Line || pos.Column >= Line)
        {
            return false;
        }
        return true;
    }

    public void CheckPosition(Position pos)
    {
        if (!ValidatePosition(pos))
        {
            throw new BoardException("\nPosition out of bounds of the board!\n");
        }
    }

    public void SetPiecePosition(Piece p, Position pos)
    {
        if (ExistsPiece(pos))
        {
            throw new BoardException("\nThere is a piece in this position\n");
        }
        _pieces[pos.Line, pos.Column] = p;
        p.Position = pos;
    }
}