using DemoUserManagementQuerySide.Entities;

namespace DemoUserManagementQuerySide.QueryHandler.MemberSubscription
{
    public record MemberSubscriptionResult(
        List<Subscriper> Subscripers,
        int Page,
        int PageSize,
        int TotalResult
        );
}
