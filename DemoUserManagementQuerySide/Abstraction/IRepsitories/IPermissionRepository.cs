using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.Abstraction.IRepsitories
{
    public interface IPermissionRepository : ITasksRepository<Permission>
    {
        Task ChangePermissions(Permission entity);
        Task UpdateSequence(string aggregateId, int sequence);
    }
}
