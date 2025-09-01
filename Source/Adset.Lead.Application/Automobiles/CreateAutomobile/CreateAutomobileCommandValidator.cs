using Adset.Lead.Domain.Abstractions;
using Adset.Lead.Domain.Automobiles;

namespace Adset.Lead.Application.Automobiles.CreateAutomobile;

internal static class CreateAutomobileCommandValidator
{
    public static Result Validate(CreateAutomobileCommand command)
    {
        var errors = new List<Error>();

        // Validação de Brand
        if (string.IsNullOrWhiteSpace(command.Brand))
            errors.Add(new Error("Brand.Required", "Marca é obrigatória"));
        else if (command.Brand.Length > 100)
            errors.Add(new Error("Brand.MaxLength", "Marca deve ter no máximo 100 caracteres"));

        // Validação de Model
        if (string.IsNullOrWhiteSpace(command.Model))
            errors.Add(new Error("Model.Required", "Modelo é obrigatório"));
        else if (command.Model.Length > 100)
            errors.Add(new Error("Model.MaxLength", "Modelo deve ter no máximo 100 caracteres"));

        // Validação de Year
        if (command.Year < 1900)
            errors.Add(new Error("Year.MinValue", "Ano deve ser maior que 1900"));
        else if (command.Year > DateTime.Now.Year + 1)
            errors.Add(new Error("Year.MaxValue", $"Ano deve ser menor ou igual a {DateTime.Now.Year + 1}"));

        // Validação de Plate
        if (string.IsNullOrWhiteSpace(command.Plate))
            errors.Add(new Error("Plate.Required", "Placa é obrigatória"));
        else if (command.Plate.Length > 10)
            errors.Add(new Error("Plate.MaxLength", "Placa deve ter no máximo 10 caracteres"));

        // Validação de Color
        if (string.IsNullOrWhiteSpace(command.Color))
            errors.Add(new Error("Color.Required", "Cor é obrigatória"));
        else if (command.Color.Length > 50)
            errors.Add(new Error("Color.MaxLength", "Cor deve ter no máximo 50 caracteres"));

        // Validação de Price
        if (command.Price <= 0)
            errors.Add(new Error("Price.GreaterThanZero", "Preço deve ser maior que zero"));


        // Validação de Portal
        if (!Enum.IsDefined(typeof(Portal), command.Portal))
            errors.Add(new Error("Portal.Invalid", "Portal inválido"));

        // Validação de Package
        if (!Enum.IsDefined(typeof(Package), command.Package))
            errors.Add(new Error("Package.Invalid", "Pacote inválido"));

        return errors.Any() 
            ? Result.Failure(new Error("Validation.Failed", string.Join("; ", errors.Select(e => e.Name))))
            : Result.Success();
    }
}
