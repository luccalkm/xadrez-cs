using ConsoleChess.BoardNS;
using ConsoleChess.Chess;

namespace ConsoleChess
{
    class Screen
    {
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
                    PrintPiece(Board.AccessPiece(i, j));
                }
                Console.WriteLine();
            }
        }

        public static void PrintBoard(Board Board, bool[,] possibleMovements)
        {
            ConsoleColor possibleMovementBackground = ConsoleColor.DarkGray;

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
                        Console.BackgroundColor = ConsoleColor.Black;
                    }
                    PrintPiece(Board.AccessPiece(i, j));
                }
                Console.WriteLine();
                Console.BackgroundColor = ConsoleColor.Black;
            }
        }

        public static void PrintMatrixes(bool[,] possibleMovements)
        {
            for(int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write(possibleMovements[i,j] + " ");
                }
                Console.WriteLine();
            }
        }

        public static void PrintPiece(Piece piece)
        {
            // Print piece or blank
            if (piece == null)
            {
                Console.Write(" - ");
            }
            else
            {
                if (piece.Color == Color.White)
                {
                    Console.Write(piece.PrintPiece() + " ");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write(piece.PrintPiece() + " ");
                    Console.ResetColor();
                }
            }
        }

        public static ChessPosition ReadChessPosition()
        {
            string s = Console.ReadLine().Trim().ToLower();
            char column = s[0];
            int line = int.Parse(s[1] + "");
            return new ChessPosition(column, line);
        }
    }
}
