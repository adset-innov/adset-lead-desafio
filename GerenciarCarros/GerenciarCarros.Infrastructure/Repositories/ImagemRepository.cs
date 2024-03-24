using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Repositories;
using GerenciarCarros.Infrastructure.Context;
using GerenciarCarros.Infrastructure.Repositories.Base;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Repositories
{
    public class ImagemRepository : BaseRepository<Imagem>, IImagemRepository
    {
        public ImagemRepository(CarrosDBContext context) : base(context) { }

        public async Task<IEnumerable<Imagem>> ObterPorIdCarro(Guid id)
        {
            return await Db.Imagens.AsNoTracking().Where(p => p.IdCarro == id).ToListAsync();
        }
    }
}
