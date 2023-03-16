using XadrezConsole.TabuleiroNS;
using XadrezConsole;
using XadrezConsole.Xadrez;

namespace XadrezConsole;
class Program
{
    static void Main(string[] args)
    {
        Tabuleiro tabuleiro = new Tabuleiro();

        tabuleiro.ColocarPeca(new Torre(Cor.Branca, tabuleiro), new Posicao(0, 0));

        Tela.ImprimirTabuleiro(tabuleiro);
    }
}