using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.QueryHandler.OwnerInvitationPending
{
    public record OwnerInvitationPendingResult(
            List<Invitation> invitations,
            int Page,
            int PageSize,
            int TotalResult
            );
}
