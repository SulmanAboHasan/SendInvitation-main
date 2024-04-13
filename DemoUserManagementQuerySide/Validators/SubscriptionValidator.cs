using FluentValidation;

namespace DemoUserManagementQuerySide.Validators
{
    public class SubscriptionValidator : AbstractValidator<GetSubscriptionsRequest>
    {
        public SubscriptionValidator()
        {
            RuleFor(r => r.SubscriptionId).NotEmpty();
        }
    }
}
