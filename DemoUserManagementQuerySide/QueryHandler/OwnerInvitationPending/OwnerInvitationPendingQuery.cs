using MediatR;
using System.Drawing;

namespace DemoUserManagementQuerySide.QueryHandler.OwnerInvitationPending
{
    public record OwnerInvitationPendingQuery(
        string UserId,
        int Page,
        int Size
        ): IRequest<OwnerInvitationPendingResult>;
}
