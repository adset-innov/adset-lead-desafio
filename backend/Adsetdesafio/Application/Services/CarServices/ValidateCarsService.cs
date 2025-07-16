using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Application.Resources.ValidationMessagesResource;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Models.Entities;
using Adsetdesafio.Shared.Utils;
using Adsetdesafio.Shared.Utils.AppServiceBase;
using Adsetdesafio.Shared.Utils.Enums;
using Microsoft.AspNetCore.Components.Forms;

namespace Adsetdesafio.Application.Services.CarServices
{
    public class ValidateCarsService : DefaultReturn
    {
        private readonly ICarRepository _carsRepository;

        public ValidateCarsService(ICarRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task<bool> ValidatePut(CarsDTO dto)
        {
            if (dto == null)
            {
                this.ErrorMessage("Objeto de entrada está nulo.");
                return true;
            }
            ValidateFields(dto);
            return Message.Count > 0;
        }

        private void ValidateFields(CarsDTO dto)
        {
            if (string.IsNullOrWhiteSpace(dto.Marca))
                this.ErrorMessage("Marca é obrigatória.");

            if (string.IsNullOrWhiteSpace(dto.Modelo))
                this.ErrorMessage("Modelo é obrigatório.");

            if (string.IsNullOrWhiteSpace(dto.Placa))
                this.ErrorMessage("Placa é obrigatória.");

            if (dto.Ano <= 0)
                this.ErrorMessage("Ano deve ser maior que zero.");

            if (dto.Preco <= 0)
                this.ErrorMessage("Preço deve ser maior que zero.");

            if (!Enum.IsDefined(typeof(EnumCor), dto.Cor))
                this.ErrorMessage("Cor do veículo inválida.");

            if (dto.OpcionaisVeiculo != null)
            {
                foreach (var opcional in dto.OpcionaisVeiculo)
                {
                    if (!Enum.IsDefined(typeof(EnumOptionsCar), opcional))
                        this.ErrorMessage($"Opcional inválido: {opcional}");
                }
            }

            if (dto.PortaisCategorias != null)
            {
                foreach (var item in dto.PortaisCategorias)
                {
                    if (string.IsNullOrWhiteSpace(item.NomePortal))
                        this.ErrorMessage("Nome do portal não pode ser vazio.");

                    if (!Enum.IsDefined(typeof(EnumCategory), item.Categoria))
                        this.ErrorMessage($"Categoria inválida para o portal {item.NomePortal}: {item.Categoria}");
                }
            }
        }

      
    }
}
