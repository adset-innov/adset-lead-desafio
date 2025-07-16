using Adsetdesafio.Application.DTOs;
using Adsetdesafio.Application.Services.CarServices;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Adsetdesafio.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CarsController : ControllerBase
    {
        private readonly PutCarsService _putService;
        private readonly GetCarsService _getService;
        private readonly DeleteCarsService _deleteService;

        public CarsController(PutCarsService putService, GetCarsService getService, DeleteCarsService deleteService)
        {
            _putService = putService;
            _getService = getService;
            _deleteService = deleteService;
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetById(long id)
        {
            await _getService.GetById(id);
            return _getService.Return(this);
        }

        [HttpGet("Resume")]
        public async Task<IActionResult> GetResume()
        {
            await _getService.GetResume();
            return _getService.Return(this);
        }

        [HttpGet("Filtered")]
        public async Task<IActionResult> GetFiltered([FromQuery] CarsFilterDTO dto)
        {
            await _getService.GetByFilter(dto);
            return _getService.Return(this);
        }

        [HttpPut]
        public async Task<IActionResult> Put(CarsDTO dto)
        {
            await _putService.Put(dto);
            return _putService.Return(this);
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(long id)
        {
            await _deleteService.Delete(id);
            return _deleteService.Return(this);
        }
    }
}
