namespace AdSet.Lead.Domain.Interfaces;

public interface IEntity
{
    public Guid Id { get; }
    public DateTime CreatedOn { get; }
    public DateTime UpdatedOn { get; }
}