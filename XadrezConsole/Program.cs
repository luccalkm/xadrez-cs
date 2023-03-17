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
            try
            {
                Console.Clear();
                Screen.PrintBoard(match.Board);
                Console.Write($"\nRound #{match.Round}");
                Console.Write($"\n{match.CurrentPlayerColor}'s turn.");
                Console.Write("\n\nOrigin position: ");
                Position origin = Screen.ReadChessPosition().ConvertToMatrixPosition();
                match.ValidateOriginPosition( origin );

                // Access Possible Movements of piece
                bool[,] possiblePositions = match.Board.AcessPieceAt(origin).PossibleMovements();

                Console.Clear();
                Screen.PrintBoard(match.Board, possiblePositions);
                Screen.PrintMatrixes(possiblePositions);

                Console.Write("\nDestination: ");
                Position destination = Screen.ReadChessPosition().ConvertToMatrixPosition();
                match.ValidateDestinationPosition(origin, destination);

                match.ExecuteMove(origin, destination);
            }
            catch (BoardException e)
            {
                Console.WriteLine(e.Message + "\nPress any key to try again.");
                Console.ReadKey();
            }
        }
    }
}