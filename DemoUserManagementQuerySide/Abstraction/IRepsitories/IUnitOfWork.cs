namespace DemoUserManagementQuerySide.Abstraction.IRepsitories
{
    public interface IUnitOfWork
    {
        IInvitationRepository Invitation { get; }
        ISubscriperRepository Subscriper { get; }
        IPermissionRepository Permission { get; }

        Task CommitAsync(CancellationToken cancellationToken);
    }
}
