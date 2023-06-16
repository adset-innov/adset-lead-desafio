
using AdSetLead.Core.Models;
using FluentValidation;
using System;

namespace AdSetLead.Core.Validators
{
    public class CarroValidator : AbstractValidator<Carro>
    {

        public CarroValidator()
        {
            RulesValidator();
        }

        private void RulesValidator()
        {
            RuleSet("Insert", () => InsertCarroValidator());
            RuleSet("Update", () => UpdateCarroValidator());
        }

        private void InsertCarroValidator()
        {
            RuleFor(carro => carro.Ano).GreaterThan(0).WithMessage("O ano do carro é obrigatório")
                .InclusiveBetween(1985, DateTime.Now.Year).WithMessage($"O ano deve estar entre 1985 até {DateTime.Now.Year}");

            RuleFor(carro => carro.Placa).NotEmpty().WithMessage("O placa do carro é obrigatório")
                .Length(6, 7).WithMessage("Placa deve ser entre 6 e 7 caracteres");

            RuleFor(carro => carro.MarcaId).GreaterThan(0).WithMessage("Marca do carro é obrigatória");

            RuleFor(carro => carro.ModeloId).GreaterThan(0).WithMessage("O modelo do carro é obrigatório");

            RuleFor(carro => carro.Cor).NotEmpty().WithMessage("A cor do carro é obrigatório");

            RuleFor(carro => carro.Preco).NotEmpty().WithMessage("O preço do carro é obrigatório");
        }

        private void UpdateCarroValidator()
        {
            InsertCarroValidator();
        }
    }
}
