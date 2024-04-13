using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Commands.SendInvitaionRequest;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Exceptions;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.AcceptInvitation
{
    public class AcceptInvitationCommandHandler(IEventStore eventStore) : IRequestHandler<AcceptInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;

        public async Task<string> Handle(AcceptInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync(command.Id, cancellationToken);

            if (events.Count == 0)
                throw new NotFoundException("The invitation not found ..!");

            var invitation = InvitationMember.LoadFromHistory(events);

            invitation.Accept(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return command.Id;

        }
    }
}
