﻿using ConsoleChess.BoardNS;
using ConsoleChess;
using ConsoleChess.Chess;

namespace ConsoleChess;
class Program
{
    static void Main(string[] args)
    {
        Match match = new Match();
        while(!match.isMatchOver)
        {
            Console.Clear();
            Screen.PrintBoard(match.Board);
            Console.Write("\nOrigin position: ");
            Position origin = Screen.ReadChessPosition().ConvertToMatrixPosition();

            // Access Possible Movements of piece
            bool[,] possiblePositions = match.Board.AccessPiece(origin).PossibleMovements();

            Console.Clear();
            Screen.PrintBoard(match.Board, possiblePositions);

            Console.Write("\nDestination: ");
            Position destination = Screen.ReadChessPosition().ConvertToMatrixPosition();

            match.ExecuteMove(origin, destination);
        }
    }
}