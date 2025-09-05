using adset_api.DTOs;

namespace adset_api.Aplicacao.Interfaces
{
    public interface IVeiculoService
    {
        Task<VeiculoResponseDTO> ConsultarPorId(int id);
        Task<ListaPaginasDTO<VeiculoResponseDTO>> ConsultarVeiculosComFiltro(BuscarVeiculoRequestDTO filtro);
        Task<int> CreateVeiculo(NovoVeiculoRequestDTO veiculo);
        Task<int> UpdateVeiculo(VeiculoUpdateRequestDTO veiculo);
        Task<int> DeleteVeiculo(int id);
    }
}
