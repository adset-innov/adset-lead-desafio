using API.Contexto;
using API.Entidades;
using API.Repositorios.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace API.Repositorios.Implementacoes
{
    public class PortalRepository : IPortalRepository
    {
        private readonly AdSetContext _context;
        public PortalRepository(AdSetContext context) {

            _context = context;
        }
        public async Task<List<Portal>> Listar()
        {
            List<Portal> listaPortais = await _context.Portal.Include(p => p.Pacote).ToListAsync();
            return listaPortais;
        }

        public async Task<Portal?> ObterPorId(int id)
        {
            Portal? portal = await _context.Portal.Include(p => p.Pacote).Where(p => p.Id.Equals(id)).FirstOrDefaultAsync();
            return portal;
        }
    }
}
