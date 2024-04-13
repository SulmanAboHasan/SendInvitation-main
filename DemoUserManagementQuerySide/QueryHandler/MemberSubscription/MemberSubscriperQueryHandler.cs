using DemoUserManagementQuerySide.Abstraction.IRepsitories;
using DemoUserManagementQuerySide.Exceptions;
using MediatR;

namespace DemoUserManagementQuerySide.QueryHandler.MemberSubscription
{
    public class MemberSubscriperQueryHandler(IUnitOfWork unitOfWork) : IRequestHandler<MemberSubscriptionQuery, MemberSubscriptionResult>
    {
        private readonly IUnitOfWork _unitOfWork = unitOfWork;

        public async Task<MemberSubscriptionResult> Handle(MemberSubscriptionQuery request, CancellationToken cancellationToken)
        {
            var subscriper = await _unitOfWork.Subscriper.GetAllAsync(
                s => s.UserId == request.UserId
                && (s.Status == "Joined" || s.Status == "Accepted"),
                includeProperties: "Subscription, User",
                request.Page, request.Size
                );

            if (!subscriper.Any())
                throw new NotFoundException("There are no Subscription to this Member");

            return new MemberSubscriptionResult(
                Subscripers: subscriper.ToList(),
                Page: request.Page,
                PageSize: request.Size,
                TotalResult: subscriper.Count()
                );
        }
    }
}
