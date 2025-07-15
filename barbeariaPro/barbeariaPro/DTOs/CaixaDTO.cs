using barbeariaPro.Validations;
using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class CaixaDTO
{
    public int Id { get; set; }

    [Required(ErrorMessage = "Saldo inicial é obrigatório.")]
    [Range(0, double.MaxValue, ErrorMessage = "Saldo inicial não pode ser negativo.")]
    public decimal SaldoInicial { get; set; }

    public decimal SaldoFinal { get; set; }

    [Required]
    public DateTime DataAbertura { get; set; }

    public DateTime DataFechamento { get; set; }

    public string Status { get; set; }

    [Required]
    public int UsuarioFk { get; set; }
}
