using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Changed
{
    public class PermissionChangedHandler(IUnitOfWork unitOfWork, ILogger<InvitationSentHandler> logger) : IRequestHandler<PermissionChanged, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(PermissionChanged @event, CancellationToken cancellationToken)
        {
            var permssions = await _unitOfWork.Permission.GetAsync(p => p.Id == @event.AggregateId);

            if (permssions is null)
            {
                _logger.LogWarning("Permissions {AggregateId} not found", @event.AggregateId);
                return false;
            }

            if (@event.Sequence <= permssions.Sequence) return true;

            if (@event.Sequence > permssions.Sequence + 1)
            {
                _logger.LogWarning("{Sequence} is not the expected sequence for permission {AggregateId}", @event.Sequence, @event.AggregateId);
                return false;
            }

            await _unitOfWork.Permission.ChangePermissions(Permission.PermissionChangedEvent(@event));

            await _unitOfWork.Subscriper.UpdateSequence(@event.AggregateId, @event.Sequence);
            await _unitOfWork.Invitation.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
