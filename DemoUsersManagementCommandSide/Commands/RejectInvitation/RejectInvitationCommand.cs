using DemoUsersManagementCommandSide.Abstraction;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.RejectInvitation
{
    public record RejectInvitationCommand(
        string Id,
        string AccountId,
        string SubscriptionId,
        string MemberId,
        string UserId

    ) : IRequest<string>;
}
