using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Accepted
{
    public class InvitationAcceptedHandler(ILogger<InvitationSentHandler> logger, IUnitOfWork unitOfWork) : IRequestHandler<InvitationAccepted, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(InvitationAccepted @event, CancellationToken cancellationToken)
        {
            var invite = await _unitOfWork.Invitation.GetAsync(i => i.Id == @event.AggregateId);

            if (invite is null)
            {
                _logger.LogWarning("Invitation {AggregateId} not found", @event.AggregateId);
                return false;
            }

            if (@event.Sequence <= invite.Sequence) return true;

            if (@event.Sequence > invite.Sequence + 1)
            {
                _logger.LogWarning("{Sequence} is not the expected sequence for invitation {AggregateId}", @event.Sequence, @event.AggregateId);
                return false;
            }

            await _unitOfWork.Invitation.ChangeStatusAsync(Invitation.AcceptedEvent(@event));

            var subscriber = await _unitOfWork.Subscriper.GetAsync(s => s.Id == @event.AggregateId);
            if (subscriber is not null)
            {
                await _unitOfWork.Subscriper.ChangeStatusAsync(Subscriper.AcceptedEvent(@event));
            }
            else
            {
                await _unitOfWork.Subscriper.AddAsync(Subscriper.AcceptedEvent(@event), cancellationToken);
            }

            await _unitOfWork.Permission.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
