using API.Entidades;

namespace API.Repositorios.Interfaces
{
    public interface IFotoRepository
    {
        Task<Foto> ObterFotoPeloIdAsync(int id);
        Task EditarFotoAsync(Foto foto);
        Task DeletarFotoAsync(Foto foto);
        Task InserirFotoAsync(Foto foto);
    }
}
