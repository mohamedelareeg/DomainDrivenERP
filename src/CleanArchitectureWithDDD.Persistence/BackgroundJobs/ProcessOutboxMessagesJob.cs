using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Persistence.OutBox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Polly;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.BackgroundJobs
{
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
        
        public async Task Execute(IJobExecutionContext context)
        {
            try
            {
                var messages = await _context.Set<OutboxMessage>()
                    .Where(a => a.ProcessedOnUtc == null)
                    .Take(20)
                    .ToListAsync(context.CancellationToken);
                foreach (var message in messages)
                {
                    var domainEvent = JsonConvert.DeserializeObject<IDomainEvent>(
                        message.Content,
                        jsonSerializerSettings);
                    if (domainEvent is null)
                    {
                        //TODO : Logging
                        continue;
                    }
                    var policy = Policy
                        .Handle<Exception>()
                        .WaitAndRetryAsync(
                            3, 
                            attempt => TimeSpan.FromMicroseconds(50 * attempt));
                    var result = await policy.ExecuteAndCaptureAsync(() => 
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

                //TODO Logging
            }
           
        }
    }
}
