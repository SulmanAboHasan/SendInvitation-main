using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Exceptions;
using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.Subscriptions
{
    public class SubscriptionsQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<SubscriptionsQuery, SubscriptionsResult>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<SubscriptionsResult> Handle(SubscriptionsQuery request, CancellationToken cancellationToken)
        {
            var subscriper = await _unitOfWork.Subscriper.GetAllAsync(
                s => s.SubscriptionId == request.SubscriptionId
            && (s.Status == "Joined" || s.Status == "Accepted"),
                includeProperties: "Subscription, User",
            request.Page, request.Size
            );

            if(!subscriper.Any())
            {
                throw new NotFoundException("There are no members in this subscroption");
            }

            return new SubscriptionsResult(
                subscripers: subscriper.ToList(),
                Page: request.Page,
                PageSize: request.Size,
                TotalResult: subscriper.Count()
                );
        }
    }
}
