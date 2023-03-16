using ConsoleChess.BoardNS;
using ConsoleChess;
using ConsoleChess.Chess;

namespace ConsoleChess;
class Program
{
    static void Main(string[] args)
    {
        Match match = new Match();
        Screen.PrintBoard(match.Board);
    }
}