using barbeariaPro.Validations;
using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.DTOs;

public class ProfissionalDTO
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Sobrenome { get; set; }
    public string Telefone { get; set; }
    
    [Required]
    [CpfValidation]
    public string CPF { get; set; }
    
    [EmailValidation]
    public string Email { get; set; }
    public DateTime DataNasc { get; set; }
    public string Especialidade { get; set; }
}
