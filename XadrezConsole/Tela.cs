using XadrezConsole.TabuleiroNS;

namespace XadrezConsole
{
    class Tela
    {
        public static void ImprimirTabuleiro(Tabuleiro tabuleiro)
        {
            for(int i = 0; i < tabuleiro.Linhas; i++) 
            {
                for (int j = 0; j < tabuleiro.Colunas; j++) 
                {
                    if(tabuleiro.PecaIndividual(i,j) == null)
                    {
                        Console.Write("- ");
                    }
                    else
                    {
                        Console.Write(tabuleiro.PecaIndividual(i, j).MostrarPeca() + " ");
                    }
                }
                Console.WriteLine();
            }
        }
    }
}
