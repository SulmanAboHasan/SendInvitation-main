using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Exceptions;
using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.MemberInvitationPending
{
    public class MemberInvitationPendingHandler(IUnitOfWork unitOfWork) : IRequestHandler<MemberInvitationPendingQuery, MemberInvitationPendingResult>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<MemberInvitationPendingResult> Handle(MemberInvitationPendingQuery request, CancellationToken cancellationToken)
        {
            var invitation = await _unitOfWork.Invitation.GetAllAsync(
                i => i.UserId == request.UserId
                && i.Status == "Pending",
                includeProperties: "Subscription,User",
                request.Page, request.Size
            );

            if (!invitation.Any())
                throw new NotFoundException("no Pending Invitations Found..!");

            return new MemberInvitationPendingResult(
                invitations: invitation.ToList(),
                Page: request.Page,
                PageSize: request.Size,
                TotalResult: invitation.Count()
                );
        }
    }
}
