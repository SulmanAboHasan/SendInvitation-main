using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
        public class RemoveMemberRequestValidator: AbstractValidator<RemoveMemberRequest>
        {
            public RemoveMemberRequestValidator()
            {
                RuleFor(c => c.Id)
                    .NotEmpty();

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
