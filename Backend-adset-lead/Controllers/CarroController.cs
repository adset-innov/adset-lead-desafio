using Backend_adset_lead.DTOs;
using Backend_adset_lead.Models;
using Backend_adset_lead.Services;
using Microsoft.AspNetCore.Mvc;

namespace Backend_adset_lead.Controllers
{
    [Route("v1/[controller]")]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _service;

        public CarroController(ICarroService service) => _service = service;

        [HttpPost]
        public async Task<IActionResult> Add([FromBody] NovoCarroRequestDTO request)
        {
            try
            {
                if (!ModelState.IsValid)
                {
                    return BadRequest(new
                    {
                        message = "Campos obrigatórios não foram preenchidos.",
                        errors = ModelState
                            .Where(x => x.Value.Errors.Count > 0)
                            .ToDictionary(
                                kvp => kvp.Key,
                                kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                            )
                    });
                }
                return Ok(new {message = "Post", request});
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
            
        }

        [HttpGet]
        public async Task<IActionResult> GetFiltered([FromQuery] BuscaCarroRequestDTO request)
        {
            try
            {
                return Ok(new { message = "Get", request });
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
                return Ok(new { message = "Delete", id });
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Alterar([FromBody] Carro request)
        {
            try
            {
                return Ok(new { message = "Put", request });
            }
            catch (Exception ex)
            {
                return SendBadRequest(ex);
            }
        }

        private IActionResult SendBadRequest(Exception ex)
        {
            return BadRequest(new { message = ex.Message });
        }
    }
}
