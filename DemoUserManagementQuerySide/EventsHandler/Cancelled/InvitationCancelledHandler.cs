﻿using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Entities;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using MediatR;

namespace DemoUserManagementQuerySide.EventsHandler.Cancelled
{
    public class InvitationCancelledHandler(IUnitOfWork unitOfWork, ILogger<InvitationSentHandler> logger) : IRequestHandler<InvitationCancelled, bool>
    {
        private readonly ILogger<InvitationSentHandler> _logger = logger;
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<bool> Handle(InvitationCancelled @event, CancellationToken cancellationToken)
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

            await _unitOfWork.Invitation.ChangeStatusAsync(Invitation.CancelledEvent(@event));
            await _unitOfWork.Subscriper.UpdateSequence(@event.AggregateId, @event.Sequence);

            var permssions = await _unitOfWork.Permission.GetAsync(p => p.Id == @event.AggregateId);
            if (permssions is not null)
                await _unitOfWork.Permission.RemoveAsync(permssions);


            await _unitOfWork.CommitAsync(cancellationToken);
            return true;
        }
    }
}
