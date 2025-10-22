namespace API.Repositorios.Interfaces
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> ObterPeloId(int id);
        Task<List<T>> Listar();
        Task<T> Inserir(T entity);
        Task Atualizar(T entity);
        Task Deletar(int id);
    }
}
