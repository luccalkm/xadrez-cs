using ConsoleChess.BoardNS;
using ConsoleChess.Chess;
using System.Collections.Generic;

namespace ConsoleChess
{
    class Screen
    {
        // Print 
        public static void PrintMatch(Match match)
        {
            PrintBoard(match.Board);
            PrintCapturedPieces(match);
            Console.Write($"\nRound #{match.Round}");
            Console.Write($"\n{match.CurrentPlayerColor}'s turn.");
            Console.Write("\n\nOrigin position: ");
        }

        public static void PrintCapturedPieces(Match match)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.WriteLine("════════════════════════════");
            Console.WriteLine(" Captured Pieces: ");
            Console.Write($" White: "); 
            PrintGroup(match.GetCapturedPieces(Color.White));
            Console.Write($" Black: "); 
            PrintGroup(match.GetCapturedPieces(Color.Black));
            Console.WriteLine("════════════════════════════");

            if(match.isMatchOver)
            {
                Console.WriteLine($" CHECK MATE!\nWinner is {match.CurrentPlayerColor}.");
            }
            else if(match.isKingChecked(match.CurrentPlayerColor))
            {
                Console.WriteLine($" {match.CurrentPlayerColor} King is checked!");
            }
            Console.ResetColor();
        }

        public static void PrintGroup(HashSet<Piece> pieces)
        {
            Console.Write("[");
            foreach(Piece p in pieces)
            {
                Console.Write(p.GetPieceName() + " ");
            }
            Console.Write("]\n");
        }

        public static void PrintBoard(Board Board)
        {
            for (int i = 0; i < Board.Lines; i++) 
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (i == 0)
                {
                    Console.WriteLine("   ║ a  b  c  d  e  f  g  h ");
                    Console.WriteLine("════════════════════════════");
                }
                Console.Write($" {Board.Lines - i} ║");
                Console.ResetColor();
                for (int j = 0; j < Board.Columns; j++) 
                {
                    PrintBoardBackgroundColor(i, j);
                    GetPieceName(Board.AcessPieceAt(i, j));
                }
                Console.WriteLine();
            }
        }

        public static void PrintBoard(Board Board, bool[,] possibleMovements)
        {
            ConsoleColor possibleMovementBackground = ConsoleColor.DarkRed;

            for (int i = 0; i < Board.Lines; i++)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                if (i == 0)
                {
                    Console.WriteLine("   ║ a  b  c  d  e  f  g  h ");
                    Console.WriteLine("════════════════════════════");
                }
                Console.Write($" {Board.Lines - i} ║");
                Console.ResetColor();
                for (int j = 0; j < Board.Columns; j++)
                {
                    if (possibleMovements[i , j])
                    {
                        Console.BackgroundColor = possibleMovementBackground;
                    }
                    else
                    {
                        PrintBoardBackgroundColor(i, j);
                    }
                    GetPieceName(Board.AcessPieceAt(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static void GetPieceName(Piece piece)
        {
            // Print piece or blank
            if (piece == null)
            {
                Console.Write("   ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    ChangePieceColor(piece, ConsoleColor.White);
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Black;
                    Console.Write(piece.GetPieceName() + " ");
                    Console.ResetColor();
                }
            }
            Console.BackgroundColor = ConsoleColor.Black;
        }

        private static void ChangePieceColor(Piece piece, ConsoleColor consoleColor)
        {
            Console.ForegroundColor = consoleColor;
            Console.Write(piece.GetPieceName() + " ");
            Console.ResetColor();
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine().Trim().ToLower();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }

        public static void PrintMatrixes(bool[,] possibleMovements)
        {
            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(possibleMovements[i, j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void PrintBoardBackgroundColor(int i, int j)
        {
            if (i % 2 == 0)
            {
                if (j % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
            }
            else
            {
                if (j % 2 == 0)
                {
                    Console.BackgroundColor = ConsoleColor.DarkGray;
                }
                else
                {
                    Console.BackgroundColor = ConsoleColor.DarkYellow;
                }
            }
        }
    }
}
