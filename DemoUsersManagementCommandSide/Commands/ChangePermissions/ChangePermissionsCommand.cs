using DemoUsersManagementCommandSide.Domain;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.ChangePermissions
{
    public class ChangePermissionsCommand : IRequest<string>
    {
        public required string Id { get; init; }
        public required string AccountId { get; init; }
        public required string SubscriptionId { get; init; }
        public required string MemberId { get; init; }
        public required string UserId { get; init; }
        public required Permission Permissions { get; init; }
    }
}

