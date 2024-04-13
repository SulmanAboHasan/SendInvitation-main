using DemoUserManagementQuerySide.Extentions;
using Grpc.Core;
using MediatR;

namespace DemoUserManagementQuerySide.Interceptors
{
    public class MemberService(IMediator mediator) : Members.MembersBase
    {
        private readonly IMediator _mediator = mediator;

        public override async Task<GetSubscriptionsResponse> GetSubscriptions(GetSubscriptionsRequest request, ServerCallContext context)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query, context.CancellationToken);

            var outputs = result.subscripers.Select(s => s.ToSubscriberOutput());

            return new GetSubscriptionsResponse()
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalResults = result.TotalResult,
                Subscribers = { outputs }
            };
        }

        public override async Task<GetOwnerInvitationPendingResponse> GetOwnerInvitationPending(GetOwnerInvitationPendingRequest request, ServerCallContext context)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query, context.CancellationToken);

            var outputs = result.invitations.Select(i => i.ToInvitationOutput());

            return new GetOwnerInvitationPendingResponse()
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalResults = result.TotalResult,
                Invitations = { outputs }
            };
        }

        public override async Task<GetMemberInvitationPendingResponse> GetMemberInvitationPending(GetMemberInvitationPendingRequest request, ServerCallContext context)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query, context.CancellationToken);

            var outputs = result.invitations.Select(i => i.ToInvitationOutput());

            return new GetMemberInvitationPendingResponse()
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalResults = result.TotalResult,
                Invitations = { outputs }
            };
        }

        public override async Task<GetMemberSubscriptionResponse> GetMemberSubscription(GetMemberSubscriptionRequest request, ServerCallContext context)
        {
            var query = request.ToQuery();

            var result = await _mediator.Send(query, context.CancellationToken);

            var outputs = result.Subscripers.Select(s => s.ToSubscriberOutput());

            return new GetMemberSubscriptionResponse()
            {
                Page = result.Page,
                PageSize = result.PageSize,
                TotalResults = result.TotalResult,
                Subscribers = { outputs }
            };

        }
    }
}
