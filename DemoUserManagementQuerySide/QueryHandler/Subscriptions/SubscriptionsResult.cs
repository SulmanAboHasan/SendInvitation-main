using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.QueryHandler.Subscriptions
{
    public record SubscriptionsResult(
        List<Subscriper> subscripers,
        int Page,
        int PageSize,
        int TotalResult
        );
    
}
