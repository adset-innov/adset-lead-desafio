using AutoMapper;

namespace AdSet.Application.UseCases
{
    public interface ISearchVehicles : IUseCase<SearchVehiclesRequest, PagedList<VehicleResponseViewModel>> { }

    public class SearchVehicles : ISearchVehicles
    {
        private readonly IMapper mapper;
        private readonly IVehiclesRepository vehiclesRepository;
        
        public SearchVehicles(IMapper mapper, IVehiclesRepository vehiclesRepository)
        {
            this.mapper = mapper;
            this.vehiclesRepository = vehiclesRepository;
        }

        public async Task<PagedList<VehicleResponseViewModel>> Execute(SearchVehiclesRequest request)
        {
            var result = await vehiclesRepository.Search(request.filters, request.CurrentPage, request.PageSize);   
            var mappedItems = mapper.Map<List<VehicleResponseViewModel>>(result.Items);

            return new PagedList<VehicleResponseViewModel>(
                mappedItems,
                result.TotalPages,
                result.CurrentPage,
                result.PageSize
            );
        }
    }

    public record SearchVehiclesRequest(SearchVehiclesFilter filters, int CurrentPage, int PageSize);
}
