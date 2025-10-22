
using API.Dtos.Veiculo;
using API.Entidades;


namespace API.Repositorios.Interfaces
{
    public interface IVeiculoRepository : IBaseRepository<Veiculo>
    {
        Task<List<Opcional>> ListarOpcionais();
        Task<(IEnumerable<Veiculo>, int totalRegistros)> ListarPaginadoAsync(VeiculoFiltroDto filtro);
        Task<List<RelacaoVeiculoPacotePortal>> ObterRelacoesPorVeiculoIds(List<int> veiculoIds);
        Task AtualizarRelacoesEmMassa(
            List<RelacaoVeiculoPacotePortal> paraAdicionar,
            List<RelacaoVeiculoPacotePortal> paraRemover);

        Task SalvarAlteracoesAsync();
        Task AdicionarFoto(Foto foto);

        Task<List<string>> ListarCoresDisponiveis();
        Task<List<Foto>> ListarFotos(int idVeiculo);
        Task<Veiculo> ObterVeiculoPorPlaca(string placa);

     




    }
}
