using FluentValidation;

namespace DemoUsersManagementCommandSide.Validators
{
    public class DeleteRequestValidator : AbstractValidator<DeleteInvitationRequest>
    {
        public DeleteRequestValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty()
                .Must(id => Guid.TryParse(id, out _));

            RuleFor(c => c.UserId)
                .NotEmpty();

        }
    }


}
