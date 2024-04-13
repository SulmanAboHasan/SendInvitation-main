using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Events;

namespace DemoUsersManagementCommandSide.Abstraction
{
    public interface IEventStore
    {       
        Task<List<Event>> GetAllAsync(string aggregateId, CancellationToken cancellationToken);
        Task CommiteAsync(IAggregate aggregate, CancellationToken cancellationtoken);
    }
}
