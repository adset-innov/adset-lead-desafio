using adset_api.Data;
using adset_api.DTOs;

namespace adset_api.Repository.Interfaces
{
    public interface IVeiculoRepositorio
    {
        Task<Veiculo?> ConsultarPorId(int id);
        Task<ListaPaginasDTO<Veiculo>> ConsultarComFiltro(BuscarVeiculoRequestDTO filtro);
        Task<int> AddContext(Veiculo carro);
        Task<int> UpdateContext(VeiculoUpdateRequestDTO carro);
        Task<int> Delete(int id);
    }
}
