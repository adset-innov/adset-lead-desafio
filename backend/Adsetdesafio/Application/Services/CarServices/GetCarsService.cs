using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Models.Entities;
using Adsetdesafio.Shared.Utils.AppServiceBase;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System.Linq.Expressions;

namespace Adsetdesafio.Application.Services.CarServices
{
    public class GetCarsService : DefaultReturn
    {
        private readonly ICarRepository _carsRepository;

        public GetCarsService(ICarRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task GetById(long id)
        {
            Car entity = await _carsRepository.GetEntityAsync(x => x.Id == id, include: q => q.Include(c => c.PortaisAnuncio));

            if (entity is null)
            {
                StatusCode = System.Net.HttpStatusCode.NotFound;
                return;
            }

            StatusCode = System.Net.HttpStatusCode.OK;

            Data = new CarsDTO
            {
                Id = entity.Id,
                Ano = entity.Ano,
                Cor = entity.Cor,
                Km = entity.Km,
                Marca = entity.Marca,
                Modelo = entity.Modelo,
                Placa = entity.Placa,
                OpcionaisVeiculo = entity.OpcionaisVeiculo,
                Preco = entity.Preco,
                PortaisCategorias = entity.PortaisAnuncio
                    .Select(pa => new CategoryPortalDTO
                    {
                        NomePortal = pa.NomePortal,
                        Categoria = pa.Categoria
                    }).ToList()
            };
        }

        public async Task GetByFilter(CarsFilterDTO dto)
        {
            IList<Car> carList = await _carsRepository.GetListByFilterAsync(dto);

            if (carList == null || carList.Count == 0)
            {
                StatusCode = System.Net.HttpStatusCode.NotFound;
                return;
            }

            StatusCode = System.Net.HttpStatusCode.OK;

            Data = carList.Select(entity => new CarsDTO
            {
                Id = entity.Id,
                Ano = entity.Ano,
                Cor = entity.Cor,
                Km = entity.Km,
                Marca = entity.Marca,
                Modelo = entity.Modelo,
                Placa = entity.Placa,
                OpcionaisVeiculo = entity.OpcionaisVeiculo,
                Preco = entity.Preco,
                PortaisCategorias = entity.PortaisAnuncio?
                    .Select(pa => new CategoryPortalDTO
                    {
                        NomePortal = pa.NomePortal,
                        Categoria = pa.Categoria
                    }).ToList() ?? new List<CategoryPortalDTO>()
            }).ToList();
        }

        public async Task GetResume()
        {
            ResumeVehicleDTO resume = await _carsRepository.GetResumoVeiculosAsync();

            if (resume == null)
            {
                StatusCode = System.Net.HttpStatusCode.NotFound;
                return;
            }

            StatusCode = System.Net.HttpStatusCode.OK;
            Data = resume;
        }
    }

}
