using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.LeaveMember
{
    public class LeaveMemberCommandHandler(IEventStore eventStore) : IRequestHandler<LeaveMemberCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(LeaveMemberCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if (events.Count == 0)
                throw new NotFoundException("this member not joined before..!");

            var leaveMember = InvitationMember.LoadFromHistory(events);

            leaveMember.LeaveMember(command);

            await _eventStore.CommiteAsync(leaveMember, cancellationToken);

            return command.Id;
        }
    }
}
