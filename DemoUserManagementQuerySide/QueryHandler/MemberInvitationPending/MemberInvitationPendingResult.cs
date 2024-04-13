using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.QueryHandler.MemberInvitationPending
{
    public record MemberInvitationPendingResult(
        List<Invitation> invitations,
        int Page,
        int PageSize,
        int TotalResult
        );
}
