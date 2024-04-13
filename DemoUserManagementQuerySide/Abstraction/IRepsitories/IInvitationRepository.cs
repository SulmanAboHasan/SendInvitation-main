using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.Abstraction.IRepsitories
{
    public interface IInvitationRepository : ITasksRepository<Invitation>
    {
        Task ChangeStatusAsync(Invitation entity);
        Task UpdateSequence(string aggregateId, int sequence);
    }
}
