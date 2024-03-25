using GerenciarCarros.Application.Interfaces;
using GerenciarCarros.Application.Models;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace GerenciarCarros.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarroController : ControllerBase
    {
        private readonly ICarroService _carroService;
        public CarroController(ICarroService service)
        {
            _carroService = service;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(_carroService.ObterTodos());
        }

        [HttpPost("paginacao")]
        public async Task<IActionResult> GetPaginados([FromBody] PaginacaoCarroModel paginacao)
        {
            try
            {
                var carros = await _carroService.Paginacao(paginacao);
                return Ok(carros);
            }
            catch (Exception ex)
            {

                throw;
            }
           
        }


        [HttpGet("{id:guid}")]
        public async Task<IActionResult> Get(Guid id)
        {
            return Ok(await _carroService.ObterPorId(id));
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CarroModel value)
        {
            try
            {                
                return Ok(await _carroService.Adicionar(value));
            }
            catch (Exception)
            {

                return BadRequest();
            }
            
        }

        [HttpPut("{id:guid}")]
        public async Task<IActionResult> Put(Guid id, [FromBody] CarroModel value)
        {
            try
            {
                if (id != value.Id)
                {
                    return NotFound();
                }
                var carro = await _carroService.Atualizar(value);
                return Ok(carro);
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            try
            {
                await _carroService.Remover(id);
                return Ok();    
            }
            catch (Exception)
            {
                return Ok();
            }
        }

        [HttpDelete("imagem/{id:guid}")]
        public async Task<IActionResult> DeleteImage(Guid id)
        {
            try
            {
                await _carroService.RemoverImagem(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpDelete("opcional/{id:guid}")]
        public async Task<IActionResult> DeleteOpcional(Guid id)
        {
            try
            {
                await _carroService.RemoverOpcional(id);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("upload/{id:guid}")]
        public async Task<IActionResult> Upload([FromForm] IList<IFormFile> imagens, Guid id)
        {
            try
            {
                foreach (var item in imagens)
                {
                    var image = new ImagemModel
                    {
                        Tipo = item.ContentType,
                        IdCarro = id,
                        Nome =  item.FileName
                    };
                    if (item.Length > 0)
                    {
                        using (var ms = new MemoryStream())
                        {
                            item.CopyTo(ms);
                            var fileBytes = ms.ToArray();
                            image.Bytes = fileBytes;
                        }
                    }
                    
                    await _carroService.UploadImagem(image);
                }
                
                return Ok();
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }


        [HttpGet("totais/{tipo}")]
        public async Task<IActionResult> GetTotais(string tipo)
        {
            return Ok(await _carroService.ObterTotais(tipo));
        }

        [HttpGet("anos")]
        public async Task<IActionResult> GetAnos()
        {
            return Ok(await _carroService.Anos());
        }

        [HttpDelete("pacote/{id:guid}/{tipo:int}")]
        public async Task<IActionResult> DeletePacote(Guid id, int tipo)
        {
            try
            {
                await _carroService.RemoverPacote(id, tipo);
                return Ok();
            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        [HttpPost("vincularPacote")]
        public async Task<IActionResult> VincularPacote([FromBody] PacoteModel value)
        {
            try
            {
                return Ok(await _carroService.VincularPacote(value));
            }
            catch (Exception)
            {

                return BadRequest();
            }

        }
    }
}
