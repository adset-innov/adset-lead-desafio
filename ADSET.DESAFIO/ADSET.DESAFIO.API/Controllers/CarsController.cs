using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.APPLICATION.Handlers.Queries;
using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace ADSET.DESAFIO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;
        public CarsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll([FromQuery] CarFilterDTO filter)
        {
            return Ok((List<Car>)await _mediator.Send(new GetAllCarQuery(filter)));
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok((Car)await _mediator.Send(new GetCarByIdQuery(id)));
        }

        [HttpPost("register")]
        public async Task<IActionResult> Create([FromForm] CarCreateDTO dto)
        {
            Car? created = await _mediator.Send(new RegisterCarCommand(dto));
            return CreatedAtAction("GetById", new { id = created.Id }, created);
        }

        [HttpPut("update")]
        public async Task<IActionResult> Update(int id, [FromForm] CarUpdateDTO dto)
        {
            return Ok(await _mediator.Send(new UpdateCarCommand(id, dto)));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int id)
        {
            await _mediator.Send(new DeleteCarCommand(id));
            return NoContent();
        }
    }
}