namespace AdSet.Application.UseCases.Optionals
{
    public interface IGetAllOptionals : IUseCaseWithResponse<List<OptionalViewModel>> { }

    public class GetAllOptionals : IGetAllOptionals
    {
        private readonly IOptionalRepository optionalRepository;

        public GetAllOptionals(IOptionalRepository optionalRepository)
        {
            this.optionalRepository = optionalRepository;
        }

        public async Task<List<OptionalViewModel>> Execute()
        {
            var result = await optionalRepository.GetAll();

            return result.Select(o => new OptionalViewModel
            {
                Id = o.Id,
                Nome = o.Name
            }).ToList();
        }
    }
}
