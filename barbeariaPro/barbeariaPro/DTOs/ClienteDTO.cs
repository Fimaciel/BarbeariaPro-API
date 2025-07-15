using barbeariaPro.Validations;
using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace barbeariaPro.DTOs;

public class ClienteDTO 
{
    [Required]
    public int? Id { get; set; }
    public string? Nome { get; set; }

    public string? Sobrenome { get; set; }
    public string? Telefone { get; set; }

    [EmailValidation]
    public string? Email { get; set; }

    [Required]
    [CpfValidation]
    public string? Cpf { get; set; }

    public DateOnly? DataNascimento { get; set; }
}
