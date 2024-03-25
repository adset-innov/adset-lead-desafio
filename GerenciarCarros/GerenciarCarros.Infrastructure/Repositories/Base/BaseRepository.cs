using GerenciarCarros.Domain.Entities;
using GerenciarCarros.Domain.Entities.Base;
using GerenciarCarros.Domain.Pagination;
using GerenciarCarros.Domain.Repositories.Base;
using GerenciarCarros.Infrastructure.Context;
using Microsoft.EntityFrameworkCore;

namespace GerenciarCarros.Infrastructure.Repositories.Base
{
    public abstract class BaseRepository<TEntity> : IBaseRepository<TEntity> where TEntity : BaseEntity, new()
    {
        protected readonly CarrosDBContext Db;
        protected readonly DbSet<TEntity> DbSet;

        protected BaseRepository(CarrosDBContext db)
        {
            Db = db;
            DbSet = db.Set<TEntity>();
        }
        public virtual async Task Adicionar(TEntity entity)
        {
            DbSet.Add(entity);
            await SaveChanges();
        }

        public virtual async Task Atualizar(TEntity entity)
        {
            DbSet.Update(entity);
            await SaveChanges();
        }

        public void Dispose()
        {
            Db?.Dispose();
        }

        public virtual async Task<TEntity> ObterPorId(Guid id)
        {
            return await DbSet.FindAsync(id);
        }

        public virtual async Task<List<TEntity>> ObterTodos()
        {
            return await DbSet.ToListAsync();
        }

        public virtual async Task Remover(Guid id)
        {
            DbSet.Remove(new TEntity { Id = id });
            await SaveChanges();
        }
        public async Task<int> SaveChanges()
        {
            return await Db.SaveChangesAsync();
        }
    }
}
