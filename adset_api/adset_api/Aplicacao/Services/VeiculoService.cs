using adset_api.Aplicacao.Interfaces;
using adset_api.Data;
using adset_api.DTOs;
using adset_api.Repository.Interfaces;

namespace adset_api.Aplicacao.Services
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepositorio _repositorioVeiculo;

        public VeiculoService(IVeiculoRepositorio repositorioVeiculo) => _repositorioVeiculo = repositorioVeiculo;


        public async Task<VeiculoResponseDTO> ConsultarPorId(int idVeiculo)
        {
            var response = await _repositorioVeiculo.ConsultarPorId(idVeiculo);
            if (response == null) 
                throw new Exception("O veículo desejado não existe ou não foi encontrado!");

            var result = new VeiculoResponseDTO
            {
                IdVeiculo = response.IdVeiculo,
                Marca = response.Marca,
                Modelo = response.Modelo,
                Ano = response.Ano,
                Placa = response.Placa,
                Quilometragem = response.Quilometragem,
                Cor = response.Cor,
                Preco = response.Preco,
                ListaOpcionais = response.ListaOpcionais,
                PortalPacotes = response.Pacote.Select(p => new PacoteResponseDTO
                {
                    IdPacote = p.IdPacote,
                    IdVeiculo = p.IdVeiculo,
                    TipoPortal = p.TipoPortal,
                    TipoPacote = p.TipoPacote
                }).ToList(),
                Fotos = response.FotosVeiculo.Select(f => new FotoResponseDTO
                {
                    IdFotoVeiculo = f.IdFoto,
                    CaminhoUrl = f.CaminhoUrl,
                    IdVeiculo = f.IdVeiculo,
                }).ToList()
            };

            return result;
        }

        public async Task<ListaPaginasDTO<VeiculoResponseDTO>> ConsultarVeiculosComFiltro(BuscarVeiculoRequestDTO filtro)
        {
            var response = await _repositorioVeiculo.ConsultarComFiltro(filtro);

            var result = response.Items.Select(r => new VeiculoResponseDTO
            {
                IdVeiculo = r.IdVeiculo,
                Marca = r.Marca,
                Modelo = r.Modelo,
                Ano = r.Ano,
                Placa = r.Placa,
                Quilometragem = r.Quilometragem,
                Cor = r.Cor,
                Preco = r.Preco,
                ListaOpcionais = r.ListaOpcionais,
                PortalPacotes = r.Pacote.Select(p => new PacoteResponseDTO
                {
                    IdPacote = p.IdPacote,
                    IdVeiculo = p.IdVeiculo,
                    TipoPortal = p.TipoPortal,
                    TipoPacote = p.TipoPacote
                }).ToList(),
                Fotos = r.FotosVeiculo.Select(f => new FotoResponseDTO
                {
                    IdFotoVeiculo = f.IdFoto,
                    CaminhoUrl = f.CaminhoUrl,
                    IdVeiculo = f.IdVeiculo,
                }).ToList()
            }).ToList();

            return new ListaPaginasDTO<VeiculoResponseDTO>
            {
                Items = result,
                TotalPaginas = response.TotalPaginas,
                TotalCarrosCadastrados = response.TotalCarrosCadastrados,
                TotalCarrosFiltrados = response.TotalCarrosFiltrados,
                TotalCarrosComFotos = response.TotalCarrosComFotos,
                TotalCarrosSemFotos = response.TotalCarrosSemFotos,
                Cores = response.Cores,
            };
        }

        public async Task<int> CreateVeiculo(NovoVeiculoRequestDTO veiculo)
        {
            var novoVeiculo = new Veiculo
            {
                Marca = veiculo.Marca,
                Modelo = veiculo.Modelo,
                Ano = veiculo.Ano,
                Placa = veiculo.Placa,
                Quilometragem = veiculo.Quilometragem,
                Cor = veiculo.Cor,
                Preco = veiculo.Preco,
                ListaOpcionais = veiculo.ListaOpcionais,
                Pacote = veiculo.Pacotes
                .Select(p => new Pacote
                {
                    TipoPortal = p.TipoPortal,
                    TipoPacote = p.TipoPacote,
                }).ToList(),
                FotosVeiculo = veiculo.Fotos.Select(f => new FotoVeiculo
                {
                    CaminhoUrl = f.CaminhoUrl,
                }).ToList(),
            };

            return await _repositorioVeiculo.AddContext(novoVeiculo);
        }
        public async Task<int> UpdateVeiculo(VeiculoUpdateRequestDTO veiculo)
        {
            return await _repositorioVeiculo.UpdateContext(veiculo);
        }

        public async Task<int> DeleteVeiculo(int id)
        {
            return await _repositorioVeiculo.Delete(id);
        }
    }
}
