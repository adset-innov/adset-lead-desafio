using adset_api.Aplicacao.Interfaces;
using adset_api.DTOs;
using Microsoft.AspNetCore.Mvc;

namespace adset_api.Controllers
{
    [ApiController]
    [Route("v1/[controller]")]
    public class VeiculoController : ControllerBase
    {
        #region Propriedades controller
        private readonly IVeiculoService _serviceVeiculo;

        public VeiculoController(IVeiculoService serviceVeiculo) => _serviceVeiculo = serviceVeiculo;

        #endregion

        #region Actions Results

        [HttpGet("{id:int}")]
        public async Task<IActionResult> ConsultarPorId(int id)
        {
            try
            {
                var response = await _serviceVeiculo.ConsultarPorId(id);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }

        }

        [HttpGet]
        public async Task<IActionResult> ConsultaPorFiltro([FromQuery] BuscarVeiculoRequestDTO request)
        {
            try
            {
                var response = await _serviceVeiculo.ConsultarVeiculosComFiltro(request);
                return Ok(new
                {
                    Content = response.Items,
                    response.TotalCarrosCadastrados,
                    response.TotalCarrosFiltrados,
                    response.TotalCarrosComFotos,
                    response.TotalCarrosSemFotos,
                    response.Cores,
                    request.Page,
                    request.PageSize,
                    response.TotalPaginas,
                });
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NovoVeiculoRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Os campos não foram preenchidos corretamente.",
                        errors = ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            )
                    });
                }

                var response = await _serviceVeiculo.CreateVeiculo(request);
                return Ok(new { Model = response });
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }

        }

        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] VeiculoUpdateRequestDTO request)
        {
            try
            {
                var response = await _serviceVeiculo.UpdateVeiculo(request);
                return Ok(new { RegistrosAlterados = response });
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var response = await _serviceVeiculo.DeleteVeiculo(id);
                return NoContent();
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
        }

        #endregion

        #region Métodos Auxiliares
        private IActionResult SendBadRequest(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }

        #endregion
    }
}
