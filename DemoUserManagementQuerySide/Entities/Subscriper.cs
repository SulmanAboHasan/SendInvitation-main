using DemoUserManagementQuerySide.EventsHandler.Accepted;
using DemoUserManagementQuerySide.EventsHandler.Joined;
using DemoUserManagementQuerySide.EventsHandler.Left;
using DemoUserManagementQuerySide.EventsHandler.Removed;


namespace DemoUserManagementQuerySide.Entities
{
    public class Subscriper
    {
        public string Id { get; private set; }
        public int Sequence { get; private set; }
        public string SubscriptionId { get; private set; }
        public Subscription? Subscription { get; private set; }
        public string UserId { get; private set; }
        public User? User { get; private set; }
        public string Status { get; private set; }
        public DateTime JoinedAt { get; private set; }

        private Subscriper(
            string id,
            int sequence,
            string subscriptionId,
            string userId,
            string status,
            DateTime joinedAt
            )
        {
            Id = id;
            Sequence = sequence;
            SubscriptionId = subscriptionId;
            UserId = userId;
            Status = status;
            JoinedAt = joinedAt;
        }

        public static Subscriper AcceptedEvent(InvitationAccepted @event)
          => new(
              id: @event.AggregateId,
              sequence: @event.Sequence,
              subscriptionId: @event.Data.SubscriptionId,
              userId: @event.Data.MemberId,
              status: "Accepted",
              joinedAt: @event.DateTime
              );

        public static Subscriper MemberJoinedEvent(MemberJoined @event)
            => new(
                id: @event.AggregateId,
                sequence: @event.Sequence,
                subscriptionId: @event.Data.SubscriptionId,
                userId: @event.Data.MemberId,
                status: "Joined",
                joinedAt: @event.DateTime
                );

        public static Subscriper MemberRemovedEvent(MemberRemoved @event)
            => new(
                id: @event.AggregateId,
                sequence: @event.Sequence,
                subscriptionId: @event.Data.SubscriptionId,
                userId: @event.Data.MemberId,
                status: "Removed",
                joinedAt: @event.DateTime
                );

        public static Subscriper MemberLeftEvent(MemberLeft @event)
           => new(
               id: @event.AggregateId,
               sequence: @event.Sequence,
               subscriptionId: @event.Data.SubscriptionId,
               userId: @event.Data.MemberId,
               status: "Left",
               joinedAt: @event.DateTime
               );

        public void ChangeStatus(Subscriper entity)
        {
            Sequence = entity.Sequence;
            Status = entity.Status;
            if (entity.Status is "Accepted" or "Joined")
                JoinedAt = entity.JoinedAt;
        }

        public void UpdateSequence(int sequence)
        {
            Sequence = sequence;
        }

        public void IncrementSequence() => Sequence++;
    }
}
