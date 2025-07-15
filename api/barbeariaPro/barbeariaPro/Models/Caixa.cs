namespace barbeariaPro.Models;

public class Caixa
{
    public int Id { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataFechamento { get; set; }
    public string Status { get; set; }
    public DateTime? DataExclusao { get; set; }

    public int UsuarioFk { get; set; }
    public Usuario Usuario { get; set; }

    public ICollection<MovimentacaoCaixa> Movimentacoes { get; set; }
}
