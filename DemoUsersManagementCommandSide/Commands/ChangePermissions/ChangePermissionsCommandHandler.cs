using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.ChangePermissions
{
    public class ChangePermissionsCommandHandler(IEventStore eventStore) : IRequestHandler<ChangePermissionsCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(ChangePermissionsCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if(events.Count == 0)
                throw new NotFoundException("there is no member in this subscription");

            var invitationMember = InvitationMember.LoadFromHistory(events);

            invitationMember.ChangePermission(command);

            await _eventStore.CommiteAsync(invitationMember, cancellationToken);

            return command.Id;
        }
    }
}
