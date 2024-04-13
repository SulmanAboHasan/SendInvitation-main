using FluentValidation;

namespace DemoUserManagementQuerySide.Validators
{
    public class MemberInvitationPendingValidator : AbstractValidator<GetMemberInvitationPendingRequest>
    {
        public MemberInvitationPendingValidator()
        {
            RuleFor(r => r.MemberId).NotEmpty();
        }
    }
}
