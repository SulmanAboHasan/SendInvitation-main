using FluentValidation;

namespace DemoUserManagementQuerySide.Validators
{
    public class MemberSubscriptionsValidator : AbstractValidator<GetMemberSubscriptionRequest>
    {
        public MemberSubscriptionsValidator()
        {
            RuleFor(r => r.MemberId).NotEmpty();
        }
    }
}
