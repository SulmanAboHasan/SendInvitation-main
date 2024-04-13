using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Repository
{
    public class InvitationRepository(InvitationDbContext context) : TasksRepository<Invitation>(context), IInvitationRepository
    {
        private readonly InvitationDbContext _context = context;

        public async Task ChangeStatusAsync(Invitation entity)
        {
            var invite = await _context.invitations.FirstOrDefaultAsync(i => i.Id == entity.Id);
            invite?.ChangeStatus(entity);
        }

        public async Task UpdateSequence(string aggregateId, int sequence)
        {
            var invite = await _context.invitations.FirstOrDefaultAsync(i => i.Id == aggregateId);
            invite?.UpdateSequence(sequence);
        }
    }
}
