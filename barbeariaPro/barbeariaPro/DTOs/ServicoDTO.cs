using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class ServicoDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    [Range(0, double.MaxValue, ErrorMessage = "Saldo não pode ser negativo.")]
    public decimal Valor { get; set; }

    public DateTime DuracaoEstimada { get; set; }
    public int DuracaoMinutos { get; set; }
    public string Categoria { get; set; }
}
