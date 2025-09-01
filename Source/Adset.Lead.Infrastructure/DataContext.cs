using Adset.Lead.Domain.Abstractions;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Adset.Lead.Infrastructure;

public sealed class DataContext : DbContext, IUnitOfWork
{
    private readonly IServiceProvider _serviceProvider;
    
    public DataContext(DbContextOptions<DataContext> options,
        IServiceProvider serviceProvider) : base(options)
    {
        _serviceProvider =  serviceProvider;
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(typeof(DataContext).Assembly);

        base.OnModelCreating(modelBuilder);
    }
    
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        var result = await base.SaveChangesAsync(cancellationToken);

        await PublicDomainEventsAsync();
        
        return result;
    }
    
    private async Task PublicDomainEventsAsync()
    {
        var domainEvents = ChangeTracker
            .Entries<Entity>()
            .Select(entry => entry.Entity)
            .SelectMany(entity =>
            {
                var domainEvents = entity.GetDomainEvents();
                entity.ClearDomainEvents();
                return domainEvents;
            }).ToList();

        var publisher = _serviceProvider.GetService<IPublisher>();
        if (publisher != null)
        {
            foreach (var domainEvent in domainEvents)
            {
                await publisher.Publish(domainEvent);
            }
        }
    }
}