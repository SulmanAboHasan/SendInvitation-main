using DemoUsersManagementCommandSide.Events;

namespace DemoUsersManagementCommandSide.Abstraction
{
    public interface IAggregate
    {
        string Id { get; }
        int Sequence { get; }
        IReadOnlyList<Event> GetUncommittedEvents();
        void MarkChangesAsCommitted();

    }
}
