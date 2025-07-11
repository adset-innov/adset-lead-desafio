using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adsetdesafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        // Simulando dados fixos para exemplo
        private static readonly Dictionary<int, string> Cars = new()
    {
        { 1, "Fusca" },
        { 2, "Gol" },
        { 3, "Civic" }
    };

        [HttpGet("{id}")]
        public IActionResult GetById(int id)
        {
            if (Cars.TryGetValue(id, out var car))
                return Ok(new { Id = id, Nome = car });

            return NotFound(new { Message = $"Carro com ID {id} não encontrado." });
        }

    }
}
