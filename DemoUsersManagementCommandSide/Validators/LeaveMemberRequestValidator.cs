using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
        public class LeaveMemberRequestValidator: AbstractValidator<LeaveMemberRequest>
        {
            public LeaveMemberRequestValidator()
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
