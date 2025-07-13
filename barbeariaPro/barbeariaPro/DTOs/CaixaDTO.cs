namespace barbeariaPro.DTOs;

public class CaixaDTO
{
    public int Id { get; set; }
    public decimal SaldoInicial { get; set; }
    public decimal SaldoFinal { get; set; }
    public DateTime DataAbertura { get; set; }
    public DateTime DataFechamento { get; set; }
    public string Status { get; set; }

    public int UsuarioFk { get; set; }
}
