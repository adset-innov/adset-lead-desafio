using API.Dtos.Foto;
using API.Dtos.Opcional;
using API.Dtos.RelacaoVeiculoPacotePortal;
using API.Dtos.Veiculo;
using API.Filtro;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{


    [ApiController]
    [Route("api/[controller]")]
    public class VeiculoController : ControllerBase
    {
        private readonly IVeiculoService _service;

        public VeiculoController(IVeiculoService service)
        {
            _service = service;
        }

        [HttpPost]
        public async Task<IActionResult> CadastrarVeiculo([FromForm] CadastrarVeiculoDto veiculo)
        {
            CadastrarVeiculoDto veiculoInserido = await _service.CadastrarVeiculoAsync(veiculo);
            return CreatedAtAction(nameof(ObterVeiculoPeloID), new { id = veiculoInserido.Id }, veiculoInserido);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> AtualizarVeiculo(int id, [FromBody] AtualizarVeiculoDto veiculo)
        {
            if (!veiculo.Id.HasValue)
                return BadRequest("O id do veiculo não foi fornecido em seu corpo");

            if (id != veiculo.Id || id.Equals(0))
                return BadRequest("O id fornecido não identifica o recurso");

            AtualizarVeiculoDto veiculoAtualizado = await _service.AtualizarVeiculoAsync(veiculo);
            return Ok(veiculoAtualizado);

        }

        [HttpGet("{id}")]
        public async Task<IActionResult> ObterVeiculoPeloID(int id)
        {
            if (id <= 0)
                return BadRequest("O ID do veículo é inválido.");

            VeiculoDto veiculo = await _service.ObterPorIdAsync(id);
            return Ok(veiculo);
        }


        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarVeiculo(int id)
        {
            if ((id <= 0))
                return BadRequest("O ID do veículo é inválido.");

            await _service.DeletarVeiculoAsync(id);
            return NoContent();
        }


        [HttpGet("Opcionais")]
        public async Task<IActionResult> ListarOpcionais()
        {
            List<OpcionalDto> opcionais = await _service.ListarOpcionaisAsync();
            return Ok(opcionais);

        }

        [HttpGet("Informacoes")]
        public async Task<IActionResult> ObterInformacoes()
        {
            InformacoesVeiculosDto informacoes = await _service.ObterInformacoesAsync();
            return Ok(informacoes);

        }

        [HttpGet]
        public async Task<IActionResult> ListarVeiculos([FromQuery] VeiculoFiltroDto filtro)
        {
            PaginacaoResultado<VeiculoDto> resultado = await _service.ListarVeiculosAsync(filtro);
            return Ok(resultado);
        }


        [HttpPut("Vinculos")]
        public async Task<IActionResult> AlterarVinculoVeiculoPacotePortal([FromBody] List<AtualizarRelacaoVeiculoPacotePortalDto> relacoes)
        {
            await _service.AtualizarRelacoesPacotePortalAsync(relacoes);
            return Ok();
        }

        [HttpGet("Cores")]
        public async Task<IActionResult> ListarCores()
        {
            List<string> cores = await _service.ObterCoresAsync();
            return Ok(cores);
        }


        [HttpGet("Fotos/{id}")]
        public async Task<IActionResult> ObterFotosPeloID(int id)
        {
            if (id <= 0)
                return BadRequest("O ID do veículo é inválido.");

            List<FotoDto> fotos = await _service.ObterFotosVeiculoAsync(id);
            return Ok(fotos);
        }


      



      
    }
}
