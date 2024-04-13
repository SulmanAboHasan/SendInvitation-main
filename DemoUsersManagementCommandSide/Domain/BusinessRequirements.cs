using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Commands.ChangePermissions;
using DemoUsersManagementCommandSide.Exceptions;

namespace DemoUsersManagementCommandSide.Domain
{
    public class BusinessRequirements(InvitationMember invitaionMembers)
    {
        private readonly InvitationMember _invitaionMembers = invitaionMembers;
        
        public void RequireRequestValidation()
        {
            if (_invitaionMembers.IsJoined)
                throw new RuleViolationException("this member is already Joined..!");

            if (!_invitaionMembers.HasInvitationPending && !_invitaionMembers.IsJoined)
                throw new RuleViolationException("This invitation is invalid");
        }

        public void RequireToDelete()
        {
            if (_invitaionMembers.IsDeleted)
                throw new NotFoundException("this invitation has been deleted..!");
        }

        public void RequireMemberJoin() 
        {
            if (_invitaionMembers.IsJoined)
                throw new AlreadyExistsException("This Member Has been Joined");
        }

        public void RequireMemberLeaveOrRemove() 
        {
            if (!_invitaionMembers.IsJoined)
                throw new NotFoundException("this member not found in this subscription");
        }

        public void RequireMemberChangePermission(ChangePermissionsCommand command)
        {
            if (!_invitaionMembers.IsJoined)
                throw new NotFoundException("This member not found in this subscription");

            if (_invitaionMembers.HasInvitationPending)
                throw new RuleViolationException("Invitation still pending");

            if (_invitaionMembers.Permissions == command.Permissions)
                throw new RuleViolationException("The member already has these permissions");
        }

    }

}
