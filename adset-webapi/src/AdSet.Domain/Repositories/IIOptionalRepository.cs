namespace AdSet.Domain.Repositories
{
    public interface IOptionalRepository
    {
        Task<Optional> Add(Optional optional);
        Task<List<Optional>> FindByNames(List<string> optionalNames);
    }
}
