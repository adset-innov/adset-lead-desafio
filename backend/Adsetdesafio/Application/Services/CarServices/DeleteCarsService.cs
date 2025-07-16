using Adsetdesafio.Domain.Infraestructure.Interfaces;
using Adsetdesafio.Domain.Models.Entities;
using Adsetdesafio.Shared.Utils.AppServiceBase;

namespace Adsetdesafio.Application.Services.CarServices
{
    public class DeleteCarsService : DefaultReturn
    {
        private readonly ICarRepository _carsRepository;

        public DeleteCarsService(ICarRepository carsRepository)
        {
            _carsRepository = carsRepository;
        }

        public async Task Delete(long id)
        {
            Car entity = _carsRepository.GetEntity(x => x.Id == id);

            if (entity != null)
            {
                _carsRepository.Delete(entity);
                await _carsRepository.SaveChangesAsync();
                StatusCode = System.Net.HttpStatusCode.OK;
                Message.Add("Deleted successfully");
                return;
            }
        }
    }
}
