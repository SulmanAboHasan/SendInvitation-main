using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.MemberSubscription
{
    public record MemberSubscriptionQuery(
        string UserId,
        int Page,
        int Size
        
        ): IRequest<MemberSubscriptionResult>;
}
