using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Exceptions;
using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.OwnerInvitationPending
{
    public class OwnerInvitationPendingQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<OwnerInvitationPendingQuery , OwnerInvitationPendingResult>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<OwnerInvitationPendingResult> Handle(OwnerInvitationPendingQuery request, CancellationToken cancellationToken)
        {
            var invitation = await _unitOfWork.Invitation.GetAllAsync(
                i => i.Subscriptions.UserId == request.UserId
                && i.Status == "Pending",
                includeProperties: "Subscription, User",
                request.Page, request.Size
                );

            if (!invitation.Any())
                throw new NotFoundException("There are no Invitation Member Found");

            return new OwnerInvitationPendingResult(
                invitations: invitation.ToList(),
                Page: request.Page,
                PageSize: request.Size,
                TotalResult: invitation.Count()

                );
        }
    }
}
