using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class ClienteDTO
{
    [Required]
    public string? Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Cpf { get; set; }
    public DateOnly? DataNascimento { get; set; }
}