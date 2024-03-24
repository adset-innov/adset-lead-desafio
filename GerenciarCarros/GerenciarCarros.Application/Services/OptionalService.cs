using GerenciarCarros.Application.Interfaces;
using GerenciarCarros.Application.Models;
using GerenciarCarros.Domain.Entities;

namespace GerenciarCarros.Application.Services
{
    public class OptionalService : IOptionalService
    {
        public Task Adicionar(OpcionalModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<Opcionais> ObterPorId(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task<List<OpcionalModel>> ObterPorIdCarro(Guid id)
        {
            throw new NotImplementedException();
        }

        public Task Remover(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
