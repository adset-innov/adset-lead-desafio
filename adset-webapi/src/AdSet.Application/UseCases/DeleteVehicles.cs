namespace AdSet.Application.UseCases
{
    public interface IDeleteVehicles : IUseCaseWithRequest<int> { }

    public class DeleteVehicles : IDeleteVehicles
    {

        private readonly IVehiclesRepository vehiclesRepository;

        public DeleteVehicles(IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;
        }

        public async Task Execute(int id)
        {
            var vehicleEntity = await vehiclesRepository.FindById(id);
            await vehiclesRepository.Delete(vehicleEntity);
        }
    }
}
