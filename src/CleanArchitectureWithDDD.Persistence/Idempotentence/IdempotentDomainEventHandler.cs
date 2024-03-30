using CleanArchitectureWithDDD.Application.Abstractions.Messaging;
using CleanArchitectureWithDDD.Domain.Primitives;
using CleanArchitectureWithDDD.Persistence.Outbox;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CleanArchitectureWithDDD.Persistence.Idempotentence
{
    public sealed class IdempotentDomainEventHandler<TDomainEvent> : IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly INotificationHandler<TDomainEvent> _decorated;
        private readonly ApplicationDbContext _context;

        public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated, ApplicationDbContext context)
        {
            _decorated = decorated;
            _context = context;
        }

        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            //TODO: Search on How to Order The DomainEvents if it has many Handlers for one DomainEvent
            string consumer = _decorated.GetType().Name;
            bool exists = await _context.Set<OutboxMessageConsumer>()
                .AnyAsync(outboxMessageConsumer =>
                    outboxMessageConsumer.Id == notification.Id &&
                    outboxMessageConsumer.Name == consumer,
                cancellationToken);
            if (exists)
            {
                return;
            }

            await _decorated.Handle(notification, cancellationToken);

            var outboxMessageConsumer = new OutboxMessageConsumer
            {
                Id = notification.Id,
                Name = consumer
            };
            _context.Set<OutboxMessageConsumer>().Add(outboxMessageConsumer);

            await _context.SaveChangesAsync(cancellationToken);
        }

    }
}
