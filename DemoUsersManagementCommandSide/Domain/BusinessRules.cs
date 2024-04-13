using DemoUsersManagementCommandSide.Commands.AcceptInvitation;
using DemoUsersManagementCommandSide.Commands.CancelInvitationRequest;
using DemoUsersManagementCommandSide.Commands.ChangePermissions;
using DemoUsersManagementCommandSide.Commands.DeleteCommand;
using DemoUsersManagementCommandSide.Commands.JoinMember;
using DemoUsersManagementCommandSide.Commands.LeaveMember;
using DemoUsersManagementCommandSide.Commands.RejectInvitation;
using DemoUsersManagementCommandSide.Commands.RemoveMember;


namespace DemoUsersManagementCommandSide.Domain
{
    public class BusinessRules
    {
        private readonly BusinessRequirements _requirements;

        public BusinessRules(InvitationMember invitation)
        {
            _requirements = new BusinessRequirements(invitation);
        }
        public void Validate(CancelInvitationCommand command)
        {
            _requirements.RequireRequestValidation();
        }

        public void Validate(AcceptInvitationCommand command)
        {
            _requirements.RequireRequestValidation();

        }

        public void Validate(RejectInvitationCommand command)
        {
            _requirements.RequireRequestValidation();

        }

        public void Validate(DeleteInvitationCommand command)
        {
            _requirements.RequireToDelete();
        }

        public void Validate(JoinMemberCommand command)
        {
            _requirements.RequireMemberJoin();
        }

        public void Validate(RemoveMemberCommand command)
        {
            _requirements.RequireMemberLeaveOrRemove();
        }

        public void Validate(LeaveMemberCommand command)
        {
            _requirements.RequireMemberLeaveOrRemove();
        }

        public void Validate(ChangePermissionsCommand command)
        {
            _requirements.RequireMemberChangePermission(command);
        }
    }

}
