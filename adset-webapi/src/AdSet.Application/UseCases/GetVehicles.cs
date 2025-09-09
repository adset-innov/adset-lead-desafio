namespace AdSet.Application.UseCases
{
    public interface IGetVehicles  : IUseCaseWithResponse<IEnumerable<VehiclesViewModel>> { }

    public class GetVehicles : IGetVehicles
    {
        private readonly IViewModelMapper mapper;
        public GetVehicles(IViewModelMapper mapper)
        {
            this.mapper = mapper;
        }
  

        public async Task<IEnumerable<VehiclesViewModel>> Execute()
        {
            return new List<VehiclesViewModel>();
        }
    }
}
