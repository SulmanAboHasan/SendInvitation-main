using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;
using System.Security.Cryptography.Xml;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Repository
{
    public class PermissionRepository(InvitationDbContext context) : TasksRepository<Permission>(context), IPermissionRepository
    {
        private readonly InvitationDbContext _context = context;

        public async Task ChangePermissions(Permission entity)
        {
            var permission = await _context.permissions.FirstOrDefaultAsync(p =>
            p.Id == entity.Id);
            permission?.ChangePermission(entity);
        }

        public async Task UpdateSequence(string aggregateId, int sequence)
        {
            var permission = await _context.permissions.FirstOrDefaultAsync(p =>
            p.Id == aggregateId);
            permission?.UpdateSequence(sequence);
        }
    }
}
