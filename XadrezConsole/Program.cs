using XadrezConsole.BoardNS;
using XadrezConsole;
using XadrezConsole.Xadrez;

namespace XadrezConsole;
class Program
{
    static void Main(string[] args)
    {
        Board Board = new Board();

        Board.SetPiecePosition(new Tower(Color.White, Board), new Position(0, 0));


        Screen.PrintBoard(Board);
    }
}