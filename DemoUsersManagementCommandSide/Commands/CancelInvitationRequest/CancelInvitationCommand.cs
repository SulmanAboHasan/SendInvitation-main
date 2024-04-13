using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Domain;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.CancelInvitationRequest
{
    public record CancelInvitationCommand 
    (
        
        string Id, 
        string AccountId,
        string SubscriptionId,
        string MemberId,
        string UserId

    ) : IRequest<string>;
}
