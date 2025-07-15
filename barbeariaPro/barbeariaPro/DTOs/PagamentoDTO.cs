using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class PagamentoDTO
{
    public int Id { get; set; }
    public string FormaPagamento { get; set; }
    public bool Status { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Saldo não pode ser negativo.")]
    public decimal Valor { get; set; }
    public string ComprovantePath { get; set; }
    public DateTime DataEstorno { get; set; }

    public int AgendamentoFk { get; set; }
}
