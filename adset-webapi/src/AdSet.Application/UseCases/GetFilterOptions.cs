namespace AdSet.Application.UseCases 
{
    public interface IGetFilterOptions : IUseCaseWithRequest<FilterOptionsViewModel> { }

    public class GetFilterOptions : IGetFilterOptions
    {
        private readonly IVehiclesRepository vehiclesRepository;

        public GetFilterOptions(IVehiclesRepository vehiclesRepository)
        {
            this.vehiclesRepository = vehiclesRepository;

        }
        public async Task<FilterOptionsViewModel> Execute(FilterOptionsViewModel model)
        { 
            for (int year = 2000; year <= 2024; year++)
            {
                model.Years.Add(year);
            }

            model.PriceRanges.AddRange([ "10mil a 50mil", "50mil a 90mil", "+90mil" ]);
            model.Colors = await vehiclesRepository.SearchColorsDistinct();

            return model;
        }

        Task IUseCaseWithRequest<FilterOptionsViewModel>.Execute(FilterOptionsViewModel request)
        {
            throw new NotImplementedException();
        }
    }
}
