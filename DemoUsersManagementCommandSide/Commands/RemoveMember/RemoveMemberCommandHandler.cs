using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.RemoveMember
{
    public class RemoveMemberCommandHandler(IEventStore eventStore) : IRequestHandler<RemoveMemberCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(RemoveMemberCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if(events.Count == 0)
                throw new NotFoundException("there is no member in this subscription");

            var invitationMember = InvitationMember.LoadFromHistory(events);

            invitationMember.RemoveMember(command);

            await _eventStore.CommiteAsync(invitationMember, cancellationToken);

            return command.Id;
        }
    }
}
