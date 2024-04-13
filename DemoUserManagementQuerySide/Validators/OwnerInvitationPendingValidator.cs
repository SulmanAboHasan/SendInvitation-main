using FluentValidation;

namespace DemoUserManagementQuerySide.Validators
{
    public class OwnerInvitationPendingValidator: AbstractValidator<GetOwnerInvitationPendingRequest>
    {
        public OwnerInvitationPendingValidator()
        {
            RuleFor(r => r.OwnerId).NotEmpty();
        }
    }
}
