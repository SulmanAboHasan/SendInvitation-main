using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.Subscriptions
{
    public record SubscriptionsQuery(    
        string SubscriptionId,
        int Page,
        int Size
    ): IRequest<SubscriptionsResult>;
}
