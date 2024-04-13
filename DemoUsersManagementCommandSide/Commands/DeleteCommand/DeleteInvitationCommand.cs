using DemoUsersManagementCommandSide.Abstraction;
using MediatR;

namespace DemoUsersManagementCommandSide.Commands.DeleteCommand
{
    public record DeleteInvitationCommand(
       string Id,
       string UserId
   ) : IRequest<string>;
}