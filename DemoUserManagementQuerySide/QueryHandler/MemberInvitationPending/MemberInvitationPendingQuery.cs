using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.MemberInvitationPending
{
    public record MemberInvitationPendingQuery(
        string UserId,
        int Page,
        int Size
        ):IRequest<MemberInvitationPendingResult>;
}
