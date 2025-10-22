using API.Dtos.Foto;
using API.Dtos.Opcional;
using API.Dtos.RelacaoVeiculoPacotePortal;
using API.Dtos.Veiculo;
using API.Entidades;
using API.Exceptions;
using API.Filtro;
using API.Repositorios.Interfaces;
using API.Services.Interfaces;
using AutoMapper;


namespace API.Services.Implementacoes
{
    public class VeiculoService : IVeiculoService
    {
        private readonly IVeiculoRepository _repository;
        private readonly IMapper _mapper;


        public VeiculoService(IVeiculoRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;

        }

        public async Task<AtualizarVeiculoDto> AtualizarVeiculoAsync(AtualizarVeiculoDto veiculoDto)
        {

            Veiculo? veiculoAtual = await _repository.ObterPeloId(veiculoDto.Id.Value);

            if (veiculoAtual == null)
                throw new NotFoundException("Veículo não encontrado.");

            Veiculo? veiculoAtualPlacaExistente = await _repository.ObterVeiculoPorPlaca(veiculoDto.Placa);

            if (veiculoAtualPlacaExistente != null && veiculoAtualPlacaExistente.Id != veiculoAtual.Id)
                throw new ConflictException("Já existe um veiculo com essa placa");

            _mapper.Map(veiculoDto, veiculoAtual);


            List<int>? novosOpcionaisIds = veiculoDto.Opcionais ?? new List<int>();

            List<int>? opcionaisAtuaisIds = veiculoAtual.RelacaoVeiculoOpcional
                                                 .Select(r => r.OpcionalId)
                                                 .ToList();


            List<RelacaoVeiculoOpcional> relacionamentosParaRemover = veiculoAtual.RelacaoVeiculoOpcional
                .Where(r => !novosOpcionaisIds.Contains(r.OpcionalId))
                .ToList();


            List<int>? idsParaAdicionar = novosOpcionaisIds
                .Where(id => !opcionaisAtuaisIds.Contains(id))
                .ToList();


            foreach (RelacaoVeiculoOpcional relacao in relacionamentosParaRemover)
            {
                veiculoAtual.RelacaoVeiculoOpcional.Remove(relacao);
            }

            foreach (int opcionalId in idsParaAdicionar)
            {
                veiculoAtual.RelacaoVeiculoOpcional.Add(new RelacaoVeiculoOpcional(opcionalId));
            }


            await _repository.Atualizar(veiculoAtual);

            return _mapper.Map<AtualizarVeiculoDto>(veiculoAtual);
        }

        public async Task<CadastrarVeiculoDto> CadastrarVeiculoAsync(CadastrarVeiculoDto cadastrarVeiculoDto)
        {
            Veiculo? veiculoComPlacaIgual = await _repository.ObterVeiculoPorPlaca(cadastrarVeiculoDto.Placa);

            if (veiculoComPlacaIgual != null)
                throw new ConflictException("Já existe um veiculo com essa placa");
            
            cadastrarVeiculoDto.Placa = cadastrarVeiculoDto.Placa.ToUpper();
            Veiculo veiculo = _mapper.Map<Veiculo>(cadastrarVeiculoDto);
            List<string> errosUpload = new List<string>();

            
            await _repository.Inserir(veiculo);

            if (cadastrarVeiculoDto.Fotos == null || !cadastrarVeiculoDto.Fotos.Any())
                return _mapper.Map<CadastrarVeiculoDto>(veiculo);

            try
            {

                string pastaVeiculo = Path.Combine(Directory.GetCurrentDirectory(), "Uploads", "veiculos", veiculo.Id.ToString());

                if (!Directory.Exists(pastaVeiculo))
                {
                    Directory.CreateDirectory(pastaVeiculo);
                }

                foreach (IFormFile fotoFile in cadastrarVeiculoDto.Fotos)
                {
                    try
                    {
                        string extensao = Path.GetExtension(fotoFile.FileName);
                        string nomeArquivoFisico = $"{Guid.NewGuid()}{extensao}"; 
                        string caminhoCompletoArquivo = Path.Combine(pastaVeiculo, nomeArquivoFisico);
                        string pathHttp = string.Concat("veiculos/", veiculo.Id,"/", nomeArquivoFisico);

                        using (FileStream stream = new FileStream(caminhoCompletoArquivo, FileMode.Create))
                        {
                            await fotoFile.CopyToAsync(stream);
                        }

                 
                        Foto fotoEntity = new Foto(
                            veiculo.Id,
                            fotoFile.FileName,
                            pathHttp
                        );

                        await _repository.AdicionarFoto(fotoEntity);
                    }
                    catch (Exception)
                    {
                        errosUpload.Add($"Falha ao salvar o arquivo: {fotoFile.FileName}");
                    }
                }

                await _repository.SalvarAlteracoesAsync();
            }
            catch (Exception)
            {
                errosUpload.Add("Erro de sistema: Não foi possível criar o diretório para as fotos.");
            }

            CadastrarVeiculoDto dto = _mapper.Map<CadastrarVeiculoDto>(veiculo);

            if (errosUpload.Any())
            {
                dto.ErrosUpload = "O veículo foi cadastrado, mas ocorreram os seguintes problemas: " + string.Join("; ", errosUpload);
            }

            return dto;
        }

