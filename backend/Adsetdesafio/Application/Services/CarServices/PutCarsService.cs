using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Application.Resources.ValidationMessagesResource;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Infraestructure.Repositories;
using Adsetdesafio.Domain.Models.Entities;
using Adsetdesafio.Shared.Utils.AppServiceBase;
using Adsetdesafio.Shared.Utils.Enums;

namespace Adsetdesafio.Application.Services.CarServices
{
    public class PutCarsService : DefaultReturn
    {
        private readonly ICarRepository _carsRepository;
        private readonly ValidateCarsService _validateService;

        public PutCarsService(ICarRepository carsRepository, ValidateCarsService validateService)
        {
            _carsRepository = carsRepository;
            _validateService = validateService;
        }

        public async Task Put(CarsDTO dto)
        {
            var validationResult = await _validateService.ValidatePut(dto);

            if (validationResult)
            {
                this.StatusCode = _validateService.StatusCode;
                this.Message.AddRange(_validateService.Message);
                return;
            }

            Car entity;

            if (dto.Id > 0)
            {
                entity = await _carsRepository.GetEntityAsync(x => x.Id == dto.Id);

                if (entity == null)
                {
                    StatusCode = System.Net.HttpStatusCode.NotFound;
                    Message.Add("Vehicle not found.");
                    return;
                }

                // Atualiza os dados
                entity.Ano = dto.Ano;
                entity.Cor = dto.Cor;
                entity.Km = dto.Km;
                entity.Marca = dto.Marca;
                entity.Modelo = dto.Modelo;
                entity.Placa = dto.Placa;
                entity.Preco = dto.Preco;
                entity.OpcionaisVeiculo = dto.OpcionaisVeiculo != null
                    ? new List<EnumOptionsCar>(dto.OpcionaisVeiculo)
                    : new List<EnumOptionsCar>();
                entity.PortaisAnuncio = dto.PortaisCategorias != null
                    ? dto.PortaisCategorias.Select(pc => new AnnouncementPortal
                    {
                        NomePortal = pc.NomePortal,
                        Categoria = pc.Categoria
                    }).ToList()
                    : new List<AnnouncementPortal>();

                _carsRepository.Update(entity);
                StatusCode = System.Net.HttpStatusCode.OK;
                Message.Add("Vehicle updated.");
            }
            else
            {
                entity = new Car
                {
                    Ano = dto.Ano,
                    Cor = dto.Cor,
                    Km = dto.Km,
                    Marca = dto.Marca,
                    Modelo = dto.Modelo,
                    Placa = dto.Placa,
                    Preco = dto.Preco,
                    OpcionaisVeiculo = dto.OpcionaisVeiculo != null
                        ? new List<EnumOptionsCar>(dto.OpcionaisVeiculo)
                        : new List<EnumOptionsCar>(),
                    PortaisAnuncio = dto.PortaisCategorias != null
                        ? dto.PortaisCategorias.Select(pc => new AnnouncementPortal
                        {
                            NomePortal = pc.NomePortal,
                            Categoria = pc.Categoria
                        }).ToList()
                        : new List<AnnouncementPortal>()
                };

                await _carsRepository.AddAsync(entity);
                StatusCode = System.Net.HttpStatusCode.Created;
                Message.Add("Vehicle created.");
            }

            await _carsRepository.SaveChangesAsync();
            Data = dto;
        }

    }
}
