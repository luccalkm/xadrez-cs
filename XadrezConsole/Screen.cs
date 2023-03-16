using XadrezConsole.BoardNS;

namespace XadrezConsole
{
    class Screen
    {
        public static void PrintBoard(Board Board)
        {
            for (int i = 0; i < Board.Lines; i++) 
            {
                if (i == 0)
                {
                    Console.WriteLine("   ║ a   b   c   d   e   f   g   h  ");
                    Console.WriteLine("════════════════════════════════════");
                }
                Console.Write($" {i+1} ║ "); 
                
                for (int j = 0; j < Board.Columns; j++) 
                {
                    if(Board.AccessPiece(i,j) == null)
                    {
                        Console.Write("-   ");
                    }
                    else
                    {
                        Console.Write(Board.AccessPiece(i, j).PrintPiece() + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
