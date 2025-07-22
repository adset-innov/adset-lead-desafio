using ADSET.DESAFIO.APPLICATION.Common;
using ADSET.DESAFIO.APPLICATION.DTOs;
using ADSET.DESAFIO.APPLICATION.Handlers.Commands;
using ADSET.DESAFIO.APPLICATION.Handlers.Queries;
using ADSET.DESAFIO.APPLICATION.Interfaces;
using ADSET.DESAFIO.DOMAIN.Entities;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using ClosedXML.Excel;
using System.IO;

namespace ADSET.DESAFIO.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CarsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IExportFileService _exportFileService;
        public CarsController(IMediator mediator, IExportFileService exportFileService)
        {
            _mediator = mediator;
            _exportFileService = exportFileService;
        }

        [HttpGet("get-all")]
        public async Task<IActionResult> GetAll()
        {
            return Ok(await _mediator.Send(new GetAllCarQuery()));
        }

        [HttpGet("get-all-filter")]
        public async Task<IActionResult> GetAllFilter(int pageNumber, int pageSize, [FromQuery] CarFilterDTO filter)
        {
            PaginatedList<Car> result = await _mediator.Send(new GetAllFilterCarQuery(pageNumber, pageSize, filter));
            List<Car> listCar = await _mediator.Send(new GetAllCarQuery());
            int total = listCar.Count;
            int withPhotos = listCar.Where(c => c.Photos != null && c.Photos.Any()).Count();

            return Ok(new
            {
                pageIndex = result.PageIndex,
                totalPages = result.TotalPages,
                totalItems = listCar.Count,
                withPhotos = withPhotos,
                withoutPhotos = total - withPhotos,
                items = result
            });
        }

        [HttpGet("get-by-id/{id}")]
        public async Task<IActionResult> GetById(int id)
        {
            return Ok((Car)await _mediator.Send(new GetCarByIdQuery(id)));
        }

        [HttpGet("export-excel")]
        public async Task<IActionResult> ExportExcel()
        {
            return File(await _exportFileService.ExportToExcelAsync(), "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "cars.xlsx");
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