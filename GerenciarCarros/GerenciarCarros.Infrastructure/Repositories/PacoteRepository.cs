using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Entities.Enums;
using GerenciarCarros.Domain.Repositories;
using GerenciarCarros.Infrastructure.Context;
using GerenciarCarros.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Repositories
{
    public sealed class PacoteRepository : BaseRepository<Pacote>, IPacoteRepository
    {
        public PacoteRepository(CarrosDBContext context) : base(context) { }

        public async Task<IEnumerable<Pacote>> ObterPorIdCarro(Guid id)
        {
            var pacotes = Db.Pacotes.Where(x => x.IdCarro == id).ToListAsync();
            return await pacotes;
        }

        public async Task<IEnumerable<Pacote>> ObterPorIdCarroEPacote(Guid id, TipoPacote tipo)
        {
            var pacotes = Db.Pacotes.Where(x => x.IdCarro == id && x.TipoPacote == tipo).ToListAsync();
            return await pacotes;
        }
    }
}
