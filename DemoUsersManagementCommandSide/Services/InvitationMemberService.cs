using DemoUsersManagementCommandSide.Extensions;
using Grpc.Core;
using MediatR;

namespace DemoUsersManagementCommandSide.Services
{
    public class InvitationMemberService: InvitaionMembers.InvitaionMembersBase
    {
        private readonly ILogger<InvitationMemberService> _logger;
        private readonly IMediator _mediator;

        public InvitationMemberService(ILogger<InvitationMemberService> logger, IMediator mediator)
        {
            _logger = logger;
            _mediator = mediator;
        }

        public override async Task<Response> SendInvitation(SendInvitationRequest request, ServerCallContext context)
        {
            var command = request.ToSendCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The invitation has been sent successfuly"
            };

        }

        public override async Task<Response> CancelInvitation(CancelInvitationRequest request, ServerCallContext context)
        {
            var command = request.ToCancelCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The invitation has been Canceled."
            };

        }

        public override async Task<Response> AcceptInvitation(AcceptInvitationRequest request, ServerCallContext context)
        {
            var command = request.ToAcceptCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The invitation has been Accepted."
            };

        }

        public override async Task<Response> RejectInvitation(RejectInvitationRequest request, ServerCallContext context)
        {
            var command = request.ToRejectCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The invitation has been rejected."
            };

        }

        public override async Task<Response> DeleteInvitation(DeleteInvitationRequest request, ServerCallContext context)
        {
            var command = request.ToDeletCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The invitation has been deleted."
            };

        }

        public override async Task<Response> JoinMember(JoinMemberRequest request, ServerCallContext context)
        {
            var command = request.ToJoinMemberCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The member has been joined."
            };

        }

        public override async Task<Response> RemoveMember(RemoveMemberRequest request, ServerCallContext context)
        {
            var command = request.ToRemoveMemberCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The member has been Removed."
            };

        }

        public override async Task<Response> LeaveMember(LeaveMemberRequest request, ServerCallContext context)
        {
            var command = request.ToLeaveMemberCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The member has been Left."
            };

        }

        public override async Task<Response> ChangePermission(ChangePermissionRequest request, ServerCallContext context)
        {
            var command = request.ToChangePermissionsCommand();

            var id = await _mediator.Send(command);

            return new Response()
            {
                Id = id.ToString(),
                Message = "The Permissions Changed Successfuly."
            };

        }

    }
}
