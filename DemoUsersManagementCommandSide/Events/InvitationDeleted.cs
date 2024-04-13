namespace DemoUsersManagementCommandSide.Events
{
    public record InvitationDeleted : Event<object>
    {
        public InvitationDeleted(
           string aggregateId,
            int sequence,
            DateTime dateTime,
            object data,
            string userId,
            int version
        ) : base(aggregateId, sequence, dateTime, data, userId, version)
        {
        }
    }
}
