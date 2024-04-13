
using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Changed;
using DemoUserManagementQuerySide.EventsHandler.Sent;

namespace DemoUserManagementQuerySide.Entities
{
    public class Permission
    {
        public string Id { get; private set; }
        public int Sequence { get; private set; }
        public string UserId { get; private set; }
        public User? User { get; private set; }
        public string SubscriptionId { get; private set; }
        public Subscription? Subscription { get; private set; }
        public bool Transfer { get; private set; }
        public bool PurchaseCards { get; private set; }
        public bool ManageDevices { get; private set; }

        private Permission(
            string id,
            int sequence,
            string userId,
            string subscriptionId,
            bool transfer,
            bool purchaseCards,
            bool manageDevices)
        {
            Id = id;
            Sequence = sequence;
            UserId = userId;
            SubscriptionId = subscriptionId;
            Transfer = transfer;
            PurchaseCards = purchaseCards;
            ManageDevices = manageDevices;
        }

        public static Permission SentEvent(InvitationSent @event)
         => new(
             id: @event.AggregateId,
             sequence: @event.Sequence,
             userId: @event.Data.MemberId,
             subscriptionId: @event.Data.SubscriptionId,
             transfer: @event.Data.Permissions.Transfer,
             purchaseCards: @event.Data.Permissions.PurchaseCards,
             manageDevices: @event.Data.Permissions.ManageDevices
             );

        public static Permission MemberJoinedEvent(MemberJoined @event)
           => new(
               id: @event.AggregateId,
               sequence: @event.Sequence,
               userId: @event.Data.MemberId,
               subscriptionId: @event.Data.SubscriptionId,
               transfer: @event.Data.Permissions.Transfer,
               purchaseCards: @event.Data.Permissions.PurchaseCards,
               manageDevices: @event.Data.Permissions.ManageDevices
               );

        public static Permission PermissionChangedEvent(PermissionChanged @event)
        {
            if (@event is null)
            {
                throw new ArgumentNullException(nameof(@event));
            }

            return new(
             id: @event.AggregateId,
             sequence: @event.Sequence,
             userId: @event.Data.MemberId,
             subscriptionId: @event.Data.SubscriptionId,
             transfer: @event.Data.Permissions.Transfer,
             purchaseCards: @event.Data.Permissions.PurchaseCards,
             manageDevices: @event.Data.Permissions.ManageDevices
             );
        }

        public void ChangePermission(Permission entity)
        {
            Sequence = entity.Sequence;
            Transfer = entity.Transfer;
            PurchaseCards = entity.PurchaseCards;
            ManageDevices = entity.ManageDevices;
        }

        public void UpdateSequence(int sequence)
        {
            Sequence = sequence;
        }
    }
}
