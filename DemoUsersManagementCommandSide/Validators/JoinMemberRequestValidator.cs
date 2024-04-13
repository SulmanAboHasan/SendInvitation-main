using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
    public partial class JoinMemberRequestValidator : AbstractValidator<JoinMemberRequest>
    {
        public JoinMemberRequestValidator()
        {
            RuleFor(c => c.AccountId)
                .NotEmpty();

            RuleFor(c => c.SubscriptionId)
                .NotEmpty();

            RuleFor(c => c.MemberId)
                .NotEmpty();

            RuleFor(c => c.UserId)
                .NotEmpty();
        }
    }
}
