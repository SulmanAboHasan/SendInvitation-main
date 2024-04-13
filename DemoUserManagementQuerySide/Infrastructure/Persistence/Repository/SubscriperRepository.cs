using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using Microsoft.EntityFrameworkCore;

namespace DemoUserManagementQuerySide.Infrastructure.Persistence.Repository
{
    public class SubscriperRepository(InvitationDbContext context) : TasksRepository<Subscriper>(context), ISubscriperRepository
    {
        private readonly InvitationDbContext _context = context;

        public async Task ChangeStatusAsync(Subscriper entity)
        {
            var subscriper = await _context.subscripers.FirstOrDefaultAsync(s => s.Id == entity.Id);
            subscriper?.ChangeStatus(entity);
        }

        public async Task UpdateSequence(string aggregateId, int sequence)
        {
            var subscriper = await _context.subscripers.FirstOrDefaultAsync(s => s.Id == aggregateId);
            subscriper?.UpdateSequence(sequence);
        }
    }
}
