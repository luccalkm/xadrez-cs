using XadrezConsole.TabuleiroNS;

namespace XadrezConsole.Xadrez
{
    class Torre : Peca
    {
        public Torre(Cor cor, Tabuleiro tabuleiro) : base(cor, tabuleiro)
        {

        }

        public override char MostrarPeca()
        {
            return 'T';
        }
    }
}
