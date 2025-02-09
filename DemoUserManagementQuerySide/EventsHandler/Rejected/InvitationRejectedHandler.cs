﻿using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Rejected
{
    public class InvitationRejectedHandler(ILogger<InvitationSentHandler> logger, IUnitOfWork unitOfWork) : IRequestHandler<InvitationRejected, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(InvitationRejected @event, CancellationToken cancellationToken)
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

            await _unitOfWork.Invitation.ChangeStatusAsync(Invitation.RejectedEvent(@event));

            var permssions = await _unitOfWork.Permission.GetAsync(p => p.Id == @event.AggregateId);

            if (permssions is not null)
                await _unitOfWork.Permission.RemoveAsync(permssions);

            await _unitOfWork.Subscriper.UpdateSequence(@event.AggregateId, @event.Sequence);

            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
