using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class MovimentacaoCaixaDTO
{
    public int Id { get; set; }
    public string Tipo { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Saldo não pode ser negativo.")]
    public decimal Valor { get; set; }
    public string Categoria { get; set; }
    public string Descricao { get; set; }
    public string ComprovantePath { get; set; }

    public int CaixaFk { get; set; }
}
