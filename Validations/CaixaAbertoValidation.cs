using barbeariaPro.DTOs;
using barbeariaPro.Services;
using System.ComponentModel.DataAnnotations;

namespace barbeariaPro.Validations;

public class CaixaAbertoValidation : ValidationAttribute
{
    protected override ValidationResult IsValid(object value, ValidationContext validationContext)
    {
        var caixaService = validationContext.GetService(typeof(CaixaService)) as CaixaService;
        var ultimoCaixa = caixaService?.ObterUltimoCaixa().Result;

        if (ultimoCaixa != null && ultimoCaixa.Status == "Aberto")
        {
            return new ValidationResult("Já existe um caixa aberto. Feche o caixa atual antes de abrir um novo.");
        }

        return ValidationResult.Success;
    }
}
