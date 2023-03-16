using XadrezConsole.BoardNS;

namespace XadrezConsole
{
    class Screen
    {
        public static void PrintBoard(Board Board)
        {
            for(int i = 0; i < Board.Line; i++) 
            {
                for (int j = 0; j < Board.Columns; j++) 
                {
                    if(Board.AccessPiece(i,j) == null)
                    {
                        Console.Write("- ");
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
