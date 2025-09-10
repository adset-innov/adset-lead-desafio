using AutoMapper;

namespace AdSet.Application.UseCases
{
    public interface ISearchVehicles : IUseCase<SearchVehiclesRequest, PagedList<VehiclesViewModel>> { }

    public class SearchVehicles : ISearchVehicles
    {
        private readonly IMapper mapper;
        private readonly IVehiclesRepository vehiclesRepository;
        
        public SearchVehicles(IMapper mapper, IVehiclesRepository vehiclesRepository)
        {
            this.mapper = mapper;
            this.vehiclesRepository = vehiclesRepository;
        }

        public async Task<PagedList<VehiclesViewModel>> Execute(SearchVehiclesRequest request)
        {
            var result = await vehiclesRepository.Search(request.filters, request.CurrentPage, request.PageSize);
            var pagedVehiclesResult = mapper.Map<PagedList<VehiclesViewModel>>(result);
            return pagedVehiclesResult;
        }
    }

    public record SearchVehiclesRequest(SearchVehiclesFilter filters, int CurrentPage, int PageSize);
}
