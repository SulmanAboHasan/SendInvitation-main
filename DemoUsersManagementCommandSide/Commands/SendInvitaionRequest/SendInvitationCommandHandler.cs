using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Events;
using DemoUsersManagementCommandSide.Events.DataType;
using DemoUsersManagementCommandSide.Exceptions;
using DemoUsersManagementCommandSide.Extensions;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.SendInvitaionRequest
{
    public class SendInvitationCommandHandler(IEventStore eventStore): IRequestHandler<SendInvitationCommand, string>
    {
        private readonly IEventStore _eventStore = eventStore;

        public async Task<string> Handle(SendInvitationCommand command, CancellationToken cancellationToken)
        {
            var events = await _eventStore.GetAllAsync($"{command.SubscriptionId}_{command.MemberId}", cancellationToken);
            
            InvitationMember invitation;

            if(events.Count != 0)
                invitation = InvitationMember.LoadFromHistory(events);
            else
                invitation = new();


            invitation.Send(command);

            await _eventStore.CommiteAsync(invitation, cancellationToken);

            return invitation.Id;
        }
        
    }
}
