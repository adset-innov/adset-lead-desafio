using API.Dtos.Foto;
using API.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class FotoController : ControllerBase
    {
        private readonly IFotoService _service;
        public FotoController(IFotoService service)
        {
            _service = service;
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> EditarFoto(int id,  IFormFile foto)
        {
            if (id <= 0)
                return BadRequest("O ID da foto é inválido.");

            if (foto == null || foto.Length == 0)
                return BadRequest("Nenhum arquivo de foto foi enviado.");

            FotoDto fotoEditada = await _service.EditarFotoVeiculoAsync(id, foto);
            return Ok(fotoEditada);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletarFoto(int id)
        {
            if (id <= 0)
                return BadRequest("O ID da foto é inválido.");

            await _service.DeletarFotoAsync(id);
            return Ok();
        }

        [HttpPost("{id}")]
        public async Task<IActionResult> CadastrarFotosVeiculo(int id, [FromForm] List<IFormFile> fotos)
        {
            await _service.CadastrarFotos(id, fotos);
            return Ok();
        }

    }
}
