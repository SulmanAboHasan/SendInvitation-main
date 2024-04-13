using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.IncrementSequence
{
    public class UnknownEventHandler(IUnitOfWork unitOfWork, ILogger<InvitationSentHandler> logger) : IRequestHandler<UnknownEvent, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(UnknownEvent @event, CancellationToken cancellationToken)
        {
            var subscriber = await _unitOfWork.Subscriper.GetAsync(s => s.Id == @event.AggregateId);

            if (subscriber is null)
            {
                _logger.LogWarning("Aggregate {AggregateId} not found", @event.AggregateId);
                return false;
            }

            if (@event.Sequence <= subscriber.Sequence) return true;

            if (@event.Sequence > subscriber.Sequence + 1)
            {
                _logger.LogWarning("{Sequence} is not the expected sequence for aggregate {AggregateId}", @event.Sequence, @event.AggregateId);
                return false;
            }

            await _unitOfWork.Subscriper.UpdateSequence(@event.AggregateId, @event.Sequence);
            await _unitOfWork.Invitation.UpdateSequence(@event.AggregateId, @event.Sequence);
            await _unitOfWork.Permission.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
