using API.Dtos.Foto;
using API.Dtos.Opcional;
using API.Dtos.RelacaoVeiculoPacotePortal;
using API.Dtos.Veiculo;
using API.Filtro;

namespace API.Services.Interfaces
{
    public interface IVeiculoService
    {
        Task<CadastrarVeiculoDto> CadastrarVeiculoAsync(CadastrarVeiculoDto veiculo);
        Task<AtualizarVeiculoDto> AtualizarVeiculoAsync(AtualizarVeiculoDto veiculo);
        Task<PaginacaoResultado<VeiculoDto>> ListarVeiculosAsync(VeiculoFiltroDto veiculoFiltro);
        Task DeletarVeiculoAsync(int id);
        Task<VeiculoDto> ObterPorIdAsync(int id);
        Task<List<OpcionalDto>> ListarOpcionaisAsync();
        Task<InformacoesVeiculosDto> ObterInformacoesAsync();
        Task AtualizarRelacoesPacotePortalAsync(List<AtualizarRelacaoVeiculoPacotePortalDto> relacoes);
        Task<List<string>> ObterCoresAsync();
        Task<List<FotoDto>> ObterFotosVeiculoAsync(int idVeiculo);
        


 
    }
}
