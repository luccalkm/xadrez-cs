using XadrezConsole.BoardNS;
using XadrezConsole;
using XadrezConsole.Xadrez;

namespace XadrezConsole;
class Program
{
    static void Main(string[] args)
    {
        ChessPosition chessPos = new ChessPosition('a', 2);
        Console.WriteLine(chessPos);
        Console.WriteLine(chessPos.ConvertToMatrixPosition());
    }
}