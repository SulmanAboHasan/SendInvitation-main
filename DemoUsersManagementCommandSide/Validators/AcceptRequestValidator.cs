using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
    public class AcceptRequestValidator : AbstractValidator<AcceptInvitationRequest>
    {
        public AcceptRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _));

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