        public async Task DeletarVeiculoAsync(int id)
        {
            Veiculo? veiculo = await _repository.ObterPeloId(id);
            if (veiculo == null)
            {
                throw new NotFoundException("Veículo não encontrado");
            }

            await _repository.Deletar(id);

            string caminhoFisicoFotos = Path.Combine(Directory.GetCurrentDirectory(), "Uploads","veiculos", veiculo.Id.ToString());

            if (Directory.Exists(caminhoFisicoFotos))
            {
                Directory.Delete(caminhoFisicoFotos, true);            
            }
            
        }

        public async Task<VeiculoDto> ObterPorIdAsync(int id)
        {
            Veiculo? veiculo = await _repository.ObterPeloId(id);

            if (veiculo == null)
            {
                throw new NotFoundException("Veículo não encontrado");
            }
            return _mapper.Map<VeiculoDto>(veiculo);
        }

        public async Task<List<OpcionalDto>> ListarOpcionaisAsync()
        {
            List<Opcional> opcionais = await _repository.ListarOpcionais();

            List<OpcionalDto> opcionaisDto = _mapper.Map<List<OpcionalDto>>(opcionais);

            return opcionaisDto;
        }

        public async Task<InformacoesVeiculosDto> ObterInformacoesAsync()
        {
            List<Veiculo> veiculos = await _repository.Listar();
            int totalVeiculos = veiculos.Count;
            int totalComFotos = veiculos.Count(v => v.Foto.Any());
            int totalSemFotos = totalVeiculos - totalComFotos;

            InformacoesVeiculosDto informacoes = new InformacoesVeiculosDto
            {
                Ids = veiculos.Select(v => v.Id).ToList(),
                Total = totalVeiculos,
                TotalFotos = totalComFotos,
                TotalSemFotos = totalSemFotos
            };

            return informacoes;
        }

        public async Task<PaginacaoResultado<VeiculoDto>> ListarVeiculosAsync(VeiculoFiltroDto filtro)
        {
            const int TAMANHO_MAXIMO_PAGINA = 10;
            if (filtro.TamanhoPagina <= 0)
                filtro.TamanhoPagina = 10;
            else if (filtro.TamanhoPagina > TAMANHO_MAXIMO_PAGINA)
                filtro.TamanhoPagina = TAMANHO_MAXIMO_PAGINA;

            if (filtro.Pagina <= 0)
                filtro.Pagina = 1;

            var (veiculos, totalRegistros) = await _repository.ListarPaginadoAsync(filtro);

            List<VeiculoDto> dtos = _mapper.Map<List<VeiculoDto>>(veiculos);

            return new PaginacaoResultado<VeiculoDto>
            {
                Itens = dtos,
                PaginaAtual = filtro.Pagina,
                TamanhoPagina = filtro.TamanhoPagina,
                TotalRegistros = totalRegistros
            };
        }

        public async Task AtualizarRelacoesPacotePortalAsync(List<AtualizarRelacaoVeiculoPacotePortalDto> relacoesDto)
        {

            if (relacoesDto == null || !relacoesDto.Any())
            {
                return;
            }


            List<int> veiculoIds = relacoesDto.Select(r => r.VeiculoId).Distinct().ToList();


            List<RelacaoVeiculoPacotePortal> relacoesAtuaisDoBanco = await _repository.ObterRelacoesPorVeiculoIds(veiculoIds);


            List<RelacaoVeiculoPacotePortal> relacoesParaAdicionar = new List<RelacaoVeiculoPacotePortal>();
            List<RelacaoVeiculoPacotePortal> relacoesParaRemover = new List<RelacaoVeiculoPacotePortal>();


            foreach (AtualizarRelacaoVeiculoPacotePortalDto dto in relacoesDto)
            {

                RelacaoVeiculoPacotePortal? relacaoExistente = relacoesAtuaisDoBanco.FirstOrDefault(
                    r => r.VeiculoId == dto.VeiculoId && r.PortalId == dto.PortalId
                );

                if (relacaoExistente != null)
                {

                    if (dto.PacoteId == null)
                    {

                        relacoesParaRemover.Add(relacaoExistente);
                    }
                    else if (relacaoExistente.PacoteId != dto.PacoteId)
                    {

                        relacaoExistente.SetPacoteId(dto.PacoteId.Value);
                    }
                }
                else
                {

                    if (dto.PacoteId != null)
                    {

                        RelacaoVeiculoPacotePortal novaRelacao = _mapper.Map<RelacaoVeiculoPacotePortal>(dto);
                        relacoesParaAdicionar.Add(novaRelacao);
                    }
                }
            }


            await _repository.AtualizarRelacoesEmMassa(relacoesParaAdicionar, relacoesParaRemover);
        }

        public async Task<List<string>> ObterCoresAsync()
        {
            List<string> cores = await _repository.ListarCoresDisponiveis();
            return cores;
        }

        public async  Task<List<FotoDto>> ObterFotosVeiculoAsync(int idVeiculo)
        {
            List<Foto> fotos = await _repository.ListarFotos(idVeiculo);
            List<FotoDto> fotosDto = _mapper.Map<List<FotoDto>>(fotos);
            return fotosDto;
        }


    
    }
}
