using API.Dtos.Portal;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PortalController : ControllerBase
    {
        private readonly IPortalService _service;
        public PortalController(IPortalService service)
        {
            _service = service;
        }

        [HttpGet]
        public async Task<IActionResult> ListarPortais()
        {
            List<PortalDto> portais = await _service.ListarPortaisAsync();
            return Ok(portais);
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> ObterDetalhesPortal(int id)
        {
            if (id <= 0)
                return BadRequest("Id informado é invalido");

            DetalharPortalDto portalDetalhes = await _service.ObterPortalPorIDAsync(id);
            return Ok(portalDetalhes);
        }
    }
}
