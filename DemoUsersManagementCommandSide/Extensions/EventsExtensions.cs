using DemoUsersManagementCommandSide.Commands.AcceptInvitation;
using DemoUsersManagementCommandSide.Commands.CancelInvitationRequest;
using DemoUsersManagementCommandSide.Commands.DeleteCommand;
using DemoUsersManagementCommandSide.Commands.JoinMember;
using DemoUsersManagementCommandSide.Commands.LeaveMember;
using DemoUsersManagementCommandSide.Commands.RejectInvitation;
using DemoUsersManagementCommandSide.Commands.RemoveMember;
using DemoUsersManagementCommandSide.Commands.SendInvitaionRequest;
using DemoUsersManagementCommandSide.Commands.ChangePermissions;
using DemoUsersManagementCommandSide.Domain;
using DemoUsersManagementCommandSide.Events;
using DemoUsersManagementCommandSide.Events.DataType;
using System.Text;

namespace DemoUsersManagementCommandSide.Extensions
{
    public static class EventsExtensions
    {
        public static InvitationSent ToEvent(this SendInvitationCommand command, int sequence)
         => new(
                aggregateId: command.SubscriptionId + "_" + command.MemberId,
                sequence: sequence,
                dateTime: DateTime.UtcNow,
                Data: new InvitationSentData(
                    AccountId: command.AccountId,
                    SubscriptionId: command.SubscriptionId,
                    MemberId: command.MemberId,                   
                    Permissions: command.Permissions
                ),
                userId: command.UserId,
                version: 1
            );
       

        public static InvitationCanceled ToEvent(this CancelInvitationCommand command, int sequence)
            => new(
                aggregateId: command.Id,
                sequence: sequence,
                dateTime: DateTime.UtcNow,
                data: new InvitationCanceledData(
                    AccountId: command.AccountId,
                    SubscriptionId: command.SubscriptionId,
                    MemberId: command.MemberId                    
                ),
                userId: command.UserId,
                version: 1
            );

        public static InvitationAccepted ToEvent(this AcceptInvitationCommand command, int sequence)
           => new(
               aggregateId: command.Id,
               sequence: sequence,
               dateTime: DateTime.UtcNow,
               data: new InvitationAcceptedtData(
                   AccountId: command.AccountId,
                   SubscriptionId: command.SubscriptionId,
                   MemberId: command.MemberId
               ),
               userId: command.UserId,
               version: 1
               
           );

        public static InvitationRejected ToEvent(this RejectInvitationCommand command, int sequence)
           => new(
               aggregateId: command.Id,
               sequence: sequence,
               dateTime: DateTime.UtcNow,
               data: new InvitationRejectedtData(
                   AccountId: command.AccountId,
                   SubscriptionId: command.SubscriptionId,
                   MemberId: command.MemberId
               ),
               userId: command.UserId,
               version: 1
           );

        public static InvitationDeleted ToEvent(this DeleteInvitationCommand command, int sequence)
            => new(
                aggregateId: command.Id,
                sequence: sequence,
                dateTime: DateTime.UtcNow,
                data :new object(),
                userId: command.UserId,
                version: 1
                );

        public static MemberJoined ToEvent(this JoinMemberCommand command, int sequence)
         => new(
             aggregateId: command.SubscriptionId + "_" + command.MemberId,
             sequence: sequence,
             dateTime: DateTime.UtcNow,
             data: new MemberJoinedData(
                 AccountId: command.AccountId,
                 SubscriptionId: command.SubscriptionId,
                 MemberId: command.MemberId,
                 Permissions: command.Permissions
                 ),
             userId: command.UserId,
             version: 1
             );

        public static MemberRemoved ToEvent(this RemoveMemberCommand command, int sequence)
            => new(
                aggregateId: command.Id,
                sequence: sequence,
                dateTime: DateTime.UtcNow,
                data: new MemberRemovedData(
                    AccountId: command.AccountId,
                    SubscriptionId: command.SubscriptionId,
                    MemberId: command.MemberId
                ),
                userId: command.UserId,
                version: 1
                );

        public static MemberLeft ToEvent(this LeaveMemberCommand command, int sequence)
           => new(
               aggregateId: command.Id,
               sequence: sequence,
               dateTime: DateTime.UtcNow,
               data: new MemberLeftData(
                   AccountId: command.AccountId,
                   SubscriptionId: command.SubscriptionId,
                   MemberId: command.MemberId
               ),
               userId: command.UserId,
               version: 1
               );


        public static PermissionChanged ToEvent(this ChangePermissionsCommand command, int sequence)
           => new(
               aggregateId: command.Id,
               sequence: sequence,
               dateTime: DateTime.UtcNow,
               data: new PermissionChangedData(
                   AccountId: command.AccountId,
                   SubscriptionId: command.SubscriptionId,
                   MemberId: command.MemberId,
                   Permissions: command.Permissions
               ),
               userId: command.UserId,
               version: 1
               );
    }
}
