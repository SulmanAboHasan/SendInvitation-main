using DemoUserManagementQuerySide.EventsHandler.Joined;

namespace DemoUserManagmentQuery.Test.Fakers.Joined
{
    public class MemberJoinedFaker : EventFaker<MemberJoined, MemberJoinedData>
    {
        public MemberJoinedFaker(int sequence)
        {
            RuleFor(e => e.Sequence, sequence);
            RuleFor(e => e.Data, new MemberJoinedDataFaker());
        }
    }
}
