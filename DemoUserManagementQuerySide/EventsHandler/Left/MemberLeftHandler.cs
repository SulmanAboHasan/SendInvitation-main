using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Left
{
    public class MemberLeftHandler(IUnitOfWork unitOfWork, ILogger<InvitationSentHandler> logger) : IRequestHandler<MemberLeft, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(MemberLeft @event, CancellationToken cancellationToken)
        {
            var subscriber = await _unitOfWork.Subscriper.GetAsync(s => s.Id == @event.AggregateId);

            if (subscriber is null)
            {
                _logger.LogWarning("Subscriber {AggregateId} not found", @event.AggregateId);
                return false;
            }

            if (@event.Sequence <= subscriber.Sequence) return true;

            if (@event.Sequence > subscriber.Sequence + 1)
            {
                _logger.LogWarning("{Sequence} is not the expected sequence for subscriber {AggregateId}", @event.Sequence, @event.AggregateId);
                return false;
            }

            await _unitOfWork.Subscriper.ChangeStatusAsync(Subscriper.MemberLeftEvent(@event));

            var permssions = await _unitOfWork.Permission.GetAsync(p => p.Id == @event.AggregateId);

            if (permssions is not null)
                await _unitOfWork.Permission.RemoveAsync(permssions);

            await _unitOfWork.Invitation.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
