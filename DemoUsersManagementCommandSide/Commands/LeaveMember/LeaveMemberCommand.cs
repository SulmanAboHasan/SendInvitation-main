using MediatR;

namespace DemoUsersManagementCommandSide.Commands.LeaveMember
{
    public class LeaveMemberCommand : IRequest<string>
    {
        public required string Id { get; init; }
        public required string AccountId { get; init; }
        public required string SubscriptionId { get; init; }
        public required string MemberId { get; init; }
        public required string UserId { get; init; }
    }
}
