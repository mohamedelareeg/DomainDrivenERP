using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Persistence.Data;
using CleanArchitectureWithDDD.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;

namespace CleanArchitectureWithDDD.Persistence.BackgroundJobs;

[DisallowConcurrentExecution]
public class ProcessOutboxMessagesJob : IJob
{
    private readonly ApplicationDbContext _context;
    private readonly IPublisher _publisher;
    public ProcessOutboxMessagesJob(ApplicationDbContext context, IPublisher publisher)
    {
        _context = context;
        _publisher = publisher;
    }
    private static readonly JsonSerializerSettings jsonSerializerSettings = new()
    {
        TypeNameHandling = TypeNameHandling.All,
    };
    // Idempotency >> Certain Operation Multiple times without change intial result
    public async Task Execute(IJobExecutionContext context)
    {
        try
        {
            List<OutboxMessage> messages = await _context.Set<OutboxMessage>()
                .Where(a => a.ProcessedOnUtc == null)
                .Take(20)
                .ToListAsync(context.CancellationToken);
            foreach (OutboxMessage? message in messages)
            {
                IDomainEvent? domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                    message.Content,
                    jsonSerializerSettings);
                if (domainEvent is null)
                {
                    // TODO : Logging
                    continue;
                }
                Polly.Retry.AsyncRetryPolicy policy = Policy
                    .Handle<Exception>()
                    .WaitAndRetryAsync(
                        3,
                        attempt => TimeSpan.FromMicroseconds(50 * attempt));
                PolicyResult result = await policy.ExecuteAndCaptureAsync(() =>
                    _publisher.Publish(
                        domainEvent,
                        context.CancellationToken));
                message.Error = result.FinalException?.ToString();
                message.ProcessedOnUtc = DateTime.UtcNow;

            }
            await _context.SaveChangesAsync();
        }
        catch (Exception ex)
        {
            // Its return duplicate 
            // Violation of PRIMARY KEY constraint 'PK_OutboxMessageConsumers'. Cannot insert duplicate key in object 'dbo.OutboxMessageConsumers'
            // TODO Logging
        }

    }
}
