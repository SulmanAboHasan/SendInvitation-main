using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly InvitationDbContext _context;

        public IInvitationRepository Invitation {get; private set;}
                                                
        public ISubscriperRepository Subscriper {get; private set;}
                                                
        public IPermissionRepository Permission { get; private set; }

        public UnitOfWork(InvitationDbContext context)
        {
            _context = context;
            Invitation = new InvitationRepository(_context);
            Subscriper = new SubscriperRepository(_context);
            Permission = new PermissionRepository(_context);
        }
        public async Task CommitAsync(CancellationToken cancellationToken)
        {
            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
