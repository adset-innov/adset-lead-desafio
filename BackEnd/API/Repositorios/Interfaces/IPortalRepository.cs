using API.Entidades;

namespace API.Repositorios.Interfaces
{
    public interface IPortalRepository
    {
        Task<List<Portal>> Listar();
        Task<Portal?> ObterPorId(int id);
    }
}
