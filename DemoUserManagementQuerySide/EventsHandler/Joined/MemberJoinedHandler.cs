using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Joined
{
    public class MemberJoinedHandler(IUnitOfWork unitOfWork, ILogger<InvitationSentHandler> logger) : IRequestHandler<MemberJoined, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(MemberJoined @event, CancellationToken cancellationToken)
        {
            var subscriber = await _unitOfWork.Subscriper.GetAsync(s => s.Id == @event.AggregateId);

            if (subscriber is not null)
            {
                if (@event.Sequence <= subscriber.Sequence) return true;

                if (@event.Sequence > subscriber.Sequence + 1)
                {
                    _logger.LogWarning("{Sequence} is not the expected sequence for subscriber {AggregateId}", @event.Sequence, @event.AggregateId);
                    return false;
                }

                await _unitOfWork.Subscriper.ChangeStatusAsync(Subscriper.MemberJoinedEvent(@event));
            }
            else
            {
                await _unitOfWork.Subscriper.AddAsync(Subscriper.MemberJoinedEvent(@event), cancellationToken);
            }

            await _unitOfWork.Permission.AddAsync(Permission.MemberJoinedEvent(@event), cancellationToken);

            await _unitOfWork.Invitation.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
