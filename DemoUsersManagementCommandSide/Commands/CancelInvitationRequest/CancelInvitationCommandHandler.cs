using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoUsersManagementCommandSide.Commands.CancelInvitationRequest
{
    public class CancelInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<CancelInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;

        public async Task<string> Handle(CancelInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if (events.Count == 0)
                throw new NotFoundException("The Invitation not found ..!");

            var invitation = InvitationMember.LoadFromHistory(events);

            invitation.Canceled(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return command.Id;

        }


    }
}
