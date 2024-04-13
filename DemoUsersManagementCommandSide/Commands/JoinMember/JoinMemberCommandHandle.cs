using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.JoinMember
{
    public class JoinMemberCommandHandle(IEventStore eventStore): IRequestHandler<JoinMemberCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;
        public async Task<string> Handle(JoinMemberCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync($"{command.SubscriptionId}_{command.MemberId}", cancellationToken);

            InvitationMember invitation;

            if (events.Count != 0)
                invitation = InvitationMember.LoadFromHistory(events);
            else
                invitation = new();

            invitation.JoinMember(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return invitation.Id;

        }
    }
}
