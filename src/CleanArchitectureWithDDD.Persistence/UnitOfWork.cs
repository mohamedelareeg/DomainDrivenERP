using CleanArchitectureWithDDD.Domain.Abstractions.Persistence;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Persistence.Outbox;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Newtonsoft.Json;

namespace CleanArchitectureWithDDD.Persistence;

// Acting like a Transaction Boundary
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public UnitOfWork(ApplicationDbContext context)
    {
        _context = context;
    }
    //UnitOfWork Pattern & Move Outbox Interceptor and Auditable Interceptor inside UnitOfWork
    public Task SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        //Move The Logic From Interceptors To UnitOfWork SaveChangesAsync
        ConvertDomainEventsToOutboxMessages();
        UpdateAuditableEntities();
        return _context.SaveChangesAsync(cancellationToken);
    }
    private void ConvertDomainEventsToOutboxMessages()
    {
        var outboxMessages = _context.ChangeTracker
         .Entries<AggregateRoot>()
         .Select(x => x.Entity)
         .SelectMany(aggregateRoot =>
         {
             IReadOnlyCollection<IDomainEvent> domainEvents = aggregateRoot.GetDomainEvents();

             aggregateRoot.ClearDomainEvents();

             return domainEvents;
         })
         .Select(domainEvent => new OutboxMessage
         {
             Id = Guid.NewGuid(),
             OccurredOnUtc = DateTime.UtcNow,
             Type = domainEvent.GetType().Name,
             Content = JsonConvert.SerializeObject(
                 domainEvent,
                 new JsonSerializerSettings
                 {
                     TypeNameHandling = TypeNameHandling.All
                 })
         })
         .ToList();

        _context.Set<OutboxMessage>().AddRange(outboxMessages);
    }
    private void UpdateAuditableEntities()
    {
        IEnumerable<EntityEntry<IAuditableEntity>> entries =
         _context
             .ChangeTracker
             .Entries<IAuditableEntity>();

        foreach (EntityEntry<IAuditableEntity> entityEntry in entries)
        {
            if (entityEntry.State == EntityState.Added)
            {
                entityEntry.Property(a => a.CreatedOnUtc).CurrentValue = DateTime.UtcNow;
            }

            if (entityEntry.State == EntityState.Modified)
            {
                entityEntry.Property(a => a.ModifiedOnUtc).CurrentValue = DateTime.UtcNow;
            }
        }
    }
}
