using GerenciarCarros.Application.Interfaces;
using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Entities.Enums;
using GerenciarCarros.Domain.Pagination;
using GerenciarCarros.Domain.Repositories;

namespace GerenciarCarros.Application.Services
{
    public class CarroService : ICarroService
    {
        private readonly ICarroRepository _carroRepository;
        private readonly IOpcionalRepository _opcionalRepository;
        private readonly IImagemRepository _imagemRepository;
        private readonly IPacoteRepository _pacoteRepository;

        public CarroService(ICarroRepository carroRepository, IOpcionalRepository opcionalRepository, 
            IImagemRepository imagemRepository, IPacoteRepository pacoteRepository)
        {
            _carroRepository = carroRepository;
            _opcionalRepository = opcionalRepository;   
            _imagemRepository = imagemRepository;
            _pacoteRepository = pacoteRepository;
        }

        public async Task<Carro> Adicionar(CarroModel entity)
        {
            var carro = new Carro {
                Ano = entity.Ano,
                Km  = entity.Km,
                Marca = entity.Marca,
                Modelo = entity.Modelo,
                Placa = entity.Placa,
                Preco = entity.Preco,
                Cor = entity.Cor  
            
            };

           await _carroRepository.Adicionar(carro);

            if (entity.Opcionais is not null) {
                foreach (var item in entity.Opcionais)
                {
                    var opcional = new Opcionais { Descricao = item.Descricao, IdCarro = carro.Id };
                    await _opcionalRepository.Adicionar(opcional);
                }

            }
            return carro;
        }

        public async Task<Carro> Atualizar(CarroModel entity)
        {
            var carro = new Carro
            {
                Id = entity.Id.Value,
                Ano = entity.Ano,
                Km = entity.Km,
                Marca = entity.Marca,
                Modelo = entity.Modelo,
                Placa = entity.Placa,
                Preco = entity.Preco,
                Cor = entity.Cor

            };

            await _carroRepository.Atualizar(carro);

            // await RemoverOpcionaisEImagens(carro.Id);

            if (entity.Opcionais is not null)
            {
                foreach (var item in entity.Opcionais)
                {
                    if (item.Id == null)
                    {
                        var opcional = new Opcionais { Descricao = item.Descricao, IdCarro = carro.Id };
                        await _opcionalRepository.Adicionar(opcional);
                    }
                }
            }

            return await ObterPorId(carro.Id);
        }

        public async Task<Carro> ObterPorId(Guid id)
        {
            var carro = await _carroRepository.ObterPorId(id);
            carro.Imagens = await _imagemRepository.ObterPorIdCarro(id);
            carro.Opcionais = await _opcionalRepository.ObterPorIdCarro(id);
            return carro;
        }

        public async Task<List<Carro>> ObterTodos()
        {
            return await _carroRepository.ObterTodos();
        }

        public async Task<PaginacaoList<Carro>> Paginacao(PaginacaoCarroModel paginacao)
        {
            try
            {
                var carros = await _carroRepository.Paginacao(paginacao.TamanhoPagina, paginacao.NumeroPagina, 
                    paginacao.Ordenacao,paginacao.Marca,paginacao.Modelo, paginacao.Cor, paginacao.Preco, paginacao.Opcionais
                    ,paginacao.AnoMin, paginacao.AnoMax, paginacao.Placa);
                return carros;

            }
            catch (Exception ex)
            {

                throw;
            }
           
        }

        public async Task Remover(Guid id)
        {
            await RemoverOpcionaisEImagens(id);   
            await _carroRepository.Remover(id);
        }

        private async Task RemoverOpcionaisEImagens(Guid id) 
        {

            var imagens = await _imagemRepository.ObterPorIdCarro(id);
            var opcionais = await _opcionalRepository.ObterPorIdCarro(id);

            if (imagens is not null) {
                foreach (var item in opcionais)
                {
                    await _opcionalRepository.Remover(item.Id);
                }
            }

            if (opcionais is not null) {
                foreach (var item in imagens)
                {
                    await _imagemRepository.Remover(item.Id);
                }
            }           
        }

        public async Task<bool> UploadImagem(ImagemModel item) {
            try
            {
                var imagem = new Imagem { IdCarro = item.IdCarro.Value, Tipo = item.Tipo, Bytes = item.Bytes, Nome = item.Nome };
                await _imagemRepository.Adicionar(imagem);
                return true;
            }
            catch (Exception ex)
            {
                return false;

            }

        }

        public async Task<int> ObterTotais(string tipo)
        {
            return await _carroRepository.Totais(tipo);
        }

        public async Task<List<int>> Anos()
        {
            return await _carroRepository.Anos();
        }

        public async Task RemoverImagem(Guid id)
        {
            await _imagemRepository.Remover(id);
        }

        public async Task RemoverOpcional(Guid id)
        {
            await _opcionalRepository.Remover(id);
        }

        public async Task<PacoteModel> VincularPacote(PacoteModel entity)
        {
            var pacote = new Pacote { 
                IdCarro = entity.IdCarro.Value,
                TipoPacote = entity.TipoPacote
                };
            await _pacoteRepository.Adicionar(pacote);
            entity.Id = pacote.Id;
            return entity; 
        }

        public async Task RemoverPacote(Guid id, int tipoPacote)
        {
            var pacotes = await _pacoteRepository.ObterPorIdCarroEPacote(id, (TipoPacote)tipoPacote);
            foreach (var item in pacotes)
            {
                await _pacoteRepository.Remover(item.Id);
            }
          
        }
    }
}
