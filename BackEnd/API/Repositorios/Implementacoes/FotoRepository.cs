using API.Contexto;
using API.Entidades;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositorios.Implementacoes
{
    public class FotoRepository : IFotoRepository
    {
        private readonly AdSetContext _context;

        public FotoRepository(AdSetContext context)
        {
            _context = context;
        }

        public async Task DeletarFotoAsync(Foto foto)
        {
            _context.Foto.Remove(foto);
            await _context.SaveChangesAsync();
        }

        public async Task EditarFotoAsync(Foto foto)
        {
            _context.Foto.Update(foto);
            await _context.SaveChangesAsync();
        }

        public async Task InserirFotoAsync(Foto foto)
        {
           await _context.Foto.AddAsync(foto);
           await _context.SaveChangesAsync();
        }

        public async Task<Foto> ObterFotoPeloIdAsync(int id)
        {
            return await _context.Foto.Where(f => f.Id == id).FirstOrDefaultAsync();
        }
    }
}
