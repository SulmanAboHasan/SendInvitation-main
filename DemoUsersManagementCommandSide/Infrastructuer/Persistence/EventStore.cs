using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Events;
using DemoUsersManagementCommandSide.Infrastructuer.MessageBus;
using Microsoft.Azure.Cosmos;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata;

namespace DemoUsersManagementCommandSide.Infrastructuer.Persistence
{
    public class EventStore(ApplicationDbContext context, ServiceBusPublisher publisher) : IEventStore
    {
        private readonly ApplicationDbContext _context = context;
        private readonly ServiceBusPublisher _publisher = publisher;

        public Task<List<Event>> GetAllAsync(string aggregateId, CancellationToken cancellationToken) =>
            _context.Events
            .Where(x => x.aggregateId == aggregateId)
            .OrderBy(x => x.Id)
            .ToListAsync(cancellationToken);


        public async Task CommiteAsync(IAggregate aggregate, CancellationToken cancellationToken)
        {
            var events = aggregate.GetUncommittedEvents();

            var messages = events.Select(x => new OutboxMessage(x));

            await _context.Events.AddRangeAsync(events, cancellationToken);
            await _context.OutboxMessages.AddRangeAsync(messages, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            _publisher.StartPublishing();
        }
    }

}
