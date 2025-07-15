using System.ComponentModel.DataAnnotations.Schema;
namespace barbeariaPro.Models;
public class Cliente
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string? Sobrenome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    public string? Cpf { get; set; }

    [System.ComponentModel.DataAnnotations.Schema.Column("data_nascimento")]
    public DateOnly? DataNascimento { get; set; }
}
