 using DemoCommandSide.Domain;
using DemoUsersManagementCommandSide.Abstraction;
using DemoUsersManagementCommandSide.Commands.AcceptInvitation;
using DemoUsersManagementCommandSide.Commands.CancelInvitationRequest;
using DemoUsersManagementCommandSide.Commands.DeleteCommand;
using DemoUsersManagementCommandSide.Commands.JoinMember;
using DemoUsersManagementCommandSide.Commands.LeaveMember;
using DemoUsersManagementCommandSide.Commands.RejectInvitation;
using DemoUsersManagementCommandSide.Commands.RemoveMember;
using DemoUsersManagementCommandSide.Commands.SendInvitaionRequest;
using DemoUsersManagementCommandSide.Commands.ChangePermissions;
using DemoUsersManagementCommandSide.Events;
using DemoUsersManagementCommandSide.Exceptions;
using DemoUsersManagementCommandSide.Extensions;

namespace DemoUsersManagementCommandSide.Domain
{
    public class InvitationMember : Aggregate<InvitationMember>, IAggregate
    {
        private readonly BusinessRules _businessRules;

        public InvitationMember()
        {
            _businessRules = new BusinessRules(this);
        }

        public bool IsDeleted { get; set; }
        public bool IsJoined { get; private set; }
        public bool HasInvitationPending { get; private set; }
        public Permission? Permissions { get; private set; }

        

        public void Send(SendInvitationCommand command)
        {
            if (HasInvitationPending)
                throw new RuleViolationException("Invitation still pending");

            if (IsJoined)
                throw new AlreadyExistsException("The member has been joined befor");

            ApplyNewChange(command.ToEvent(NextSequence));

        }

        private void Mutate(InvitationSent @event)
        {
            IsJoined = false;
            HasInvitationPending = true;
            Permissions = @event.Data.Permissions;
        }

        internal void Canceled(CancelInvitationCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        private void Mutate(InvitationCanceled _)
        {
            IsJoined = false;
            HasInvitationPending = false;
        }

        internal void Accept(AcceptInvitationCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        private void Mutate(InvitationAccepted _)
        {
            IsJoined = true;
            HasInvitationPending = false;
        }

        internal void Reject(RejectInvitationCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        private void Mutate(InvitationRejected _)
        {
            IsJoined = false;
            HasInvitationPending = false;
        }

        internal void Delete(DeleteInvitationCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);

        }

        private void Mutate(InvitationDeleted _)
        {
            IsDeleted = true; 
            HasInvitationPending = false;
        }

        internal void JoinMember(JoinMemberCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);

        }

        public void Mutate(MemberJoined @event)
        {
            IsJoined = true;
            HasInvitationPending = false;
            Permissions = @event.data.Permissions;
        }

        internal void RemoveMember(RemoveMemberCommand command)
        {
            _businessRules.Validate(command);
            
            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        public void Mutate(MemberRemoved _)
        {
            IsJoined = false;
            HasInvitationPending = false;
        }

        internal void LeaveMember(LeaveMemberCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        public void Mutate(MemberLeft _)
        {
            IsJoined = false;
            HasInvitationPending = false;
        }

        internal void ChangePermission(ChangePermissionsCommand command)
        {
            _businessRules.Validate(command);

            var @event = command.ToEvent(NextSequence);

            ApplyNewChange(@event);
        }

        public void Mutate(PermissionChanged @event)
        {
            Permissions = @event.data.Permissions;
        }


        protected override void Mutate(Event @event)
        {
            switch (@event)
            {                
                case InvitationSent taskSent:
                    Mutate(taskSent);
                    break;

                case InvitationCanceled taskCanceled:
                    Mutate(taskCanceled);
                    break;

                case InvitationAccepted taskAccepted:
                    Mutate(taskAccepted);
                    break;

                case InvitationRejected taskRejected:
                    Mutate(taskRejected);
                    break;

                case InvitationDeleted taskDeleted:
                    Mutate(taskDeleted);
                    break;

                case MemberJoined joined:
                    Mutate(joined);
                    break;

                case MemberRemoved Removed:
                    Mutate(Removed);
                    break;

                case MemberLeft Left:
                    Mutate(Left);
                    break;

                case PermissionChanged permissionChanged:
                    Mutate(permissionChanged);
                    break;

                default:
                    break;           

            }

        }
    }
}
