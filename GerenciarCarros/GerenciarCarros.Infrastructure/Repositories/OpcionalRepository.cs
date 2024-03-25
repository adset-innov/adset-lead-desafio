using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Repositories;
using GerenciarCarros.Infrastructure.Context;
using GerenciarCarros.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Repositories
{
    public class OpcionalRepository : BaseRepository<Opcionais>, IOpcionalRepository
    {
        public OpcionalRepository(CarrosDBContext context) : base(context) { }

        public async Task<IEnumerable<Opcionais>> ObterPorIdCarro(Guid id)
        {
            return await Db.Opcionais.AsNoTracking().Where(p => p.IdCarro == id).ToListAsync();
        }
    }
}
