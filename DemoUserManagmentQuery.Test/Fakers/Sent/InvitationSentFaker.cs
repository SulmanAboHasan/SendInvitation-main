
using DemoUserManagementQuerySide.EventsHandler.Sent;


namespace DemoUserManagmentQuery.Test.Fakers.Sent
{
    public class InvitationSentFaker : EventFaker<InvitationSent, InvitationSentData>
    {
        public InvitationSentFaker(int sequence)
        {
            RuleFor(e => e.Sequence, sequence);
            RuleFor(e => e.Data, new InvitationSentDataFaker());
        }


    }
}
