using DemoUsersManagementCommandSide.Abstraction;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.AcceptInvitation
{
    public record AcceptInvitationCommand(
        string Id,
        string AccountId,
        string SubscriptionId,
        string MemberId,
        string UserId

    ) : IRequest<string>;
}
