using XadrezConsole.TabuleiroNS;

namespace XadrezConsole.Xadrez
{
    class Rei : Peca
    {
        public Rei(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {

        }

        public override char MostrarPeca()
        {
            return 'R';
        }
    }
}
