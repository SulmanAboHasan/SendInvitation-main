using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoUsersManagementCommandSide.Commands.RejectInvitation
{
    public class RejectInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<RejectInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;

        public async Task<string> Handle(RejectInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if (events.Count == 0)
                throw new NotFoundException("The invitation not found..!");

            var invitation = InvitationMember.LoadFromHistory(events);

            invitation.Reject(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return command.Id;
        }
    }
}
