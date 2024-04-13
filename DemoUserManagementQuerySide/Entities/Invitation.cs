using DemoUserManagementQuerySide.EventsHandler.Accepted;
using DemoUserManagementQuerySide.EventsHandler.Cancelled;
using DemoUserManagementQuerySide.EventsHandler.Rejected;
using DemoUserManagementQuerySide.EventsHandler.Sent;
using Grpc.Core;

namespace DemoUserManagementQuerySide.Entities
{
    public partial class Invitation
    {

        public string Id { get; private set; }
        public int Sequence { get; private set; }
        public string SubscriptionId { get; private set; }
        public Subscription Subscriptions { get; private set; } = default!;
        public string UserId { get; private set; }
        public User Users { get; private set; } = default!;
        public string Status { get; private set; }
        public DateTime SentAt { get; private set; }

        private Invitation(
            string id,
            int sequence,
            string subscriptionId,
            string userId,
            string status,
            DateTime sentAt)
        {
            Id = id;
            Sequence = sequence;
            SubscriptionId = subscriptionId;
            UserId = userId;
            Status = status;
            SentAt = sentAt;
        }

        public static Invitation SentEvent(InvitationSent @event)
            => new(
                id: @event.AggregateId,
                sequence: @event.Sequence,
                subscriptionId: @event.Data.SubscriptionId,
                userId: @event.Data.MemberId,
                status: "Pending",
                sentAt: @event.DateTime
                );


        public static Invitation CancelledEvent(InvitationCancelled @event)
            => new(
                id: @event.AggregateId,
                sequence: @event.Sequence,
                subscriptionId: @event.Data.SubscriptionId,
                userId: @event.Data.MemberId,
                status: "Cancelled",
                sentAt: @event.DateTime
                );

        public static Invitation AcceptedEvent(InvitationAccepted @event)
            => new(
                id: @event.AggregateId,
                sequence: @event.Sequence,
                subscriptionId: @event.Data.SubscriptionId,
                userId: @event.Data.MemberId,
                status: "Accepted",
                sentAt: @event.DateTime
                );

        public static Invitation RejectedEvent(InvitationRejected @event)
           => new(
               id: @event.AggregateId,
               sequence: @event.Sequence,
               subscriptionId: @event.Data.SubscriptionId,
               userId: @event.Data.MemberId,
               status: "Rejected",
               sentAt: @event.DateTime
               );

        public void ChangeStatus(Invitation entity)
        {
            Sequence = entity.Sequence;
            Status = entity.Status;
            if (entity.Status == "Sent")
                SentAt = entity.SentAt;
        }

        public void UpdateSequence(int sequence)
        {
            Sequence = sequence;
        }
    }
}
