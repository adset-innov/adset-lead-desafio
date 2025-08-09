using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Backend_adset_lead.Repositories;

namespace Backend_adset_lead.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _repository;

        public CarroService(ICarroRepository repository) => _repository = repository;

        public async Task<int> AddAsync(NovoCarroRequestDTO carro)
        {
            var novoCarro = new Carro
            {
                Marca = carro.Marca,
                Modelo = carro.Modelo,
                Ano = carro.Ano,
                Placa = carro.Placa,
                Quilometragem = carro.Quilometragem,
                Cor = carro.Cor,
                Preco = carro.Preco,
                ListaOpcionais = carro.ListaOpcionais,
                PortalPacotes = carro.PortalPacotes
                .Select(p => new PortalPacote
                {
                    Portal = p.Portal,
                    Pacote = p.Pacote,
                }).ToList(),
                Fotos = carro.Fotos.Select(f => new Foto
                {
                    Url = f.Url,
                }).ToList(),
            };

            return await _repository.Add(novoCarro);
        }

        public async Task<int> DeleteAsync(int id)
        {
            return await _repository.Delete(id);
        }

        public async Task<CarroResponseDTO> GetByIdAsync(int id)
        {
            var response = await _repository.GetById(id);
            if (response == null) throw new ArgumentNullException("Carro não encontrado");

            var result = new CarroResponseDTO
            {
                Id = response.Id,
                Marca = response.Marca,
                Modelo = response.Modelo,
                Ano = response.Ano,
                Placa = response.Placa,
                Quilometragem = response.Quilometragem,
                Cor = response.Cor,
                Preco = response.Preco,
                ListaOpcionais = response.ListaOpcionais,
                PortalPacotes = response.PortalPacotes.Select(pp => new PortalPacoteResponseDTO
                {
                    Id = pp.Id,
                    CarroId = pp.CarroId,
                    Portal = pp.Portal,
                    Pacote = pp.Pacote
                }).ToList(),
                Fotos = response.Fotos.Select(f => new FotoResponseDTO
                {
                    Id = f.Id,
                    Url = f.Url,
                    CarroId = f.CarroId,
                }).ToList()
            };

            return result;
        }

        public async Task<PagedListDTO<CarroResponseDTO>> GetFilteredAsync(BuscaCarroRequestDTO filtro)
        {
            var response = await _repository.GetFiltered(filtro);

            var result = response.Items.Select(r => new CarroResponseDTO
            {
                Id = r.Id,
                Marca = r.Marca,
                Modelo = r.Modelo,
                Ano = r.Ano,
                Placa = r.Placa,
                Quilometragem = r.Quilometragem,
                Cor = r.Cor,
                Preco = r.Preco,
                ListaOpcionais = r.ListaOpcionais,
                PortalPacotes = r.PortalPacotes.Select(pp => new PortalPacoteResponseDTO
                {
                    Id = pp.Id,
                    CarroId = pp.CarroId,
                    Portal = pp.Portal,
                    Pacote = pp.Pacote
                }).ToList(),
                Fotos = r.Fotos.Select(f => new FotoResponseDTO
                {
                    Id = f.Id,
                    Url = f.Url,
                    CarroId = f.CarroId,
                }).ToList()
            }).ToList();

            return new PagedListDTO<CarroResponseDTO>
            {
                Items = result,
                TotalPages = response.TotalPages,
                TotalCarros = response.TotalCarros,
                TotalCarrosComFotos = response.TotalCarrosComFotos,
                TotalCarrosSemFotos = response.TotalCarrosSemFotos,
                Cores = response.Cores,
            };
        }

        public async Task<int> UpdateAsync(CarroUpdateRequestDTO carro)
        {
            return await _repository.Update(carro);
        }
    }
}
