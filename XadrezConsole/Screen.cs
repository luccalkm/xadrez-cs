using ConsoleChess.BoardNS;

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
                    if(Board.AccessPiece(i,j) == null)
                    {
                        Console.Write(" - ");
                    }
                    else
                    {
                        PrintPiece(Board.AccessPiece(i, j));
                    }
                }
                Console.WriteLine();
            }
        }

        public static void PrintPiece(Piece piece)
        {
            if(piece.Color == Color.White)
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
}
