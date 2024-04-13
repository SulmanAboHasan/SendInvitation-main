using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
    public class SendRequestValidator : AbstractValidator<SendInvitationRequest>
    {
        public SendRequestValidator()
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
