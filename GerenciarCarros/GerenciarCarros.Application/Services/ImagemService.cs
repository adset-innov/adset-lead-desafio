using GerenciarCarros.Application.Interfaces;
using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;

namespace GerenciarCarros.Application.Services
{
    public class ImagemService : IImagemService
    {
        public Task Adicionar(ImagemModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<Imagem> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<ImagemModel>> ObterPorIdCarro(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
