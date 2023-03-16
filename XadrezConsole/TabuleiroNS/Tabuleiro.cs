namespace XadrezConsole.TabuleiroNS;

class Tabuleiro
{
    public int Linhas { get; set; }
    public int Colunas { get; set; }
    private Peca[,] _pecas;

    public Tabuleiro()
    {
        Linhas = 8;
        Colunas = 8;
        this._pecas = new Peca[8, 8];
    }

    public Peca PecaIndividual(int linha, int coluna)
    {
        return _pecas[linha, coluna];
    }

    public void ColocarPeca(Peca p, Posicao pos)
    {
        _pecas[pos.Linha, pos.Coluna] = p;
        p.Posicao = pos;
    }
}
