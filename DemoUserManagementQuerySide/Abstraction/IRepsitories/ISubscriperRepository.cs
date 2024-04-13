using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.Abstraction.IRepsitories
{
    public interface ISubscriperRepository : ITasksRepository<Subscriper>
    {
        Task ChangeStatusAsync(Subscriper entity);
        Task UpdateSequence(string aggregateId, int sequence);
    }
}
