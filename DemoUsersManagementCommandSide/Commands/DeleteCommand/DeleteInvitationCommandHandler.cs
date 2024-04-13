using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Commands.RejectInvitation;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace DemoUsersManagementCommandSide.Commands.DeleteCommand
{
    public class DeleteInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<DeleteInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;

        public async Task<string> Handle(DeleteInvitationCommand command, CancellationToken cancellationToken)
        {

            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if (events.Count == 0)
                throw new NotFoundException("the invitation Has benn Deleted..!");

            var invitation = InvitationMember.LoadFromHistory(events);

            invitation.Delete(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return command.Id;
        }
    }
}
