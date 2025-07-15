using System.ComponentModel.DataAnnotations;
using System.Text.RegularExpressions;

namespace barbeariaPro.Validations;

public class CpfValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var cpf = value as string;

        if (string.IsNullOrEmpty(cpf))
        {
            return new ValidationResult("CPF é obrigatório.");
        }

        cpf = Regex.Replace(cpf, "[^0-9]", "");

        if (cpf.Length != 11 || !CpfValido(cpf))
        {
            return new ValidationResult("CPF inválido.");
        }

        return ValidationResult.Success;
    }

    private bool CpfValido(string cpf)
    {
        if (new string(cpf[0], cpf.Length) == cpf) return false;

        var multiplicador1 = new int[9] { 10, 9, 8, 7, 6, 5, 4, 3, 2 };
        var multiplicador2 = new int[10] { 11, 10, 9, 8, 7, 6, 5, 4, 3, 2 };

        string tempCpf = cpf.Substring(0, 9);
        int soma = 0;

        for (int i = 0; i < 9; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador1[i];

        int resto = soma % 11;
        int digito1 = resto < 2 ? 0 : 11 - resto;

        tempCpf += digito1;
        soma = 0;

        for (int i = 0; i < 10; i++)
            soma += int.Parse(tempCpf[i].ToString()) * multiplicador2[i];

        resto = soma % 11;
        int digito2 = resto < 2 ? 0 : 11 - resto;

        return cpf.EndsWith(digito1.ToString() + digito2.ToString());
    }
}

public class EmailValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var email = value as string;

        if (string.IsNullOrEmpty(email) || !Regex.IsMatch(email, @"^[^\s@]+@[^\s@]+\.[^\s@]+$"))
        {
            return new ValidationResult("E-mail inválido.");
        }

        return ValidationResult.Success;
    }
}

public class SenhaValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var senha = value as string;

        if (string.IsNullOrEmpty(senha))
        {
            return new ValidationResult("Senha é obrigatória.");
        }

        if (senha.Length < 8 || !Regex.IsMatch(senha, "^(?=.*[a-z])(?=.*[A-Z])(?=.*\\d)(?=.*[!@#$%^&*()]).{8,}$"))
        {
            return new ValidationResult("Senha deve conter pelo menos 8 caracteres, incluindo letras maiúsculas, minúsculas, números e caracteres especiais.");
        }

        return ValidationResult.Success;
    }
}