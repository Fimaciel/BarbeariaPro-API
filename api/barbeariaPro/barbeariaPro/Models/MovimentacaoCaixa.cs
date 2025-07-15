namespace barbeariaPro.Models;

public class MovimentacaoCaixa
{
    public int Id { get; set; }
    public string Tipo { get; set; }
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
    public string Descricao { get; set; }
    public string ComprovantePath { get; set; }

    public int CaixaFk { get; set; }
    public Caixa Caixa { get; set; }
}
