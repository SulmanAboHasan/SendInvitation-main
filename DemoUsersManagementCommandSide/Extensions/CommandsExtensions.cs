using DemoUsersManagementCommandSide.Commands.AcceptInvitation;
using DemoUsersManagementCommandSide.Commands.CancelInvitationRequest;
using DemoUsersManagementCommandSide.Commands.ChangePermissions;
using DemoUsersManagementCommandSide.Commands.DeleteCommand;
using DemoUsersManagementCommandSide.Commands.JoinMember;
using DemoUsersManagementCommandSide.Commands.LeaveMember;
using DemoUsersManagementCommandSide.Commands.RejectInvitation;
using DemoUsersManagementCommandSide.Commands.RemoveMember;
using DemoUsersManagementCommandSide.Commands.SendInvitaionRequest;
using DemoUsersManagementCommandSide.Domain;


namespace DemoUsersManagementCommandSide.Extensions
{
    public static class CommandsExtensions
    {
        public static SendInvitationCommand ToSendCommand(this SendInvitationRequest request)
           => new()
           {
               AccountId = request.AccountId,
               SubscriptionId = request.SubscriptionId,
               MemberId = request.MemberId,
               UserId = request.UserId,
               Permissions = new Permission
               (
                   Transfer: request.Permissions.Transfer,
                   PurchaseCards: request.Permissions.PurchaseCards,
                   ManageDevices: request.Permissions.ManageDevices
               )
           };


        public static CancelInvitationCommand ToCancelCommand(this CancelInvitationRequest request)
            => new(
                Id: request.Id,
                AccountId: request.AccountId,
                SubscriptionId: request.SubscriptionId,
                MemberId: request.MemberId,
                UserId: request.UserId
            );

        public static AcceptInvitationCommand ToAcceptCommand(this AcceptInvitationRequest request)
            => new(
                Id: request.Id,
                AccountId: request.AccountId,
                SubscriptionId: request.SubscriptionId,
                MemberId: request.MemberId,
                UserId: request.UserId
            );

        public static RejectInvitationCommand ToRejectCommand(this RejectInvitationRequest request)
           => new(
                Id: request.Id,
                AccountId: request.AccountId,
                SubscriptionId: request.SubscriptionId,
                MemberId: request.MemberId,
                UserId: request.UserId
           );

        public static DeleteInvitationCommand ToDeletCommand(this DeleteInvitationRequest request)
            => new(
                Id: request.Id,
                UserId: request.UserId
            );

        public static JoinMemberCommand ToJoinMemberCommand(this JoinMemberRequest request)
          => new()
          {
              AccountId = request.AccountId,
              SubscriptionId = request.SubscriptionId,
              MemberId = request.MemberId,
              UserId = request.UserId,
              Permissions = new Permission
               (
                   Transfer: request.Permissions.Transfer,
                   PurchaseCards: request.Permissions.PurchaseCards,
                   ManageDevices: request.Permissions.ManageDevices
               )
          };

        public static RemoveMemberCommand ToRemoveMemberCommand(this RemoveMemberRequest request)
          => new()
          {
              Id = request.Id,
              AccountId = request.AccountId,
              SubscriptionId = request.SubscriptionId,
              MemberId = request.MemberId,
              UserId = request.UserId
          };

        public static LeaveMemberCommand ToLeaveMemberCommand(this LeaveMemberRequest request)
         => new()
         {
             Id = request.Id,
             AccountId = request.AccountId,
             SubscriptionId = request.SubscriptionId,
             MemberId = request.MemberId,
             UserId = request.UserId
         };


         public static ChangePermissionsCommand ToChangePermissionsCommand(this ChangePermissionRequest request)
          => new()
          {
              Id = request.Id,
              AccountId = request.AccountId,
              SubscriptionId = request.SubscriptionId,
              MemberId = request.MemberId,
              UserId = request.UserId,
              Permissions = new Permission
               (
                   Transfer: request.Permissions.Transfer,
                   PurchaseCards: request.Permissions.PurchaseCards,
                   ManageDevices: request.Permissions.ManageDevices
               )
          };

    }
}
