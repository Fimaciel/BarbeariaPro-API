using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace barbeariaPro.DTOs;

public class ClienteDTO : IValidatableObject
{
    [Required]
    public string? Nome { get; set; }

    public string? Sobrenome { get; set; }
    public string? Telefone { get; set; }
    public string? Email { get; set; }
    
    public string? Cpf { get; set; }

    public DateOnly? DataNascimento { get; set; }

    public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
    {
        if (!string.IsNullOrWhiteSpace(Cpf))
        {
            string cpf = Regex.Replace(Cpf, "[^0-9]", "");

            if (cpf.Length != 11 || new string(cpf[0], 11) == cpf)
            {
                yield return new ValidationResult("CPF inválido.", new[] { nameof(Cpf) });
                yield break;
            }

            int[] mult1 = { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
            int[] mult2 = { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

            string temp = cpf[..9];
            int soma = 0;

            for (int i = 0; i < 9; i++)
                soma += int.Parse(temp[i].ToString()) * mult1[i];

            int resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;
            temp += resto.ToString();

            soma = 0;
            for (int i = 0; i < 10; i++)
                soma += int.Parse(temp[i].ToString()) * mult2[i];

            resto = soma % 11;
            resto = resto < 2 ? 0 : 11 - resto;

            if (!cpf.EndsWith($"{temp[9]}{resto}"))
            {
                yield return new ValidationResult("CPF inválido.", new[] { nameof(Cpf) });
            }
        }
    }
}
